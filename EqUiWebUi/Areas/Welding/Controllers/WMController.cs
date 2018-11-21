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
            IQueryable<TimerBreakdownDatachange> data = db.TimerBreakdownDatachange.AsQueryable();
            return View(data);
        }


        public static class welfaultdata
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


        //hier de join maken van de dummy
        public ActionResult _WeldFaultprotocol()
        {

            IQueryable<WeldFaultProtocol> data = db.WeldFaultProtocol.AsQueryable();

            return View(data);

        }


        /*
                // POST: Welding/WeldFaultProtocols/Create
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Create([Bind(Include = "NPT,Name,FaultStart,FaultEnd,breakdownTime,errorCode1_txt,errorCode2_txt,CountFaultsInJob,progNo,CDSID,WMComment,id")] WeldFaultProtocol weldFaultProtocol)
                {
                    if (ModelState.IsValid)
                    {
                        db.WeldFaultProtocol.Add(weldFaultProtocol);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }

                    return View(weldFaultProtocol);
                }

                // GET: Welding/WeldFaultProtocols/Edit/5
                public ActionResult Edit(string id)
                {
                    if (id == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    WeldFaultProtocol weldFaultProtocol = db.WeldFaultProtocol.Find(id);
                    if (weldFaultProtocol == null)
                    {
                        return HttpNotFound();
                    }
                    return View(weldFaultProtocol);
                }

                // POST: Welding/WeldFaultProtocols/Edit/5
                // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
                // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
                [HttpPost]
                [ValidateAntiForgeryToken]
                public ActionResult Edit([Bind(Include = "NPT,Name,FaultStart,FaultEnd,breakdownTime,errorCode1_txt,errorCode2_txt,CountFaultsInJob,progNo,CDSID,WMComment,id")] WeldFaultProtocol weldFaultProtocol)
                {
                    if (ModelState.IsValid)
                    {
                        db.Entry(weldFaultProtocol).State = EntityState.Modified;
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                    return View(weldFaultProtocol);
                }

                protected override void Dispose(bool disposing)
                {
                    if (disposing)
                    {
                        db.Dispose();
                    }
                    base.Dispose(disposing);
                }


                public ActionResult _WeldFaultCount()
                {
                    IQueryable<WeldfaultCount> data = db.WeldfaultCount.AsQueryable();

                    var query1 = from x in db.WeldfaultCount
                                 orderby x.countofTimerFaults descending

                                 select x;

                    return PartialView(data);
                }

                public ActionResult WeldFaultIndex()
                {
                    IQueryable<WeldfaultCount> data = db.WeldfaultCount.AsQueryable();
                    return View(data);
                }

                // GET: Welding/Update
                //test for in line edit 1 method for 1 value
           /*     public void UpdateLabel1(string id, string value)
                {
                    WeldfaultCount weldFaultCount = db.WeldfaultCount.Find(Int32.Parse(id));
                    weldFaultCount.WMComment = value;
                    db.SaveChanges();
                }

                public void UpdateLabel2(string id, string value)
                {
                    rt_weldfault rt_weldfault = db.rt_weldfault.Find(Int32.Parse(id));
                    rt_weldfault.WMComment = value;

                   db.SaveChanges();
                }

            */

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
        
    






/*  { 

      var query = from g in db.rt_weldfault
                  join t in db.Timer on g.timerId equals t.ID
                  join z in db.NPT on t.NptId equals z.ID
                  join y in db.Users on z.OwnerId equals y.ID

                  join e in db.rt_job on g.id equals e.rt_weldfaultprot_id_Start
                  join u in db.rt_job_breakdown on e.id equals u.rt_job_id
                  join o in db.rt_alarm on u.rt_alarm_id equals o.id


                  orderby System.Data.Entity.SqlServer.SqlFunctions.DateDiff("minute", u.ts_breakdownStart, u.ts_breakdownEnd) descending

                  select new
                  {
                      g.id,

                      t.Name,
                      u.ts_breakdownStart,
                      u.ts_breakdownEnd,
                      breakdownTime = System.Data.Entity.SqlServer.SqlFunctions.DateDiff("minute", u.ts_breakdownStart, u.ts_breakdownEnd),
                      o.errorCode1_txt,
                      o.errorCode2_txt,
                      e.rt_weldfaultprot_count,
                      g.progNo,
                      g.WMComment

                  };

      return PartialView(query);

  }
    */




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

