using GroupDocs.Annotation.MVC.Products.Common.Entity.Web;
using System.Collections.Generic;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web
{
    public class AnnotatedDocumentEntity : DocumentDescriptionEntity
    {
        public string guid;
        public AnnotationDataEntity[] annotations;
        public string[] supportedAnnotations;
    }
}