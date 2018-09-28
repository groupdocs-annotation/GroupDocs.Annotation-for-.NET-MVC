using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class WatermarkAnnotator : BaseAnnotator
    {
        public WatermarkAnnotator(AnnotationDataEntity annotationData, DocumentInfoContainer documentInfo)
            : base(annotationData, documentInfo)
        {
        }

        /// <summary>
        /// Annotate Word document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateWord()
        {
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }

        /// <summary>
        /// Annotate PDf document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotatePdf()
        {
            AnnotationInfo watermarkAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                FieldText = annotationData.text,
                FontFamily = annotationData.font,
                FontSize = annotationData.fontSize,
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                PageNumber = annotationData.pageNumber - 1,
                Type = AnnotationType.Watermark
            };
            return watermarkAnnotation;
        }

        /// <summary>
        /// Annotate Excel document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateCells()
        {
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }

        /// <summary>
        /// Annotate Power POint document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateSlides()
        {
            AnnotationInfo watermarkAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                FieldText = annotationData.text,
                FontFamily = annotationData.font,
                FontSize = annotationData.fontSize,
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                PageNumber = annotationData.pageNumber - 1,
                Type = AnnotationType.Watermark
            };
            return watermarkAnnotation;
        }

        /// <summary>
        /// Annotate image file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateImage()
        {
            AnnotationInfo watermarkAnnotation = new AnnotationInfo()
            {
                AnnotationPosition = new Point(annotationData.left, annotationData.top),
                FieldText = annotationData.text,
                FontFamily = annotationData.font,
                FontSize = annotationData.fontSize,
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                PageNumber = annotationData.pageNumber - 1,
                Type = AnnotationType.Watermark
            };
            return watermarkAnnotation;
        }

        /// <summary>
        /// Annotate AutoCad file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateDiagram()
        {
            throw new NotSupportedException("Annotation of type " + annotationData.type + " for this file type is not supported");
        }
    }
}