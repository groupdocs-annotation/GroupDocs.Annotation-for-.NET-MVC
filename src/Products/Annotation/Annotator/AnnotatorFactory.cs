
using GroupDocs.Annotation.Domain;
using GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Annotator
{
    public class AnnotatorFactory
    {
        /// <summary>
        /// Create annotator instance depending on type of annotation
        /// </summary>
        /// <param name="annotationData">AnnotationDataEntity</param>
        /// <param name="pageData">PageData</param>
        /// <returns></returns>
        public static BaseAnnotator createAnnotator(AnnotationDataEntity annotationData, PageData pageData)
        {
            switch (annotationData.type)
            {
                case "text":
                    return new TextAnnotator(annotationData, pageData);
                case "area":
                    return new AreaAnnotator(annotationData, pageData);
                case "point":
                    return new PointAnnotator(annotationData, pageData);
                case "textStrikeout":
                    return new TexStrikeoutAnnotator(annotationData, pageData);
                case "polyline":
                    return new PolylineAnnotator(annotationData, pageData);
                case "textField":
                    return new TextFieldAnnotator(annotationData, pageData);
                case "watermark":
                    return new WatermarkAnnotator(annotationData, pageData);
                case "textReplacement":
                    return new TextReplacementAnnotator(annotationData, pageData);
                case "arrow":
                    return new ArrowAnnotator(annotationData, pageData);
                case "textRedaction":
                    return new TextRedactionAnnotator(annotationData, pageData);
                case "resourcesRedaction":
                    return new ResourceRedactionAnnotator(annotationData, pageData);
                case "textUnderline":
                    return new TexUnderlineAnnotator(annotationData, pageData);
                case "distance":
                    return new DistanceAnnotator(annotationData, pageData);
                default:
                    throw new System.Exception("Wrong annotation data without annotation type!");
            }
        }
    }
}