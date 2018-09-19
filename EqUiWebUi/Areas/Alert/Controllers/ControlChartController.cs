﻿using EQUICommunictionLib;
using EqUiWebUi.Areas.Alert.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
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
            //get opt data labels 
            c_triggers trigger = db.c_triggers.Find(chartSettings.c_trigger_id);
            if (trigger.OptValueLabels != null)
            {
                chartSettings.optDataLabels = trigger.OptValueLabels.Split(';').ToList();
            }
            //
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
        DECLARE @optDatanum as int
        DECLARE @alarmobject as varchar(max)
        set @c_trigger_id = 22
        set @alarmobject = '35200R01_gun1'
        set @optDatanum = 0
*/
        [HttpGet]
        public JsonResult _getData(ChartSettings chartSettings)
        {
            ConnectionManager connectionManager = new ConnectionManager();
            c_triggers c_Trigger = db.c_triggers.Where(c => c.id == chartSettings.c_trigger_id).First();

            //update the scalelabel 
            if (c_Trigger.controlChartYlabel != null)
            {
                chartSettings.scaleLabel = c_Trigger.controlChartYlabel;
            }
            else
            {
                chartSettings.scaleLabel = "<%=value%>";
            }

            //get data
            List<l_dummyControlchartResult> result = new List<l_dummyControlchartResult>();
            try
            {
                //Query to get value data
                IQueryable<l_dummyControlchartResult> Qresult = db.l_dummyControlchartResult.SqlQuery(c_Trigger.controlChartSqlStatement,
                                  new SqlParameter("@c_trigger_id", chartSettings.c_trigger_id),
                                  new SqlParameter("@alarmobject", chartSettings.alarmobject),
                                  new SqlParameter("@optDatanum", chartSettings.optDatanum)
                 ).Where(l => l.timestamp > chartSettings.startdate && l.timestamp < chartSettings.enddate).AsQueryable();
                 result = Qresult.ToList();
            }
            catch (Exception ex)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                Response.StatusDescription = ex.Message;
                return Json(null, JsonRequestBehavior.AllowGet);
            }

            object ValueData = from e in result
                              select new
                              {
                                x = ((e.timestamp - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                y = Math.Round(e.value,3),
                                r = SetPointSize(e)
                              };

            //get optional datasets
            object OptValueData = from e in result
                               select new
                               {
                                   x = ((e.timestamp - UnixEpoch).Ticks / TimeSpan.TicksPerMillisecond),
                                   y = Math.Round(e.OptValue.GetValueOrDefault(), 3),
                                   r = 0.7
                               };
  

            //get the control limits seperate. (else a point is returned for each record and the rendering looks bad.
            double controllimitPointSize = 0.6;

            List<l_controlLimits> limits = db.l_controlLimits.Where(l =>
                    l.c_trigger_id == chartSettings.c_trigger_id
                    && l.alarmobject == chartSettings.alarmobject
                    &&
                    (
                    (l.CreateDate > chartSettings.startdate && l.CreateDate < chartSettings.enddate) //allows control limits CREATEd in the daterange to be in.
                    || (l.ChangeDate > chartSettings.startdate && l.ChangeDate < chartSettings.enddate) //allows control limts CLOSED in the daterange to be in.
                    || l.isdead == false//allows the active control limit to always in 
                    )
                    ).ToList();

            //adjest the dead control limits to fit in the 'view' windowd.
            List<l_controlLimits> oldLimits = limits.Where(l => l.isdead == true).ToList();
            foreach (l_controlLimits oldlimit in oldLimits)
            {
                if (oldlimit.CreateDate < chartSettings.startdate) //if created before viewwindows set startdate to start of window.
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
            else if (activeLimit.CreateDate > chartSettings.enddate)//if the active limit is newer than the daterange. => Drop it.
            {
                limits.Remove(activeLimit);
            }
            else if (activeLimit.CreateDate < chartSettings.startdate) //active limit was created before the datarange. => Set create date to start of daterange and set the changedate to the end of the daterange.
            {
                activeLimit.CreateDate = chartSettings.startdate;
                activeLimit.ChangeDate = chartSettings.enddate;
            }
            else //active limit was created in the daterange. => set the changedate to the end of the daterange.
            {
                activeLimit.ChangeDate = chartSettings.enddate;
            }

            //build the control limit data objects.
            object UCLData = (from e in limits
                              select new //startpoints
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
                              select new //startpoints
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
            data.Add(OptValueData);
            //
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}