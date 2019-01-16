﻿using GroupDocs.Annotation.MVC.Products.Common.Entity.Web;
using Newtonsoft.Json;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Entity.Web
{
    public class PageDataDescriptionEntity : PageDescriptionEntity
    {
        /// List of annotation data  
        [JsonProperty]
        private AnnotationDataEntity[] annotations;       

        public void SetAnnotations(AnnotationDataEntity[] annotations) {
            this.annotations = annotations;
        }

        public AnnotationDataEntity[] GetAnnotations()
        {
            return annotations;
        }
    }
}