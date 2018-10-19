using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EqUiWebUi.Areas.VWSC.Models;

namespace EqUiWebUi.Areas.VWSC.Controllers
{
    public class vwscController : Controller
    {
        private GADATAEntitiesVWSC db = new GADATAEntitiesVWSC();

        /// <summary>
        /// GET: VWSC/vwsc
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// get: VWSC/rt_job
        /// </summary>
        /// <param name="bShowUnfinishedJobs"></param>
        /// <returns></returns>
        public ActionResult rt_job(bool bShowUnfinishedJobs = false)
        {   
            IQueryable<VWSC_rt_job> data = db.rt_job.Where(c => c.ts_End.HasValue != bShowUnfinishedJobs).AsQueryable();
            return View(data);
        }

        public ActionResult _jobdetails(int id)
        {
            VWSC_rt_job job = db.rt_job.Find(id);
            return PartialView(job);
        }

        public ActionResult _jobBreakdown(int id)
        {
            IQueryable<VWSC_rt_job_breakdown> breakdowns = db.rt_job_breakdown.Where(c => c.rt_job_id == id).AsQueryable();
            return PartialView(breakdowns);
        }

        public ActionResult _weldMeasure(int timerId, int weldmeasureprotddw_id_Start, int weldmeasureprotddw_id_End)
        {
            IQueryable<VWSC_rt_weldmeasureprotddw> data = db.rt_weldmeasureprotddw.Where(c => 
            c.timerId == timerId 
            && c.id >= weldmeasureprotddw_id_Start
            && c.id <= weldmeasureprotddw_id_End
            ).AsQueryable();
            return PartialView(data);
        }

        public ActionResult _weldFault(int timerId, int weldfaultprot_id_Start, int weldfaultprot_id_End)
        {
            IQueryable<VWSC_rt_weldfault> data = db.rt_weldfault.Where(c =>
            c.timerId == timerId
            && c.id >= weldfaultprot_id_Start
            && c.id <= weldfaultprot_id_End
            ).AsQueryable();
            return PartialView(data);
        }


    }
}