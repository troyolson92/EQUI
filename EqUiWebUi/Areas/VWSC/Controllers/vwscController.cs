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

        // GET: VWSC/vwsc
        public ActionResult Index()
        {
            return View();
        }

        //get: VWSC/rt_job
        public ActionResult rt_job()
        {
            IQueryable<VWSC_rt_job> data = db.rt_job.AsQueryable();

            return View(data);
        }

        public ActionResult _welds(int timerId, int weldmeasureprotddw_id_Start, int weldmeasureprotddw_id_End)
        {
            IQueryable<VWSC_rt_weldmeasureprotddw> data = db.rt_weldmeasureprotddw.Where(c => 
            c.timerId == timerId 
            && c.id >= weldmeasureprotddw_id_Start
            && c.id <= weldmeasureprotddw_id_End
            ).AsQueryable();
            return PartialView(data);
        }
    }
}