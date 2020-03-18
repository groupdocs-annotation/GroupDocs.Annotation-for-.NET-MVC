using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class ArrowAnnotator : BaseAnnotator
    {
        private bool withGuid = false;
        private ArrowAnnotation arrowAnnotation;

        public ArrowAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            this.arrowAnnotation = new ArrowAnnotation()
            {
                Box = GetBox(),
                Opacity = 0.7,
                PenColor = 65535,
                PenStyle = PenStyle.Dot,
                PenWidth = 3
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            //withGuid = false;
            arrowAnnotation = InitAnnotationBase(arrowAnnotation) as ArrowAnnotation;
            return arrowAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            withGuid = false;
            arrowAnnotation = InitAnnotationBase(arrowAnnotation) as ArrowAnnotation;
            return arrowAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            withGuid = true;
            // init annotation object
            arrowAnnotation = InitAnnotationBase(arrowAnnotation) as ArrowAnnotation;
            return arrowAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            withGuid = false;
            // init annotation object
            arrowAnnotation = InitAnnotationBase(arrowAnnotation) as ArrowAnnotation;
            return arrowAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            withGuid = false;
            // init annotation object
            arrowAnnotation = InitAnnotationBase(arrowAnnotation) as ArrowAnnotation;
            return arrowAnnotation;
        }

        protected Reply getAnnotationReplyInfo(CommentsEntity comment)
        {
            Reply annotationReplyInfo = base.GetAnnotationReplyInfo(comment);
            if (withGuid)
            {
                annotationReplyInfo.ParentReply = new Reply() { Id = annotationData.id };
            }
            return annotationReplyInfo;
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.Arrow;
        }

        protected override Rectangle GetBox()
        {
            // TODO: check possiblity to move in base class
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }
    }
}