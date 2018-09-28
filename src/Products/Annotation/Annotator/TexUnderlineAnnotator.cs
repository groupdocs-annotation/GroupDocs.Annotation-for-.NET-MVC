using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TexUnderlineAnnotator : BaseAnnotator
    {
        public TexUnderlineAnnotator(AnnotationDataEntity annotationData, DocumentInfoContainer documentInfo)
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
            // calculation of the X-shift
            float topRightX = annotationData.left + annotationData.width;
            // calculation of the Y-shift
            float bottomRightY = topPosition - annotationData.height;
            // init possible types of annotations
            AnnotationInfo underlineAnnotation = new AnnotationInfo()
            {
                PageNumber = annotationData.pageNumber - 1,
                //   draw annotation properties
                Box = new Rectangle(annotationData.left, topPosition, annotationData.width, annotationData.height),
                SvgPath = "[{\"x\":" + annotationData.left +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + annotationData.left +
                        ",\"y\":" + bottomRightY +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + bottomRightY + "}]",
                //  annotation type
                Type = AnnotationType.TextUnderline,
                //  line color
                PenColor = 1201033
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
                underlineAnnotation.Replies = replies;
            }

            return underlineAnnotation;
        }

        /// <summary>
        /// Annotate PDF document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotatePdf()
        {
            // we use such calculation since the GroupDocs.Annotation library takes text line position from the bottom of the page
            float topPosition = documentInfo.Pages[annotationData.pageNumber - 1].Height - annotationData.top;
            // calculation of the X-shift
            float topRightX = annotationData.left + annotationData.width;
            // calculation of the Y-shift
            float bottomRightY = topPosition - annotationData.height;
            // init possible types of annotations
            AnnotationInfo underlineAnnotation = new AnnotationInfo()
            {
                PageNumber = annotationData.pageNumber - 1,
                //   draw annotation properties
                Box = new Rectangle(annotationData.left, topPosition, annotationData.width, annotationData.height),
                SvgPath = "[{\"x\":" + annotationData.left +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + annotationData.left +
                        ",\"y\":" + bottomRightY +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + bottomRightY + "}]",
                //  annotation type
                Type = AnnotationType.TextUnderline,
                //  line color
                PenColor = 1201033
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
                underlineAnnotation.Replies = replies;
            }

            return underlineAnnotation;
        }

        /// <summary>
        /// Annotate Excel document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }

        /// <summary>
        /// Annotate Power Point file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateSlides()
        {
            // we use such calculation since the GroupDocs.Annotation library takes text line position from the bottom of the page
            float topPosition = documentInfo.Pages[annotationData.pageNumber - 1].Height - annotationData.top;
            // calculation of the X-shift
            float topRightX = annotationData.left + annotationData.width;
            // calculation of the Y-shift
            float bottomRightY = topPosition - annotationData.height;
            // init possible types of annotations
            AnnotationInfo underlineAnnotation = new AnnotationInfo()
            {
                PageNumber = annotationData.pageNumber - 1,
                //   draw annotation properties
                Box = new Rectangle(annotationData.left, topPosition, annotationData.width, annotationData.height),
                SvgPath = "[{\"x\":" + annotationData.left +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + topPosition +
                        "},{\"x\":" + annotationData.left +
                        ",\"y\":" + bottomRightY +
                        "},{\"x\":" + topRightX +
                        ",\"y\":" + bottomRightY + "}]",
                //  annotation type
                Type = AnnotationType.TextUnderline,
                //  line color
                PenColor = 1201033
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
                underlineAnnotation.Replies = replies;
            }

            return underlineAnnotation;
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