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
            this.strikeoutAnnotation = new StrikeoutAnnotation()
            {
                Opacity = 0.7,
                FontColor = annotationData.fontColor == 0 ? 65535 : annotationData.fontColor,
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
            // init possible types of annotations
            strikeoutAnnotation = InitAnnotationBase(strikeoutAnnotation) as StrikeoutAnnotation;
            return strikeoutAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            //SetFixTop(false);
            strikeoutAnnotation = InitAnnotationBase(strikeoutAnnotation) as StrikeoutAnnotation;
            return strikeoutAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
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
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextStrikeout;
        }

        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }
    }
}