using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class ResourceRedactionAnnotator : BaseAnnotator
    {
        public ResourceRedactionAnnotator(AnnotationDataEntity annotationData, DocumentInfoContainer documentInfo)
            : base(annotationData, documentInfo)
        {
        }

        /// <summary>
        /// Annotate WOrd document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateWord()
        {
            AnnotationInfo resourceRedactionAnnotation = new AnnotationInfo()
            {
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                PageNumber = annotationData.pageNumber - 1,
                Type = AnnotationType.ResourcesRedaction
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
                resourceRedactionAnnotation.Replies = replies;
            }

            return resourceRedactionAnnotation;
        }

        /// <summary>
        /// Annotate PDF document
        /// </summary>
        /// <returns></returns>
        public override AnnotationInfo AnnotatePdf()
        {
            // initiate AnnotationInfo object
            AnnotationInfo resourceRedactionAnnotation = new AnnotationInfo()
            {
                // set annotation X, Y position
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                // draw annotation options
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                // set page number to add annotation
                PageNumber = annotationData.pageNumber - 1,
                // set annotation type
                Type = AnnotationType.ResourcesRedaction
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
                resourceRedactionAnnotation.Replies = replies;
            }

            return resourceRedactionAnnotation;
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
            // initiate AnnotationInfo object
            AnnotationInfo resourceRedactionAnnotation = new AnnotationInfo()
            {
                // set annotation X, Y position
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                // draw annotation options
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                // set page number to add annotation
                PageNumber = annotationData.pageNumber - 1,
                // set annotation type
                Type = AnnotationType.ResourcesRedaction
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
                resourceRedactionAnnotation.Replies = replies;
            }

            return resourceRedactionAnnotation;
        }

        /// <summary>
        /// Annotate image file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateImage()
        {
            // initiate AnnotationInfo object
            AnnotationInfo resourceRedactionAnnotation = new AnnotationInfo()
            {
                // set annotation X, Y position
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                // draw annotation options
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                // set page number to add annotation
                PageNumber = annotationData.pageNumber - 1,
                // set annotation type
                Type = AnnotationType.ResourcesRedaction
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
                resourceRedactionAnnotation.Replies = replies;
            }

            return resourceRedactionAnnotation;
        }

        /// <summary>
        /// Annotate AutoCad document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateDiagram()
        {
            // initiate AnnotationInfo object
            AnnotationInfo resourceRedactionAnnotation = new AnnotationInfo()
            {
                // set annotation X, Y position
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                // draw annotation options
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                // set page number to add annotation
                PageNumber = annotationData.pageNumber - 1,
                // set annotation type
                Type = AnnotationType.ResourcesRedaction
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
                resourceRedactionAnnotation.Replies = replies;
            }

            return resourceRedactionAnnotation;
        }
    }
}