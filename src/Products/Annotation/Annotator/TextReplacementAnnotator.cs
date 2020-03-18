using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;
using System.Collections.Generic;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TextReplacementAnnotator : AbstractSvgAnnotator
    {

        private ReplacementAnnotation replacementAnnotation;

        public TextReplacementAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            this.replacementAnnotation = new ReplacementAnnotation()
            {
                Opacity = 0.7,
                FontColor = annotationData.fontColor == 0 ? 65535 : annotationData.fontColor,
                Points = new List<Point>
                {
                    new Point(annotationData.left, annotationData.top + annotationData.height),
                    new Point(annotationData.left + annotationData.width, annotationData.top + annotationData.height),
                    new Point(annotationData.left, annotationData.top),
                    new Point(annotationData.left + annotationData.width, annotationData.top)
                }
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            // init possible types of annotations
            replacementAnnotation = InitAnnotationBase(replacementAnnotation) as ReplacementAnnotation;
            return replacementAnnotation;
        }

        protected new AnnotationBase InitAnnotationBase()
        {
            replacementAnnotation = InitAnnotationBase(replacementAnnotation) as ReplacementAnnotation;
            return replacementAnnotation;
        }

        protected String buildSvgPath()
        {
            double topPosition = pageData.Height - annotationData.top;
            double leftPosition = pageData.Width - annotationData.left;
            double topRightX = annotationData.left + annotationData.width;
            double bottomRightY = topPosition - annotationData.height;
            return base.GetSvgString(topPosition, leftPosition, topRightX, bottomRightY);
        }

        public override AnnotationBase AnnotatePdf()
        {
            // init possible types of annotations
            replacementAnnotation = InitAnnotationBase(replacementAnnotation) as ReplacementAnnotation;
            return replacementAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateImage()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateDiagram()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextReplacement;
        }
    }
}