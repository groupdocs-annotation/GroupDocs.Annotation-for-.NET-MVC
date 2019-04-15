
using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

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
        public AnnotationDataEntity[] mapForPage(AnnotationInfo[] annotations, int pageNumber)
        {
            // initiate annotations data array
            AnnotationDataEntity[] pageAnnotations = new AnnotationDataEntity[annotations.Length];
            //  each annotation data - this functionality used since annotations data returned by the
            // GroupDocs.Annotation library are obfuscated
            for (int n = 0; n < annotations.Length; n++)
            {
                AnnotationInfo annotationInfo = annotations[n];
                if (pageNumber == annotationInfo.PageNumber + 1)
                {
                    AnnotationDataEntity annotation = mapAnnotationDataEntity(annotationInfo);
                    pageAnnotations[n] = annotation;
                }
            }
            return pageAnnotations;
        }

        /// <summary>
        /// Map AnnotationInfo instances into AnnotationDataEntity
        /// </summary>
        /// <param name="annotationInfo">AnnotationInfo</param>
        /// <returns>AnnotationDataEntity</returns>
        public AnnotationDataEntity mapAnnotationDataEntity(AnnotationInfo annotationInfo)
        {
            AnnotationDataEntity annotation = new AnnotationDataEntity();
            annotation.font = annotationInfo.FontFamily;
            double fontSize = Convert.ToDouble((annotationInfo.FontSize == null) ? 0 : annotationInfo.FontSize);
            annotation.fontSize = (float)fontSize;
            annotation.fontColor = (annotationInfo.FontColor == null) ? 0 : (int)annotationInfo.FontColor;
            annotation.height = annotationInfo.Box.Height;
            annotation.left = annotationInfo.Box.X;
            annotation.pageNumber = (int)annotationInfo.PageNumber + 1;           
            annotation.svgPath = (annotationInfo.SvgPath != null) ? annotationInfo.SvgPath.Replace("l", "L") : null;
            string text = (annotationInfo.Text == null) ? annotationInfo.FieldText : annotationInfo.Text;
            annotation.text = text;
            annotation.top = annotationInfo.Box.Y;
            annotation.type = Char.ToLowerInvariant(Enum.GetName(typeof(AnnotationType), annotationInfo.Type)[0]) + Enum.GetName(typeof(AnnotationType), annotationInfo.Type).Substring(1);
            annotation.width = annotationInfo.Box.Width;
            //  each reply data
            AnnotationReplyInfo[] replies = annotationInfo.Replies;
            if (replies != null && replies.Length > 0)
            {
                CommentsEntity[] comments = new CommentsEntity[replies.Length];
                for (int m = 0; m < replies.Length; m++)
                {
                    CommentsEntity comment = new CommentsEntity();
                    AnnotationReplyInfo reply = replies[m];
                    comment.text = reply.Message;
                    comment.time = reply.RepliedOn.ToString("yyyy-MM-dd HH:mm:ss");
                    comment.userName = reply.UserName;
                    comments[m] = comment;
                }
                annotation.comments = comments;
            }
            return annotation;
        }
    }
}