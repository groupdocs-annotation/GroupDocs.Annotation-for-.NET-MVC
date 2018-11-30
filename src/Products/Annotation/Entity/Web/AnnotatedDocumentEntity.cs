using GroupDocs.Annotation.MVC.Products.Common.Entity.Web;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web
{
    public class AnnotatedDocumentEntity : DocumentDescriptionEntity
    {
        public string guid;
        public AnnotationDataEntity[] annotations;
        public string data;
    }
}