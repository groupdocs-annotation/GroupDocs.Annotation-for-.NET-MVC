using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class DistanceAnnotator : AbstractBoxAnnotator
    {

        public DistanceAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
        }

        public override AnnotationInfo AnnotateWord()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationInfo AnnotatePdf()
        {
            AnnotationInfo distanceAnnotation = InitAnnotationInfo();

            return distanceAnnotation;
        }

        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationInfo AnnotateSlides()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationInfo AnnotateImage()
        {
            // init annotation object
            AnnotationInfo distanceAnnotation = InitAnnotationInfo();
            distanceAnnotation.BackgroundColor = 15988609;
            return distanceAnnotation;
        }

        public override AnnotationInfo AnnotateDiagram()
        {
            // init annotation object
            AnnotationInfo distanceAnnotation = InitAnnotationInfo();
            distanceAnnotation.BackgroundColor = 15988609;
            return distanceAnnotation;
        }

        protected new AnnotationInfo InitAnnotationInfo()
        {
            AnnotationInfo distanceAnnotation = base.InitAnnotationInfo();
            // add replies
            String text = (annotationData.text == null) ? "" : annotationData.text;
            CommentsEntity[]
            comments = annotationData.comments;
            if (comments != null && comments.Length != 0)
            {
                AnnotationReplyInfo reply = distanceAnnotation.Replies[0];
                if (reply != null)
                {
                    reply.Message = String.Format("{0} {1}", annotationData.text, reply.Message);
                }
            }
            else
            {
                distanceAnnotation.FieldText = text;
            }
            return distanceAnnotation;
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.Distance;
        }
    }
}