using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TextFieldAnnotator : AbstractTextAnnotator
    {
        public TextFieldAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }
        
        public override AnnotationInfo AnnotateWord()
        {
            // init possible types of annotations
            AnnotationInfo textFieldAnnotation = InitAnnotationInfo();
            return textFieldAnnotation;
        }
        
        public override AnnotationInfo AnnotatePdf()
        {
            // init possible types of annotations
            // Text field annotation
            AnnotationInfo textFieldAnnotation = InitAnnotationInfo();
            textFieldAnnotation.AnnotationPosition = new Point(annotationData.left, annotationData.top);
            return textFieldAnnotation;
        }
        
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }
        
        public override AnnotationInfo AnnotateSlides()
        {
            // init possible types of annotations
            AnnotationInfo textFieldAnnotation = InitAnnotationInfo();
            return textFieldAnnotation;
        }
        
        public override AnnotationInfo AnnotateImage()
        {
            // init possible types of annotations
            AnnotationInfo textFieldAnnotation = InitAnnotationInfo();           
            return textFieldAnnotation;
        }
        
        public override AnnotationInfo AnnotateDiagram()
        {
            // init possible types of annotations
            AnnotationInfo textFieldAnnotation = InitAnnotationInfo();           
            return textFieldAnnotation;
        }
        
        protected override AnnotationType GetType()
        {
            return AnnotationType.TextField;
        }
    }
}