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
            var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<ScreenHub>();
            context.Clients.All.Announce("somebody hit index");
            // or
            context.Clients.Group("GroupA").Announce("GroupA");
            context.Clients.Group("GroupB").Announce("GroupB");
            context.Clients.Group("GroupC").Announce("GroupC");


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