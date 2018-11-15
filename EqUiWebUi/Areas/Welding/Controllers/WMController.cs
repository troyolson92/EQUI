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

using ConnectionState = EqUiWebUi.Areas.Welding.Models.ConnectionState;

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
            IQueryable<TimerBreakdownDatachange> data = db.TimerBreakdownDatachange.AsQueryable();
            return View(data);
        }
        public ActionResult _WeldFaultprotocol()
        {
            IQueryable<WeldFaultProtocol> data = db.WeldFaultProtocol.AsQueryable();

            var query = from i in db.WeldFaultProtocol
                        orderby i.breakdownTime descending
                        
                        select i;

            return PartialView(data.Take(10));
        }
        // GET: Welding/Update
        //test for in line edit 1 method for 1 value
        public void UpdateLabel(string id, string value)
        {
            WeldFaultProtocol weldFaultProtocol = db.WeldFaultProtocol.Find(Int32.Parse(id));
            weldFaultProtocol.WMComment = value;
            db.SaveChanges();
        }
    }
}
    

   /* public static class welfaultdata
    {
        public static List<EqUiWebUi.Areas.Welding.Models.rt_alarm> rt_alarm { get; set; }
        public static List<EqUiWebUi.Areas.Welding.Models.rt_job> rt_job { get; set; }
        public static List<EqUiWebUi.Areas.Welding.Models.rt_job_breakdown> rt_job_breakdown { get; set; }
        public static List<EqUiWebUi.Areas.Welding.Models.rt_weldfault> weldfaults { get; set; }
        public static List<EqUiWebUi.Areas.Welding.Models.NPT> NPT { get; set; }
        public static List<EqUiWebUi.Areas.Welding.Models.Timer> Timer { get; set; }
        public static List<EqUiWebUi.Areas.Welding.Models.Users> Users { get; set; }

    }

    public partial class WeldfaultDummy
    {
        public string name { get; set; }
        public string ts_breakdownStart { get; set; }
        public string ts_breakdownEnd { get; set; }

        public string errorCode1_txt { get; set; }
        public string errorCode2_txt { get; set; }
        public string rt_weldfaultprot_count { get; set; }
        public string progNo { get; set; }
        public string CDSID { get; set; }
        public int WMComment { get; set; }
    }


    public class _Weldfaults
    {
        GADATAEntitiesWelding db = new GADATAEntitiesWelding();

        public void UpdateWeldFault(PerformContext context)
        {
            List<WeldfaultDummy> data = new List<Areas.Welding.WeldfaultDummy>();


        }

        // GET: Welding/Update
        //test for in line edit 1 method for 1 value
        public void UpdateLabel(string id, string value)
        {
            rt_weldfault _Weldfault = db.rt_weldfault.Find(Int32.Parse(id));
            _Weldfault.WMComment = value;
            //  db.SaveChanges();
        }


        public ActionResult _Weldfaultanalyse()
        {
            var query = from i in db.rt_alarm

                        join s in db.rt_job_breakdown on i.id equals s.rt_alarm_id
                        join t in db.rt_job on s.rt_job_id equals t.id
                        join fault in db.rt_weldfault on i.protRecord_ID equals fault.id
                        join timer in db.Timer on fault.timerId equals timer.ID
                        join NPT in db.NPT on timer.NptId equals NPT.ID
                        join user in db.Users on NPT.OwnerId equals user.ID
                        where i.errorCode1 != 87 && i.errorCode1 != 80 && i.errorCode1 != 94
                        && i.errorCode1 != 140

                        select new
                        {
                            timer.Name,
                            s.ts_breakdownStart,
                            s.ts_breakdownEnd,
                            i.errorCode1_txt,
                            i.errorCode2_txt,
                            t.rt_weldfaultprot_count,
                            fault.progNo,
                            user.CDSID,
                            fault.WMComment
                        };
            return query;

        }
        public ActionResult _WeldFaulprotocolTop10()
        {
            IQueryable<WeldFaultProtocol> data = db.WeldFaultProtocol.AsQueryable();



            var list = from t in db.WeldFaultProtocol
                       where t.FaultStart >= DateTime.Now.AddDays(-1)
                       orderby t.breakdownTime
                       select t;
            return list;
        }
    }
    }
    */

   