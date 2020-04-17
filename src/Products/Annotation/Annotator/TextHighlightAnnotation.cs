using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;
using System.Collections.Generic;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TextHighlightAnnotation : AbstractSvgAnnotator
    {
        private HighlightAnnotation highlightAnnotation;

        public TextHighlightAnnotation(AnnotationDataEntity annotationData, PageInfo pageInfo)
            : base(annotationData, pageInfo)
        {
            //SetFixTop(false);
            this.highlightAnnotation = new HighlightAnnotation()
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
            highlightAnnotation = InitAnnotationBase(highlightAnnotation) as HighlightAnnotation;
            return highlightAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            highlightAnnotation = InitAnnotationBase(highlightAnnotation) as HighlightAnnotation;
            return highlightAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            highlightAnnotation = InitAnnotationBase(highlightAnnotation) as HighlightAnnotation;
            return highlightAnnotation;
        }

        public override AnnotationBase AnnotateSlides()
        {
            highlightAnnotation = InitAnnotationBase(highlightAnnotation) as HighlightAnnotation;
            return highlightAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            highlightAnnotation = InitAnnotationBase(highlightAnnotation) as HighlightAnnotation;
            return highlightAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            throw new NotSupportedException(string.Format(Message, annotationData.type));
        }

        protected Rectangle getBox()
        {
            return new Rectangle(annotationData.left / 4, annotationData.top, annotationData.width, annotationData.height);
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextHighlight;
        }
    }
}