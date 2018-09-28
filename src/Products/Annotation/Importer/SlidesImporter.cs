using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Handler;
using System.IO;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Importer
{
    public class SlidesImporter : BaseImporter
    {
        
        public SlidesImporter(Stream documentStream, AnnotationImageHandler annotator)
            : base(documentStream, annotator)
        {            
        }

        public override AnnotationInfo[] ImportAnnotations()
        {
            AnnotationInfo[] annotations = annotator.ImportAnnotations(documentStream, DocumentType.Slides);
            return annotations;
        }
    }
}