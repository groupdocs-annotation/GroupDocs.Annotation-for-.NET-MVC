using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Handler;
using System.IO;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Importer
{
    public class WordImporter : BaseImporter
    {
        
        public WordImporter(Stream documentStream, AnnotationImageHandler annotator)
            : base(documentStream, annotator)
        {            
        }

        public override AnnotationInfo[] ImportAnnotations()
        {
            AnnotationInfo[] annotations = annotator.ImportAnnotations(documentStream, DocumentType.Words);
            return annotations;
        }
    }
}