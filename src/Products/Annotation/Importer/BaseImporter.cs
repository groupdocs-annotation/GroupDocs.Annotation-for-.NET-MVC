using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Handler;
using System.IO;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Importer
{
    public class BaseImporter
    {
        protected FileStream documentStream;
        protected AnnotationImageHandler annotator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentStream"></param>
        /// <param name="annotator"></param>
        public BaseImporter(FileStream documentStream, AnnotationImageHandler annotator)
        {
            this.documentStream = documentStream;
            this.annotator = annotator;
        }

        /// <summary>
        /// Import the annotations from document
        /// </summary>
        /// <param name="docType">int</param>
        /// <returns>AnnotationInfo[]</returns>
        public AnnotationInfo[] ImportAnnotations(DocumentType docType)
        {
            AnnotationInfo[] annotations = annotator.ImportAnnotations(documentStream, docType);
            return annotations;
        }

    }
}