using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class PowerBiController : Controller
    {
        // GET: PowerBi
        public ActionResult Index()
        {
            return View();
        }

        //Sample APP
        //https://app.powerbi.com/Redirect?action=OpenApp&appId=50b0c9ed-8ec4-4f6e-8e0c-b6e7c9128195&ctid=81fa766e-a349-4867-8bf4-ab35e250a08f
        //Sample render 
        //https://app.powerbi.com/reportEmbed?reportId=48a95708-6371-4546-ad10-4687dec61149&autoAuth=true

        //test with Iframe no api 
        public ActionResult EmbeddedDesktop(string RepportID = "48a95708-6371-4546-ad10-4687dec61149")
        {
            ViewBag.workbook = "testworkbook";
            ViewBag.sheet = "testsheet";
            ViewBag.RepportID = RepportID;
            return View();
        }

        //test with  api 
        https://docs.microsoft.com/en-us/power-bi/developer/embed-sample-for-customers
        public ActionResult EmbeddedDesktop2(string RepportID = "48a95708-6371-4546-ad10-4687dec61149")
        {
            ViewBag.workbook = "testworkbook";
            ViewBag.sheet = "testsheet";
            ViewBag.RepportID = RepportID;
            return View();
        }
    }
}