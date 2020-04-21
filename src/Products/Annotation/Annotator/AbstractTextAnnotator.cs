using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System.Collections.Generic;
using System.Text;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public abstract class AbstractTextAnnotator : BaseAnnotator
    {
        protected AbstractTextAnnotator(AnnotationDataEntity annotationData, PageInfo pageInfo)
            : base(annotationData, pageInfo)
        {

        }
        protected static List<Point> GetPoints(AnnotationDataEntity annotationData, PageInfo pageInfo)
        {
            return new List<Point>
                {
                    new Point(annotationData.left, pageInfo.Height - annotationData.top),
                    new Point(annotationData.left + annotationData.width, pageInfo.Height - annotationData.top),
                    new Point(annotationData.left, pageInfo.Height - annotationData.top - annotationData.height),
                    new Point(annotationData.left + annotationData.width, pageInfo.Height - annotationData.top - annotationData.height)
                };
        }
    }
}