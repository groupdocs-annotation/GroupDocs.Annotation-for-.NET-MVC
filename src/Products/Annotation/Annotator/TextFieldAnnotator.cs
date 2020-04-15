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

        public TextFieldAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            this.textFieldAnnotation = new TextFieldAnnotation() {
                BackgroundColor = 65535,
                Box = GetBox(),
                Opacity = 0.7,
                FontFamily = !string.IsNullOrEmpty(annotationData.font) ? annotationData.font : "Arial",
                FontColor = annotationData.fontColor == 0 ? 65535 : annotationData.fontColor,
                FontSize = annotationData.fontSize == 0 ? 12 : annotationData.fontSize,
                Text = annotationData.text
            };
        }
        
        public override AnnotationBase AnnotateWord()
        {
            // init possible types of annotations
            textFieldAnnotation = InitAnnotationBase(textFieldAnnotation) as TextFieldAnnotation;

            return textFieldAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            // init possible types of annotations
            textFieldAnnotation = InitAnnotationBase(textFieldAnnotation) as TextFieldAnnotation;
            return textFieldAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            // init possible types of annotations
            textFieldAnnotation = InitAnnotationBase(textFieldAnnotation) as TextFieldAnnotation;
            return textFieldAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            // init possible types of annotations
            textFieldAnnotation = InitAnnotationBase(textFieldAnnotation) as TextFieldAnnotation;
            return textFieldAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            // init possible types of annotations
            textFieldAnnotation = InitAnnotationBase(textFieldAnnotation) as TextFieldAnnotation;
            return textFieldAnnotation;
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextField;
        }

        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }
    }
}