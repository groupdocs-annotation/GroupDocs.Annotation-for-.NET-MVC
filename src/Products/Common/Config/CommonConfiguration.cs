using GroupDocs.Annotation.MVC.Products.Common.Util.Parser;
using System;
using System.Collections.Specialized;
using System.Configuration;

namespace GroupDocs.Annotation.MVC.Products.Common.Config
{
    /// <summary>
    /// CommonConfiguration
    /// </summary>
    public class CommonConfiguration : ConfigurationSection
    {
        public bool isPageSelector { get; set; }    
        public bool isDownload { get; set; }
        public bool isUpload { get; set; }
        public bool isPrint { get; set; }
        public bool isBrowse { get; set; }
        public bool isRewrite { get; set; }
        private NameValueCollection commonConfiguration = (NameValueCollection)System.Configuration.ConfigurationManager.GetSection("commonConfiguration");

        /// <summary>
        /// Constructor
        /// </summary>
        public CommonConfiguration()
        {
            YamlParser parser = new YamlParser();
            dynamic configuration = parser.GetConfiguration("common");
            ConfigurationValuesGetter valuesGetter = new ConfigurationValuesGetter(configuration);
            isPageSelector = valuesGetter.GetBooleanPropertyValue("pageSelector", isPageSelector);
            isDownload = valuesGetter.GetBooleanPropertyValue("download", isDownload);
            isUpload = valuesGetter.GetBooleanPropertyValue("upload", isUpload);
            isPrint = valuesGetter.GetBooleanPropertyValue("print", isPrint);
            isBrowse = valuesGetter.GetBooleanPropertyValue("browse", isBrowse);
            isRewrite = valuesGetter.GetBooleanPropertyValue("rewrite", isRewrite);
        }
    }
}