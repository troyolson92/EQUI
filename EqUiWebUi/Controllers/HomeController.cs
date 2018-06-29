using System;
using System.Linq;
using System.Web.Mvc;
using EqUiWebUi.Areas.user_management;

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

        //this page is a redirect page for when i whant to close a windown
        public ActionResult Close()
        {
            return View();
        }

    }
}