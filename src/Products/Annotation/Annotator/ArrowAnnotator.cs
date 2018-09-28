using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class ArrowAnnotator : BaseAnnotator
    {

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="annotationData">AnnotationDataEntity</param>
        /// <param name="documentInfo">DocumentInfoContainer</param>
        public ArrowAnnotator(AnnotationDataEntity annotationData, DocumentInfoContainer documentInfo)
            : base(annotationData, documentInfo)
        {
        }

        /// <summary>
        /// Annotate Word document
        /// </summary>
        /// <returns></returns>
        public override AnnotationInfo AnnotateWord()
        {
            // calculate correct coordinates for the annotation - this is used since the GroupDocs.Annotation library counts the coordinates from the bottom of the document
            string startPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[0];
            string endPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[1];
            float startX = float.Parse(startPoint.Split(',')[0].Replace("M", ""));
            float startY = float.Parse(startPoint.Split(',')[1]);
            float endX = float.Parse(endPoint.Split(',')[0].Replace("L", "")) - startX;
            float endY = float.Parse(endPoint.Split(',')[1]) - startY;
            // set annotation position
            AnnotationInfo arrowAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                Box = new Rectangle(startX, startY, endX, endY),
                //set page number
                PageNumber = annotationData.pageNumber - 1,
                SvgPath = "M" + startX + "," + startY + "L" + endX + "," + endY,
                Type = AnnotationType.Arrow
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
                arrowAnnotation.Replies = replies;
            }
            return arrowAnnotation;
        }

        /// <summary>
        /// Annotate PDF document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotatePdf()
        {
            // calculate correct coordinates for the annotation - this is used since the GroupDocs.Annotation library counts the coordinates from the bottom of the document
            string startPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[0];
            string endPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[1];
            float startX = float.Parse(startPoint.Split(',')[0].Replace("M", ""));
            float startY = float.Parse(startPoint.Split(',')[1]);
            float endX = float.Parse(endPoint.Split(',')[0].Replace("L", "")) - startX;
            float endY = float.Parse(endPoint.Split(',')[1]) - startY;
            // set annotation position
            AnnotationInfo arrowAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                Box = new Rectangle(startX, startY, endX, endY),
                //set page number
                PageNumber = annotationData.pageNumber - 1,
                SvgPath = "M" + startX + "," + startY + "L" + endX + "," + endY,
                Type = AnnotationType.Arrow
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
                arrowAnnotation.Replies = replies;
            }
            return arrowAnnotation;
        }

        /// <summary>
        /// Arrow annotation doesn't supported by the Excel document
        /// </summary>
        /// <returns></returns>
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }

        /// <summary>
        /// Annotate Power POint file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateSlides()
        {
            // calculate correct coordinates for the annotation - this is used since the GroupDocs.Annotation library counts the coordinates from the bottom of the document
            string startPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[0];
            string endPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[1];
            float startX = float.Parse(startPoint.Split(',')[0].Replace("M", ""));
            float startY = float.Parse(startPoint.Split(',')[1]);
            float endX = float.Parse(endPoint.Split(',')[0].Replace("L", "")) - startX;
            float endY = float.Parse(endPoint.Split(',')[1]) - startY;
            // set annotation position
            AnnotationInfo arrowAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                Box = new Rectangle(startX, startY, endX, endY),
                //set page number
                PageNumber = annotationData.pageNumber - 1,
                SvgPath = "M" + startX + "," + startY + "L" + endX + "," + endY,
                Type = AnnotationType.Arrow
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
                arrowAnnotation.Replies = replies;
            }
            return arrowAnnotation;
        }

        /// <summary>
        /// Annotate image file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateImage()
        {
            // calculate correct coordinates for the annotation - this is used since the GroupDocs.Annotation library counts the coordinates from the bottom of the document
            string startPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[0];
            string endPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[1];
            float startX = float.Parse(startPoint.Split(',')[0].Replace("M", ""));
            float startY = float.Parse(startPoint.Split(',')[1]);
            float endX = float.Parse(endPoint.Split(',')[0].Replace("L", "")) - startX;
            float endY = float.Parse(endPoint.Split(',')[1]) - startY;
            // set annotation position
            AnnotationInfo arrowAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                Box = new Rectangle(startX, startY, endX, endY),
                //set page number
                PageNumber = annotationData.pageNumber - 1,
                SvgPath = "M" + startX + "," + startY + "L" + endX + "," + endY,
                Type = AnnotationType.Arrow
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
                arrowAnnotation.Replies = replies;
            }
            return arrowAnnotation;
        }

        /// <summary>
        /// Annotate AutoCad file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateDiagram()
        {
            // calculate correct coordinates for the annotation - this is used since the GroupDocs.Annotation library counts the coordinates from the bottom of the document
            string startPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[0];
            string endPoint = annotationData.svgPath.Replace("[a-zA-Z]+", "").Split(' ')[1];
            float startX = float.Parse(startPoint.Split(',')[0].Replace("M", ""));
            float startY = float.Parse(startPoint.Split(',')[1]);
            float endX = float.Parse(endPoint.Split(',')[0].Replace("L", "")) - startX;
            float endY = float.Parse(endPoint.Split(',')[1]) - startY;
            // set annotation position
            AnnotationInfo arrowAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                Box = new Rectangle(startX, startY, endX, endY),
                //set page number
                PageNumber = annotationData.pageNumber - 1,
                SvgPath = "M" + startX + "," + startY + "L" + endX + "," + endY,
                Type = AnnotationType.Arrow
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
                arrowAnnotation.Replies = replies;
            }
            return arrowAnnotation;
        }
    }
}