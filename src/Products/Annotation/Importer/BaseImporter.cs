using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Handler;
using System.IO;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Importer
{
    public class BaseImporter
    {
        protected FileStream documentStream;
        protected string password;
        protected AnnotationImageHandler annotator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentStream"></param>
        /// <param name="annotator"></param>
        public BaseImporter(FileStream documentStream, AnnotationImageHandler annotator, string password)
        {
            this.documentStream = documentStream;
            this.annotator = annotator;
            this.password = password;
        }

        /// <summary>
        /// Import the annotations from document
        /// </summary>
        /// <param name="docType">int</param>
        /// <returns>AnnotationInfo[]</returns>
        public AnnotationInfo[] ImportAnnotations(DocumentType docType)
        {
            AnnotationInfo[] annotations = null;

            if (docType.Equals(DocumentType.Images))
            {
               annotations = annotator.ImportAnnotations(documentStream, docType);
            }
            else
            {
               annotations = annotator.ImportAnnotations(documentStream, docType, password);
            }           
            return annotations;
        }

    }
}