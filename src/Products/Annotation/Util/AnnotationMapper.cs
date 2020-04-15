﻿
using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.Models.AnnotationModels.Interfaces.Properties;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Util
{
    public class AnnotationMapper
    {
        /// <summary>
        /// Instance of mapper
        /// </summary>
        public static readonly AnnotationMapper instance = new AnnotationMapper();

        private AnnotationMapper()
        {
        }

        /// <summary>
        /// Map AnnotationInfo instances into AnnotationDataEntity
        /// </summary>
        /// <param name="annotations">AnnotationInfo[]</param>
        /// <param name="pageNumber">int</param>
        /// <returns></returns>
        public AnnotationDataEntity[] mapForPage(AnnotationBase[] annotations, int pageNumber, PageInfo pageInfo)
        {
            // initiate annotations data array
            IList<AnnotationDataEntity> pageAnnotations = new List<AnnotationDataEntity>();
            //  each annotation data - this functionality used since annotations data returned by the
            // GroupDocs.Annotation library are obfuscated
            for (int n = 0; n < annotations.Length; n++)
            {
                AnnotationBase annotationInfo = annotations[n];
                if (pageNumber == annotationInfo.PageNumber + 1)
                {
                    AnnotationDataEntity annotation = mapAnnotationDataEntity(annotationInfo, pageInfo);
                    pageAnnotations.Add(annotation);
                }
            }

            return pageAnnotations.ToArray();
        }

        /// <summary>
        /// Map AnnotationInfo instances into AnnotationDataEntity
        /// </summary>
        /// <param name="annotationInfo">AnnotationInfo</param>
        /// <returns>AnnotationDataEntity</returns>
        public AnnotationDataEntity mapAnnotationDataEntity(AnnotationBase annotationInfo, PageInfo pageInfo)
        {
            string annotationTypeName = Enum.GetName(typeof(AnnotationType), annotationInfo.Type);
            float maxY = 0, minY = 0, maxX = 0, minX = 0;
            float boxX = 0, boxY = 0, boxHeight = 0, boxWidth = 0;

            if (annotationInfo is IPoints)
            {
                maxY = ((IPoints)annotationInfo).Points.Max(p => p.Y);
                minY = ((IPoints)annotationInfo).Points.Min(p => p.Y);
                maxX = ((IPoints)annotationInfo).Points.Max(p => p.X);
                minX = ((IPoints)annotationInfo).Points.Min(p => p.X);
            }

            if (annotationInfo is IBox)
            {
                boxX = ((IBox)annotationInfo).Box.X;
                boxY = ((IBox)annotationInfo).Box.Y;
                boxHeight = ((IBox)annotationInfo).Box.Height;
                boxWidth = ((IBox)annotationInfo).Box.Width;
            }

            AnnotationDataEntity annotation = new AnnotationDataEntity();
            annotation.font = annotationInfo is IFontFamily ? ((IFontFamily)annotationInfo).FontFamily : "";
            double fontSize = annotationInfo is IFontSize ? Convert.ToDouble((((IFontSize)annotationInfo).FontSize == null) ? 0 : ((IFontSize)annotationInfo).FontSize) : 0;
            annotation.fontSize = (float)fontSize;
            annotation.fontColor = annotationInfo is IFontColor ? ((((IFontColor)annotationInfo).FontColor == null) ? 0 : (int)((IFontColor)annotationInfo).FontColor) : 0;
            annotation.height = annotationInfo is IBox ? boxHeight : (annotationInfo is IPoints ? (maxY - minY) : 0);
            annotation.left = annotationInfo is IBox ? boxX : (annotationInfo is IPoints ? minX : 0);
            annotation.pageNumber = (int)annotationInfo.PageNumber + 1;
            annotation.svgPath = annotationInfo is ISvgPath ? ((((ISvgPath)annotationInfo).SvgPath != null) ? ((ISvgPath)annotationInfo).SvgPath.Replace("l", "L") : null) : null;
            string text = annotationInfo is IText ? (((IText)annotationInfo).Text ?? (annotationInfo is ITextToReplace ? ((ITextToReplace)annotationInfo).TextToReplace : "")) : "";
            annotation.text = text;
            // TODO: implement backward top fix only for specific annotation types
            //annotation.top = annotationInfo is IBox ? boxY : (annotationInfo is IPoints ? minY : 0);
            annotation.top = annotationInfo is IBox ? boxY : (annotationInfo is IPoints ? pageInfo.Height - maxY : 0);
            annotation.type = char.ToLowerInvariant(annotationTypeName[0]) + annotationTypeName.Substring(1);
            annotation.width = annotationInfo is IBox ? boxWidth : (annotationInfo is IPoints ? (maxX - minX) : 0);
            //  each reply data
            Reply[] replies = annotationInfo.Replies.ToArray();
            if (replies != null && replies.Length > 0)
            {
                CommentsEntity[] comments = new CommentsEntity[replies.Length];
                for (int m = 0; m < replies.Length; m++)
                {
                    CommentsEntity comment = new CommentsEntity();
                    Reply reply = replies[m];
                    comment.text = reply.Comment;
                    comment.time = reply.RepliedOn.ToString("yyyy-MM-dd HH:mm:ss");
                    comment.userName = reply.User.Name;
                    comments[m] = comment;
                }
                annotation.comments = comments;
            }
            return annotation;
        }
    }
}