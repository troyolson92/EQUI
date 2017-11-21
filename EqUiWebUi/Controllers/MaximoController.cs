using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EqUiWebUi.Models;
using System.Data;

namespace EqUiWebUi.Controllers
{
    public class MaximoController : Controller
    {
        EQUICommunictionLib.MaximoComm lmaxCom = new MaximoComm();

        //
        // GET: /Maximo/

        public ActionResult Index()
        {
            return HttpNotFound();
        }

        public ActionResult WoDetails(string wonum)
        {
            var workorder = new MaximoWorkorder() { sWoNum = wonum, sWoDetails = lmaxCom.getMaximoDetails(wonum) };
            //return View(workorder);
            workorder.sWoDetails =  workorder.sWoDetails.Replace("font-size: small","font-size: xx-small");
            return Content(workorder.sWoDetails);
        }


        [HttpGet]
        public ActionResult WojqGrid()
        {
            return View();
        }

        [HttpGet]
        public ActionResult workordersOnLocation(string location)
        {
            //get data from maximo
            EQUICommunictionLib.MaximoComm maximoComm = new MaximoComm();
            DataTable dataTable = new DataTable();
            string qry = string.Format(@"
select 
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
from maximo.locancestor where locancestor.location like '{0}' 
and locancestor.ORGID = 'VCCBE' 
and locancestor.location <> locancestor.ancestor 
order by locancestor.LOCANCESTORID)
where rownum = 1
)
ORDER BY WORKORDER.STATUSDATE DESC
", location);


            dataTable = maximoComm.oracle_runQuery(qry);
            //parse datatable to listobject
            List<Workorder> workorders = new List<Workorder>();


            foreach (DataRow row in dataTable.Rows)
            {
                Workorder workorder = new Workorder();
                workorder.WONUM = row.Field<string>("WONUM");
                workorder.STATUS = row.Field<string>("STATUS");
                workorder.STATUSDATE = row.Field<DateTime>("STATUSDATE");
                workorder.WORKTYPE = row.Field<string>("WORKTYPE");
                workorder.DESCRIPTION = row.Field<string>("DESCRIPTION");
                workorder.LOCATION = row.Field<string>("LOCATION");
                workorder.REPORTEDBY = row.Field<string>("REPORTEDBY");
                workorder.REPORTDATE = row.Field<DateTime>("REPORTDATE");
                workorder.ANCESTOR = row.Field<string>("ANCESTOR");


                workorders.Add(workorder);
            }


            //parse listobject to view 
            ViewBag.Location = location;
            var data = workorders;
            return View(data);
        }


    }
}
