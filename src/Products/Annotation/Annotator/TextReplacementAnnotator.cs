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
            replacementAnnotation = new ReplacementAnnotation()
            {
                Points = new List<Point>
                {
                    new Point(annotationData.left, pageData.Height - annotationData.top),
                    new Point(annotationData.left + annotationData.width, pageData.Height - annotationData.top),
                    new Point(annotationData.left, pageData.Height - annotationData.top - annotationData.height),
                    new Point(annotationData.left + annotationData.width, pageData.Height - annotationData.top - annotationData.height)
                },
                TextToReplace = annotationData.text
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            replacementAnnotation = InitAnnotationBase(replacementAnnotation) as ReplacementAnnotation;
            return replacementAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            replacementAnnotation = InitAnnotationBase(replacementAnnotation) as ReplacementAnnotation;
            return replacementAnnotation;
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

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextReplacement;
        }
    }
}