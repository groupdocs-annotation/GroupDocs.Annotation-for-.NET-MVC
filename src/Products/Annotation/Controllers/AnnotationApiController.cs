using GroupDocs.Annotation.Config;
using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.Domain.Image;
using GroupDocs.Annotation.Domain.Options;
using GroupDocs.Annotation.Handler;
using GroupDocs.Annotation.MVC.Products.Annotation.Annotator;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Request;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.MVC.Products.Annotation.Importer;
using GroupDocs.Annotation.MVC.Products.Annotation.Util;
using GroupDocs.Annotation.MVC.Products.Annotation.Util.Directory;
using GroupDocs.Annotation.MVC.Products.Common.Entity.Web;
using GroupDocs.Annotation.MVC.Products.Common.Resources;
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
        private List<string> SupportedImageFormats = new List<string>() { ".bmp", ".jpeg", ".jpg", ".tiff", ".tif", ".png", ".gif", ".emf", ".wmf", ".dwg", ".dicom", ".djvu" };
        private List<string> SupportedDiagrammFormats = new List<string>() { ".vsd", ".vdx", ".vss", ".vsx", ".vst", ".vtx", ".vsdx", ".vdw", ".vstx", ".vssx" };
        private static AnnotationImageHandler AnnotationImageHandler;
        private DirectoryUtils DirectoryUtils;

        /// <summary>
        /// Constructor
        /// </summary>        
        public AnnotationApiController()
        {
            GlobalConfiguration = new Common.Config.GlobalConfiguration();
            // create annotation directories
            DirectoryUtils = new DirectoryUtils(GlobalConfiguration.Annotation);

            // create annotation application configuration
            AnnotationConfig config = new AnnotationConfig
            {
                // set storage path
                StoragePath = DirectoryUtils.FilesDirectory.GetPath()
            };           
            // initialize Annotation instance for the Image mode
            AnnotationImageHandler = new AnnotationImageHandler(config);
        }


        /// <summary>
        /// Get all files and directories from storage
        /// </summary>
        /// <param name="postedData">SignaturePostedDataEntity</param>
        /// <returns>List of files and directories</returns>
        [HttpPost]
        [Route("loadFileTree")]
        public HttpResponseMessage LoadFileTree(AnnotationPostedDataEntity fileTreeRequest)
        {
            string relDirPath = fileTreeRequest.path;
            // get file list from storage path
            FileTreeOptions fileListOptions = new FileTreeOptions(relDirPath);
            // get temp directory name
            string tempDirectoryName = new AnnotationConfig().TempFolderName;
            try
            {
                FileTreeContainer fileListContainer = AnnotationImageHandler.LoadFileTree(fileListOptions);

                List<FileDescriptionEntity> fileList = new List<FileDescriptionEntity>();
                // parse files/folders list
                foreach (FileDescription fd in fileListContainer.FileTree)
                {
                    FileDescriptionEntity fileDescription = new FileDescriptionEntity
                    {
                        guid = fd.Guid
                    };
                    // check if current file/folder is temp directory or is hidden
                    FileInfo fileInfo = new FileInfo(fileDescription.guid);
                    if (tempDirectoryName.ToLower().Equals(fileDescription.guid) || fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
                    {
                        // ignore current file and skip to next one
                        continue;
                    }
                    else
                    {
                        // set file/folder name
                        fileDescription.name = fd.Name;
                    }
                    // set file type
                    fileDescription.docType = fd.DocumentType;
                    // set is directory true/false
                    fileDescription.isDirectory = fd.IsDirectory;
                    // set file size
                    fileDescription.size = fd.Size;
                    // add object to array list
                    fileList.Add(fileDescription);
                }
                return Request.CreateResponse(HttpStatusCode.OK, fileList);
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.OK, new Resources().GenerateException(ex));
            }
        }

        /// <summary>
        /// Get document description
        /// </summary>
        /// <param name="loadDocumentRequest">AnnotationPostedDataEntity</param>
        /// <returns>Document description</returns>
        [HttpPost]
        [Route("loadDocumentDescription")]
        public HttpResponseMessage LoadDocumentDescription(AnnotationPostedDataEntity loadDocumentRequest)
        {
            string password = "";
            try
            {
                // get/set parameters
                string documentGuid = loadDocumentRequest.guid;
                password = loadDocumentRequest.password;
                DocumentInfoContainer documentDescription;
                // get document info container              
                string documentPath = GetDocumentPath(documentGuid);
                List<PageImage> pageImages = null;
                ImageOptions imageOptions = new ImageOptions();
                // set password for protected document
                if (!String.IsNullOrEmpty(password))
                {
                    imageOptions.Password = password;
                }
                if (GlobalConfiguration.Annotation.GetPreloadPageCount() == 0)
                {
                    Stream document = File.Open(documentPath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    pageImages = AnnotationImageHandler.GetPages(document, imageOptions);
                    document.Dispose();
                    document.Close();
                }
                documentDescription = AnnotationImageHandler.GetDocumentInfo(documentPath, password);
                string documentType = documentDescription.DocumentType;
                string fileExtension = Path.GetExtension(documentPath);
                // check if document type is image
                if (SupportedImageFormats.Contains(fileExtension))
                {
                    documentType = "image";
                }
                else if (SupportedDiagrammFormats.Contains(fileExtension))
                {
                    documentType = "diagram";
                }
                // check if document contains annotations
                AnnotationInfo[] annotations = GetAnnotations(documentPath, documentType, password);
                // initiate pages description list
                // initiate custom Document description object
                AnnotatedDocumentEntity description = new AnnotatedDocumentEntity();

                description.guid = documentGuid;
                description.supportedAnnotations = new SupportedAnnotations().GetSupportedAnnotations(documentType);

                // get info about each document page
                for (int i = 0; i < documentDescription.Pages.Count; i++)
                {
                    PageDataDescriptionEntity page = new PageDataDescriptionEntity
                    {
                        height = documentDescription.Pages[i].Height,
                        width = documentDescription.Pages[i].Width,
                        number = documentDescription.Pages[i].Number
                    };
                    // set annotations data if document page contains annotations
                    if (annotations != null && annotations.Length > 0)
                    {
                        page.SetAnnotations(AnnotationMapper.instance.mapForPage(annotations, page.number));
                    }
                    if (pageImages != null)
                    {
                        byte[] bytes;
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            Stream imageStream = pageImages[i].Stream;
                            imageStream.Position = 0;
                            imageStream.CopyTo(memoryStream);
                            bytes = memoryStream.ToArray();
                        }
                        string encodedImage = Convert.ToBase64String(bytes);
                        page.SetData(encodedImage);
                    }
                    description.pages.Add(page);
                }
                // return document description
                return Request.CreateResponse(HttpStatusCode.OK, description);
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.OK, new Resources().GenerateException(ex, password));
            }
        }      

        /// <summary>
        /// Get document page
        /// </summary>
        /// <param name="loadDocumentPageRequest"></param>
        /// <returns>Document page image</returns>
        [HttpPost]
        [Route("loadDocumentPage")]
        public HttpResponseMessage LoadDocumentPage(AnnotationPostedDataEntity loadDocumentPageRequest)
        {
            string password = "";
            try
            {
                // get/set parameters
                string documentGuid = loadDocumentPageRequest.guid;
                int pageNumber = loadDocumentPageRequest.page;
                password = loadDocumentPageRequest.password;
                PageDescriptionEntity loadedPage = new PageDescriptionEntity();
                ImageOptions imageOptions = new ImageOptions()
                {
                    PageNumber = pageNumber,
                    CountPagesToConvert = 1,
                    Password = password
                };
                string documentPath = GetDocumentPath(documentGuid);               
                // get page image
                byte[] bytes;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (Stream document = File.Open(documentGuid, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        List<PageImage> images = AnnotationImageHandler.GetPages(document, imageOptions);
                        Stream imageStream = images[0].Stream;
                        
                        imageStream.Position = 0;
                        imageStream.CopyTo(memoryStream);
                        bytes = memoryStream.ToArray();
                        foreach (PageImage page in images)
                        {
                            page.Stream.Close();
                        }
                    }
                }
                string encodedImage = Convert.ToBase64String(bytes);
                loadedPage.SetData(encodedImage);
                DocumentInfoContainer documentDescription = AnnotationImageHandler.GetDocumentInfo(documentPath, password);
                loadedPage.height = documentDescription.Pages[pageNumber - 1].Height;
                loadedPage.width = documentDescription.Pages[pageNumber - 1].Width;                
                // return loaded page object
                return Request.CreateResponse(HttpStatusCode.OK, loadedPage);
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.OK, new Resources().GenerateException(ex, password));
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
            // prepare response message
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);          
           
            // add file into the response
            if (File.Exists(path))
            {
                
                var fileStream = GetCleanDocumentStream(path);
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = Path.GetFileName(path)
                };
                return response;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Download document
        /// </summary>
        /// <param name="path">string</param>
        /// <param name="annotated">bool</param>
        /// <returns></returns>
        [HttpPost]
        [Route("downloadAnnotated")]
        public HttpResponseMessage DownloadAnnotated(AnnotationPostedDataEntity annotateDocumentRequest)
        {
            // prepare response message
            AnnotationDataEntity[] annotationsData = annotateDocumentRequest.annotationsData;
            if (annotationsData == null || annotationsData.Length == 0)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new Resources().GenerateException(new ArgumentNullException("Annotations data is empty")));
            }

            // get document path
            String documentGuid = annotateDocumentRequest.guid;
            String fileName = Path.GetFileName(documentGuid);
            try
            {
                Stream inputStream = AnnotateByStream(annotateDocumentRequest);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(inputStream);
                // add file into the response
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = Path.GetFileName(fileName);
                return response;
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new Resources().GenerateException(ex));
            }
        }

        private Stream AnnotateByStream(AnnotationPostedDataEntity annotateDocumentRequest)
        {
            try
            {
                // get/set parameters
                string documentGuid = annotateDocumentRequest.guid;
                string password = annotateDocumentRequest.password;
                string documentType = annotateDocumentRequest.documentType;
                AnnotationDataEntity[] annotationsData = annotateDocumentRequest.annotationsData;
                // initiate AnnotatedDocument object
                // initiate list of annotations to add
                List<AnnotationInfo> annotations = new List<AnnotationInfo>();
                // get document info - required to get document page height and calculate annotation top position
                string fileName = System.IO.Path.GetFileName(documentGuid);
                FileInfo fi = new FileInfo(documentGuid);
                DirectoryInfo parentDir = fi.Directory;

                string documentPath = "";
                string parentDirName = parentDir.Name;
                if (parentDir.FullName == GlobalConfiguration.Annotation.GetFilesDirectory().Replace("/", "\\"))
                {
                    documentPath = fileName;
                }
                else
                {
                    documentPath = Path.Combine(parentDirName, fileName);
                }
                DocumentInfoContainer documentInfo = AnnotationImageHandler.GetDocumentInfo(documentPath, password);
                // check if document type is image
                if (SupportedImageFormats.Contains(Path.GetExtension(documentGuid)))
                {
                    documentType = "image";
                }
                // initiate annotator object  
                string notSupportedMessage = "";
                for (int i = 0; i < annotationsData.Length; i++)
                {
                    // create annotator
                    AnnotationDataEntity annotationData = annotationsData[i];
                    PageData pageData = documentInfo.Pages[annotationData.pageNumber - 1];
                    // add annotation, if current annotation type isn't supported by the current document type it will be ignored
                    try
                    {
                        BaseAnnotator annotator = AnnotatorFactory.createAnnotator(annotationData, pageData);
                        if (annotator.IsSupported(documentType))
                        {
                            annotations.Add(annotator.GetAnnotationInfo(documentType));
                        }
                        else
                        {
                            notSupportedMessage = annotator.Message;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        throw new System.Exception(ex.Message, ex);
                    }
                }

                // Add annotation to the document
                DocumentType type = DocumentTypesConverter.GetDocumentType(documentType);
                RemoveAnnotations(documentGuid);
                // check if annotations array contains at least one annotation to add
                if (annotations.Count != 0)
                {
                    Stream cleanDoc = new FileStream(documentGuid, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Stream result = AnnotationImageHandler.ExportAnnotationsToDocument(cleanDoc, annotations, type);
                    cleanDoc.Dispose();
                    cleanDoc.Close();
                    result.Position = 0;
                    return result;
                }
                else
                {
                    throw new InvalidDataException("Annotations data are empty");
                }               
            }
            catch (System.Exception ex)
            {
                // set exception message
                throw new InvalidDataException("Failed to annotate stream");
            }
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
                return Request.CreateResponse(HttpStatusCode.OK, new Resources().GenerateException(ex));
            }
        }

        /// <summary>
        /// Annotate document
        /// </summary>      
        /// <returns>Annotated document info</returns>
        [HttpPost]
        [Route("annotate")]
        public HttpResponseMessage Annotate(AnnotationPostedDataEntity annotateDocumentRequest)
        {
            AnnotatedDocumentEntity annotatedDocument = new AnnotatedDocumentEntity();
            try
            {
                // get/set parameters
                string documentGuid = GetDocumentPath(annotateDocumentRequest.guid);
                string password = annotateDocumentRequest.password;
                string documentType = annotateDocumentRequest.documentType;
                AnnotationDataEntity[] annotationsData = annotateDocumentRequest.annotationsData;
                // initiate AnnotatedDocument object
                // initiate list of annotations to add
                List<AnnotationInfo> annotations = new List<AnnotationInfo>();
              
                DocumentInfoContainer documentInfo = AnnotationImageHandler.GetDocumentInfo(documentGuid, password);
                // check if document type is image
                if (SupportedImageFormats.Contains(Path.GetExtension(documentGuid)))
                {
                    documentType = "image";
                }
                // initiate annotator object  
                string notSupportedMessage = "";
                for (int i = 0; i < annotationsData.Length; i++)
                {
                    // create annotator
                    AnnotationDataEntity annotationData = annotationsData[i];
                    PageData pageData = documentInfo.Pages[annotationData.pageNumber - 1];
                    // add annotation, if current annotation type isn't supported by the current document type it will be ignored
                    try
                    {
                        BaseAnnotator annotator = AnnotatorFactory.createAnnotator(annotationData, pageData);
                        if (annotator.IsSupported(documentType))
                        {
                            annotations.Add(annotator.GetAnnotationInfo(documentType));
                        }
                        else
                        {
                            notSupportedMessage = annotator.Message;
                        }
                    }
                    catch (System.Exception ex)
                    {
                        throw new System.Exception(ex.Message, ex);
                    }
                }

                // Add annotation to the document
                DocumentType type = DocumentTypesConverter.GetDocumentType(documentType);              
                RemoveAnnotations(documentGuid);              
                // check if annotations array contains at least one annotation to add
                if (annotations.Count != 0)
                {
                    Stream cleanDoc = new FileStream(documentGuid, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    Stream result = AnnotationImageHandler.ExportAnnotationsToDocument(cleanDoc, annotations, type);
                    cleanDoc.Dispose();
                    cleanDoc.Close();
                    File.Delete(documentGuid);
                    // Save result stream to file.               
                    if (annotateDocumentRequest.print)
                    {
                        documentGuid = documentGuid.Replace(System.IO.Path.GetFileNameWithoutExtension(documentGuid), System.IO.Path.GetFileNameWithoutExtension(documentGuid) + "Temp");
                    }
                    using (FileStream fileStream = new FileStream(documentGuid, FileMode.Create))
                    {
                        byte[] buffer = new byte[result.Length];
                        result.Seek(0, SeekOrigin.Begin);
                        result.Read(buffer, 0, buffer.Length);
                        fileStream.Write(buffer, 0, buffer.Length);
                        fileStream.Close();
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new Resources().GenerateException(new NotSupportedException(notSupportedMessage)));
                }
                annotatedDocument = new AnnotatedDocumentEntity();
                annotatedDocument.guid = documentGuid;
                if (annotateDocumentRequest.print)
                {
                    annotatedDocument.pages = GetAnnotatedPagesForPrint(documentGuid);
                    File.Delete(documentGuid);
                }
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.OK, new Resources().GenerateException(ex));
            }
            return Request.CreateResponse(HttpStatusCode.OK, annotatedDocument);
        }

        /// <summary>
        /// Get all annotations from the document
        /// </summary>
        /// <param name="documentGuid">string</param>
        /// <param name="documentType">string</param>
        /// <returns>AnnotationInfo[]</returns>
        private AnnotationInfo[] GetAnnotations(string documentGuid, string documentType, string password)
        {
            try
            {
                FileStream documentStream = new FileStream(documentGuid, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                DocumentType docType = DocumentTypesConverter.GetDocumentType(documentType);
                AnnotationInfo[] annotations = new BaseImporter(documentStream, AnnotationImageHandler, password).ImportAnnotations(docType);
                documentStream.Dispose();
                documentStream.Close();
                return annotations;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private List<PageDataDescriptionEntity> GetAnnotatedPagesForPrint(string path)
        {
            AnnotatedDocumentEntity description = new AnnotatedDocumentEntity();
            try
            {

                Stream document = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                List<PageImage> pageImages = AnnotationImageHandler.GetPages(document, new ImageOptions());
                document.Dispose();
                document.Close();
                for (int i = 0; i < pageImages.Count; i++)
                {
                    PageDataDescriptionEntity page = new PageDataDescriptionEntity();
                    if (pageImages != null)
                    {
                        byte[] bytes;
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            Stream imageStream = pageImages[i].Stream;
                            imageStream.Position = 0;
                            imageStream.CopyTo(memoryStream);
                            bytes = memoryStream.ToArray();
                        }
                        string encodedImage = Convert.ToBase64String(bytes);
                        page.SetData(encodedImage);
                    }
                    description.pages.Add(page);
                }

                return description.pages;
            }
            catch (FileNotFoundException ex)
            {
                throw ex;
            }
        }

        public void RemoveAnnotations(string path)
        {
            try
            {
                Stream resultStream = null;
                string tempFilePath = "";
                using (Stream inputStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    resultStream = AnnotationImageHandler.RemoveAnnotationStream(inputStream);
                    resultStream.Position = 0;
                    tempFilePath = Resources.GetFreeFileName(GlobalConfiguration.Annotation.GetFilesDirectory(), Path.GetFileName(path));
                    using (Stream tempFile = File.Create(tempFilePath))
                    {
                        resultStream.Seek(0, SeekOrigin.Begin);
                        resultStream.CopyTo(tempFile);
                    }
                    resultStream.Dispose();
                    resultStream.Close();
                }
                File.Delete(path);
                File.Move(tempFilePath, path);
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public Stream GetCleanDocumentStream(string path)
        {
            try
            {
                Stream resultStream = null;
               
                using (Stream inputStream = File.Open(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    resultStream = AnnotationImageHandler.RemoveAnnotationStream(inputStream);
                    resultStream.Position = 0;                   
                }
                return resultStream;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        private string GetDocumentPath(string documentGuid)
        {
            string documentPath = "";
            if (Path.IsPathRooted(documentGuid))
            {
                return documentGuid;
            }
            else
            {
                string fileName = System.IO.Path.GetFileName(documentGuid);
                if (String.IsNullOrEmpty(Path.GetDirectoryName(documentGuid)))
                {
                    documentPath = Path.Combine(GlobalConfiguration.Annotation.GetFilesDirectory(), documentGuid);
                }
                else
                {
                    FileInfo fi = new FileInfo(documentGuid);
                    DirectoryInfo parentDir = fi.Directory;

                    string parentDirName = parentDir.Name;

                    if (parentDir.FullName == GlobalConfiguration.Annotation.GetFilesDirectory().Replace("/", "\\"))
                    {
                        documentPath = documentGuid;
                    }
                    else
                    {
                        documentPath = Path.Combine(parentDirName, fileName);
                    }
                }
            }
            return documentPath;
        }
    }
}