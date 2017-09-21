using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EqUiWebUi.Models;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using HtmlDiff;

namespace EqUiWebUi.Controllers
{
    public class MaximoController : Controller
    {
        EQUICommunictionLib.MaximoComm lMxCom = new EQUICommunictionLib.MaximoComm();

        //
        // GET: /Maximo/

        public ActionResult Index()
        {
            return HttpNotFound();
        }

        public ActionResult WoDetails(string wonum)
        {
            var workorder = new MaximoWorkorder() { sWoNum = wonum, sWoDetails = lMxCom.getMaximoDetails(wonum) };
            //return View(workorder);
            workorder.sWoDetails =  workorder.sWoDetails.Replace("font-size: small","font-size: xx-small");
            return Content(workorder.sWoDetails);
        }

        public ActionResult WoDiff(string wonum)
        {
            //7437793
            var workorder = new MaximoWorkorder() { sWoNum = wonum, sWoDetails = lMxCom.getMaximoDetails(wonum) };

            string oldText = workorder.sWoDetails.Replace("font-size: small", "font-size: xx-small");
            string newText = oldText.Replace("6","5").Replace("OK", "OK en nog wa meer zever");

            //change rendering
            /*         
ins { 
    background-color: #cfc; 
    text-decoration: none;  
}  
del { 

    color: #999; 
    background-color:#FEC8C8;  
} 
*/
            HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(oldText, newText);
           
            string diffOutput = diffHelper.Build();


            workorder.sWoDetails = diffOutput;
            return Content(workorder.sWoDetails);
        }

        [HttpGet]
        public ActionResult WojqGrid()
        {
            return View();
        }

        public ActionResult GetWorkorders(string sidx, string sord, int page, int rows)
        {
            string qry =
 @"select 
 WORKORDER.WONUM WONUM
,WORKORDER.STATUS
,WORKORDER.STATUSDATE
,WORKORDER.WORKTYPE
,WORKORDER.DESCRIPTION
,WORKORDER.LOCATION
,WORKORDER.REPORTEDBY
,WORKORDER.REPORTDATE
,locancestor.ANCESTOR
from MAXIMO.WORKORDER WORKORDER
join MAXIMO.locancestor locancestor on 
locancestor.LOCATION = WORKORDER.LOCATION
and 
locancestor.ORGID = 'VCCBE'
and 
locancestor.ANCESTOR = 
(
select ancestor from (select locancestor.ancestor 
from maximo.locancestor where locancestor.location like '53100%' 
and locancestor.ORGID = 'VCCBE' 
and locancestor.location <> locancestor.ancestor 
order by locancestor.LOCANCESTORID)
where rownum = 1
)
ORDER BY WORKORDER.STATUSDATE DESC";

            //this wil get everything each time and not limit to page
            var workorders = lMxCom.oracle_runQuery(qry);
            
            int pageIndex = Convert.ToInt32(page) - 1;
            int pageSize = rows;
            int totalRecords = workorders.Rows.Count;
            int totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);

            var data =   workorders.AsEnumerable()
                         .OrderBy(x => x.Field<string>("WONUM"))
                         .Skip(pageSize * (page - 1))
                         .Take(pageSize).ToList();

            var objType = JArray.FromObject(data, JsonSerializer.CreateDefault(new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })).FirstOrDefault(); // Get the first row            
            var js = objType.ToString(); 

            var jsonData = new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = js
            };

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


    }
}
