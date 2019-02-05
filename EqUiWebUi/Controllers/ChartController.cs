﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using System.Data;
using System.Globalization;
using EqUiWebUi.Models;

namespace EqUiWebUi.Controllers
{
    public class ChartController : Controller
    {
        // GET: Chart
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// standalone view to get error trend  Gets used in VSTO!
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetErrorTrend(LogInfo logInfo)
        {
            return View(logInfo);
        }

        /// <summary>
        /// partial view to get error trend. Get called by "gadata\_moreinfo" modal
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        public ActionResult _getErrorTrend(LogInfo logInfo)
        {
            return PartialView(logInfo);
        }

        /// <summary>
        /// get data for the ErrorTrendchart => returns j son result
        /// </summary>
        /// <param name="logInfo"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="grouptType"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult _getData(LogInfo logInfo, DateTime startDate, DateTime endDate, grouptType grouptType = grouptType.Hour)
        {
            ConnectionManager connectionManager = new ConnectionManager();

            //
            // need to handle how many days we fetch! use the group type 
            //

            //if start date is today set it to now. (else it will plots hours in the future)
            if (startDate.Date == System.DateTime.Now.Date)
            {
                startDate = System.DateTime.Now;
            }

            //get the data
            //added temporary switch to get vcch up 
           string qry;
           if(System.Configuration.ConfigurationManager.AppSettings["Maximo_SiteID"].ToString() == "VCG")
           {
                 qry = $"EXEC [EqUi].[GetErrorTrentData] @Location = '{logInfo.location}' ,@ERRORNUM = '{logInfo.errornum}' ,@Logtext = '{logInfo.logtext}' ,@logType = '{logInfo.logtype}' ,@refID={logInfo.refid}";
           }
           else
           {
                 qry = $"EXEC [EqUi].[VCCHGetErrorTrentData] @Location = '{logInfo.location}' ,@ERRORNUM = '{logInfo.errornum}' ,@Logtext = '{logInfo.logtext}' ,@logType = '{logInfo.logtype}' ,@refID={logInfo.refid}";
           }

                
            DataTable dt = connectionManager.RunQuery(qry);

            //if group mode auto (0) find out best grouping mode based on set timespan.
            if (grouptType == grouptType.auto)
            {
                double span = (startDate - endDate).TotalDays;
                if (span > 365)
                {
                    grouptType = grouptType.Month;
                }
                else if(span > 100)
                {
                    grouptType = grouptType.Week;
                }
                else if(span > 6)
                {
                    grouptType = grouptType.Day;
                }
                else
                {
                    grouptType = grouptType.Hour;
                }
            }

            //create data and use the auto fill system.
            object data = null;
            switch (grouptType)
            {
                case grouptType.Hour:
                    data = completeDataAndGroupByHour(dt, "starttime", startDate, endDate);
                    break;

                case grouptType.Day:
                    data = completeDataAndGroupByDay(dt, "starttime", startDate, endDate);
                    break;

                case grouptType.Week:
                    data = completeDataAndGroupByWeek(dt, "starttime", startDate, endDate);
                    break;

                case grouptType.Month:
                    data = completeDataAndGroupByMonth(dt, "starttime", startDate, endDate);
                    break;

                default:
                    return null;
            }
            //
            return Json(data, JsonRequestBehavior.AllowGet);            
        }

        //helps to group a data table column list
        public enum grouptType {auto, Hour, Day, Week, Month };
        //generate 'timestamps' that have no data. to help complete missing graph data
        //data from inDt is also filter using the startDate and endDate!
        public object completeDataAndGroupByHour(DataTable inDt, string groupcol, DateTime startDate, DateTime endDate)
        {
            //we need to calculate all missing data in the lowest display format we want (hours)

            //calculate how many days we have to traverse.
            int hours = System.Convert.ToInt32(System.Math.Ceiling((startDate - endDate).TotalHours));

            // Gather the data we have in the database, which will be incomplete for the graph (missing dates).
            var currentCalendar = CultureInfo.CurrentCulture.Calendar;
            var dataQuery =
                from tr in inDt.AsEnumerable()
                where (tr.Field<DateTime>(groupcol) > endDate) && (tr.Field<DateTime>(groupcol) < startDate)
                group tr by new { tr.Field<DateTime>(groupcol).Year, tr.Field<DateTime>(groupcol).Month, tr.Field<DateTime>(groupcol).Day, tr.Field<DateTime>(groupcol).Hour } into g
                select new
                {
                    Datetime = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day, g.Key.Hour, 0, 0),
                    Count = g.Count()
                };

            // Generate the complete list of Dates we want.
            var roundedStartdate = startDate.RoundToNearestHour();
            var datetimes = new List<DateTime>();
            for (int i = 0; i < hours; i++)
            {
                datetimes.Add(roundedStartdate.AddHours(-i));
            }

            // Generate the empty table, which is the shape of the output we want but without counts.
            var emptyTableQuery =
                from dt in datetimes
                select new
                {
                    Datetime = new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, 0, 0),
                    Count = 0
                };

            // Perform an outer join of the empty table with the real data and use the magic DefaultIfEmpty
            // to handle the "there's no data from the database case".
            var finalQuery =
                from e in emptyTableQuery
                join realData in dataQuery on
                    new { e.Datetime } equals
                    new { realData.Datetime } into g
                from realDataJoin in g.DefaultIfEmpty()
                select new
                {
                    Datetime = e.Datetime,
                    Label = string.Format("{0}W{1}D{2}h{3}"
                                                , e.Datetime.Year % 100
                                                , currentCalendar.GetWeekOfYear(e.Datetime, System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                                                , (int)e.Datetime.DayOfWeek
                                                , e.Datetime.Hour
                                                ),
                    Count = realDataJoin == null ? 0 : realDataJoin.Count
                };

            //sort it and return 
            return finalQuery.OrderByDescending(x => x.Datetime);
        }
        public object completeDataAndGroupByDay(DataTable inDt, string groupcol, DateTime startDate, DateTime endDate)
        {
            //we need to calculate all missing data in the lowest display format we want (days)

            //calculate how many days we have to traverse.
            int days = System.Convert.ToInt32(System.Math.Ceiling((startDate - endDate).TotalDays));

            // Gather the data we have in the database, which will be incomplete for the graph (missing dates).
            var currentCalendar = CultureInfo.CurrentCulture.Calendar;
            var dataQuery =
                from tr in inDt.AsEnumerable()
                where (tr.Field<DateTime>(groupcol) > endDate) && (tr.Field<DateTime>(groupcol) < startDate)
                group tr by new { tr.Field<DateTime>(groupcol).Year, tr.Field<DateTime>(groupcol).Month, tr.Field<DateTime>(groupcol).Day} into g
                select new
                {
                    Datetime = new DateTime(g.Key.Year, g.Key.Month, g.Key.Day, 0, 0, 0),
                    Count = g.Count()
                };

            // Generate the complete list of Dates we want.
            var roundedStartdate = startDate.RoundToNearestHour();
            var datetimes = new List<DateTime>();
            for (int i = 0; i < days; i++)
            {
                datetimes.Add(roundedStartdate.AddDays(-i));
            }

            // Generate the empty table, which is the shape of the output we want but without counts.
            var emptyTableQuery =
                from dt in datetimes
                select new
                {
                    Datetime = new DateTime(dt.Year, dt.Month, dt.Day, 0, 0, 0),
                    Count = 0
                };

            // Perform an outer join of the empty table with the real data and use the magic DefaultIfEmpty
            // to handle the "there's no data from the database case".
            var finalQuery =
                from e in emptyTableQuery
                join realData in dataQuery on
                    new { e.Datetime } equals
                    new { realData.Datetime } into g
                from realDataJoin in g.DefaultIfEmpty()
                select new
                {
                    Datetime = e.Datetime,
                    Label = string.Format("{0}W{1}D{2}"
                                                , e.Datetime.Year % 100
                                                , currentCalendar.GetWeekOfYear(e.Datetime, System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                                                , (int)e.Datetime.DayOfWeek
                                                ),
                    Count = realDataJoin == null ? 0 : realDataJoin.Count
                };

            //sort it and return 
            return finalQuery.OrderByDescending(x => x.Datetime);
        }
        public object completeDataAndGroupByWeek(DataTable inDt, string groupcol, DateTime startDate, DateTime endDate)
        {
            //we need to calculate all missing data in the lowest display format we want (Weeks)

            //calculate how many days we have to traverse.
            int days = System.Convert.ToInt32(System.Math.Ceiling((startDate - endDate).TotalDays));

            // Gather the data we have in the database, which will be incomplete for the graph (missing dates).
            var currentCalendar = CultureInfo.CurrentCulture.Calendar;
            var dataQuery =
                from tr in inDt.AsEnumerable()
                where (tr.Field<DateTime>(groupcol) > endDate) && (tr.Field<DateTime>(groupcol) < startDate)
                group tr by new { tr.Field<DateTime>(groupcol).Year, Week = currentCalendar.GetWeekOfYear(tr.Field<DateTime>(groupcol), System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek) } into g
                select new
                {
                    Datetime = FirstDateOfWeekISO8601(g.Key.Year, g.Key.Week),
                    Count = g.Count()
                };

            // Generate the complete list of Dates we want.
            var roundedStartdate = startDate.RoundToNearestHour();
            var datetimes = new List<DateTime>();
            for (int i = 0; i < days; i++)
            {
                datetimes.Add(roundedStartdate.AddDays(-i));
            }

            // Generate the empty table, which is the shape of the output we want but without counts.
            var emptyTableQuery =
                from dt in datetimes
                select new
                {
                    Datetime = FirstDateOfWeekISO8601(dt.Year, currentCalendar.GetWeekOfYear(dt, System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)),
                    Count = 0
                };

            //because we could not do AddWeek our empty table now has for earch month X days labels. We need to filter this to distict
            emptyTableQuery = emptyTableQuery.Distinct();

            // Perform an outer join of the empty table with the real data and use the magic DefaultIfEmpty
            // to handle the "there's no data from the database case".
            var finalQuery =
                from e in emptyTableQuery
                join realData in dataQuery on
                    new { e.Datetime } equals
                    new { realData.Datetime } into g
                from realDataJoin in g.DefaultIfEmpty()
                select new
                {
                    Datetime = e.Datetime,
                    Label = string.Format("{0}W{1}"
                                                , e.Datetime.Year%100
                                                , currentCalendar.GetWeekOfYear(e.Datetime, System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                                                ),
                    Count = realDataJoin == null ? 0 : realDataJoin.Count
                };

            //sort it and return 
            return finalQuery.OrderByDescending(x => x.Datetime);
        }
        public static DateTime FirstDateOfWeekISO8601(int year, int weekOfYear)
        {
            DateTime jan1 = new DateTime(year, 1, 1);
            int daysOffset = DayOfWeek.Thursday - jan1.DayOfWeek;

            DateTime firstThursday = jan1.AddDays(daysOffset);
            var cal = CultureInfo.CurrentCulture.Calendar;
            int firstWeek = cal.GetWeekOfYear(firstThursday, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);

            var weekNum = weekOfYear;
            if (firstWeek <= 1)
            {
                weekNum -= 1;
            }
            var result = firstThursday.AddDays(weekNum * 7);
            return result.AddDays(-3);
        }
        public object completeDataAndGroupByMonth(DataTable inDt, string groupcol, DateTime startDate, DateTime endDate)
        {
            //we need to calculate all missing data in the lowest display format we want (Months)

            //calculate how many days we have to traverse.
            int days = System.Convert.ToInt32(System.Math.Ceiling((startDate - endDate).TotalDays));

            // Gather the data we have in the database, which will be incomplete for the graph (missing dates).
            var currentCalendar = CultureInfo.CurrentCulture.Calendar;
            var dataQuery =
                from tr in inDt.AsEnumerable()
                where (tr.Field<DateTime>(groupcol) > endDate) && (tr.Field<DateTime>(groupcol) < startDate)
                group tr by new { tr.Field<DateTime>(groupcol).Year, tr.Field<DateTime>(groupcol).Month } into g
                select new
                {
                    Datetime = new DateTime(g.Key.Year, g.Key.Month, 1, 0, 0, 0),
                    Count = g.Count()
                };

            // Generate the complete list of Dates we want.
            var roundedStartdate = startDate.RoundToNearestHour();
            var datetimes = new List<DateTime>();
            for (int i = 0; i < days; i++)
            {
                datetimes.Add(roundedStartdate.AddDays(-i));
            }

            // Generate the empty table, which is the shape of the output we want but without counts.
            var emptyTableQuery =
                from dt in datetimes
                select new
                {
                    Datetime = new DateTime(dt.Year, dt.Month, 1, 0, 0, 0),
                    Count = 0
                };

            //because we could not do AddMonth our empty table now has for earch month X days labels. We need to filter this to distict
            emptyTableQuery = emptyTableQuery.Distinct();

            // Perform an outer join of the empty table with the real data and use the magic DefaultIfEmpty
            // to handle the "there's no data from the database case".
            var finalQuery =
                from e in emptyTableQuery
                join realData in dataQuery on
                    new { e.Datetime } equals
                    new { realData.Datetime } into g
                from realDataJoin in g.DefaultIfEmpty()
                select new
                {
                    Datetime = e.Datetime,
                    Label = string.Format("{0} {1}"
                                                , e.Datetime.Year
                                                ,e.Datetime.ToString("MMM")
                                                ),
                    Count = realDataJoin == null ? 0 : realDataJoin.Count
                };

            //sort it and return 
            return finalQuery.OrderByDescending(x => x.Datetime);
        }

    }
}