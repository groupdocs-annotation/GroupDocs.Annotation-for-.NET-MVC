using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System.Collections.Generic;
using System.Text;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public abstract class AbstractSvgAnnotator : BaseAnnotator
    {
        private bool fixTop = false;

        protected AbstractSvgAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }

        protected new AnnotationBase InitAnnotationBase(AnnotationBase annotationBase)
        {
            annotationBase = base.InitAnnotationBase(annotationBase);

            return annotationBase;
        }

        /// <summary>
        /// Calculate svg position
        /// </summary>
        /// <returns>string</returns>
        protected string BuildSvgPath()
        {
            // we use such calculation since the GroupDocs.Annotation library takes text line position from the bottom of the page
            double topPosition = pageData.Height - annotationData.top;
            // calculation of the X-shift
            double topRightX = annotationData.left + annotationData.width;
            // calculation of the Y-shift
            double bottomRightY = topPosition - annotationData.height;
            return GetSvgString(topPosition, annotationData.left, topRightX, bottomRightY);
        }

        /// <summary>
        /// Build svg string
        /// </summary>
        /// <param name="top">double</param>
        /// <param name="left">double</param>
        /// <param name="right">double</param>
        /// <param name="bottom">double</param>
        /// <returns>string</returns>
        protected string GetSvgString(double top, double left, double right, double bottom)
        {
            return new StringBuilder().
                    Append("[{\"x\":").Append(left).
                    Append(",\"y\":").Append(top).
                    Append("},{\"x\":").Append(right).
                    Append(",\"y\":").Append(top).
                    Append("},{\"x\":").Append(left).
                    Append(",\"y\":").Append(bottom).
                    Append("},{\"x\":").Append(right).
                    Append(",\"y\":").Append(bottom).
                    Append("}]").ToString();
        }

        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }

        public void SetFixTop(bool fixTop)
        {
            this.fixTop = fixTop;
        }
    }
}