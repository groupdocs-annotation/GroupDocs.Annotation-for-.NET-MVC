using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TexStrikeoutAnnotator : AbstractSvgAnnotator
    {
        public TexStrikeoutAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }
        
        public override AnnotationInfo AnnotateWord()
        {
            SetFixTop(true);
            // init possible types of annotations
            AnnotationInfo strikeoutAnnotation = InitAnnotationInfo();
            return strikeoutAnnotation;
        }
        
        public override AnnotationInfo AnnotatePdf()
        {
            SetFixTop(false);
            AnnotationInfo strikeoutAnnotation = InitAnnotationInfo();
            strikeoutAnnotation.AnnotationPosition = new Point(annotationData.left, annotationData.top);
            strikeoutAnnotation.PenColor = 0;
            strikeoutAnnotation.Guid = annotationData.id.ToString();
            return strikeoutAnnotation;
        }
        
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }
        
        public override AnnotationInfo AnnotateSlides()
        {
            SetFixTop(true);
            AnnotationInfo strikeoutAnnotation = InitAnnotationInfo();
            strikeoutAnnotation.AnnotationPosition = new Point(annotationData.left, annotationData.top);
            strikeoutAnnotation.PenColor = 0;
            return strikeoutAnnotation;
        }

        public override AnnotationInfo AnnotateImage()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationInfo AnnotateDiagram()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextStrikeout;
        }
    }
}