using EqUiWebUi.Areas.Welding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Welding.Controllers
{

   
    [Authorize(Roles = "QteamPoweruser")]
    public class QteamController : Controller
    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();
        // GET: Welding/Qteam
        public ActionResult Index()
        {
            return View();
        }

        //for file "Extra controles"
        public ActionResult rt_ExtraControles()
        {
            IQueryable<rt_ExtraControles> data = db.rt_ExtraControles.AsQueryable();
            return View(data);
        }
        public ActionResult rt_AutoWorkFlowULPlans()
        {
            IQueryable<rt_AutoWorkFlowULPlans> data = db.rt_AutoWorkFlowULPlans.AsQueryable();
            return View(data);
        }

        public ActionResult ULRapportering()
        {
            IQueryable<ULRapportering> data = db.ULRapporterings.AsQueryable();
            return View(data);
        }
    }
}



