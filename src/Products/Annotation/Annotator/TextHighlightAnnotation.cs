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

        public TextHighlightAnnotation(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            //SetFixTop(false);
            this.highlightAnnotation = new HighlightAnnotation()
            {
                Points = new List<Point>
                {
                    new Point(annotationData.left, annotationData.top + annotationData.height),
                    new Point(annotationData.left + annotationData.width, annotationData.top + annotationData.height),
                    new Point(annotationData.left, annotationData.top),
                    new Point(annotationData.left + annotationData.width, annotationData.top)
                },
                BackgroundColor = 65535,
                Opacity = 0.5,
                FontColor = 0
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            // init possible types of annotations
            highlightAnnotation = InitAnnotationBase(highlightAnnotation) as HighlightAnnotation;
            return highlightAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            // init possible types of annotations
            highlightAnnotation = InitAnnotationBase(highlightAnnotation) as HighlightAnnotation;
            return highlightAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            // we use such calculation since the GroupDocs.Annotation library takes text line position from the bottom of the page
            highlightAnnotation = InitAnnotationBase(highlightAnnotation) as HighlightAnnotation;
            return highlightAnnotation;
        }

        public override AnnotationBase AnnotateSlides()
        {
            // init possible types of annotations
            highlightAnnotation = InitAnnotationBase(highlightAnnotation) as HighlightAnnotation;
            return highlightAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            // init possible types of annotations
            highlightAnnotation = InitAnnotationBase(highlightAnnotation) as HighlightAnnotation;
            return highlightAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        protected Rectangle getBox()
        {
            return new Rectangle(annotationData.left / 4, annotationData.top, annotationData.width, annotationData.height);
        }

        protected override AnnotationType GetType()
        {
            // TODO: check the type
            return AnnotationType.TextHighlight;
        }
    }
}