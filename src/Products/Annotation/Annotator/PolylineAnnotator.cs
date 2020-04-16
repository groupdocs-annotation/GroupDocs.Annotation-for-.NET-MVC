using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class PolylineAnnotator : BaseAnnotator
    {
        private PolylineAnnotation polylineAnnotation;

        public PolylineAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            this.polylineAnnotation = new PolylineAnnotation()
            {
                Box = GetBox(),
                PenColor = 1201033,
                PenWidth = 2,
                SvgPath = annotationData.svgPath
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            polylineAnnotation = InitAnnotationBase(polylineAnnotation) as PolylineAnnotation;
            return polylineAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            polylineAnnotation = InitAnnotationBase(polylineAnnotation) as PolylineAnnotation;
            return polylineAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(string.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            polylineAnnotation = InitAnnotationBase(polylineAnnotation) as PolylineAnnotation;
            FillCreatorName(polylineAnnotation);
            return polylineAnnotation;
        }

        /// <summary>
        /// Fill creator name field in annotation info
        /// </summary>
        /// <param name="polylineAnnotation">AnnotationBase</param>
        protected void FillCreatorName(AnnotationBase polylineAnnotation)
        {
            CommentsEntity[] comments = annotationData.comments;
            if (comments != null && comments.Length > 0 && comments[0] != null)
            {
                polylineAnnotation.User = new User() 
                { 
                    Name = comments[0].userName 
                };
            }
        }

        public override AnnotationBase AnnotateImage()
        {
            polylineAnnotation = InitAnnotationBase(polylineAnnotation) as PolylineAnnotation;
            FillCreatorName(polylineAnnotation);
            return polylineAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            polylineAnnotation = InitAnnotationBase(polylineAnnotation) as PolylineAnnotation;
            FillCreatorName(polylineAnnotation);
            return polylineAnnotation;
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.Polyline;
        }
    }
}