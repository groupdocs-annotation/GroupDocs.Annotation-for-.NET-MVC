using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TextAnnotator : AbstractSvgAnnotator
    {
        public TextAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            SetFixTop(false);
        }
        
        public override AnnotationInfo AnnotateWord()
        {
            // init possible types of annotations
            AnnotationInfo textAnnotation = InitAnnotationInfo();
            return textAnnotation;
        }
        
        protected new AnnotationInfo InitAnnotationInfo()
        {
            AnnotationInfo textAnnotation = base.InitAnnotationInfo();
            textAnnotation.Guid = annotationData.id.ToString();           
            return textAnnotation;
        }
        
        public override AnnotationInfo AnnotatePdf()
        {
            // init possible types of annotations
            AnnotationInfo textAnnotation = InitAnnotationInfo();
            return textAnnotation;
        }
        
        public override AnnotationInfo AnnotateCells()
        {
            // init possible types of annotations
            AnnotationInfo textAnnotation = base.InitAnnotationInfo();
            // we use such calculation since the GroupDocs.Annotation library takes text line position from the bottom of the page
            double topPosition = pageData.Height - annotationData.top;
            textAnnotation.AnnotationPosition = new Point(annotationData.left, topPosition);
            return textAnnotation;
        }
        
        public override AnnotationInfo AnnotateSlides()
        {
            // init possible types of annotations
            AnnotationInfo textAnnotation = InitAnnotationInfo();
            return textAnnotation;
        }
        
        public override AnnotationInfo AnnotateImage()
        {
            // init possible types of annotations
            AnnotationInfo textAnnotation = InitAnnotationInfo();
            return textAnnotation;
        }
        
        public override AnnotationInfo AnnotateDiagram()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }
        
        protected Rectangle getBox()
        {
            return new Rectangle(annotationData.left / 4, annotationData.top, annotationData.width, annotationData.height);
        }
        
        protected override AnnotationType GetType()
        {
            return AnnotationType.Text;
        }
    }
}