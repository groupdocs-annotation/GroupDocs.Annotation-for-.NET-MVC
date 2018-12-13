using GroupDocs.Annotation.MVC.Products.Annotation.Entity;

namespace GroupDocs.Annotation.MVC.Products.Annotation.Util
{
    public class SupportedAnnotations
    {
        public string[] GetSupportedAnnotations(string documentType)
        {           
            Annotations annotations = new Annotations();
            switch (documentType)
            {
                case "Portable Document Format":
                case "PDF":
                    return annotations.Pdf;
                case "Microsoft Word":
                case "WORDS":
                    return annotations.Word;
                case "Microsoft PowerPoint":
                case "SLIDES":
                    return annotations.Slides;
                case "image":
                    return annotations.Image;
                case "Microsoft Excel":
                case "CELLS":
                    return annotations.Cells;
                case "AutoCAD Drawing File Format":
                case "diagram":
                    return annotations.Diagram;
                default:
                    return annotations.Pdf;              
            }           
        }
    }
}