using GroupDocs.Annotation.MVC.Products.Annotation.Config;

namespace GroupDocs.Annotation.MVC.Products.Common.Config
{
    /// <summary>
    /// Global configuration
    /// </summary>
    public class GlobalConfiguration
    {
        public ServerConfiguration Server;
        public ApplicationConfiguration Application;
        public CommonConfiguration Common;       
        public AnnotationConfiguration Annotation;       

        /// <summary>
        /// Get all configurations
        /// </summary>
        public GlobalConfiguration()
        {            
            Server = new ServerConfiguration();
            Application = new ApplicationConfiguration();            
            Common = new CommonConfiguration();
            Annotation = new AnnotationConfiguration();            
        }
    }
}