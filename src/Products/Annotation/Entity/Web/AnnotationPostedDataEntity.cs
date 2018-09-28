using GroupDocs.Annotation.MVC.Products.Common.Entity.Web;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web
{
    /// <summary>
    /// SignaturePostedDataEntity
    /// </summary>
    public class AnnotationPostedDataEntity : PostedDataEntity
    {
        public int pageNumber { get; set; }
        public AnnotationDataEntity[] annotationsData { get; set;}
    }
}