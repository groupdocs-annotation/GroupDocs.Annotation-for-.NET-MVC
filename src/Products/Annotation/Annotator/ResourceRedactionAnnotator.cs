using GroupDocs.Annotation.Models;
using GroupDocs.Annotation.Models.AnnotationModels;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using GroupDocs.Annotation.Options;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class ResourceRedactionAnnotator : BaseAnnotator
    {
        private ResourcesRedactionAnnotation resourcesRedactionAnnotation;

        public ResourceRedactionAnnotator(AnnotationDataEntity annotationData, PageData pageData)
            : base(annotationData, pageData)
        {
            this.resourcesRedactionAnnotation = new ResourcesRedactionAnnotation()
            {
                Box = GetBox()
            };
        }

        public override AnnotationBase AnnotateWord()
        {
            resourcesRedactionAnnotation = InitAnnotationBase(resourcesRedactionAnnotation) as ResourcesRedactionAnnotation;
            return resourcesRedactionAnnotation;
        }

        public override AnnotationBase AnnotatePdf()
        {
            resourcesRedactionAnnotation = InitAnnotationBase(resourcesRedactionAnnotation) as ResourcesRedactionAnnotation;
            return resourcesRedactionAnnotation;
        }

        public override AnnotationBase AnnotateCells()
        {
            throw new NotSupportedException(String.Format(Message, annotationData.type));
        }

        public override AnnotationBase AnnotateSlides()
        {
            resourcesRedactionAnnotation = InitAnnotationBase(resourcesRedactionAnnotation) as ResourcesRedactionAnnotation;
            return resourcesRedactionAnnotation;
        }

        public override AnnotationBase AnnotateImage()
        {
            resourcesRedactionAnnotation = InitAnnotationBase(resourcesRedactionAnnotation) as ResourcesRedactionAnnotation;
            return resourcesRedactionAnnotation;
        }

        public override AnnotationBase AnnotateDiagram()
        {
            // init annotation object
            resourcesRedactionAnnotation = InitAnnotationBase(resourcesRedactionAnnotation) as ResourcesRedactionAnnotation;
            return resourcesRedactionAnnotation;
        }

        protected override Rectangle GetBox()
        {
            return new Rectangle(annotationData.left, annotationData.top, annotationData.width, annotationData.height);
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.ResourcesRedaction;
        }
    }
}