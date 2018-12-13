using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class PolylineAnnotator : BaseAnnotator
    {
        public PolylineAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }
        
        public override AnnotationInfo AnnotateWord()
        {
            AnnotationInfo polylineAnnotation = InitAnnotationInfo();
            return polylineAnnotation;
        }
        
        public override AnnotationInfo AnnotatePdf()
        {
            AnnotationInfo polylineAnnotation = InitAnnotationInfo();
            return polylineAnnotation;
        }
        
        protected new AnnotationInfo InitAnnotationInfo()
        {
            AnnotationInfo polylineAnnotation = base.InitAnnotationInfo();
            polylineAnnotation.PenColor = 1201033;
            polylineAnnotation.PenWidth = (byte)2;
            polylineAnnotation.SvgPath = annotationData.svgPath;
            return polylineAnnotation;
        }
        
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }
        
        public override AnnotationInfo AnnotateSlides()
        {
            AnnotationInfo polylineAnnotation = InitAnnotationInfo();
            fillCreatorName(polylineAnnotation);
            return polylineAnnotation;
        }

        /// <summary>
        /// Fill creator name field in annotation info
        /// </summary>
        /// <param name="polylineAnnotation">AnnotationInfo</param>
        protected void fillCreatorName(AnnotationInfo polylineAnnotation)
        {
            CommentsEntity[] comments = annotationData.comments;
            if (comments != null && comments.Length > 0 && comments[0] != null)
            {
                polylineAnnotation.CreatorName = comments[0].userName;
            }
        }
        
        public override AnnotationInfo AnnotateImage()
        {
            AnnotationInfo polylineAnnotation = InitAnnotationInfo();
            fillCreatorName(polylineAnnotation);
            return polylineAnnotation;
        }
        
        public override AnnotationInfo AnnotateDiagram()
        {
            AnnotationInfo polylineAnnotation = InitAnnotationInfo();
            fillCreatorName(polylineAnnotation);
            return polylineAnnotation;
        }
        
        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }
        
        protected override AnnotationType GetType()
        {
            return AnnotationType.Polyline;
        }
    }
}