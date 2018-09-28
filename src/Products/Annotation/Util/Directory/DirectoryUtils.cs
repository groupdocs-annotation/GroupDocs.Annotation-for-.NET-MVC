﻿
using GroupDocs.Annotation.MVC.Products.Annotation.Config;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Util.Directory
{
    public class DirectoryUtils
    {
        public FilesDirectoryUtils FilesDirectory;
        public OutputDirectoryUtils OutputDirectory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="annotationConfiguration">AnnotationConfiguration</param>
        public DirectoryUtils(AnnotationConfiguration annotationConfiguration)
        {
            FilesDirectory = new FilesDirectoryUtils(annotationConfiguration);
            OutputDirectory = new OutputDirectoryUtils(annotationConfiguration);
        }
    }
}