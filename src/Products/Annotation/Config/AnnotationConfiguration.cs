using GroupDocs.Annotation.MVC.Products.Common.Config;
using GroupDocs.Annotation.MVC.Products.Common.Util.Parser;
using System;
using System.IO;
using System.Linq;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Config
{
    /// <summary>
    /// AnnotationConfiguration
    /// </summary>
    public class AnnotationConfiguration
    {
        public string FilesDirectory = "DocumentSamples/Annotation";
        public string OutputDirectory = "";
        public string DefaultDocument = "";
        public int PreloadPageCount = 0;
        public bool isTextAnnotation = true;
        public bool isAreaAnnotation = true;
        public bool isPointAnnotation = true;
        public bool isTextStrikeoutAnnotation = true;
        public bool isPolylineAnnotation = true;
        public bool isTextFieldAnnotation = true;
        public bool isWatermarkAnnotation = true;
        public bool isTextReplacementAnnotation = true;
        public bool isArrowAnnotation = true;
        public bool isTextRedactionAnnotation = true;
        public bool isResourcesRedactionAnnotation = true;
        public bool isTextUnderlineAnnotation = true;
        public bool isDistanceAnnotation = true;
        public bool isDownloadOriginal = true;
        public bool isDownloadAnnotated = true;     

        /// <summary>
        /// Get annotation configuration section from the Web.config
        /// </summary>
        public AnnotationConfiguration()
        {
            YamlParser parser = new YamlParser();
            dynamic configuration = parser.GetConfiguration("annotation");
            ConfigurationValuesGetter valuesGetter = new ConfigurationValuesGetter(configuration);
            // get Viewer configuration section from the web.config
            FilesDirectory = valuesGetter.GetStringPropertyValue("filesDirectory", FilesDirectory);
            if (!IsFullPath(FilesDirectory))
            {
                FilesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilesDirectory);
                if (!Directory.Exists(FilesDirectory))
                {                   
                    Directory.CreateDirectory(FilesDirectory);
                }
            }
            OutputDirectory = valuesGetter.GetStringPropertyValue("outputDirectory", OutputDirectory);
            isTextAnnotation = valuesGetter.GetBooleanPropertyValue("textAnnotation", isTextAnnotation);
            isAreaAnnotation = valuesGetter.GetBooleanPropertyValue("areaAnnotation", isAreaAnnotation);
            isPointAnnotation = valuesGetter.GetBooleanPropertyValue("pointAnnotation", isPointAnnotation);
            isTextStrikeoutAnnotation = valuesGetter.GetBooleanPropertyValue("textStrikeoutAnnotation", isTextStrikeoutAnnotation);
            isPolylineAnnotation = valuesGetter.GetBooleanPropertyValue("polylineAnnotation", isPolylineAnnotation);
            isTextFieldAnnotation = valuesGetter.GetBooleanPropertyValue("textFieldAnnotation", isTextFieldAnnotation);
            isWatermarkAnnotation = valuesGetter.GetBooleanPropertyValue("watermarkAnnotation", isWatermarkAnnotation);
            isTextReplacementAnnotation = valuesGetter.GetBooleanPropertyValue("textReplacementAnnotation", isTextReplacementAnnotation);
            isArrowAnnotation = valuesGetter.GetBooleanPropertyValue("arrowAnnotation", isArrowAnnotation);
            isTextRedactionAnnotation = valuesGetter.GetBooleanPropertyValue("textRedactionAnnotation", isTextRedactionAnnotation);
            isResourcesRedactionAnnotation = valuesGetter.GetBooleanPropertyValue("resourcesRedactionAnnotation", isResourcesRedactionAnnotation);
            isTextUnderlineAnnotation = valuesGetter.GetBooleanPropertyValue("textUnderlineAnnotation", isTextUnderlineAnnotation);
            isDistanceAnnotation = valuesGetter.GetBooleanPropertyValue("distanceAnnotation", isDistanceAnnotation);
            isDownloadOriginal = valuesGetter.GetBooleanPropertyValue("downloadOriginal", isDownloadOriginal);
            isDownloadAnnotated = valuesGetter.GetBooleanPropertyValue("downloadAnnotated", isDownloadAnnotated);
            PreloadPageCount = valuesGetter.GetIntegerPropertyValue("preloadPageCount", PreloadPageCount);
        }

        private static bool IsFullPath(string path)
        {
            return !String.IsNullOrWhiteSpace(path)
                && path.IndexOfAny(System.IO.Path.GetInvalidPathChars().ToArray()) == -1
                && Path.IsPathRooted(path)
                && !Path.GetPathRoot(path).Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal);
        }
    }
}