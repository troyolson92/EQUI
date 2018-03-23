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

    }
}