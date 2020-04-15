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

        public TexUnderlineAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            this.underlineAnnotation = new UnderlineAnnotation()
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
            underlineAnnotation = InitAnnotationBase(underlineAnnotation) as UnderlineAnnotation;
            underlineAnnotation.Points = GetPointsWithFixedTop();
            return underlineAnnotation;
        }

        // TODO: implement top fix only for specific annotation types
        private List<Point> GetPointsWithFixedTop()
        {
            return new List<Point>
                {
                    new Point(annotationData.left, pageData.Height - annotationData.top + annotationData.height - 20),
                    new Point(annotationData.left + annotationData.width, pageData.Height - annotationData.top + annotationData.height - 20),
                    new Point(annotationData.left, pageData.Height - annotationData.top - 20),
                    new Point(annotationData.left + annotationData.width, pageData.Height - annotationData.top - 20)
                };
        }

        public override AnnotationBase AnnotatePdf()
        {
            //SetFixTop(false);
            underlineAnnotation = InitAnnotationBase(underlineAnnotation) as UnderlineAnnotation;
            underlineAnnotation.Points = GetPointsWithFixedTop();
            return underlineAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            //SetFixTop(true);
            underlineAnnotation = InitAnnotationBase(underlineAnnotation) as UnderlineAnnotation;
            return underlineAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            //SetFixTop(false);
            underlineAnnotation = InitAnnotationBase(underlineAnnotation) as UnderlineAnnotation;
            return underlineAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.TextUnderline;
        }

        protected override Rectangle GetBox()
        {
            throw new NotImplementedException();
        }
    }
}