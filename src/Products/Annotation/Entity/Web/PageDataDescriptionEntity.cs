using GroupDocs.Annotation.MVC.Products.Common.Entity.Web;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web
{
    public class PageDataDescriptionEntity : PageDescriptionEntity
    {
        /// Annotation data    
        public string data;
        
        /// List of annotation data        
        public AnnotationDataEntity[] annotations;
    }
}