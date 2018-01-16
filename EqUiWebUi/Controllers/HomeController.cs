using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Models;
using EqUiWebUi.Areas.user_management;

namespace EqUiWebUi.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            userCookie userCookie = new userCookie();
            var cookie = Request.Cookies[userCookie.name];
            if (cookie == null)
            {
                Response.Cookies.Add(userCookie.Cookie(System.Web.HttpContext.Current.User.Identity.Name));
            }
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

        public ActionResult Rendersites(string url1, string url2, string url3, string url4)
        {
            ViewBag.url1 = url1 == null ? "" : url1;
            ViewBag.url2 = url2 == null ? "" : url2;
            ViewBag.url3 = url3 == null ? "" : url3;
            ViewBag.url4 = url4 == null ? "" : url4;

            return View();
        }

        //show user settings 
        public ActionResult UserSettings()
        {
            return View();
        }

        //show settings 
        public ActionResult Settings()
        {
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

        //Fire background jobs once
        [Authorize(Roles = "Administrator")]
        public ActionResult BackGroundWorkBufferStatus()
        {
            ViewBag.PloegreportCount =  DataBuffer.Ploegreport.Count();
            ViewBag.SupervisieCount = DataBuffer.Supervisie.Count();
            return View();
        }

        public ActionResult UB12Home()
        {

            return View();
        }
    }
}