using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TextReplacementAnnotator : AbstractSvgAnnotator
    {

        public TextReplacementAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }
        
        public override AnnotationInfo AnnotateWord()
        {
            // init possible types of annotations
            AnnotationInfo textReplacementAnnotation = InitAnnotationInfo();
            return textReplacementAnnotation;
        }
        
        protected new AnnotationInfo InitAnnotationInfo()
        {
            AnnotationInfo textReplacementAnnotation = base.InitAnnotationInfo();
            textReplacementAnnotation.Guid = annotationData.id.ToString();
            textReplacementAnnotation.FieldText = annotationData.text;
            return textReplacementAnnotation;
        }
        
        protected String buildSvgPath()
        {
            double topPosition = pageData.Height - annotationData.top;
            double leftPosition = pageData.Width - annotationData.left;
            double topRightX = annotationData.left + annotationData.width;
            double bottomRightY = topPosition - annotationData.height;
            return base.GetSvgString(topPosition, leftPosition, topRightX, bottomRightY);
        }
        
        public override AnnotationInfo AnnotatePdf()
        {
            // init possible types of annotations
            AnnotationInfo textReplacementAnnotation = InitAnnotationInfo();
            // we use such calculation since the GroupDocs.Annotation library takes text line position from the bottom of the page
            float topPosition = pageData.Height - annotationData.top;
            textReplacementAnnotation.Box = new Rectangle(annotationData.left, topPosition, annotationData.width, annotationData.height);
            return textReplacementAnnotation;
        }
        
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException(String.Format(MESSAGE, annotationData.type));
        }
        
        public override AnnotationInfo AnnotateSlides()
        {
            throw new NotSupportedException(String.Format(MESSAGE, annotationData.type));
        }
        
        public override AnnotationInfo AnnotateImage()
        {
            throw new NotSupportedException(String.Format(MESSAGE, annotationData.type));
        }

        public override AnnotationInfo AnnotateDiagram()
        {
            throw new NotSupportedException(String.Format(MESSAGE, annotationData.type));
        }
        
        protected override Rectangle GetBox()
        {
            return new Rectangle(0, 0, 0, 0);
        }
        
        protected override AnnotationType GetType()
        {
            return AnnotationType.TextReplacement;
        }
    }
}