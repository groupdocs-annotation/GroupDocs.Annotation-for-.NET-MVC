using GroupDocs.Annotation.MVC.Products.Annotation.Config;
using GroupDocs.Annotation.MVC.Products.Common.Util.Directory;
using System;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Util.Directory
{
    /// <summary>
    /// OutputDirectoryUtils
    /// </summary>
    public class OutputDirectoryUtils : IDirectoryUtils
    {
        private string OUTPUT_FOLDER = "/Annotated";
        private AnnotationConfiguration AnnotationConfiguration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="annotationConfiguration">AnnotationConfiguration</param>
        public OutputDirectoryUtils(AnnotationConfiguration annotationConfiguration)
        {
            AnnotationConfiguration = annotationConfiguration;

            // create output directories
            if (String.IsNullOrEmpty(annotationConfiguration.OutputDirectory))
            {
                annotationConfiguration.OutputDirectory = annotationConfiguration.FilesDirectory + OUTPUT_FOLDER;
            }

            if (!System.IO.Directory.Exists(annotationConfiguration.OutputDirectory)) {
                System.IO.Directory.CreateDirectory(annotationConfiguration.OutputDirectory);
            }
        }

        /// <summary>
        /// Get path
        /// </summary>
        /// <returns>string</returns>
        public string GetPath()
        {
            return AnnotationConfiguration.OutputDirectory;
        }
    }
}