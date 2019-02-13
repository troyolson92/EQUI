using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.UltraLog.Controllers
{
    public class UltralogController : Controller
    {
        Models.UltraLogEntities db = new Models.UltraLogEntities();

        // GET: UltraLog/Ultralog
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Show active control plans and completed plan overview
        /// </summary>
        /// <returns></returns>
        public ActionResult ControlPlanViewer()
        {

            return View();
        }

        /// <summary>
        /// returns grid with active control plans
        /// </summary>
        /// <returns></returns>
        public ActionResult _activeControlPlanGrid()
        {
            return PartialView(db.rt_active_info.AsQueryable());
        }

        /// <summary>
        /// returns grid with completed control plans
        /// </summary>
        /// <returns></returns>
        public ActionResult _CompletedPlansGrid()
        {
            return PartialView(db.h_CompletedPlans.AsQueryable());
        }

        /// <summary>
        /// returns partial with plan remarks
        /// </summary>
        /// <param name="planId"></param>
        /// <returns></returns>
        public ActionResult _measurementRemarks(int? planId)
        {
            ViewBag.planId = planId;
            return PartialView();
        }

        /// <summary>
        /// returns grid with plan remarks
        /// </summary>
        /// <param name="planId">return only remarks for selected plan</param>
        /// <returns></returns>
        public ActionResult _measurementRemarksGird(int? planId)
        {
            if (planId.HasValue)
            {
                return PartialView(db.h_measurementRemarks.Where(c => c.CompletedPlans_id == planId).AsQueryable());
            }
            return PartialView(db.h_measurementRemarks.AsQueryable());
        }
    }
}