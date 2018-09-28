using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Handler;
using System.IO;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Importer
{
    public abstract class BaseImporter
    {
        protected Stream documentStream;
        protected AnnotationImageHandler annotator;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="documentStream">Stream</param>
        /// <param name="annotator">AnnotationImageHandler</param>
        public BaseImporter(Stream documentStream, AnnotationImageHandler annotator)
        {
            this.documentStream = documentStream;
            this.annotator = annotator;
        }

        /// <summary>
        /// Import annotations
        /// </summary>
        /// <returns>AnnotationInfo[]</returns>
        public abstract AnnotationInfo[] ImportAnnotations();

    }
}