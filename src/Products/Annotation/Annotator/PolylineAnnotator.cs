using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;
using System.Globalization;
using System.Text;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class PolylineAnnotator : BaseAnnotator
    {
        private PolylineAnnotation polylineAnnotation;

        public PolylineAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            StringBuilder builder = new StringBuilder().
                Append("M").Append(annotationData.left.ToString(CultureInfo.InvariantCulture)).
                Append(",").Append(annotationData.top.ToString(CultureInfo.InvariantCulture)).
                Append("L").Append(annotationData.width.ToString(CultureInfo.InvariantCulture)).
                Append(",").Append(annotationData.height.ToString(CultureInfo.InvariantCulture));

            this.polylineAnnotation = new PolylineAnnotation()
            {
                Box = GetBox(),
                Opacity = 0.7,
                PenColor = 1201033,
                PenStyle = PenStyle.Dot,
                PenWidth = (byte)2,
                SvgPath = builder.ToString()
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
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            polylineAnnotation = InitAnnotationBase(polylineAnnotation) as PolylineAnnotation;
            fillCreatorName(polylineAnnotation);
            return polylineAnnotation;
        }

        /// <summary>
        /// Fill creator name field in annotation info
        /// </summary>
        /// <param name="polylineAnnotation">AnnotationBase</param>
        protected void fillCreatorName(AnnotationBase polylineAnnotation)
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
            fillCreatorName(polylineAnnotation);
            return polylineAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            polylineAnnotation = InitAnnotationBase(polylineAnnotation) as PolylineAnnotation;
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