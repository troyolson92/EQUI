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

        //partial viaw to get control chart.
        [HttpGet]
        public ActionResult _GetControlChart(ChartSettings chartSettings)
        {
            return PartialView(chartSettings);
        }

        //helper to convert date
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        //helper so set point size of out of control limit
        private Double SetPointSize(l_dummyControlchartResult l_DummyControlchartResult)
        {
            if (l_DummyControlchartResult.value > l_DummyControlchartResult.UpperLimit || l_DummyControlchartResult.value < l_DummyControlchartResult.LowerLimit)
            {
                return 1.5;
            }
            else
            {
                return 0.75;
            }

        }

        //get data for the chart => returns json result
        /*
         DECLARE @c_trigger_id as int 
         DECLARE @alarmobject as varchar(max)
        set @c_trigger_id = 22
        set @alarmobject = '35200R01_gun1'
*/
        [HttpGet]
        public JsonResult _getData(ChartSettings chartSettings)
        {
            ConnectionManager connectionManager = new ConnectionManager();
            c_triggers c_Trigger = db.c_triggers.Where(c => c.id == chartSettings.c_trigger_id).First();


            IQueryable<l_dummyControlchartResult> Qresult = db.l_dummyControlchartResult.SqlQuery(c_Trigger.controlChartSqlStatement,
                              new SqlParameter("@c_trigger_id", chartSettings.c_trigger_id),
                              new SqlParameter("@alarmobject", chartSettings.alarmobject)
             ).Where(l => l.timestamp > chartSettings.startdate && l.timestamp < chartSettings.enddate).AsQueryable();

            List <l_dummyControlchartResult> result = Qresult.ToList();

            object ValueData = from e in result
                              select new
                              {
                                x = ((e.timestamp - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                y = Math.Round(e.value,2),
                                r = SetPointSize(e)
                              };

            object UCLData = from e in result.Where(l => l.l_controlLimits_id.HasValue)
                               select new
                               {
                                   x = ((e.timestamp - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                   y = Math.Round(e.UpperLimit.GetValueOrDefault(), 2),
                                   r = 0.6
                               };
           
            object LCLData = from e in result.Where(l => l.l_controlLimits_id.HasValue)
                             select new
                               {
                                   x = ((e.timestamp - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                   y = Math.Round(e.LowerLimit.GetValueOrDefault(), 2),
                                   r = 0.6
                               };

            //combine all data in 1 list object
            List<object> data = new List<object>();
            data.Add(chartSettings);
            data.Add(ValueData);
            data.Add(UCLData);
            data.Add(LCLData);
            //
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}