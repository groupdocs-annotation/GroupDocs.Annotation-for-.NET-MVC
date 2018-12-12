using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class ResourceRedactionAnnotator : BaseAnnotator
    {
        public ResourceRedactionAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }
        
        public override AnnotationInfo AnnotateWord()
        {
            AnnotationInfo resourceRedactionAnnotation = InitAnnotationInfo();
            return resourceRedactionAnnotation;
        }
        
        public override AnnotationInfo AnnotatePdf()
        {
            // initiate AnnotationInfo object
            AnnotationInfo resourceRedactionAnnotation = InitAnnotationInfo();
            // set annotation X, Y position
            resourceRedactionAnnotation.AnnotationPosition = new Point(annotationData.left, annotationData.top);
            return resourceRedactionAnnotation;
        }
        
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }
        
        public override AnnotationInfo AnnotateSlides()
        {
            AnnotationInfo resourceRedactionAnnotation = InitAnnotationInfo();
            return resourceRedactionAnnotation;
        }
        
        public override AnnotationInfo AnnotateImage()
        {
            AnnotationInfo resourceRedactionAnnotation = InitAnnotationInfo();
            return resourceRedactionAnnotation;
        }
        
        public override AnnotationInfo AnnotateDiagram()
        {
            // init annotation object
            AnnotationInfo resourceRedactionAnnotation = InitAnnotationInfo();
            return resourceRedactionAnnotation;
        }
        
        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }
        
        protected override AnnotationType GetType()
        {
            return AnnotationType.ResourcesRedaction;
        }
    }
}