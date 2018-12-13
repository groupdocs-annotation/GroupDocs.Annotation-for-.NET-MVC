
using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    /// <summary>
    /// BaseSigner
    /// </summary>
    public abstract class BaseAnnotator
    {
        public string Message = "Annotation of type {0} for this file type is not supported";       
        protected AnnotationDataEntity annotationData;
        protected PageData pageData;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="annotationData"></param>
        /// <param name="pageData"></param>
        public BaseAnnotator(AnnotationDataEntity annotationData, PageData pageData)
        {
            this.annotationData = annotationData;
            this.pageData = pageData;
        }

        /// <summary>
        /// Add area annotation into the Word document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotateWord();

        /// <summary>
        /// Add area annotation into the pdf document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotatePdf();

        /// <summary>
        /// Add area annotation into the Excel document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotateCells();

        /// <summary>
        /// Add area annotation into the Power Point document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotateSlides();

        /// <summary>
        /// Add area annotation into the image document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotateImage();

        /// <summary>
        /// Add area annotation into the document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotateDiagram();

        /// <summary>
        /// Initial for annotation info
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        protected AnnotationInfo InitAnnotationInfo()
        {
            AnnotationInfo annotationInfo = new AnnotationInfo();
            // draw annotation options
            annotationInfo.Box = GetBox();
            // set page number to add annotation
            annotationInfo.PageNumber = annotationData.pageNumber - 1;
            // set annotation type
            annotationInfo.Type = GetType();
            // add replies
            CommentsEntity[] comments = annotationData.comments;
            if (comments != null && comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[comments.Length];
                for (int i = 0; i < comments.Length; i++)
                {
                    AnnotationReplyInfo reply = GetAnnotationReplyInfo(comments[i]);
                    replies[i] = reply;
                }
                annotationInfo.Replies = replies;
            }
            return annotationInfo;
        }

        /// <summary>
        /// Initial for reply annotation info
        /// </summary>
        /// <param name="comment">CommentsEntity</param>
        /// <returns>AnnotationReplyInfo</returns>
        protected AnnotationReplyInfo GetAnnotationReplyInfo(CommentsEntity comment)
        {
            AnnotationReplyInfo reply = new AnnotationReplyInfo();
            reply.Message = comment.text;
            DateTime date = DateTime.Parse(comment.time);
            reply.RepliedOn = date;
            reply.UserName = comment.userName;
            return reply;
        }

        /// <summary>
        /// Get rectangle
        /// </summary>
        /// <returns>Rectangle</returns>
        protected abstract Rectangle GetBox();

        /// <summary>
        /// Get type of annotation
        /// </summary>
        /// <returns>byte</returns>
        protected new abstract AnnotationType GetType();

        /// <summary>
        /// Get Annotation info depending on document type
        /// </summary>
        /// <param name="documentType">string</param>
        /// <returns>AnnotationInfo</returns>
        public AnnotationInfo GetAnnotationInfo(string documentType)
        {
            switch (documentType)
            {
                case "Portable Document Format":
                    return AnnotatePdf();
                case "Microsoft Word":
                    return AnnotateWord();
                case "Microsoft PowerPoint":
                    return AnnotateSlides();
                case "image":
                    return AnnotateImage();
                case "Microsoft Excel":
                    return AnnotateCells();
                case "AutoCAD Drawing File Format":
                    return AnnotateDiagram();
                default:
                    throw new System.Exception("Wrong annotation data without document type!");
            }
        }

        /// <summary>
        /// Check if the current annotatin is supported
        /// </summary>
        /// <param name="documentType">string</param>
        /// <returns></returns>
        internal bool IsSupported(string documentType)
        {
            try
            {
                AnnotatorFactory.createAnnotator(annotationData, pageData).GetAnnotationInfo(documentType);
                return true;
            }
            catch (NotSupportedException)
            {
                Message = String.Format(Message, annotationData.type);
                return false;
            }
        }
    }
}