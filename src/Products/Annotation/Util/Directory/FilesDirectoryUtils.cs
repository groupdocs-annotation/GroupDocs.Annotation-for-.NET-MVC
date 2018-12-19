using GroupDocs.Annotation.MVC.Products.Annotation.Config;
using GroupDocs.Annotation.MVC.Products.Common.Util.Directory;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Util.Directory
{
    public class FilesDirectoryUtils : IDirectoryUtils
    {

        private readonly AnnotationConfiguration AnnotationConfiguration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="signatureConfiguration">SignatureConfiguration</param>
        public FilesDirectoryUtils(AnnotationConfiguration annotationConfiguration)
        {
            AnnotationConfiguration = annotationConfiguration;
        }

        /// <summary>
        /// Get path
        /// </summary>
        /// <returns>string</returns>
        public string GetPath()
        {
            return AnnotationConfiguration.GetFilesDirectory();
        }
    }
}