using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class AreaAnnotator : BaseAnnotator
    {
        private AreaAnnotation areaAnnotation;

        public AreaAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            this.areaAnnotation = new AreaAnnotation() 
            {
                Box = GetBox(),
                BackgroundColor = 65535,
                Opacity = 0.7,
                PenColor = 65535,
                PenStyle = PenStyle.Dot,
                PenWidth = 3
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            // initiate AnnotationBase object
            areaAnnotation = InitAnnotationBase(areaAnnotation) as AreaAnnotation;
            return areaAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            // initiate AnnotationBase object
            areaAnnotation = InitAnnotationBase(areaAnnotation) as AreaAnnotation;
            return areaAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            areaAnnotation = InitAnnotationBase(areaAnnotation) as AreaAnnotation;
            return areaAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            areaAnnotation = InitAnnotationBase(areaAnnotation) as AreaAnnotation;
            return areaAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            areaAnnotation = InitAnnotationBase(areaAnnotation) as AreaAnnotation;
            return areaAnnotation;
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.Area;
        }

        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }
    }
}