using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class AreaAnnotator : BaseAnnotator
    {

        public AreaAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }
        
        public override AnnotationInfo AnnotateWord()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationInfo AnnotatePdf()
        {
            // initiate AnnotationInfo object
            AnnotationInfo areaAnnotation = InitAnnotationInfo();
            // set annotation X, Y position
            areaAnnotation.AnnotationPosition = new Point(annotationData.left, annotationData.top);
            // add replies
            return areaAnnotation;
        }

        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationInfo AnnotateSlides()
        {
            return InitAnnotationInfo();
        }

        public override AnnotationInfo AnnotateImage()
        {
            return InitAnnotationInfo();
        }

        public override AnnotationInfo AnnotateDiagram()
        {
            return InitAnnotationInfo();
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