using GroupDocs.Annotation.Config;
using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.Domain.Image;
using GroupDocs.Annotation.Domain.Options;
using GroupDocs.Annotation.Handler;
using GroupDocs.Annotation.MVC.Products.Annotation.Annotator;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.MVC.Products.Annotation.Importer;
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
        private List<string> SupportedAutoCadFormats = new List<string>() { ".dxf", ".dwg" };
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
            AnnotationConfig config = new AnnotationConfig();
            // set storage path
            config.StoragePath = DirectoryUtils.FilesDirectory.GetPath();
            // set directory to store annotted documents
            GlobalConfiguration.Annotation.OutputDirectory = DirectoryUtils.OutputDirectory.GetPath();
            // initialize AnnotationImageHandler instance for the Image mode
            AnnotationImageHandler = new AnnotationImageHandler(config);
        }


        /// <summary>
        /// Get all files and directories from storage
        /// </summary>
        /// <param name="postedData">SignaturePostedDataEntity</param>
        /// <returns>List of files and directories</returns>
        [HttpPost]
        [Route("annotation/loadFileTree")]
        public HttpResponseMessage loadFileTree(AnnotationPostedDataEntity fileTreeRequest)
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
                    FileDescriptionEntity fileDescription = new FileDescriptionEntity();
                    fileDescription.guid = fd.Guid;
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
        [Route("annotation/loadDocumentDescription")]
        public HttpResponseMessage loadDocumentDescription(AnnotationPostedDataEntity loadDocumentRequest)
        {
            try
            {
                // get/set parameters
                string documentGuid = loadDocumentRequest.guid;
                string password = loadDocumentRequest.password;
                DocumentInfoContainer documentDescription;
                // get document info container
                documentDescription = AnnotationImageHandler.GetDocumentInfo(System.IO.Path.GetFileName(documentGuid), password);

                string documentType = documentDescription.DocumentType;
                // check if document type is image
                if (SupportedImageFormats.Contains(System.IO.Path.GetExtension(documentGuid)))
                {
                    documentType = "image";
                }
                else if (SupportedAutoCadFormats.Contains(System.IO.Path.GetExtension(documentGuid)))
                {
                    documentType = "diagram";
                }
                // check if document contains annotations
                AnnotationInfo[] annotations = GetAnnotations(documentGuid, documentType);
                // initiate pages description list
                List<AnnotatedDocumentEntity> pagesDescription = new List<AnnotatedDocumentEntity>();
                // get info about each document page
                for (int i = 0; i < documentDescription.Pages.Count; i++)
                {
                    //initiate custom Document description object
                    AnnotatedDocumentEntity description = new AnnotatedDocumentEntity();
                    // set current page info for result
                    description.height = documentDescription.Pages[i].Height;
                    description.width = documentDescription.Pages[i].Width;
                    description.number = documentDescription.Pages[i].Number;
                    // set annotations data if document page contains annotations
                    if (annotations != null && annotations.Length > 0)
                    {
                        description.annotations = SetAnnotations(annotations, description.number);
                    }
                    pagesDescription.Add(description);
                }
                // return document description
                return Request.CreateResponse(HttpStatusCode.OK, pagesDescription);
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.OK, new Resources().GenerateException(ex));
            }
        }

        /// <summary>
        /// Get document page
        /// </summary>
        /// <param name="loadDocumentPageRequest"></param>
        /// <returns>Document page image</returns>
        [HttpPost]
        [Route("annotation/loadDocumentPage")]
        public HttpResponseMessage loadDocumentPage(AnnotationPostedDataEntity loadDocumentPageRequest)
        {
            try
            {
                // get/set parameters
                string documentGuid = loadDocumentPageRequest.guid;
                int pageNumber = loadDocumentPageRequest.page;
                string password = loadDocumentPageRequest.password;
                LoadedPageEntity loadedPage = new LoadedPageEntity();
                ImageOptions imageOptions = new ImageOptions()
                {
                    PageNumber = pageNumber,
                    CountPagesToConvert = 1
                };
                // get page image

                byte[] bytes;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (Stream document = File.Open(documentGuid, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        List<PageImage> images = AnnotationImageHandler.GetPages(document, imageOptions);
                        Stream imageStream = images[pageNumber - 1].Stream;
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
                loadedPage.pageImage = encodedImage;
                // return loaded page object
                return Request.CreateResponse(HttpStatusCode.OK, loadedPage);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new Resources().GenerateException(ex));
            }
        }

        /// <summary>
        /// Download document
        /// </summary>
        /// <param name="path">string</param>
        /// <param name="annotated">bool</param>
        /// <returns></returns>
        [HttpGet]
        [Route("annotation/downloadDocument")]
        public HttpResponseMessage DownloadDocument(string path, bool annotated)
        {
            string pathToDownload = "";
            // prepare response message
            HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            // check if signed document should be downloaded or original
            if (annotated)
            {
                pathToDownload = System.IO.Path.Combine(DirectoryUtils.OutputDirectory.GetPath(), path);
            }
            else
            {
                pathToDownload = System.IO.Path.Combine(DirectoryUtils.FilesDirectory.GetPath(), path);
            }
            // add file into the response
            if (File.Exists(pathToDownload))
            {
                var fileStream = new FileStream(pathToDownload, FileMode.Open);
                response.Content = new StreamContent(fileStream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                response.Content.Headers.ContentDisposition.FileName = path;
                return response;
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Upload document
        /// </summary>      
        /// <returns>Uploaded document object</returns>
        [HttpPost]
        [Route("annotation/uploadDocument")]
        public HttpResponseMessage UploadDocument()
        {
            try
            {
                string url = HttpContext.Current.Request.Form["url"];
                // get documents storage path
                string documentStoragePath = GlobalConfiguration.Annotation.FilesDirectory;
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
                                fileSavePath = new Resources().GetFreeFileName(documentStoragePath, httpPostedFile.FileName);
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
                            fileSavePath = new Resources().GetFreeFileName(documentStoragePath, fileName);
                        }
                        // Download the Web resource and save it into the current filesystem folder.
                        client.DownloadFile(url, fileSavePath);
                    }
                }
                UploadedDocumentEntity uploadedDocument = new UploadedDocumentEntity();
                uploadedDocument.guid = fileSavePath;
                return Request.CreateResponse(HttpStatusCode.OK, uploadedDocument);
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.OK, new Resources().GenerateException(ex));
            }
        }

        /// <summary>
        /// Get text coordinates document
        /// </summary>      
        /// <returns>Text coordinates object</returns>
        [HttpPost]
        [Route("annotation/textCoordinates")]
        public HttpResponseMessage TextCoordinates(AnnotationPostedDataEntity textCoordinatesRequest)
        {
            string password = "";
            try
            {
                // get/set parameters
                string documentGuid = textCoordinatesRequest.guid;
                password = textCoordinatesRequest.password;
                int pageNumber = textCoordinatesRequest.pageNumber;
                // get document info
                DocumentInfoContainer info = AnnotationImageHandler.GetDocumentInfo(System.IO.Path.GetFileName(documentGuid), password);
                // get all rows info for specific page
                List<RowData> rows = info.Pages[pageNumber - 1].Rows;
                // initiate list of the TextRowEntity
                List<TextRowEntity> textCoordinates = new List<TextRowEntity>();
                // get each row info
                for (int i = 0; i < rows.Count; i++)
                {
                    TextRowEntity textRow = new TextRowEntity()
                    {
                        textCoordinates = info.Pages[pageNumber - 1].Rows[i].TextCoordinates,
                        lineTop = info.Pages[pageNumber - 1].Rows[i].LineTop,
                        lineHeight = info.Pages[pageNumber - 1].Rows[i].LineHeight
                    };
                    textCoordinates.Add(textRow);
                }
                return Request.CreateResponse(HttpStatusCode.OK, textCoordinates);
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
        [Route("annotation/annotate")]
        public HttpResponseMessage Annotate(AnnotationPostedDataEntity annotateDocumentRequest)
        {
            string password = "";
            System.Exception notSupportedException = null;
            try
            {
                // get/set parameters
                string documentGuid = annotateDocumentRequest.guid;
                password = annotateDocumentRequest.password;
                AnnotationDataEntity[] annotationsData = annotateDocumentRequest.annotationsData;
                // initiate list of annotations to add
                List<AnnotationInfo> annotations = new List<AnnotationInfo>();
                // get document info - required to get document page height and calculate annotation top position
                DocumentInfoContainer documentInfo = AnnotationImageHandler.GetDocumentInfo(System.IO.Path.GetFileName(documentGuid), password);
                // check if document type is image
                if (SupportedImageFormats.Contains(System.IO.Path.GetExtension(documentGuid)))
                {
                    annotationsData[0].documentType = "image";
                }
                // initiate annotator object
                BaseAnnotator annotator = null;
                for (int i = 0; i < annotationsData.Length; i++)
                {
                    // create annotator
                    annotator = GetAnnotator(annotationsData[i], annotator, documentInfo);
                    // add annotation, if cuurent annotation type isn't supported by the current document type it will be ignored
                    try
                    {
                        AddAnnotationOptions(annotationsData[0].documentType, annotations, annotator);
                    }
                    catch (System.Exception ex)
                    {
                        if (ex.Message.Equals("Annotation of type " + annotationsData[i].type + " for this file type is not supported"))
                        {
                            notSupportedException = ex;
                            continue;
                        }
                        else
                        {
                            throw new System.Exception(ex.Message, ex);
                        }
                    }
                }
                // check if annottions array contains at least one annotation to add
                if (annotations.Count > 0)
                {
                    Stream result = null;
                    // Add annotation to the document
                    Stream cleanDoc = new FileStream(documentGuid, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
                    switch (annotationsData[0].documentType)
                    {
                        case "Portable Document Format":
                            result = AnnotationImageHandler.ExportAnnotationsToDocument(cleanDoc, annotations, DocumentType.Pdf);
                            break;
                        case "Microsoft Word":
                            result = AnnotationImageHandler.ExportAnnotationsToDocument(cleanDoc, annotations, DocumentType.Words);
                            break;
                        case "Microsoft PowerPoint":
                            result = AnnotationImageHandler.ExportAnnotationsToDocument(cleanDoc, annotations, DocumentType.Slides);
                            break;
                        case "image":
                            result = AnnotationImageHandler.ExportAnnotationsToDocument(cleanDoc, annotations, DocumentType.Images);
                            break;
                        case "Microsoft Excel":
                            result = AnnotationImageHandler.ExportAnnotationsToDocument(cleanDoc, annotations, DocumentType.Cells);
                            break;
                        case "AutoCAD Drawing File Format":
                            result = AnnotationImageHandler.ExportAnnotationsToDocument(cleanDoc, annotations, DocumentType.Diagram);
                            break;
                    }
                    cleanDoc.Close();
                    // Save result stream to file.
                    string path = System.IO.Path.Combine(GlobalConfiguration.Annotation.OutputDirectory, System.IO.Path.GetFileName(documentGuid));
                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                    {
                        byte[] buffer = new byte[result.Length];
                        result.Seek(0, SeekOrigin.Begin);
                        result.Read(buffer, 0, buffer.Length);
                        fileStream.Write(buffer, 0, buffer.Length);
                        fileStream.Close();
                    }
                    AnnotatedDocumentEntity annotatedDocument = new AnnotatedDocumentEntity()
                    {
                        guid = path,
                    };
                    return Request.CreateResponse(HttpStatusCode.OK, annotatedDocument);
                }
                else
                {
                    throw new NotSupportedException(notSupportedException.Message, notSupportedException);
                }
            }
            catch (System.Exception ex)
            {
                // set exception message
                return Request.CreateResponse(HttpStatusCode.OK, new Resources().GenerateException(ex));
            }
        }

        /**
         * get annotator object
         * @param annotationData
         * @param annotator
         * @param documentInfo
         * @return annotator object
         */
        private BaseAnnotator GetAnnotator(AnnotationDataEntity annotationData, BaseAnnotator annotator, DocumentInfoContainer documentInfo)
        {
            switch (annotationData.type)
            {
                case "text":
                    annotator = new TextAnnotator(annotationData, documentInfo);
                    break;
                case "area":
                    annotator = new AreaAnnotator(annotationData, documentInfo);
                    break;
                case "point":
                    annotator = new PointAnnotator(annotationData, documentInfo);
                    break;
                case "textStrikeout":
                    annotator = new TexStrikeoutAnnotator(annotationData, documentInfo);
                    break;
                case "polyline":
                    annotator = new PolylineAnnotator(annotationData, documentInfo);
                    break;
                case "textField":
                    annotator = new TextFieldAnnotator(annotationData, documentInfo);
                    break;
                case "watermark":
                    annotator = new WatermarkAnnotator(annotationData, documentInfo);
                    break;
                case "textReplacement":
                    annotator = new TextReplacementAnnotator(annotationData, documentInfo);
                    break;
                case "arrow":
                    annotator = new ArrowAnnotator(annotationData, documentInfo);
                    break;
                case "textRedaction":
                    annotator = new TextRedactionAnnotator(annotationData, documentInfo);
                    break;
                case "resourcesRedaction":
                    annotator = new ResourceRedactionAnnotator(annotationData, documentInfo);
                    break;
                case "textUnderline":
                    annotator = new TexUnderlineAnnotator(annotationData, documentInfo);
                    break;
                case "distance":
                    annotator = new DistanceAnnotator(annotationData, documentInfo);
                    break;
            }
            return annotator;
        }

        /**
         * Add current annotation options to annotations collection
         * @param documentType
         * @param annotationsCollection
         * @param annotator
         * @throws ParseException
         */
        private void AddAnnotationOptions(string documentType, List<AnnotationInfo> annotationsCollection, BaseAnnotator annotator)
        {
            switch (documentType)
            {
                case "Portable Document Format":
                    annotationsCollection.Add(annotator.AnnotatePdf());
                    break;
                case "Microsoft Word":
                    annotationsCollection.Add(annotator.AnnotateWord());
                    break;
                case "Microsoft PowerPoint":
                    annotationsCollection.Add(annotator.AnnotateSlides());
                    break;
                case "image":
                    annotationsCollection.Add(annotator.AnnotateImage());
                    break;
                case "Microsoft Excel":
                    annotationsCollection.Add(annotator.AnnotateCells());
                    break;
                case "AutoCAD Drawing File Format":
                    annotationsCollection.Add(annotator.AnnotateDiagram());
                    break;
            }
        }

        /**
         * get all annotations from the document
         * @param documentType
         * @param documentType
         * @return array of the annotations
         */
        private AnnotationInfo[] GetAnnotations(string documentGuid, string documentType)
        {
            AnnotationInfo[] annotations = null;
            try
            {
                Stream documentStream = new FileStream(documentGuid, FileMode.Open, FileAccess.ReadWrite);
                switch (documentType)
                {
                    case "PDF":
                        annotations = new PdfImporter(documentStream, AnnotationImageHandler).ImportAnnotations();
                        break;
                    case "WORDS":
                        annotations = new WordImporter(documentStream, AnnotationImageHandler).ImportAnnotations();
                        break;
                    case "SLIDES":
                        annotations = new SlidesImporter(documentStream, AnnotationImageHandler).ImportAnnotations();
                        break;
                    case "image":
                        annotations = new ImageImporter(documentStream, AnnotationImageHandler).ImportAnnotations();
                        break;
                    case "CELLS":
                        annotations = new CellsImporter(documentStream, AnnotationImageHandler).ImportAnnotations();
                        break;
                    case "diagram":
                        annotations = new DiagramImporter(documentStream, AnnotationImageHandler).ImportAnnotations();
                        break;
                }
                documentStream.Close();
                return annotations;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message, ex);
            }
        }

        /**
         * set all imported annotations data
         * @param annotations
         * 
         * @return annotations data entity array
         */
        private AnnotationDataEntity[] SetAnnotations(AnnotationInfo[] annotations, int pageNumber)
        {
            // initiate annotations data array
            AnnotationDataEntity[] pageAnnotations = new AnnotationDataEntity[annotations.Length];
            // set each annotation data
            for (int n = 0; n < annotations.Length; n++)
            {
                if (pageNumber == annotations[n].PageNumber + 1)
                {
                    AnnotationDataEntity annotation = new AnnotationDataEntity()
                    {
                        font = annotations[n].FontFamily,
                        fontSize = (annotations[n].FontSize != null) ? float.Parse(annotations[n].FontSize.ToString()) : 0,
                        height = annotations[n].Box.Height,
                        left = annotations[n].Box.X,
                        pageNumber = (int)annotations[n].PageNumber + 1,
                        svgPath = (annotations[n].Type == AnnotationType.Distance) ? annotations[n].SvgPath.Replace("L", " l") : annotations[n].SvgPath,
                        text = (String.IsNullOrEmpty(annotations[n].Text)) ? annotations[n].FieldText : annotations[n].Text,
                        top = annotations[n].Box.Y,
                        type = Char.ToLowerInvariant(Enum.GetName(typeof(AnnotationType), annotations[n].Type)[0]) + Enum.GetName(typeof(AnnotationType), annotations[n].Type).Substring(1),
                        width = annotations[n].Box.Width
                    };
                    // set each creply data
                    if (annotations[n].Replies != null && annotations[n].Replies.Length > 0)
                    {
                        CommentsEntity[] comments = new CommentsEntity[annotations[n].Replies.Length];
                        for (int m = 0; m < annotations[n].Replies.Length; m++)
                        {
                            CommentsEntity comment = new CommentsEntity()
                            {
                                text = annotations[n].Replies[m].Message,
                                time = annotations[n].Replies[m].RepliedOn.ToString(),
                                userName = annotations[n].Replies[m].UserName
                            };
                            comments[m] = comment;
                        }
                        annotation.comments = comments;
                    }
                    pageAnnotations[n] = annotation;
                }
            }
            return pageAnnotations;
        }
    }
}