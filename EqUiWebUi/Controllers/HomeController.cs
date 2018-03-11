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

        //show user settings  for current users
        public ActionResult UserSettings()
        {
            return View();
        }

        //show settings page
        public ActionResult Settings()
        {
            ViewBag.ActiveSessions = EqUiWebUi.MvcApplication.Sessions().Count;
            return View();
        }

        //init background work jobs.
        [Authorize(Roles = "Administrator")]
        public void ConfiureBackgroundJobs()
        {
            //old class
            Backgroundwork backgroundwork = new Backgroundwork();
            backgroundwork.configHangfireJobs();
            //gadata jobs 
            Areas.Gadata.BackgroundWork backgroundWorkGADATA = new Areas.Gadata.BackgroundWork();
            backgroundWorkGADATA.configHangfireJobs();

        }

        //temp for ub12 (top left corner of big screen need to make this custom for a user)
        public ActionResult UB12Home()
        {
            return View();
        }

    }
}