using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class WatermarkAnnotator : AbstractTextAnnotator
    {

        public WatermarkAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }
        
        public override AnnotationInfo AnnotateWord()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationInfo AnnotatePdf()
        {
            // init possible types of annotations
            AnnotationInfo watermarkAnnotation = InitAnnotationInfo();
            watermarkAnnotation.AnnotationPosition = new Point(annotationData.left, annotationData.top);
            return watermarkAnnotation;
        }
        
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationInfo AnnotateSlides()
        {
            // init possible types of annotations
            AnnotationInfo watermarkAnnotation = InitAnnotationInfo();
            return watermarkAnnotation;
        }
        
        public override AnnotationInfo AnnotateImage()
        {
            // init possible types of annotations
            AnnotationInfo watermarkAnnotation = InitAnnotationInfo();
            watermarkAnnotation.FontColor = 15988609;
            return watermarkAnnotation;
        }
        
        public override AnnotationInfo AnnotateDiagram()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }
        
        protected override AnnotationType GetType()
        {
            return AnnotationType.Watermark;
        }
    }
}