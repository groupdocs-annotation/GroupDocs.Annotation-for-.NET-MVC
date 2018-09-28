using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.Domain.Containers;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class TextFieldAnnotator : BaseAnnotator
    {
        public TextFieldAnnotator(AnnotationDataEntity annotationData, DocumentInfoContainer documentInfo)
            : base(annotationData, documentInfo)
        {
        }

        /// <summary>
        /// Annotate Word document
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateWord()
        {
            // init possible types of annotations
            AnnotationInfo textFieldAnnotation = new AnnotationInfo()
            {
                FieldText = annotationData.text,
                FontFamily = annotationData.font,
                FontSize = annotationData.fontSize,
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                PageNumber = annotationData.pageNumber - 1,
                Type = AnnotationType.TextField
            };
            return textFieldAnnotation;
        }

        /// <summary>
        /// Annotate PDF deocument
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotatePdf()
        {
            // init possible types of annotations
            AnnotationInfo textFieldAnnotation = new AnnotationInfo()
            {
                FieldText = annotationData.text,
                FontFamily = annotationData.font,
                FontSize = annotationData.fontSize,
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                PageNumber = annotationData.pageNumber - 1,
                Type = AnnotationType.TextField
            };
            return textFieldAnnotation;
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
            // init possible types of annotations
            AnnotationInfo textFieldAnnotation = new AnnotationInfo()
            {
                FieldText = annotationData.text,
                FontFamily = annotationData.font,
                FontSize = annotationData.fontSize,
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                PageNumber = annotationData.pageNumber - 1,
                Type = AnnotationType.TextField
            };
            return textFieldAnnotation;
        }

        /// <summary>
        /// Annotate image file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateImage()
        {
            // init possible types of annotations
            AnnotationInfo textFieldAnnotation = new AnnotationInfo()
            {
                FieldText = annotationData.text,
                FontFamily = annotationData.font,
                FontSize = annotationData.fontSize,
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                PageNumber = annotationData.pageNumber - 1,
                Type = AnnotationType.TextField
            };
            return textFieldAnnotation;
        }

        /// <summary>
        /// Annotate AutoCad file
        /// </summary>
        /// <returns>AnnotationInfo</returns>
        public override AnnotationInfo AnnotateDiagram()
        {
            // init possible types of annotations
            // init possible types of annotations
            AnnotationInfo textFieldAnnotation = new AnnotationInfo()
            {
                FieldText = annotationData.text,
                FontFamily = annotationData.font,
                FontSize = annotationData.fontSize,
                Box = new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height),
                PageNumber = annotationData.pageNumber - 1,
                Type = AnnotationType.TextField
            };
            return textFieldAnnotation;
        }
    }
}