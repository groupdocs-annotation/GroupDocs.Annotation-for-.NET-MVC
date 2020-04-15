using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Annotator;
using GroupDocs.Annotation.MVC.Products.Annotation.Config;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.MVC.Products.Annotation.Util;
using GroupDocs.Annotation.MVC.Products.Annotation.Util.Directory;
using GroupDocs.Annotation.MVC.Products.Common.Config;
using GroupDocs.Annotation.MVC.Products.Common.Entity.Web;
using GroupDocs.Annotation.MVC.Products.Common.Resources;
using GroupDocs.Annotation.MVC.Products.Common.Util.Comparator;
using GroupDocs.Annotation.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Controllers
{
    /// <summary>
    /// SignatureApiController
    /// </summary>
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AnnotationApiController : ApiController
    {
        private static Common.Config.GlobalConfiguration GlobalConfiguration;

        /// <summary>
        /// Constructor
        /// </summary>
        public AnnotationApiController()
        {
            GlobalConfiguration = new Common.Config.GlobalConfiguration();
        }

        /// <summary>
        /// Load Annotation configuration
        /// </summary>
        /// <returns>Annotation configuration</returns>
        [HttpGet]
        [Route("loadConfig")]
        public AnnotationConfiguration LoadConfig()
        {
            return GlobalConfiguration.Annotation;
        }

        /// <summary>
        /// Get all files and directories from storage
        /// </summary>
        /// <param name="postedData">SignaturePostedDataEntity</param>
        /// <returns>List of files and directories</returns>
        [HttpPost]
        [Route("loadFileTree")]
        public HttpResponseMessage loadFileTree(PostedDataEntity postedData)
        {
            // get request body
            string relDirPath = postedData.path;
            // get file list from storage path
            try
            {
                // get all the files from a directory
                if (string.IsNullOrEmpty(relDirPath))
                {
                    relDirPath = GlobalConfiguration.Annotation.GetFilesDirectory();
                }
                else
                {
                    relDirPath = Path.Combine(GlobalConfiguration.Annotation.GetFilesDirectory(), relDirPath);
                }

                List<FileDescriptionEntity> fileList = new List<FileDescriptionEntity>();
                List<string> allFiles = new List<string>(Directory.GetFiles(relDirPath));
                allFiles.AddRange(Directory.GetDirectories(relDirPath));

                allFiles.Sort(new FileNameComparator());
                allFiles.Sort(new FileTypeComparator());

                foreach (string file in allFiles)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    // check if current file/folder is hidden
                    if (fileInfo.Attributes.HasFlag(FileAttributes.Hidden) ||
                        Path.GetFileName(file).Equals(Path.GetFileName(GlobalConfiguration.Annotation.GetFilesDirectory())) ||
                        Path.GetFileName(file).Equals(".gitkeep"))
                    {
                        // ignore current file and skip to next one
                        continue;
                    }
                    else
                    {
                        FileDescriptionEntity fileDescription = new FileDescriptionEntity();
                        fileDescription.guid = Path.GetFullPath(file);
                        fileDescription.name = Path.GetFileName(file);
                        // set is directory true/false
                        fileDescription.isDirectory = fileInfo.Attributes.HasFlag(FileAttributes.Directory);
                        // set file size
                        if (!fileDescription.isDirectory)
                        {
                            fileDescription.size = fileInfo.Length;
                        }
                        // add object to array list
                        fileList.Add(fileDescription);
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, fileList);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new Resources().GenerateException(ex));
            }
        }

        /// <summary>
        /// Load document description
        /// </summary>
        /// <param name="postedData">Post data</param>
        /// <returns>Document info object</returns>
        [HttpPost]
        [Route("loadDocumentDescription")]
        public HttpResponseMessage LoadDocumentDescription(AnnotationPostedDataEntity postedData)
        {
            string password = "";
            try
            {
                AnnotatedDocumentEntity loadDocumentEntity = LoadDocument(postedData);
                // return document description
                return Request.CreateResponse(HttpStatusCode.OK, loadDocumentEntity);
            }
            catch (System.Exception ex)
            {
                // set exception message
                // TODO: return InternalServerError for common Exception and Forbidden for PasswordProtectedException
                return Request.CreateResponse(HttpStatusCode.Forbidden, new Resources().GenerateException(ex, password));
            }
        }

        public AnnotatedDocumentEntity LoadDocument(AnnotationPostedDataEntity loadDocumentRequest)
        {
            string password = loadDocumentRequest.password;

            // check if document contains annotations
            AnnotationBase[] annotations = GetAnnotations(loadDocumentRequest.guid, "image", password);
            // initiate pages description list
            // initiate custom Document description object
            AnnotatedDocumentEntity description = new AnnotatedDocumentEntity();

            string documentGuid = loadDocumentRequest.guid;

            using (FileStream outputStream = File.OpenRead(documentGuid))
            {
                using (GroupDocs.Annotation.Annotator annotator = new GroupDocs.Annotation.Annotator(outputStream/*, new LoadOptions() { ImportAnnotations = false }*/))
                {
                    IDocumentInfo info = annotator.Document.GetDocumentInfo();

                    description.guid = loadDocumentRequest.guid;
                    description.supportedAnnotations = new SupportedAnnotations().GetSupportedAnnotations(info.FileType.ToString());

                    List<string> pagesContent = new List<string>();

                    // TODO: execute only if !loadAllPages
                    pagesContent = GetAllPagesContent(password, documentGuid, info);

                    for (int i = 0; i < info.PageCount; i++)
                    {
                        PageDataDescriptionEntity page = new PageDataDescriptionEntity
                        {
                            number = i + 1,
                            height = info.PagesInfo[i].Height,
                            width = info.PagesInfo[i].Width,
                        };

                        if (annotations != null && annotations.Length > 0)
                        {
                            page.SetAnnotations(AnnotationMapper.instance.mapForPage(annotations, i+1, info.PagesInfo[i]));
                        }

                        //PageDataDescriptionEntity pageData = GetPageDescriptionEntities(i + 1);
                        if (pagesContent.Count > 0)
                        {
                            page.SetData(pagesContent[i]);
                        }
                        description.pages.Add(page);
                    }
                }
            }

            description.guid = documentGuid;
            // return document description
            return description;
        }

        /// <summary>
        /// Get all annotations from the document
        /// </summary>
        /// <param name="documentGuid">string</param>
        /// <param name="documentType">string</param>
        /// <returns>AnnotationInfo[]</returns>
        private AnnotationBase[] GetAnnotations(string documentGuid, string documentType, string password)
        {
            try
            {
                FileStream documentStream = new FileStream(documentGuid, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                DocumentType docType = DocumentTypesConverter.GetDocumentType(documentType);
                AnnotationBase[] annotations;

                using (GroupDocs.Annotation.Annotator annotator = new GroupDocs.Annotation.Annotator(documentStream/*, new LoadOptions() { ImportAnnotations = false }*/))
                {
                    annotations = annotator.Get().ToArray();
                }

                documentStream.Dispose();
                documentStream.Close();
                return annotations;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private List<string> GetAllPagesContent(string password, string documentGuid, IDocumentInfo pages)
        {
            List<string> allPages = new List<string>();

            //get page HTML
            for (int i = 0; i < pages.PageCount; i++)
            {
                byte[] bytes;
                using (var memoryStream = RenderPageToMemoryStream(i + 1, documentGuid, password))
                {
                    bytes = memoryStream.ToArray();
                }

                string encodedImage = Convert.ToBase64String(bytes);
                allPages.Add(encodedImage);
            }

            return allPages;
        }

        static MemoryStream RenderPageToMemoryStream(int pageNumberToRender, string documentGuid, string password)
        {
            MemoryStream result = new MemoryStream();

            LoadOptions loadOptions = new LoadOptions() { Password = password/*, ImportAnnotations = false*/ };

            using (FileStream outputStream = File.OpenRead(documentGuid))
            {
                using (GroupDocs.Annotation.Annotator annotator = new GroupDocs.Annotation.Annotator(outputStream, loadOptions))
                {
                    PreviewOptions previewOptions = new PreviewOptions(pageNumber => result);

                    previewOptions.PreviewFormat = PreviewFormats.PNG;
                    previewOptions.PageNumbers = new int[] { pageNumberToRender };
                    previewOptions.RenderComments = false;

                    annotator.Document.GeneratePreview(previewOptions);
                }
            }

            return result;
        }

        /// <summary>
        /// Upload document
        /// </summary>      
        /// <returns>Uploaded document object</returns>
        [HttpPost]
        [Route("uploadDocument")]
        public HttpResponseMessage UploadDocument()
        {
            try
            {
                string url = HttpContext.Current.Request.Form["url"];
                // get documents storage path
                string documentStoragePath = GlobalConfiguration.Annotation.GetFilesDirectory();
                bool rewrite = bool.Parse(HttpContext.Current.Request.Form["rewrite"]);
                string fileSavePath = "";
                if (string.IsNullOrEmpty(url))
                {
                    if (HttpContext.Current.Request.Files.AllKeys != null)
                    {
                        // Get the uploaded document from the Files collection
                        var httpPostedFile = HttpContext.Current.Request.Files["file"];
                        if (httpPostedFile != null)
                        {
                            if (rewrite)
                            {
                                // Get the complete file path
                                fileSavePath = System.IO.Path.Combine(documentStoragePath, httpPostedFile.FileName);
                            }
                            else
                            {
                                fileSavePath = Resources.GetFreeFileName(documentStoragePath, httpPostedFile.FileName);
                            }

                            // Save the uploaded file to "UploadedFiles" folder
                            httpPostedFile.SaveAs(fileSavePath);
                        }
                    }
                }
                else
                {
                    using (WebClient client = new WebClient())
                    {
                        // get file name from the URL
                        Uri uri = new Uri(url);
                        string fileName = System.IO.Path.GetFileName(uri.LocalPath);
                        if (rewrite)
                        {
                            // Get the complete file path
                            fileSavePath = System.IO.Path.Combine(documentStoragePath, fileName);
                        }
                        else
                        {
                            fileSavePath = Resources.GetFreeFileName(documentStoragePath, fileName);
                        }
                        // Download the Web resource and save it into the current filesystem folder.
                        client.DownloadFile(url, fileSavePath);
                    }
                }
                UploadedDocumentEntity uploadedDocument = new UploadedDocumentEntity
                {
                    guid = fileSavePath
                };
                return Request.CreateResponse(HttpStatusCode.OK, uploadedDocument);
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new Resources().GenerateException(ex));
            }
        }

        /// <summary>
        /// Download document
        /// </summary>
        /// <param name="path">string</param>
        /// <param name="annotated">bool</param>
        /// <returns></returns>
        [HttpGet]
        [Route("downloadDocument")]
        public HttpResponseMessage DownloadDocument(string path)
        {
            // add file into the response
            if (File.Exists(path))
            {
                this.RemoveAnnotations(path);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                var fileStream = new FileStream(path, FileMode.Open);
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(path);
                return response;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        ///// <summary>
        ///// Annotate document
        ///// </summary>      
        ///// <returns>Annotated document info</returns>
        [HttpPost]
        [Route("annotate")]
        public HttpResponseMessage Annotate(AnnotationPostedDataEntity annotateDocumentRequest)
        {
            AnnotatedDocumentEntity annotatedDocument = new AnnotatedDocumentEntity();
            try
            {
                // get/set parameters
                string documentGuid = annotateDocumentRequest.guid;
                string password = annotateDocumentRequest.password;
                string documentType = annotateDocumentRequest.documentType;
                string tempFilename = Path.GetFileNameWithoutExtension(documentGuid) + "_tmp";
                string tempPath = Path.Combine(Path.GetDirectoryName(documentGuid), tempFilename + Path.GetExtension(documentGuid));

                AnnotationDataEntity[] annotationsData = annotateDocumentRequest.annotationsData;
                // initiate AnnotatedDocument object
                // initiate list of annotations to add
                List<AnnotationBase> annotations = new List<AnnotationBase>();

                using (FileStream outputStream = File.OpenRead(documentGuid))
                {
                    using (GroupDocs.Annotation.Annotator annotator = new GroupDocs.Annotation.Annotator(outputStream))
                    {
                        string notSupportedMessage = "";
                        for (int i = 0; i < annotationsData.Length; i++)
                        {
                            // create annotator
                            AnnotationDataEntity annotationData = annotationsData[i];
                            //PageData pageData = new PageData() { Height = 842, Width = 595 };
                            IDocumentInfo info = annotator.Document.GetDocumentInfo();
                            PageInfo pageInfo = info.PagesInfo[annotationsData[i].pageNumber];
                            PageData pageData = new PageData() { Height = pageInfo.Height, Width = pageInfo.Width };
                            // add annotation, if current annotation type isn't supported by the current document type it will be ignored
                            try
                            {
                                BaseAnnotator baseAnnotator = AnnotatorFactory.createAnnotator(annotationData, pageData);
                                if (baseAnnotator.IsSupported(documentType))
                                {
                                    annotations.Add(baseAnnotator.GetAnnotationBase(documentType));
                                }
                                else
                                {
                                    notSupportedMessage = baseAnnotator.Message;
                                }
                            }
                            catch (System.Exception ex)
                            {
                                throw new System.Exception(ex.Message, ex);
                            }
                        }
                    }
                }

                // Add annotation to the document
                RemoveAnnotations(documentGuid);
                // check if annotations array contains at least one annotation to add
                if (annotations.Count != 0)
                {
                    using (FileStream outputStream = File.OpenRead(documentGuid))
                    {
                        using (GroupDocs.Annotation.Annotator annotator = new GroupDocs.Annotation.Annotator(outputStream))
                        {
                            foreach (var annotation in annotations)
                            {
                                annotator.Add(annotation);
                            }

                            annotator.Save(tempPath);
                        }
                    }

                    if (File.Exists(documentGuid))
                    {
                        File.Delete(documentGuid);
                    }

                    File.Move(tempPath, documentGuid);
                }

                annotatedDocument = new AnnotatedDocumentEntity();
                annotatedDocument.guid = documentGuid;
                if (annotateDocumentRequest.print)
                {
                    //annotatedDocument.pages = GetAnnotatedPagesForPrint(documentGuid);
                    File.Move(documentGuid, annotateDocumentRequest.guid);
                }
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new Resources().GenerateException(ex));
            }

            return Request.CreateResponse(HttpStatusCode.OK, annotatedDocument);
        }

        public void RemoveAnnotations(string documentGuid) {

            string tempFilename = Path.GetFileNameWithoutExtension(documentGuid) + "_tmp";
            string tempPath = Path.Combine(Path.GetDirectoryName(documentGuid), tempFilename + Path.GetExtension(documentGuid));

            try
            {
                using (Stream inputStream = File.Open(documentGuid, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    using (GroupDocs.Annotation.Annotator annotator = new GroupDocs.Annotation.Annotator(inputStream))
                    {
                        annotator.Save(tempPath, new SaveOptions() { AnnotationTypes = AnnotationType.None });
                    }
                }
                File.Delete(documentGuid);
                // TODO: make global property
                File.Move(tempPath, documentGuid);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}