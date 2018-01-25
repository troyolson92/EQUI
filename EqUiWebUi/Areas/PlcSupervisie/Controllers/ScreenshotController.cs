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

        //display a list of jpg pictures
        public ActionResult Carousel(int? ReloadInterval)
        {
            if (ReloadInterval.HasValue)
            {
                Response.AddHeader("Refresh", ReloadInterval.ToString());
            }

            return View();
        }
    }
}