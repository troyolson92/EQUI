using EqUiWebUi.Areas.VASC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.VASC.Controllers
{
    public class BreakdownController : Controller
    {
        private GADATAEntitiesVASC db = new GADATAEntitiesVASC();

        // GET: VASC/Breakdown
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult getBreakdowns()
        {
            if (Session["LocationRoot"].ToString() != "")
            {
                string locationRoot = Session["LocationRoot"].ToString();
                return View(db.rt_job.Where(c => c.ts_breakDownStart != null && (c.c_controller.LocationTree ?? "").Contains(locationRoot)));
            }
            else
            {
                return View(db.rt_job.Where(c => c.ts_breakDownStart != null));
            }

        }

        public ActionResult rtJob(int jobId)
        {
            ViewBag.jobID = jobId;
            return View();
        }

        public ActionResult _getJob(int jobID)
        {
            //need to add this in the bag
            rt_job job = db.rt_job.Where(c => c.id == jobID).FirstOrDefault();
            //return rt_job_breakdown list
            return PartialView(db.rt_job_breakdown.Where(c => c.rt_job_active_id == jobID));
        }
    }
}