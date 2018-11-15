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

        // Get list of all rt jobs
        public ActionResult GetJobs(bool hasBreakdowns = false, bool PromptBodynum = false)
        {
            ViewBag.hasBreakdowns = hasBreakdowns;
            ViewBag.PromptBodynum = PromptBodynum;
            return View();
        }

        // Get list of all rt jobs (grid)
        public ActionResult _getJobs(int? bodyNo, bool hasBreakdowns = false)
        {
            db.Database.CommandTimeout = 60;
            IQueryable<rt_job> data;
            data = db.rt_job.Where(c => 
            (c.breakDownCount != 0 || hasBreakdowns == false)
            && (c.bodyNo == bodyNo || bodyNo == null)
            );

            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (LocationRoot != "")
            {
                data = data.Where(c => (c.c_controller.LocationTree ?? "").Contains(LocationRoot));
            }

            return PartialView(data);
        }

        //Get specific RT job (single job)
        public ActionResult RtJob(int jobId)
        {
            ViewBag.jobID = jobId;
            return View();
        }

        //Get specific RT job (single job) GRID
        public ActionResult _getJob(int jobID)
        {
            //need to add this in the bag
            rt_job job = db.rt_job.Where(c => c.id == jobID).FirstOrDefault();
            //return rt_job_breakdown list
            return PartialView(db.rt_job_breakdown.Where(c => c.rt_job_active_id == jobID));
        }
    }
}