using EQUICommunictionLib;
using EqUiWebUi.Areas.Alert.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class ControlChartController : Controller
    {
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/ControlChart
        public ActionResult Index()
        {
            return View();
        }

        //testControlchart
        [HttpGet]
        public ActionResult GetControlChart()
        {
            return View();
        }


        // 
        //partial viaw to get control chart.
        [HttpGet]
        public ActionResult _GetControlChart()
        {
            return View();
        }

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);
        //get data for the chart => returns json result
        [HttpGet]
        public JsonResult _getData()
        {
            int ControlLimitID = 766;

            ConnectionManager connectionManager = new ConnectionManager();
            l_controlLimits l_ControlLimit = db.l_controlLimits.Where(l => l.id == ControlLimitID).First();
            c_triggers c_Trigger = db.c_triggers.Where(c => c.id == 22).First();


            List<l_dummyControlchartResult> result = db.l_dummyControlchartResult.SqlQuery(c_Trigger.controlChartSqlStatement,
                              new SqlParameter("@c_trigger_id", c_Trigger.id),
                              new SqlParameter("@alarmobject", l_ControlLimit.alarmobject)
             ).Where(l => l.timestamp < System.DateTime.Now.AddDays(-30)).ToList();


            object data = from e in result
                          select new
                {
                    x = ((e.timestamp - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                    y = e.value
                };
            //
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}