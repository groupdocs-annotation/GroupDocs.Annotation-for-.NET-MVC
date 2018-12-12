using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class ArrowAnnotator : AbstractBoxAnnotator
    {

        private bool withGuid = false;

        public ArrowAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }
        
        public override AnnotationInfo AnnotateWord()
        {
            withGuid = false;
            AnnotationInfo arrowAnnotation = InitAnnotationInfo();
            return arrowAnnotation;
        }
        
        public override AnnotationInfo AnnotatePdf()
        {
            withGuid = false;
            AnnotationInfo arrowAnnotation = InitAnnotationInfo();
            return arrowAnnotation;
        }
        
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }
        
        public override AnnotationInfo AnnotateSlides()
        {
            withGuid = true;
            // init annotation object
            AnnotationInfo arrowAnnotation = InitAnnotationInfo();
            arrowAnnotation.BackgroundColor = 15988609;
            return arrowAnnotation;
        }
        
        public override AnnotationInfo AnnotateImage()
        {
            withGuid = false;
            // init annotation object
            AnnotationInfo arrowAnnotation = InitAnnotationInfo();
            arrowAnnotation.BackgroundColor = -15988609;
            return arrowAnnotation;
        }


        public override AnnotationInfo AnnotateDiagram()
        {
            withGuid = false;
            // init annotation object
            AnnotationInfo arrowAnnotation = InitAnnotationInfo();
            arrowAnnotation.BackgroundColor = 15988609;
            return arrowAnnotation;
        }


        protected AnnotationReplyInfo getAnnotationReplyInfo(CommentsEntity comment)
        {
            AnnotationReplyInfo annotationReplyInfo = base.GetAnnotationReplyInfo(comment);
            if (withGuid)
            {
                annotationReplyInfo.ParentReplyGuid = annotationData.id.ToString();
            }
            return annotationReplyInfo;
        }


        protected override AnnotationType GetType()
        {
            return AnnotationType.Arrow;
        }
    }
}