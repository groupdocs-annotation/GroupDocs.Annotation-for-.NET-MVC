using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class DistanceAnnotator : BaseAnnotator
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="annotationData">AnnotationDataEntity</param>
        /// <param name="documentInfo">documentInfo</param>
        public DistanceAnnotator(AnnotationDataEntity annotationData, DocumentInfoContainer documentInfo)
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
            // set draw annotation properties
            string startPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[0];
            string endPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[1];
            float startX = float.Parse(startPoint.Split(',')[0].Replace("M", ""));
            float startY = float.Parse(startPoint.Split(',')[1]);
            float endX = float.Parse(endPoint.Split(',')[0].Replace("L", "")) - startX;
            float endY = float.Parse(endPoint.Split(',')[1]) - startY;
            // set annotation position
            AnnotationInfo distanceAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                Box = new Rectangle(startX, startY, endX, endY),
                //set page number
                PageNumber = annotationData.pageNumber - 1,
                SvgPath = "M" + startX + ',' + startY + "L" + endX + ',' + endY,
                // sert annotation type
                Type = AnnotationType.Distance
            };
            // add replies
            string text = (annotationData.text == null) ? "" : annotationData.text;
            if (annotationData.comments != null && annotationData.comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[annotationData.comments.Length];
                for (int i = 0; i < annotationData.comments.Length; i++)
                {
                    AnnotationReplyInfo reply = new AnnotationReplyInfo();
                    if (i == 0)
                    {
                        reply.Message = text + ' ' + annotationData.comments[i].text;
                    }
                    else
                    {
                        reply.Message = annotationData.comments[i].text;
                    }
                    reply.RepliedOn = DateTime.Parse(annotationData.comments[i].time);
                    reply.UserName = annotationData.comments[i].userName;
                    replies[i] = reply;
                }
                distanceAnnotation.Replies = replies;
            }
            else
            {
                distanceAnnotation.FieldText = text;
            }
            return distanceAnnotation;
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
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }

        /// <summary>
        /// Annotate Image file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateImage()
        {
            // set draw annotation properties
            string startPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[0];
            string endPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[1];
            float startX = float.Parse(startPoint.Split(',')[0].Replace("M", ""));
            float startY = float.Parse(startPoint.Split(',')[1]);
            float endX = float.Parse(endPoint.Split(',')[0].Replace("L", "")) - startX;
            float endY = float.Parse(endPoint.Split(',')[1]) - startY;
            // set annotation position
            AnnotationInfo distanceAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                Box = new Rectangle(startX, startY, endX, endY),
                //set page number
                PageNumber = annotationData.pageNumber - 1,
                SvgPath = "M" + startX + ',' + startY + "L" + endX + ',' + endY,
                // sert annotation type
                Type = AnnotationType.Distance
            };
            // add replies
            string text = (annotationData.text == null) ? "" : annotationData.text;
            if (annotationData.comments != null && annotationData.comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[annotationData.comments.Length];
                for (int i = 0; i < annotationData.comments.Length; i++)
                {
                    AnnotationReplyInfo reply = new AnnotationReplyInfo();
                    if (i == 0)
                    {
                        reply.Message = text + ' ' + annotationData.comments[i].text;
                    }
                    else
                    {
                        reply.Message = annotationData.comments[i].text;
                    }
                    reply.RepliedOn = DateTime.Parse(annotationData.comments[i].time);
                    reply.UserName = annotationData.comments[i].userName;
                    replies[i] = reply;
                }
                distanceAnnotation.Replies = replies;
            }
            else
            {
                distanceAnnotation.FieldText = text;
            }
            return distanceAnnotation;
        }

        /// <summary>
        /// Annotate AutoCad file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateDiagram()
        {
            // set draw annotation properties
            string startPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[0];
            string endPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[1];
            float startX = float.Parse(startPoint.Split(',')[0].Replace("M", ""));
            float startY = float.Parse(startPoint.Split(',')[1]);
            float endX = float.Parse(endPoint.Split(',')[0].Replace("L", "")) - startX;
            float endY = float.Parse(endPoint.Split(',')[1]) - startY;
            // set annotation position
            AnnotationInfo distanceAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                Box = new Rectangle(startX, startY, endX, endY),
                //set page number
                PageNumber = annotationData.pageNumber - 1,
                SvgPath = "M" + startX + ',' + startY + "L" + endX + ',' + endY,
                // sert annotation type
                Type = AnnotationType.Distance
            };
            // add replies
            string text = (annotationData.text == null) ? "" : annotationData.text;
            if (annotationData.comments != null && annotationData.comments.Length != 0)
            {
                AnnotationReplyInfo[] replies = new AnnotationReplyInfo[annotationData.comments.Length];
                for (int i = 0; i < annotationData.comments.Length; i++)
                {
                    AnnotationReplyInfo reply = new AnnotationReplyInfo();
                    if (i == 0)
                    {
                        reply.Message = text + ' ' + annotationData.comments[i].text;
                    }
                    else
                    {
                        reply.Message = annotationData.comments[i].text;
                    }
                    reply.RepliedOn = DateTime.Parse(annotationData.comments[i].time);
                    reply.UserName = annotationData.comments[i].userName;
                    replies[i] = reply;
                }
                distanceAnnotation.Replies = replies;
            }
            else
            {
                distanceAnnotation.FieldText = text;
            }
            return distanceAnnotation;
        }
    }
}