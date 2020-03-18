using System;

namespace GroupDocs.Annotation.MVC.AppDomainGenerator
{
    /// <summary>
    /// DomainGenerator
    /// </summary>
    public class DomainGenerator
    {
        private readonly Products.Common.Config.GlobalConfiguration globalConfiguration;

        /// <summary>
        /// Constructor
        /// </summary>
        public DomainGenerator(string assemblyName, string className)
        {
            globalConfiguration = new Products.Common.Config.GlobalConfiguration();
        }  

        /// <summary>
        /// Set GroupDocs.Annotation license
        /// </summary>       
        public void SetAnnotationLicense()
        {
            // Set license
            License license = new License();
            license.SetLicense(globalConfiguration.Application.LicensePath);
        }        
    }
}