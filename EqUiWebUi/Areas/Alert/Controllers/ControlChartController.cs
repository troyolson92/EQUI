using EQUICommunictionLib;
using EqUiWebUi.Areas.Alert.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class ControlChartController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private GADATA_AlertModel db = new GADATA_AlertModel();

        // GET: Alert/ControlChart
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Test view to check _GetControlChart
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetControlChart()
        {
            return View();
        }

        /// <summary>
        /// Partial view that renders a control chart
        /// </summary>
        /// <param name="chartSettings"></param>
        /// <returns></returns>
        public ActionResult _GetControlChart(ChartSettings chartSettings)
        {
            c_triggers trigger = new c_triggers();
            if (chartSettings.c_trigger_id != 0) //set by trigger ID
            {
                 trigger = db.c_triggers.Find(chartSettings.c_trigger_id);
            }
            else if(chartSettings.alertType != null) //set by alert type (used in welding tool)
            {
                try
                {
                    trigger = db.c_triggers.Where(c => c.alertType.Contains(chartSettings.alertType)).First();
                }
                catch(Exception ex)
                {
                    log.Error($"Failed to find alter type:{chartSettings.alertType}", ex);
                    throw ex;
                }
                 chartSettings.c_trigger_id = trigger.id;
            }

            chartSettings.chartname = trigger.alertType;
            if (trigger.OptValueLabels != null)
            {
                chartSettings.optDataLabels = trigger.OptValueLabels.Split(';').ToList();
            }
            //
            return PartialView(chartSettings);
        }

        /// <summary>
        /// helper to convert date to unix type date
        /// </summary>
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1);

        /// <summary>
        /// helper so set point size of out of control limit
        /// <param name="controlchartResult"></param>
        /// <returns></returns>
        private Double SetPointSize(ControlchartResult controlchartResult)
        {
            if (controlchartResult.value > controlchartResult.UpperLimit || controlchartResult.value < controlchartResult.LowerLimit)
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
        DECLARE @optDatanum as int
        DECLARE @alarmobject as varchar(max)
        set @c_trigger_id = 22
        set @alarmobject = '35200R01_gun1'
        set @optDatanum = 0
        */
        [HttpGet]
        public Task<JsonResult> _getData(ChartSettings chartSettings)
        {
            return Task.Run(() => {
                ConnectionManager connectionManager = new ConnectionManager();
                c_triggers c_Trigger = db.c_triggers.Where(c => c.id == chartSettings.c_trigger_id).First();

                //update chart settings with the Y scale label 
                if (c_Trigger.controlChartYlabel != null)
                {
                    chartSettings.scaleLabel = c_Trigger.controlChartYlabel; 
                }
                else
                {
                    chartSettings.scaleLabel = "<%=value%>";
                }
                //update chart settings with data labels
                if(c_Trigger.ValueLabels != null)
                {
                    //if ValueLabels contains ; this is means there is an RefValue dataset that needs to be handled
                    if(c_Trigger.ValueLabels.Contains(';'))
                    {
                        chartSettings.ValueLabel = c_Trigger.ValueLabels.Split(';')[0];
                        chartSettings.RefValueLabel = c_Trigger.ValueLabels.Split(';')[1];
                    }
                    else
                    {
                        chartSettings.ValueLabel = c_Trigger.ValueLabels;
                        chartSettings.RefValueLabel = null; //don't plot data!
                    }
                }
                else
                {
                    chartSettings.ValueLabel = "Value";
                }

                //get data
                List<ControlchartResult> ChartData = new List<ControlchartResult>();
                //set db timeout to 10 seconds
                db.Database.CommandTimeout = 15;
                //Query to get value data
                ChartData =  db.Database.SqlQuery<ControlchartResult>(c_Trigger.controlChartSqlStatement,
                                    new SqlParameter("@c_trigger_id", chartSettings.c_trigger_id),
                                    new SqlParameter("@alarmobject", chartSettings.alarmobject),
                                    new SqlParameter("@optDatanum", chartSettings.optDatanum)
                                    ).Where(l => l.timestamp > chartSettings.startdate && l.timestamp < chartSettings.enddate).ToList();

                //main chart value 
                object ValueData = from e in ChartData
                                   where e.value != null
                                   orderby e.timestamp
                                   select new
                                  {
                                    x = ((e.timestamp - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                    y = Math.Round(e.value.GetValueOrDefault(),3),
                                    r = SetPointSize(e)
                                  };

                //main chart REF value 
                object RefValueData = from e in ChartData
                                      where e.RefValue != null
                                      orderby e.timestamp
                                      select new
                                   {
                                       x = ((e.timestamp - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                       y = Math.Round(e.RefValue.GetValueOrDefault(), 3),
                                       r = 0.7
                                   };

                //get optional datasets (this gets rendered in an extra chart below the main one
                object OptValueData = from e in ChartData
                                      where e.OptValue != null
                                      orderby e.timestamp
                                      select new
                                   {
                                       x = ((e.timestamp - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                       y = Math.Round(e.OptValue.GetValueOrDefault(), 3),
                                       r = 0.7
                                   };
  

                //get the control limits separate. (else a point is returned for each record and the rendering looks bad.
                double controllimitPointSize = 0.6;

                List<l_controlLimits> limits = db.l_controlLimits.Where(l =>
                        l.c_trigger_id == chartSettings.c_trigger_id
                        && l.alarmobject == chartSettings.alarmobject
                        &&
                        (
                        (l.CreateDate > chartSettings.startdate && l.CreateDate < chartSettings.enddate) //allows control limits CREATEd in the date range to be in.
                        || (l.ChangeDate > chartSettings.startdate && l.ChangeDate < chartSettings.enddate) //allows control limits CLOSED in the date range to be in.
                        || l.isdead == false//allows the active control limit to always in 
                        )
                        ).ToList();

                //adjust the dead control limits to fit in the 'view' window.
                List<l_controlLimits> oldLimits = limits.Where(l => l.isdead == true).ToList();
                foreach (l_controlLimits oldlimit in oldLimits)
                {
                    if (oldlimit.CreateDate < chartSettings.startdate) //if created before view windows set start date to start of window.
                    {
                        oldlimit.CreateDate = chartSettings.startdate;
                    }
                }

                //adjust the active control limit to fit in the 'View' window
                l_controlLimits activeLimit = limits.Where(l => l.isdead == false).FirstOrDefault();
                if (activeLimit == null) //no active limit. (removed system)
                {
                   //do nothing.
                }
                else if (activeLimit.CreateDate > chartSettings.enddate)//if the active limit is newer than the date range. => Drop it.
                {
                    limits.Remove(activeLimit);
                }
                else if (activeLimit.CreateDate < chartSettings.startdate) //active limit was created before the data range. => Set create date to start of date range and set the change date to the end of the date range.
                {
                    activeLimit.CreateDate = chartSettings.startdate;
                    activeLimit.ChangeDate = chartSettings.enddate;
                }
                else //active limit was created in the date range. => set the change date to the end of the date range.
                {
                    activeLimit.ChangeDate = chartSettings.enddate;
                }

                //build the control limit data objects.
                object UCLData = (from e in limits
                                  select new //start points
                                  {
                                      x = ((e.CreateDate.AddSeconds(300) - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                      y = Math.Round(e.UpperLimit.GetValueOrDefault(), 3),
                                      LCL = Math.Round(e.LowerLimit.GetValueOrDefault(), 3),
                                      r = controllimitPointSize

                                  }).Union((from e in limits
                                            select new //endpoints
                                            {
                                                x = ((e.ChangeDate.GetValueOrDefault(chartSettings.enddate) - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                                y = Math.Round(e.UpperLimit.GetValueOrDefault(), 3),
                                                LCL = Math.Round(e.LowerLimit.GetValueOrDefault(), 3),
                                                r = controllimitPointSize
                                            })).OrderBy(e => e.x);

                object LCLData = (from e in limits
                                  select new //start points
                                  {
                                      x = ((e.CreateDate.AddSeconds(300) - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                      y = Math.Round(e.LowerLimit.GetValueOrDefault(), 3),
                                      r = controllimitPointSize

                                  }).Union((from e in limits
                                            select new //endpoints
                                            {
                                                x = ((e.ChangeDate.GetValueOrDefault(chartSettings.enddate) - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                                y = Math.Round(e.LowerLimit.GetValueOrDefault(), 3),
                                                r = controllimitPointSize
                                            })).OrderBy(e => e.x);

                //combine all data in 1 list object
                List<object> data = new List<object>();
                data.Add(chartSettings);
                data.Add(ValueData);
                data.Add(UCLData);
                data.Add(LCLData);
                data.Add(RefValueData);
                data.Add(OptValueData);
                //
                return Json(data, JsonRequestBehavior.AllowGet);
            });
        }
    }
}