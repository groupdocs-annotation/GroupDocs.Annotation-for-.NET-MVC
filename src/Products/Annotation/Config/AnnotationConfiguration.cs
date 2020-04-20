using GroupDocs.Annotation.MVC.Products.Common.Config;
using GroupDocs.Annotation.MVC.Products.Common.Util.Parser;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Config
{
    /// <summary>
    /// AnnotationConfiguration
    /// </summary>
    public class AnnotationConfiguration : CommonConfiguration
    {
        [JsonProperty]
        private string filesDirectory = "DocumentSamples/Annotation";

        [JsonProperty]
        private string defaultDocument = "";

        [JsonProperty]
        private int preloadPageCount = 0;
       
        [JsonProperty]
        private bool isTextAnnotation = true;

        [JsonProperty]
        private bool isAreaAnnotation = true;
        
        [JsonProperty]
        private bool isPointAnnotation = true;

        [JsonProperty]
        private bool isTextStrikeoutAnnotation = true;

        [JsonProperty]
        private bool isPolylineAnnotation = true;

        [JsonProperty]
        private bool isTextFieldAnnotation = true;

        [JsonProperty]
        private bool isWatermarkAnnotation = true;

        [JsonProperty]
        private bool isTextReplacementAnnotation = true;

        [JsonProperty]
        private bool isArrowAnnotation = true;

        [JsonProperty]
        private bool isTextRedactionAnnotation = true;

        [JsonProperty]
        private bool isResourcesRedactionAnnotation = true;

        [JsonProperty]
        private bool isTextUnderlineAnnotation = true;

        [JsonProperty]
        private bool isDistanceAnnotation = true;

        [JsonProperty]
        private bool isDownloadOriginal = true;

        [JsonProperty]
        private bool isDownloadAnnotated = true;

        [JsonProperty]
        private bool isZoom = true;

        [JsonProperty]
        private bool isFitWidth = true;

        /// <summary>
        /// Get annotation configuration section from the Web.config
        /// </summary>
        public AnnotationConfiguration()
        {
            YamlParser parser = new YamlParser();
            dynamic configuration = parser.GetConfiguration("annotation");
            ConfigurationValuesGetter valuesGetter = new ConfigurationValuesGetter(configuration);
            // get Annotation configuration section from the web.config
            filesDirectory = valuesGetter.GetStringPropertyValue("filesDirectory", filesDirectory);
            if (!IsFullPath(filesDirectory))
            {
                filesDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filesDirectory);
                if (!Directory.Exists(filesDirectory))
                {
                    Directory.CreateDirectory(filesDirectory);
                }
            }
            defaultDocument = valuesGetter.GetStringPropertyValue("defaultDocument", defaultDocument).Replace(@"\", "/");
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
            preloadPageCount = valuesGetter.GetIntegerPropertyValue("preloadPageCount", preloadPageCount);
            isZoom = valuesGetter.GetBooleanPropertyValue("zoom", isZoom);
            isFitWidth = valuesGetter.GetBooleanPropertyValue("fitWidth", isFitWidth);
        }

        private static bool IsFullPath(string path)
        {
            return !String.IsNullOrWhiteSpace(path)
                && path.IndexOfAny(System.IO.Path.GetInvalidPathChars().ToArray()) == -1
                && Path.IsPathRooted(path)
                && !Path.GetPathRoot(path).Equals(Path.DirectorySeparatorChar.ToString(), StringComparison.Ordinal);
        }

        public void SetFilesDirectory(string filesDirectory) {
            this.filesDirectory = filesDirectory;
        }

        public string GetFilesDirectory()
        {
            return filesDirectory;
        }       

        public void SetDefaultDocument(string defaultDocument)
        {
            this.defaultDocument = defaultDocument;
        }

        public string GetDefaultDocument()
        {
            return defaultDocument;
        }

        public void SetPreloadPageCount(int preloadPageCount)
        {
            this.preloadPageCount = preloadPageCount;
        }

        public int GetPreloadPageCount()
        {
            return preloadPageCount;
        }

        public void SetIsTextAnnotation(bool isTextAnnotation)
        {
            this.isTextAnnotation = isTextAnnotation;
        }

        public bool GetIsTextAnnotation()
        {
            return isTextAnnotation;
        }

        public void SetIsAreaAnnotation(bool isAreaAnnotation)
        {
            this.isAreaAnnotation = isAreaAnnotation;
        }

        public bool GetIsAreaAnnotation()
        {
            return isAreaAnnotation;
        }

        public void SetIsPointAnnotation(bool isPointAnnotation)
        {
            this.isPointAnnotation = isPointAnnotation;
        }

        public bool GetIsPointAnnotation()
        {
            return isPointAnnotation;
        }

        public void SetIsTextStrikeoutAnnotation(bool isTextStrikeoutAnnotation)
        {
            this.isTextStrikeoutAnnotation = isTextStrikeoutAnnotation;
        }

        public bool GetIsTextStrikeoutAnnotation()
        {
            return isTextStrikeoutAnnotation;
        }

        public void SetIsPolylineAnnotation(bool isPolylineAnnotation)
        {
            this.isPolylineAnnotation = isPolylineAnnotation;
        }

        public bool GetIsPolylineAnnotation()
        {
            return isPolylineAnnotation;
        }

        public void SetIsTextFieldAnnotation(bool isTextFieldAnnotation)
        {
            this.isTextFieldAnnotation = isTextFieldAnnotation;
        }

        public bool GetIsTextFieldAnnotation()
        {
            return isTextFieldAnnotation;
        }

        public void SetIsWatermarkAnnotation(bool isWatermarkAnnotation)
        {
            this.isWatermarkAnnotation = isWatermarkAnnotation;
        }

        public bool GetIsWatermarkAnnotation()
        {
            return isWatermarkAnnotation;
        }

        public void SetIsTextReplacementAnnotation(bool isTextReplacementAnnotation)
        {
            this.isTextReplacementAnnotation = isTextReplacementAnnotation;
        }

        public bool GetIsTextReplacementAnnotation()
        {
            return isTextReplacementAnnotation;
        }

        public void SetIsArrowAnnotation(bool isArrowAnnotation)
        {
            this.isArrowAnnotation = isArrowAnnotation;
        }

        public bool GetIsArrowAnnotation()
        {
            return isArrowAnnotation;
        }

        public void SetIsTextRedactionAnnotation(bool isTextRedactionAnnotation)
        {
            this.isTextRedactionAnnotation = isTextRedactionAnnotation;
        }

        public bool GetIsTextRedactionAnnotation()
        {
            return isTextRedactionAnnotation;
        }

        public void SetIsResourcesRedactionAnnotation(bool isResourcesRedactionAnnotation)
        {
            this.isResourcesRedactionAnnotation = isResourcesRedactionAnnotation;
        }

        public bool GetIsResourcesRedactionAnnotation()
        {
            return isResourcesRedactionAnnotation;
        }

        public void SetIsTextUnderlineAnnotation(bool isTextUnderlineAnnotation)
        {
            this.isTextUnderlineAnnotation = isTextUnderlineAnnotation;
        }

        public bool GetIsTextUnderlineAnnotation()
        {
            return isTextUnderlineAnnotation;
        }

        public void SetIsDistanceAnnotation(bool isDistanceAnnotation)
        {
            this.isDistanceAnnotation = isDistanceAnnotation;
        }

        public bool GetIsDistanceAnnotation()
        {
            return isDistanceAnnotation;
        }

        public void SetIsDownloadOriginal(bool isDownloadOriginal)
        {
            this.isDownloadOriginal = isDownloadOriginal;
        }

        public bool GetIsDownloadOriginal()
        {
            return isDownloadOriginal;
        }

        public void SetIsDownloadAnnotated(bool isDownloadAnnotated)
        {
            this.isDownloadAnnotated = isDownloadAnnotated;
        }

        public bool GetIsDownloadAnnotated()
        {
            return isDownloadAnnotated;
        }

        public void SetIsZoom(bool isZoom)
        {
            this.isZoom = isZoom;
        }

        public bool GetIsZoom()
        {
            return isZoom;
        }

        public void SetIsFitWidth(bool isFitWidth)
        {
            this.isFitWidth = isFitWidth;
        }

        public bool GetIsFitWidth()
        {
            return isFitWidth;
        }
    }
}