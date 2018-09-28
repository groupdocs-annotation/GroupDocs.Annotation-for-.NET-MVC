
using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class PointAnnotator : BaseAnnotator
    {
        public PointAnnotator(AnnotationDataEntity annotationData, DocumentInfoContainer documentInfo)
            : base(annotationData, documentInfo)
        {
        }

        /// <summary>
        /// Annotate Word document
        /// </summary>
        /// <returns></returns>
        public override AnnotationInfo AnnotateWord()
        {
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }

        /// <summary>
        /// Annotate PDF document
        /// </summary>
        /// <returns></returns>
        public override AnnotationInfo AnnotatePdf()
        {
            // init annotation object
            AnnotationInfo pointAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                // set draw annotation properties
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                //set page number
                PageNumber = annotationData.pageNumber - 1,
                // sert annotation type
                Type = AnnotationType.Point
            };
            // add replies
            if (annotationData.comments != null && annotationData.comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[annotationData.comments.Length];
                for (int i = 0; i < annotationData.comments.Length; i++)
                {
                    AnnotationReplyInfo reply = new AnnotationReplyInfo();
                    reply.Message = annotationData.comments[i].text;
                    reply.RepliedOn = DateTime.Parse(annotationData.comments[i].time);
                    reply.UserName = annotationData.comments[i].userName;
                    replies[i] = reply;
                }
                pointAnnotation.Replies = replies;
            }
            return pointAnnotation;
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
        /// Annotate Power POint document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateSlides()
        {
            // init annotation object
            AnnotationInfo pointAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                // set draw properties
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                //set page number
                PageNumber = annotationData.pageNumber - 1,
                // set type
                Type = AnnotationType.Point
            };
            // add replies
            if (annotationData.comments != null && annotationData.comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[annotationData.comments.Length];
                for (int i = 0; i < annotationData.comments.Length; i++)
                {
                    AnnotationReplyInfo reply = new AnnotationReplyInfo();
                    reply.Message = annotationData.comments[i].text;
                    reply.RepliedOn = DateTime.Parse(annotationData.comments[i].time);
                    reply.UserName = annotationData.comments[i].userName;
                    replies[i] = reply;
                }
                pointAnnotation.Replies = replies;
            }
            return pointAnnotation;
        }

        /// <summary>
        /// Annotate image file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateImage()
        {
            // init annotation object
            AnnotationInfo pointAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                PageNumber = annotationData.pageNumber - 1,
                Type = AnnotationType.Point
            };
            // add replies
            if (annotationData.comments != null && annotationData.comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[annotationData.comments.Length];
                for (int i = 0; i < annotationData.comments.Length; i++)
                {
                    AnnotationReplyInfo reply = new AnnotationReplyInfo();
                    reply.Message = annotationData.comments[i].text;
                    reply.RepliedOn = DateTime.Parse(annotationData.comments[i].time);
                    reply.UserName = annotationData.comments[i].userName;
                    replies[i] = reply;
                }
                pointAnnotation.Replies = replies;
            }
            return pointAnnotation;
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