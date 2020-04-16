using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;
using System.Collections.Generic;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TexStrikeoutAnnotator : BaseAnnotator
    {
        private StrikeoutAnnotation strikeoutAnnotation;

        public TexStrikeoutAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            strikeoutAnnotation = new StrikeoutAnnotation()
            {
                Points = new List<Point>
                {
                    new Point(annotationData.left, pageData.Height - annotationData.top),
                    new Point(annotationData.left + annotationData.width, pageData.Height - annotationData.top),
                    new Point(annotationData.left, pageData.Height - annotationData.top - annotationData.height),
                    new Point(annotationData.left + annotationData.width, pageData.Height - annotationData.top - annotationData.height)
                }
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            //SetFixTop(true);
            strikeoutAnnotation = InitAnnotationBase(strikeoutAnnotation) as StrikeoutAnnotation;
            return strikeoutAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            //SetFixTop(false);
            strikeoutAnnotation = InitAnnotationBase(strikeoutAnnotation) as StrikeoutAnnotation;
            this.strikeoutAnnotation.FontColor = 0;
            return strikeoutAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(string.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            //SetFixTop(true);
            strikeoutAnnotation = InitAnnotationBase(strikeoutAnnotation) as StrikeoutAnnotation;
            return strikeoutAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            //SetFixTop(false);
            strikeoutAnnotation = InitAnnotationBase(strikeoutAnnotation) as StrikeoutAnnotation;
            return strikeoutAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            throw new NotSupportedException(string.Format(Message, annotationData.type));
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextStrikeout;
        }
    }
}