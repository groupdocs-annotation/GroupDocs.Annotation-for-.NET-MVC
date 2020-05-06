using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TextRedactionAnnotator : TextHighlightAnnotator
    {
        private TextRedactionAnnotation textRedactionAnnotation;

        public TextRedactionAnnotator(AnnotationDataEntity annotationData, PageInfo pageInfo)
            : base(annotationData, pageInfo)
        {
            textRedactionAnnotation = new TextRedactionAnnotation
            {
                Points = GetPoints(annotationData, pageInfo)
            };
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(string.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            throw new NotSupportedException(string.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateImage()
        {
            throw new NotSupportedException(string.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateDiagram()
        {
            throw new NotSupportedException(string.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotatePdf()
        {
            textRedactionAnnotation = InitAnnotationBase(textRedactionAnnotation) as TextRedactionAnnotation;
            return textRedactionAnnotation;
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextRedaction;
        }
    }
}