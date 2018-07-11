using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EqUiWebUi.Areas.user_management.Models;

namespace EqUiWebUi.Controllers
{
    public class TableauController : Controller
    {
        private GADATAEntitiesUserManagement db = new GADATAEntitiesUserManagement();
        // GET: Tableau
        public ActionResult Index()
        {
            return View();
        }

        //handels inbedded views from tableau for FullHD and smaller type monitors
        [HttpGet]
        public ActionResult EmbeddedDesktop(string workbook, string sheet)
        {
            //
            ViewBag.workbook = workbook;
            ViewBag.sheet = sheet;
            ViewBag.Weeknum = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(System.DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            ViewBag.Weekday = (int)System.DateTime.Now.DayOfWeek;
            ViewBag.LocationTreeFilter = GetUsersAreaname();
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


        [HttpGet]
        public ActionResult Test()
        {
            return View();
        }


        //get the users Areaname based on the location root
        private string GetUsersAreaname()
        {
            string LocationRoot = CurrentUser.Getuser.LocationRoot;
            if (LocationRoot != "")
            {
                c_areas areaFilters = db.c_areas.Where(l => l.LocationTreeFilter1.StartsWith(LocationRoot)).First();
                return areaFilters.Area;
            }
            else
            {
                return "";
            }
        }

        //https://onlinehelp.tableau.com/current/server/en-us/trusted_auth.htm

        //test page for getting ticket from tableau server. 
        //https://onlinehelp.tableau.com/current/server/en-us/trusted_auth_testing.htm
       public ActionResult TestTicket()
        {
           // HttpContext.Response.AddHeader("Content-Type", "application/x-www-form-urlencoded;charset=UTF-8");
            return View();
        }
    }
}