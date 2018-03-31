using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.PlcSupervisie.Controllers
{
    public class ScreenshotController : Controller
    {
        // GET: PlcSupervisie/Screenshot
        public ActionResult Index()
        {
            return View();
        }

        //display a list of jpg pictures (for ub12)
        //ReloadInterval Set reload time for refrehsing full page
        //AutoCarousel rate to change out pictures if -1 no autorotate
        public ActionResult Carousel(int? ReloadInterval, int AutoCarousel = -1)
        {
            if (ReloadInterval.HasValue)
            {
                Response.AddHeader("Refresh", ReloadInterval.ToString());
            }
            ViewBag.AutoCarousel = AutoCarousel;
            return View();
        }

        //display a single JPG
        public ActionResult ShowJpg(int? ReloadInterval, string url)
        {
            if (ReloadInterval.HasValue)
            {
                Response.AddHeader("Refresh", ReloadInterval.ToString());
            }
            ViewBag.url = url;
            return View();
        }
    }
}