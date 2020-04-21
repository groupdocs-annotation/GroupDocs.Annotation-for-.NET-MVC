using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TexUnderlineAnnotator : AbstractTextAnnotator
    {
        private UnderlineAnnotation underlineAnnotation;

        public TexUnderlineAnnotator(AnnotationDataEntity annotationData, PageInfo pageInfo)
            : base(annotationData, pageInfo)
        {
            underlineAnnotation = new UnderlineAnnotation
            {
                Points = GetPoints(annotationData, pageInfo)
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            underlineAnnotation = InitAnnotationBase(underlineAnnotation) as UnderlineAnnotation;
            underlineAnnotation.FontColor = 1201033;
            return underlineAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            return AnnotateWord();
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            underlineAnnotation = InitAnnotationBase(underlineAnnotation) as UnderlineAnnotation;
            underlineAnnotation.FontColor = 0;
            return underlineAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            return AnnotateWord();
        }

        public override AnnotationBase AnnotateDiagram()
        {
            throw new NotSupportedException(string.Format(Message, annotationData.type));
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextUnderline;
        }
    }
}