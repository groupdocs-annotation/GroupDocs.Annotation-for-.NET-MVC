using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TextAnnotator : BaseAnnotator
    {
        public TextAnnotator(AnnotationDataEntity annotationData, DocumentInfoContainer documentInfo)
            : base(annotationData, documentInfo)
        {
        }

        /// <summary>
        /// Annotate Word document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateWord()
        {
            // we use such calculation since the GroupDocs.Annotation library takes text line position from the bottom of the page
            float topPosition = documentInfo.Pages[annotationData.pageNumber - 1].Height - annotationData.top;
            float topRightX = annotationData.left + annotationData.width;
            float bottomRightY = topPosition - annotationData.height;
            // init possible types of annotations
            AnnotationInfo textAnnotation = new AnnotationInfo()
            {
                PageNumber = annotationData.pageNumber - 1,
                Guid = annotationData.id.ToString(),
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                SvgPath = "[{\"x\":" + annotationData.left +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + annotationData.left +
                        ",\"y\":" + bottomRightY +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + bottomRightY + "}]",
                Type = AnnotationType.Text
            };
            // add replies
            if (annotationData.comments != null && annotationData.comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[annotationData.comments.Length];
                for (int i = 0; i < annotationData.comments.Length; i++)
                {
                    AnnotationReplyInfo reply = new AnnotationReplyInfo()
                    {
                        Message = annotationData.comments[i].text,
                        UserName = annotationData.comments[i].userName,
                        RepliedOn = DateTime.Parse(annotationData.comments[i].time)
                    };
                    replies[i] = reply;
                }
                textAnnotation.Replies = replies;
            }
            return textAnnotation;
        }

        /// <summary>
        /// Annotate PDf document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotatePdf()
        {
            // we use such calculation since the GroupDocs.Annotation library takes text line position from the bottom of the page
            float topPosition = documentInfo.Pages[annotationData.pageNumber - 1].Height - annotationData.top;
            float topRightX = annotationData.left + annotationData.width;
            float bottomRightY = topPosition - annotationData.height;
            // init possible types of annotations
            AnnotationInfo textAnnotation = new AnnotationInfo()
            {
                PageNumber = annotationData.pageNumber - 1,
                Guid = annotationData.id.ToString(),
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                SvgPath = "[{\"x\":" + annotationData.left +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + annotationData.left +
                        ",\"y\":" + bottomRightY +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + bottomRightY + "}]",
                Type = AnnotationType.Text
            };
            // add replies
            if (annotationData.comments != null && annotationData.comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[annotationData.comments.Length];
                for (int i = 0; i < annotationData.comments.Length; i++)
                {
                    AnnotationReplyInfo reply = new AnnotationReplyInfo()
                    {
                        Message = annotationData.comments[i].text,
                        UserName = annotationData.comments[i].userName,
                        RepliedOn = DateTime.Parse(annotationData.comments[i].time)
                    };
                    replies[i] = reply;
                }
                textAnnotation.Replies = replies;
            }
            return textAnnotation;
        }

        /// <summary>
        /// Annotate Excel file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateCells()
        {
            // we use such calculation since the GroupDocs.Annotation library takes text line position from the bottom of the page
            float topPosition = documentInfo.Pages[annotationData.pageNumber - 1].Height - annotationData.top;
            float topRightX = annotationData.left + annotationData.width;
            float bottomRightY = topPosition - annotationData.height;
            // init possible types of annotations
            AnnotationInfo textAnnotation = new AnnotationInfo()
            {
                PageNumber = annotationData.pageNumber - 1,
                Guid = annotationData.id.ToString(),
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                SvgPath = "[{\"x\":" + annotationData.left +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + annotationData.left +
                        ",\"y\":" + bottomRightY +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + bottomRightY + "}]",
                Type = AnnotationType.Text
            };
            // add replies
            if (annotationData.comments != null && annotationData.comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[annotationData.comments.Length];
                for (int i = 0; i < annotationData.comments.Length; i++)
                {
                    AnnotationReplyInfo reply = new AnnotationReplyInfo()
                    {
                        Message = annotationData.comments[i].text,
                        UserName = annotationData.comments[i].userName,
                        RepliedOn = DateTime.Parse(annotationData.comments[i].time)
                    };
                    replies[i] = reply;
                }
                textAnnotation.Replies = replies;
            }
            return textAnnotation;
        }

        /// <summary>
        /// Annotate Power Point document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateSlides()
        {
            // we use such calculation since the GroupDocs.Annotation library takes text line position from the bottom of the page
            float topPosition = documentInfo.Pages[annotationData.pageNumber - 1].Height - annotationData.top;
            float topRightX = annotationData.left + annotationData.width;
            float bottomRightY = topPosition - annotationData.height;
            // init possible types of annotations
            AnnotationInfo textAnnotation = new AnnotationInfo()
            {
                PageNumber = annotationData.pageNumber - 1,
                Guid = annotationData.id.ToString(),
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                SvgPath = "[{\"x\":" + annotationData.left +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + annotationData.left +
                        ",\"y\":" + bottomRightY +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + bottomRightY + "}]",
                Type = AnnotationType.Text
            };
            // add replies
            if (annotationData.comments != null && annotationData.comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[annotationData.comments.Length];
                for (int i = 0; i < annotationData.comments.Length; i++)
                {
                    AnnotationReplyInfo reply = new AnnotationReplyInfo()
                    {
                        Message = annotationData.comments[i].text,
                        UserName = annotationData.comments[i].userName,
                        RepliedOn = DateTime.Parse(annotationData.comments[i].time)
                    };
                    replies[i] = reply;
                }
                textAnnotation.Replies = replies;
            }
            return textAnnotation;
        }

        /// <summary>
        /// Annotate image file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateImage()
        {
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }

        /// <summary>
        /// Annotate AutoCad file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateDiagram()
        {
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }
    }
}