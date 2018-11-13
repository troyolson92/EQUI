using EqUiWebUi.Areas.Welding.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Welding.Controllers
{
    public class WMController : Controller

    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();
    
        // GET: Welding/WM
        public ActionResult Index()
        {
            return View();

        }
        // GET: Weldingmaster 


        public ActionResult LastWelds()
        {
            IQueryable<LastWelds> data = db.LastWelds.AsQueryable();
            return View(data);
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
            IQueryable<TimerBreakdowns_busy > data = db.TimerBreakdowns_busy.AsQueryable();
            return PartialView(data);
        }
        public ActionResult rt_ExtraControlesWM()
        {
            IQueryable<rt_ExtraControles> data = db.rt_ExtraControles.AsQueryable();
            return PartialView(data);
        }
        public ActionResult _TimerBreakdownDatachange()
        {
            IQueryable<TimerBreakdownDatachange> data = db.TimerBreakdownDatachange.AsQueryable();
            return View(data);
        }
        public ActionResult _Weldfaultanalyse()
        {
           IQueryable<rt_alarm> data = db.rt_alarm.AsQueryable();

            var query = from i in db.rt_alarm
                        
                        join s in db.rt_job_breakdown on i.id equals s.rt_alarm_id
                        join t in db.rt_job on s.rt_job_id equals t.id
                        join fault in db.rt_weldfault on i.protRecord_ID equals fault.id join timer in db.Timer on fault.timerId equals timer.ID
                        join NPT in db.NPT on timer.NptId equals NPT.ID join user in db.Users on NPT.OwnerId equals user.ID
                        where i.errorCode1 != 87 && i.errorCode1 != 80 && i.errorCode1 != 94
                        && i.errorCode1 != 140

                        select new
                        {   timer.Name,
                            s.ts_breakdownStart,
                            s.ts_breakdownEnd,
                            i.errorCode1_txt,
                            i.errorCode2_txt,
                            t.rt_weldfaultprot_count,
                            fault.progNo,
                            user.CDSID,
                            fault.WMComment
                        };

         
            return View(data);
        }

    }


   
}