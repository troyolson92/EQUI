using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using EqUiWebUi.Areas.user_management.Models;
using System.Net;
using System.Text;
using System.IO;

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
        public ActionResult EmbeddedDesktop(string workbook, string sheet, bool TrustedAuth = true)
        {
            //
            ViewBag.workbook = workbook;
            ViewBag.sheet = sheet;
            ViewBag.Weeknum = CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(System.DateTime.Now, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
            ViewBag.Weekday = (int)System.DateTime.Now.DayOfWeek;

            ViewBag.LocationTreeFilter = GetUsersAreaname();
            //
            ViewBag.TrustedAuth = TrustedAuth;
            if (TrustedAuth)
            {
                ViewBag.Ticket = GetTableauAuthenticationTicket(tabServer: "https://tableau-test.volvocars.biz",user: "BPPEQDB1", site: "Ghent");
                if(ViewBag.Ticket == "-1")
                {
                    //auth failure
                    ViewBag.TrustedAuth = false;
                }
            }
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
        //https://onlinehelp.tableau.com/current/server/en-us/trusted_auth_testing.htm
        public ActionResult GetTicket(string tabServer = "https://tableau-test.volvocars.biz", string user = "BPPEQDB1", string site = "Ghent")
        {
            ViewBag.result = GetTableauAuthenticationTicket(tabServer, user, site);
            return View();
        }

        private static string GetTableauAuthenticationTicket(string tabServer, string user, string site)
        {
            var request = (HttpWebRequest)WebRequest.Create(tabServer + "/trusted");

            var encoding = new UTF8Encoding();
            var postData = "username=" + user;
            postData += "&target_site=" + site;
            byte[] data = encoding.GetBytes(postData);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            return new StreamReader(response.GetResponseStream()).ReadToEnd();
        }
    }
}