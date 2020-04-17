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

        public ResourceRedactionAnnotator(AnnotationDataEntity annotationData, PageInfo pageInfo)
            : base(annotationData, pageInfo)
        {
            this.resourcesRedactionAnnotation = new ResourcesRedactionAnnotation
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
            throw new NotSupportedException(string.Format(Message, annotationData.type));
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
            resourcesRedactionAnnotation = InitAnnotationBase(resourcesRedactionAnnotation) as ResourcesRedactionAnnotation;
            return resourcesRedactionAnnotation;
        }

        protected override AnnotationType GetType()
        {
            return AnnotationType.ResourcesRedaction;
        }
    }
}