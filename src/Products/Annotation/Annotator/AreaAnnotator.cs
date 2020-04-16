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
            areaAnnotation = new AreaAnnotation() 
            {
                Box = GetBox()
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            areaAnnotation = InitAnnotationBase(areaAnnotation) as AreaAnnotation;
            return areaAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
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
    }
}