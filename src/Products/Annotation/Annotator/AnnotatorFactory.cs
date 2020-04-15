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
            AnnotationDataEntity roundedAnnotationData = RoundCoordinates(annotationData);
            switch (roundedAnnotationData.type)
            {
                case "textHighlight":
                    return new TextHighlightAnnotation(roundedAnnotationData, pageData);
                case "area":
                    return new AreaAnnotator(roundedAnnotationData, pageData);
                case "point":
                    return new PointAnnotator(roundedAnnotationData, pageData);
                case "textStrikeout":
                    return new TexStrikeoutAnnotator(roundedAnnotationData, pageData);
                case "polyline":
                    return new PolylineAnnotator(roundedAnnotationData, pageData);
                case "textField":
                    return new TextFieldAnnotator(roundedAnnotationData, pageData);
                case "watermark":
                    return new WatermarkAnnotator(roundedAnnotationData, pageData);
                case "textReplacement":
                    return new TextReplacementAnnotator(roundedAnnotationData, pageData);
                case "arrow":
                    return new ArrowAnnotator(roundedAnnotationData, pageData);
                case "textRedaction":
                    return new TextRedactionAnnotator(roundedAnnotationData, pageData);
                case "resourcesRedaction":
                    return new ResourceRedactionAnnotator(roundedAnnotationData, pageData);
                case "textUnderline":
                    return new TexUnderlineAnnotator(roundedAnnotationData, pageData);
                case "distance":
                    return new DistanceAnnotator(roundedAnnotationData, pageData);
                default:
                    throw new ArgumentNullException("Wrong annotation data without annotation type!");
            }
        }

        private static AnnotationDataEntity RoundCoordinates(AnnotationDataEntity annotationData)
        {
            annotationData.height = (float)Math.Round(annotationData.height, 0);
            annotationData.left = (float)Math.Round(annotationData.left, 0);
            annotationData.top = (float)Math.Round(annotationData.top, 0);
            annotationData.width = (float)Math.Round(annotationData.width, 0);
            return annotationData;
        }
    }
}