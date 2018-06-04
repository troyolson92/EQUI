using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.Welding.Models;

namespace EqUiWebUi.Areas.Welding
{
    public class WeldingController : Controller
    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        // GET: Welding/Welding
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Pagina1()
        {
            ViewBag.data1 = "Dit is een string";
            ViewBag.data2 = 10;
            return View();
        }

        //basic grid 
        public ActionResult AutomaticWorkFlowULPlans()
        {
            var data = db.AutomaticWorkFlowULPlans;
            return View(data);
        }

        public ActionResult AutomaticWorkFlowULPlansFilter(string Ul_plan = "test")
        {
            ViewBag.Ul_planFilter = Ul_plan;
            var data = db.AutomaticWorkFlowULPlans.Where(c => c.UL_plan.Contains(Ul_plan));
            return View(data);
        }
    }
}