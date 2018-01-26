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

        //render up to 4 sites in our content
        public ActionResult Rendersites(string url1, string url2, string url3, string url4)
        {
            ViewBag.url1 = url1 == null ? "" : url1;
            ViewBag.url2 = url2 == null ? "" : url2;
            ViewBag.url3 = url3 == null ? "" : url3;
            ViewBag.url4 = url4 == null ? "" : url4;

            return View();
        }

        //show user settings  for current users
        public ActionResult UserSettings()
        {
            return View();
        }

        //show settings page
        public ActionResult Settings()
        {
            ViewBag.ActiveSessions = 0; // MvcApplication.

            return View();
        }

        //init background work jobs.
        [Authorize(Roles = "Administrator")]
        public ActionResult ConfiureBackgroundJobs()
        {
            Backgroundwork backgroundwork = new Backgroundwork();
            backgroundwork.configHangfireJobs();
            return View();
        }

        //Fire background jobs once
        [Authorize(Roles = "Administrator")]
        public ActionResult FireBackgroundJobs()
        {
            Backgroundwork backgroundwork = new Backgroundwork();
            backgroundwork.configHangfireJobs();
            return View();
        }

        //temp for ub12 (top left corner of big screen need to make this custom for a user)
        public ActionResult UB12Home()
        {
            return View();
        }
    }
}