using EQUICommunictionLib;
using EqUiWebUi.Models;
using EqUiWebUi.WebGridHelpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
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
        public ActionResult logDetails(string Location, string Subgroup, string minTimestamp, string maxTimestamp)
        {

            DateTime startDate =  DateTime.ParseExact(minTimestamp, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture).AddSeconds(-1);
            DateTime endDate = DateTime.ParseExact(maxTimestamp, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture).AddSeconds(1); 

            GADATAEntities gADATAEntities = new GADATAEntities();
            List<logDetails> data = (from logDetails in gADATAEntities.logDetails
                                     where logDetails.location.Contains(Location)
                                     && logDetails.Subgroup.Contains(Subgroup)
                                     && logDetails.timestamp > startDate
                                     && logDetails.timestamp < endDate
                                  
                                     select logDetails).ToList();
            return View(data);
        }

        //http://equi/Tableau/logDetails?location=359010CA01&Subgroup=Undefined*&minTimestamp=2017-10-13 09:20:57.730&maxTimestamp=2017-10-25 17:26:15.023
    }
}