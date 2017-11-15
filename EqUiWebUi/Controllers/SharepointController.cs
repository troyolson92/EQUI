using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class SharepointController : Controller
    {
        // GET: Sharepoint
        public ActionResult Index()
        {
            return View();
        }

        // GET: Add new issue to sharepoint.
        public ActionResult AddNewIusse(string location, string title, string refURL)
        {

            ViewBag.Location = location;
            ViewBag.Title = title;
            ViewBag.RefUrl = refURL;
            ViewBag.Exeption = "OK";
            return View();
        }
    }
}