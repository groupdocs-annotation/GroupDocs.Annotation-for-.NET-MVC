
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
        public AnnotationDataEntity[] mapForPage(AnnotationBase[] annotations, int pageNumber)
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
                    AnnotationDataEntity annotation = mapAnnotationDataEntity(annotationInfo);
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
        public AnnotationDataEntity mapAnnotationDataEntity(AnnotationBase annotationInfo)
        {
            AnnotationDataEntity annotation = new AnnotationDataEntity();
            annotation.font = annotationInfo is IFontFamily ? ((IFontFamily)annotationInfo).FontFamily : "";
            double fontSize = annotationInfo is IFontSize ? Convert.ToDouble((((IFontSize)annotationInfo).FontSize == null) ? 0 : ((IFontSize)annotationInfo).FontSize) : 0;
            annotation.fontSize = (float)fontSize;
            annotation.fontColor = annotationInfo is IFontColor ? ((((IFontColor)annotationInfo).FontColor == null) ? 0 : (int)((IFontColor)annotationInfo).FontColor) : 0;
            annotation.height = annotationInfo is IBox ? ((IBox)annotationInfo).Box.Height : (annotationInfo is IPoints ? (((IPoints)annotationInfo).Points.Max(p => p.Y) - ((IPoints)annotationInfo).Points.Min(p => p.Y)) : 0);
            annotation.left = annotationInfo is IBox ? ((IBox)annotationInfo).Box.X : (annotationInfo is IPoints ? ((IPoints)annotationInfo).Points.Min(p => p.X) : 0);
            annotation.pageNumber = (int)annotationInfo.PageNumber + 1;
            annotation.svgPath = annotationInfo is ISvgPath ? ((((ISvgPath)annotationInfo).SvgPath != null) ? ((ISvgPath)annotationInfo).SvgPath.Replace("l", "L") : null) : null;
            string text = annotationInfo is IText ? (((IText)annotationInfo).Text ?? (annotationInfo is ITextToReplace ? ((ITextToReplace)annotationInfo).TextToReplace : "")) : "";
            annotation.text = text;
            // TODO: implement backward top fix only for specific annotation types
            //annotation.top = annotationInfo is IBox ? ((IBox)annotationInfo).Box.Y : (annotationInfo is IPoints ? ((IPoints)annotationInfo).Points.Min(p => p.Y) : 0);
            annotation.top = annotationInfo is IBox ? ((IBox)annotationInfo).Box.Y : (annotationInfo is IPoints ? (-(((IPoints)annotationInfo).Points.Min(p => p.Y) - 842)) : 0);
            annotation.type = Char.ToLowerInvariant(Enum.GetName(typeof(AnnotationType), annotationInfo.Type)[0]) + Enum.GetName(typeof(AnnotationType), annotationInfo.Type).Substring(1);
            annotation.width = annotationInfo is IBox ? ((IBox)annotationInfo).Box.Width : (annotationInfo is IPoints ? (((IPoints)annotationInfo).Points.Max(p => p.X) - ((IPoints)annotationInfo).Points.Min(p => p.X)) : 0);
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