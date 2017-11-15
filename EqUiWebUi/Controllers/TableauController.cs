using EqUiWebUi.Models;
using System;
using System.Collections.Generic;
using System.Data;
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

        [HttpGet]
        public ActionResult LongTerm_Layout_STO_NGAC()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LongTerm_Treemap_STO_NGAC()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LongTerm_Layout_STW040_MAXIMO()
        {
            return View();
        }


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