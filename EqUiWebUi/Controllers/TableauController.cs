using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class TableauController : Controller
    {
        // GET: Tableau
        public ActionResult Index()
        {
            return View();
        }

        //handels inbedded views from tableau for FullHD and smaller type monitors
        [HttpGet]
        public ActionResult EmbeddedDesktop(string workbook, string sheet)
        {
            ViewBag.workbook = workbook;
            ViewBag.sheet = sheet;
            ViewBag.AssetPNG = GetUserAssetPNG();
            ViewBag.Weeknum = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(System.DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            ViewBag.Weekday = (int)System.DateTime.Now.DayOfWeek;
            return View();
        }

        //handels inbedded views from tablea 4K monitors  
        [HttpGet]
        public ActionResult Embedded4K(string workbook, string sheet)
        {
            ViewBag.workbook = workbook;
            ViewBag.sheet = sheet;
            return View();
        }

        //handles getting the user filter settings for tableu bassed on client session
        //This should be reassest... you should be able to just pass the session location root to tableau. 
        //This is an unneeded configuration part.
        private string GetUserAssetPNG()
        {
            if (Session["LocationRoot"].ToString() != "")
            {
                if (Session["LocationRoot"].ToString().Contains("VCG -> A -> A GA1.0 -> A LIJN 33"))
                {
                    return "GF_FloorAssetLevel";
                }
                else if (Session["LocationRoot"].ToString().Contains("VCG -> A -> A GA1.0 -> A LIJN 35"))
                {
                    return "GF_SidesAssetLevel";
                }
                else if (Session["LocationRoot"].ToString().Contains("VCG -> A -> A GA4.0"))
                {
                    return "GF_PreAssAssetLevel";
                }
                else
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        //handels log details sheet in tableau workbooks.
        [HttpGet]
        public ActionResult logDetails(string Location, string Subgroup, string minTimestamp, string maxTimestamp, int fontSize = 18)
        {
            //for some reason I have fotten 2 differt TS formats one with fff and one without. handle both.
            DateTime startDate; //  DateTime.ParseExact(minTimestamp, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture).AddSeconds(-1);
            DateTime endDate; // = DateTime.ParseExact(maxTimestamp, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture).AddSeconds(1);

            if (DateTime.TryParse(minTimestamp, out startDate))
            {
                startDate = startDate.AddSeconds(-1);
            }
            else
            { 
                //not able to parse 
            }

            if (DateTime.TryParse(maxTimestamp, out endDate))
            {
                endDate =  endDate.AddSeconds(1);
            }
            else
            {
                //not able to parse 
            }

            //pas info for header 
            ViewBag.info = string.Format("location: {0}, subgroup: {1}, from: {2} until: {3}", Location, Subgroup, minTimestamp, maxTimestamp);
            //pas info for fontsize
            ViewBag.fontSize = fontSize;

            GADATAEntities gADATAEntities = new GADATAEntities();
            List<logDetails> data = (from logDetails in gADATAEntities.logDetails
                                     where logDetails.location.Contains(Location)
                                     && logDetails.Subgroup.Contains(Subgroup)
                                     && logDetails.timestamp > startDate
                                     && logDetails.timestamp < endDate
                                  
                                     select logDetails).ToList();
            return View(data);
        }
    }
}