using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Maximo_ui.Controllers
{
    public class MaximoController : Controller
    {
        // GET: Maximo_ui/Maximo
        public ActionResult Index()
        {
            return View();
        }

        //lagency support for sharepoint list 
        public ActionResult WoDetails(int? wonum)
        {
            return RedirectToAction("WoDetails", "WorkorderDetails", new {area = "Maximo_ui", wonum = wonum });
        }
    }
}