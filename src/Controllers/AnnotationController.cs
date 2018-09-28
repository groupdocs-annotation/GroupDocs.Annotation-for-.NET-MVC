using System.Web.Mvc;

namespace GroupDocs.Annotation.MVC.Controllers
{
    /// <summary>
    /// Signature Web page controller
    /// </summary>
    public class AnnotationController : Controller
    {
        // View Annotation
        public ActionResult Index()
        {
            return View();
        }
    }
}