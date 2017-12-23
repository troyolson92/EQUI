using System;
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

        [HttpGet]
        public ActionResult _getErrorTrend(LogInfo logInfo)
        {
            return PartialView();
        }

        [HttpGet]
        public JsonResult _getData(string location ,int  errornum ,string logtekst ,string logtype ,grouptType grouptType = grouptType.Hour)
        {
            GadataComm gadataComm = new GadataComm();

            //
            // need to handle how many days we fetch! use the grouptype 
            //

            string qry = string.Format(@"EXEC [EqUi].[GetErrorTrentData] @Location = '{0}' ,@ERRORNUM = {1} ,@Logtext = '{2}' ,@logType = '{3}'"
            , location, errornum, logtekst, logtype);

            DataTable dt = gadataComm.RunQueryGadata(qry);

            var data = groupby(dt, "starttime", grouptType);

            return Json(data, JsonRequestBehavior.AllowGet);            
        }

        //helps to group a datatable collum list
        public enum grouptType {None, Hour, Day, Week, Month };
        public object groupby(DataTable dt,string groupcol, grouptType grouptType)
        {
            var currentCalendar = CultureInfo.CurrentCulture.Calendar;
            //
            switch (grouptType)
            {
                case grouptType.None:
                    //not implemented
                    return null;

                case grouptType.Hour:
                    var groupedByHour = from p in dt.AsEnumerable()
                                        orderby p.Field<DateTime>(groupcol) descending
                                        group p by new { hour = p.Field<DateTime>(groupcol).Hour,
                                                          day = (int)p.Field<DateTime>(groupcol).DayOfWeek,
                                                          weeknum = currentCalendar.GetWeekOfYear(p.Field<DateTime>(groupcol), System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek),
                                                          year = p.Field<DateTime>(groupcol).Year % 100 }  into d
                                        select new { timestamp = string.Format("{0}W{1}D{2} h{3}", d.Key.year, d.Key.weeknum, d.Key.day, d.Key.hour)
                                 
                                            , count = d.Count() };
                    return groupedByHour;

                case grouptType.Day:
                    var groupedByDay = from p in dt.AsEnumerable()
                                       orderby p.Field<DateTime>(groupcol) descending
                                       group p by new
                                        {
                                            day = (int)p.Field<DateTime>(groupcol).DayOfWeek,
                                            weeknum = currentCalendar.GetWeekOfYear(p.Field<DateTime>(groupcol), System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek),
                                            year = p.Field<DateTime>(groupcol).Year % 100
                                        } into d
                                        select new
                                        {
                                            timestamp = string.Format("{0}W{1}D{2}", d.Key.year, d.Key.weeknum, d.Key.day)
                                                  ,
                                            count = d.Count()
                                        };
                    return groupedByDay;

                case grouptType.Week:
                    var groupedByWeek = from p in dt.AsEnumerable()
                                        orderby p.Field<DateTime>(groupcol) descending
                                        group p by new
                                       {
                                           weeknum = currentCalendar.GetWeekOfYear(p.Field<DateTime>(groupcol), System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek),
                                           year = p.Field<DateTime>(groupcol).Year % 100
                                       } into d
                                       select new
                                       {
                                           timestamp = string.Format("{0}W{1}", d.Key.year, d.Key.weeknum)
                                                 ,
                                           count = d.Count()
                                       };
                    return groupedByWeek;

                case grouptType.Month:
                    var groupedByMonth = from p in dt.AsEnumerable()
                                         orderby p.Field<DateTime>(groupcol) descending
                                         group p by new
                                        {
                                            month = p.Field<DateTime>(groupcol).ToString("MMM"),
                                            year = p.Field<DateTime>(groupcol).Year 
                                        } into d
                                        select new
                                        {
                                            timestamp = string.Format("{0} {1}", d.Key.year, d.Key.month)
                                                  ,
                                            count = d.Count()
                                        };
                    return groupedByMonth;

                default:
                    return null;
            }
        }

    }
}