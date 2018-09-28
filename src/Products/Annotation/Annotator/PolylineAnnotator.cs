using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class PolylineAnnotator : BaseAnnotator
    {
        public PolylineAnnotator(AnnotationDataEntity annotationData, DocumentInfoContainer documentInfo)
            : base(annotationData, documentInfo)
        {
        }

        /// <summary>
        /// Annotate Word document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateWord()
        {
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }

        /// <summary>
        /// Annotate PDF document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotatePdf()
        {
            AnnotationInfo polylineAnnotation = new AnnotationInfo()
            {
                PageNumber = annotationData.pageNumber - 1,
                PenColor = 1201033,
                PenWidth = 2,
                SvgPath = annotationData.svgPath,
                Type = AnnotationType.Polyline
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
                polylineAnnotation.Replies = replies;
            }
            return polylineAnnotation;
        }

        /// <summary>
        /// Annotate Excel document
        /// </summary>
        /// <returns></returns>
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }

        /// <summary>
        /// Annotate Power Point document
        /// </summary>
        /// <returns></returns>
        public override AnnotationInfo AnnotateSlides()
        {
            AnnotationInfo polylineAnnotation = new AnnotationInfo()
            {
                PageNumber = annotationData.pageNumber - 1,
                PenColor = 1201033,
                PenWidth = 2,
                SvgPath = annotationData.svgPath,
                Type = AnnotationType.Polyline,               
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
                polylineAnnotation.Replies = replies;
            }
            return polylineAnnotation;
        }

        /// <summary>
        /// Annotate image file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateImage()
        {
            AnnotationInfo polylineAnnotation = new AnnotationInfo()
            {               
                PenColor = 1201033,
                PenWidth = 2,
                SvgPath = annotationData.svgPath,
                Type = AnnotationType.Polyline,               
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
                polylineAnnotation.Replies = replies;
            }
            return polylineAnnotation;
        }

        /// <summary>
        /// Annotate AutoCad file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateDiagram()
        {
            AnnotationInfo polylineAnnotation = new AnnotationInfo()
            {
                PageNumber = annotationData.pageNumber - 1,
                PenColor = 1201033,
                PenWidth = 2,
                SvgPath = annotationData.svgPath,
                Type = AnnotationType.Polyline,               
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
                polylineAnnotation.Replies = replies;
            }
            return polylineAnnotation;
        }
    }
}