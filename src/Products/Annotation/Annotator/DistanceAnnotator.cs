using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class DistanceAnnotator : BaseAnnotator
    {
        private DistanceAnnotation distanceAnnotation;

        public DistanceAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            this.distanceAnnotation = new DistanceAnnotation()
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
            distanceAnnotation = InitAnnotationBase(distanceAnnotation) as DistanceAnnotation;
            return distanceAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            distanceAnnotation = InitAnnotationBase(distanceAnnotation) as DistanceAnnotation;
            return distanceAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateImage()
        {
            // init annotation object
            distanceAnnotation = InitAnnotationBase(distanceAnnotation) as DistanceAnnotation;
            return distanceAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            // init annotation object
            distanceAnnotation = InitAnnotationBase(distanceAnnotation) as DistanceAnnotation;
            return distanceAnnotation;
        }

        protected new AnnotationBase InitAnnotationBase(AnnotationBase annotationBase)
        {
            AnnotationBase distanceAnnotation = base.InitAnnotationBase(annotationBase);
            // add replies
            string text = (annotationData.text == null) ? "" : annotationData.text;
            CommentsEntity[]
            comments = annotationData.comments;
            if (comments != null && comments.Length != 0)
            {
                Reply reply = distanceAnnotation.Replies[0];
                if (reply != null)
                {
                    reply.Comment = String.Format("{0} {1}", text, reply.Comment);
                }
            }

            return distanceAnnotation;
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.Distance;
        }

        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }
    }
}