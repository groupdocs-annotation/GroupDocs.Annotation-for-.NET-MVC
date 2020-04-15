using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.MVC.Products.Annotation;
using System;
using GroupDocs.Annotation.Options;
using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class WatermarkAnnotator : BaseAnnotator
    {
        private WatermarkAnnotation watermarkAnnotation;

        public WatermarkAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            this.watermarkAnnotation = new WatermarkAnnotation()
            {
                Box = GetBox(),
                Opacity = 0.7,
                FontFamily = !string.IsNullOrEmpty(annotationData.font) ? annotationData.font : "Arial",
                FontColor = annotationData.fontColor == 0 ? 65535 : annotationData.fontColor,
                FontSize = annotationData.fontSize == 0 ? 12 : annotationData.fontSize,
                Text = annotationData.text
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            // init possible types of annotations
            watermarkAnnotation = InitAnnotationBase(watermarkAnnotation) as WatermarkAnnotation;
            return watermarkAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            // init possible types of annotations
            watermarkAnnotation = InitAnnotationBase(watermarkAnnotation) as WatermarkAnnotation;
            return watermarkAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            // init possible types of annotations
            watermarkAnnotation = InitAnnotationBase(watermarkAnnotation) as WatermarkAnnotation;
            return watermarkAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            // init possible types of annotations
            watermarkAnnotation = InitAnnotationBase(watermarkAnnotation) as WatermarkAnnotation;
            return watermarkAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.Watermark;
        }

        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }
    }
}