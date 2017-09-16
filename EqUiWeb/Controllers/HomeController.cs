using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EquiWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "This is the landing page for the VCG GA UB12 WebUi";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "In case of a serious issue.";

            return View();
        }
    }
}
