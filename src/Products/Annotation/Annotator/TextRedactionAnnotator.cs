using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TextRedactionAnnotator : TextHighlightAnnotation
    {
        public TextRedactionAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }

        public AnnotationBase annotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public AnnotationBase annotateSlides()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public AnnotationBase annotateImage()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public AnnotationBase annotateDiagram()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextRedaction;
        }
    }
}