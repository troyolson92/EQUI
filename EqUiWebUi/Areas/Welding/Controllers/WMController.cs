using EqUiWebUi.Areas.Welding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using Hangfire.Server;
using Hangfire.Console;
using System.Configuration;
using System.Data.Linq.SqlClient;

using ConnectionState = EqUiWebUi.Areas.Welding.Models.ConnectionState;
using System.Data.Entity;
using System.Net;

namespace EqUiWebUi.Areas.Welding.Controllers
{
    public class WMController : Controller

    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();


        public ActionResult Index()
        {

            return View();
        }

        public ActionResult TV()
        {
            IQueryable<QISViewer> data = db.QISViewer.AsQueryable();
            return View(data);
        }


        public ActionResult _ConnectionState()
        {
            IQueryable<ConnectionState> data = db.ConnectionState.AsQueryable();
            return PartialView(data);
        }

        public ActionResult _QISvieuwer()
        {
            IQueryable<QISViewer> data = db.QISViewer.AsQueryable();
            return PartialView(data);
        }
        public ActionResult _TimerBreakdowns_busy()
        {
            IQueryable<TimerBreakdowns_busy> data = db.TimerBreakdowns_busy.AsQueryable();
            return PartialView(data);
        }

        public ActionResult _TimerBreakdownDatachange()
        {
            IQueryable<TimerBreakdowns_WMActions> data = db.TimerBreakdowns_WMActions.AsQueryable();
            return View(data);
        }

        public ActionResult _rt_alarm()
        {

         
            IQueryable<rt_alarm> data = db.rt_alarm.Where(c => c.errorCode1 != 87 && c.errorCode1 != 94 && c.errorCode1 != 80 && c.errorCode1 != 140).AsQueryable();
            return View(data);

        }

        public ActionResult _alarmdetails(int id)
        {
            rt_alarm alarm = db.rt_alarm.Find(id);
            return PartialView(alarm);
        }

        public ActionResult _breakdown(int id)
        {
            IQueryable<rt_job_breakdown1> breakdown = db.rt_job_breakdown1.Where(c => c.rt_alarm_id == id).AsQueryable();
            return PartialView(breakdown);
        }

        public ActionResult WeldFaultIndex()
        {

            return View();

        }
        public ActionResult _WeldFaultCount()
        {
            IQueryable<WeldfaultCount> data = db.WeldfaultCount.AsQueryable();

            var query1 = from x in db.WeldfaultCount
                         orderby x.countofTimerFaults descending

                         select x;

            return PartialView(data);
        }

        /// <param name="bShowUnfinishedJobs"></param>
        /// <returns></returns>
        public ActionResult _rt_job1(bool bShowUnfinishedJobs = false)
        {
            IQueryable<rt_job11> data = db.rt_job11.Where(c => c.ts_End.HasValue != bShowUnfinishedJobs &&  c.timerId == c.c_timer.ID ).AsQueryable();
            return View(data);
        }

        public ActionResult _jobdetails1(int id)
        {
            rt_job11 job = db.rt_job11.Find(id);
            return PartialView(job);
        }

        public ActionResult _jobBreakdown1(int id)
        {
            IQueryable<rt_job_breakdown1> breakdowns = db.rt_job_breakdown1.Where(c => c.rt_job_id == id).Where(c => c.index == '1').AsQueryable();
            return PartialView(breakdowns);
        }

        public ActionResult _weldMeasure1(int timerId, int weldmeasureprotddw_id_Start, int weldmeasureprotddw_id_End)
        {
            IQueryable<rt_weldmeasureprotddw> data = db.rt_weldmeasureprotddw.Where(c =>
            c.timerId == timerId
            && c.id >= weldmeasureprotddw_id_Start
            && c.id <= weldmeasureprotddw_id_End
            
            ).AsQueryable();
            return PartialView(data);
        }

        public ActionResult _weldFault1(int timerId, int weldfaultprot_id_Start, int weldfaultprot_id_End)
        {
            IQueryable<rt_weldfault> data = db.rt_weldfault.Where(c =>
            c.timerId == timerId
            && c.id >= weldfaultprot_id_Start
            && c.id <= weldfaultprot_id_End
            ).AsQueryable();
            return PartialView(data);
        }

        public ActionResult _TimerIpAdresses()
        {
            IQueryable<TimerIpAdresses> data = db.TimerIpAdresses.AsQueryable();
            return View(data);
        }

        public ActionResult _Datachange()
        {

            IQueryable<rt_datachangeprot> data = db.rt_datachangeprot.Where(c => c.c_user.id == c.c_user_id && c.timerId == c.c_timer.ID).AsQueryable();
            return View(data);
        }

        public ActionResult _Parameters()
        {

            IQueryable<ParameterOptimalisation> data = db.ParameterOptimalisation.AsQueryable();
            return View(data);
        }


        //test met proc oproepen zonder view 
        public JsonResult TestProc(int id)
        {
            if (id == 999)
            {
                return Json(new { Msg = "job OK" }, JsonRequestBehavior.AllowGet);
            }
            else if (id == 100)
            {
                throw new NotImplementedException();
            }
            else
            {
                return Json(new { Msg = "job NOK" }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
        
   
