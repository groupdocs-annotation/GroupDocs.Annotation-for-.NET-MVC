using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class PointAnnotator : BaseAnnotator
    {
        private PointAnnotation pointAnnotation;

        public PointAnnotator(AnnotationDataEntity annotationData, PageInfo pageInfo)
            : base(annotationData, pageInfo)
        {
            pointAnnotation = new PointAnnotation()
            {
                Box = GetBox()
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            pointAnnotation = base.InitAnnotationBase(pointAnnotation) as PointAnnotation;
            return pointAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            pointAnnotation = base.InitAnnotationBase(pointAnnotation) as PointAnnotation;
            return pointAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            pointAnnotation = base.InitAnnotationBase(pointAnnotation) as PointAnnotation;
            return pointAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            pointAnnotation = base.InitAnnotationBase(pointAnnotation) as PointAnnotation;
            return pointAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            pointAnnotation = base.InitAnnotationBase(pointAnnotation) as PointAnnotation;
            return pointAnnotation;
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.Point;
        }
    }
}