using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;
using System.Collections.Generic;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TexUnderlineAnnotator : BaseAnnotator
    {
        private UnderlineAnnotation underlineAnnotation;

        public TexUnderlineAnnotator(AnnotationDataEntity annotationData, PageInfo pageInfo)
            : base(annotationData, pageInfo)
        {
            underlineAnnotation = new UnderlineAnnotation()
            {
                Points = new List<Point>
                {
                    new Point(annotationData.left, pageInfo.Height - annotationData.top),
                    new Point(annotationData.left + annotationData.width, pageInfo.Height - annotationData.top),
                    new Point(annotationData.left, pageInfo.Height - annotationData.top - annotationData.height),
                    new Point(annotationData.left + annotationData.width, pageInfo.Height - annotationData.top - annotationData.height)
                }
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
            underlineAnnotation = InitAnnotationBase(underlineAnnotation) as UnderlineAnnotation;
            underlineAnnotation.FontColor = 1201033;
            return underlineAnnotation;
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
            underlineAnnotation = InitAnnotationBase(underlineAnnotation) as UnderlineAnnotation;
            underlineAnnotation.FontColor = 1201033;
            return underlineAnnotation;
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