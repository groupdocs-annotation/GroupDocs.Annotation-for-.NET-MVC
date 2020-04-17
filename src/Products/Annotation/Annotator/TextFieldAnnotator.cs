using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TextFieldAnnotator : BaseAnnotator
    {
        private TextFieldAnnotation textFieldAnnotation;

        public TextFieldAnnotator(AnnotationDataEntity annotationData, PageInfo pageInfo)
            : base(annotationData, pageInfo)
        {
            textFieldAnnotation = new TextFieldAnnotation() {
                Box = GetBox(),
                FontFamily = !string.IsNullOrEmpty(annotationData.font) ? annotationData.font : "Arial",
                FontColor = annotationData.fontColor,
                FontSize = annotationData.fontSize == 0 ? 12 : annotationData.fontSize,
                Text = annotationData.text
            };
        }
        
        public override AnnotationBase AnnotateWord()
        {
            textFieldAnnotation = InitAnnotationBase(textFieldAnnotation) as TextFieldAnnotation;
            return textFieldAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            textFieldAnnotation = InitAnnotationBase(textFieldAnnotation) as TextFieldAnnotation;
            return textFieldAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(string.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            textFieldAnnotation = InitAnnotationBase(textFieldAnnotation) as TextFieldAnnotation;
            return textFieldAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            textFieldAnnotation = InitAnnotationBase(textFieldAnnotation) as TextFieldAnnotation;
            return textFieldAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            textFieldAnnotation = InitAnnotationBase(textFieldAnnotation) as TextFieldAnnotation;
            return textFieldAnnotation;
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextField;
        }
    }
}