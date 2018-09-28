
using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    /// <summary>
    /// BaseSigner
    /// </summary>
    public abstract class BaseAnnotator
    {
        protected AnnotationDataEntity annotationData;
        protected DocumentInfoContainer documentInfo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="annotationData">AnnotationDataEntity</param>
        /// <param name="documentInfo">DocumentInfoContainer</param>
        public BaseAnnotator(AnnotationDataEntity annotationData, DocumentInfoContainer documentInfo)
        {
            this.annotationData = annotationData;
            this.documentInfo = documentInfo;
        }

        /// <summary>
        /// Annotate Word document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotateWord();

        /// <summary>
        /// Annotate PDF document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotatePdf();

        /// <summary>
        /// Annotate Excel document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotateCells();

        /// <summary>
        /// Annotate Power Point document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotateSlides();

        /// <summary>
        /// Annotate image file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotateImage();

        /// <summary>
        /// Annotate AutoCad file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public abstract AnnotationInfo AnnotateDiagram();
    }
}