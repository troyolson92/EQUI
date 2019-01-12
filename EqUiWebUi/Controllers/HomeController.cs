using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        //render an external site in our content (embedded)
        public ActionResult Rendersite(string url)
        {
            ViewBag.redirectURL = url;
            return View();
        }

        //show settings page
        public ActionResult Settings()
        {
            return View();
        }

        //this page is a redirect page for when i want to close a window
        public ActionResult Close()
        {
            return View();
        }

        //html test page
        public ActionResult TestPage()
        {
            return View();
        }

    }
}