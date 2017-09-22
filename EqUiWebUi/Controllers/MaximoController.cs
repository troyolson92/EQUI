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
using System.Text;

namespace EqUiWebUi.Controllers
{
    public class MaximoController : Controller
    {
        EQUICommunictionLib.MaximoComm lmaxCom = new MaximoComm();
        EQUICommunictionLib.GadataComm lgadataCom = new GadataComm();

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

        public ActionResult WoDiff(string wonum)
        {
            DataTable dt = lgadataCom.RunQueryGadata("SELECT TOP 1 * FROM GADATA.EQUI.QuerySnapshots order by id desc ");

            string orgHtmlResult = dt.Rows[0].Field<string>("htmlResult");
            DataTable dtNew = lmaxCom.oracle_runQuery(dt.Rows[0].Field<string>("query"));
            string newHtmlReulst = ConvertDataTableToHTML(dtNew);

            //test 
            newHtmlReulst = newHtmlReulst.Replace("INPRG", "dingens");

            HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(orgHtmlResult, newHtmlReulst);
            string diffOutput = diffHelper.Build();

            return Content(FormatHTMLTable(diffOutput));
        }

        public static string FormatHTMLTable(string input)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("<html>");
            builder.Append(@"<style>
                            ins { 
                                background-color: #cfc; 
                                text-decoration: none; } 
                            del { 
                                color: #999; 
                                background-color:#FEC8C8; } 
                            </style>");
            builder.Append("<body>");
            //builder.Append("<table border='3px' cellpadding='5' cellspacing='0' ");
            builder.Append(input);
            builder.Append("</body>");
            builder.Append("</html>");
            return builder.ToString();
        }

        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td>" + dt.Columns[i].ColumnName + "</td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
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
            var workorders = lmaxCom.oracle_runQuery(qry);
            
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
