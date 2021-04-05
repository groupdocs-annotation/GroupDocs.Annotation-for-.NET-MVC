using GroupDocs.Annotation.MVC.Products.Common.Entity.Web;
using System.Collections.Generic;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web
{
    public class AnnotatedDocumentEntity : PageDescriptionEntity
    {
        public string guid { get; set; }
        public List<PageDataDescriptionEntity> pages { get; set; } = new List<PageDataDescriptionEntity>();
        public string[] supportedAnnotations { get; set; }
    }
}