using EQUICommunictionLib;
using EqUiWebUi.Models;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace EqUiWebUi.Controllers
{
    public class DifftableController : Controller
    {
        // GET: Difftable
        public ActionResult Index()
        {
            GadataComm gadataComm = new GadataComm();
            DataTable dt = gadataComm.RunQueryGadata(
                    @"SELECT TOP(1000)
                           [id]
                          ,[name]
                          ,[timestamp]
                          ,[query]
                          ,[htmlresult]
                      FROM[GADATA].[EqUi].[querySnapshots]");
            //
            List<DiffQuery> diffQueryNames = new List<DiffQuery>();
            foreach (DataRow row in dt.Rows)
            {
                DiffQuery diffQuery = new DiffQuery
                {
                    id = row.Field<int>("id"),
                    name = string.Format("<{0}> <{1}> id:{2}", row.Field<string>("name"), "ts", row.Field<int>("id"))
                };
                diffQueryNames.Add(diffQuery);
            }

            ListDiffQuerysViewModel viewModel = new ListDiffQuerysViewModel();
            viewModel.DiffQuerys = diffQueryNames;

            return View(viewModel);
        }


        [HttpGet]
        public ActionResult DiffWebgrid()
        {
            GadataComm gadataComm = new GadataComm();
            DataTable dt = gadataComm.RunQueryGadata(
                      @"SELECT [id]
                              ,[name]
                              ,[timestamp]
                              ,[query]
                              ,[htmlresult]
                          FROM[GADATA].[EqUi].[querySnapshots]");
            //
            HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(dt.Rows[11].Field<string>("htmlresult"), dt.Rows[dt.Rows.Count-1].Field<string>("htmlresult"));
            diffHelper.AddBlockExpression(new Regex(@"[0-9]:[0-9]{1,2}:[0-9]{1,2}", RegexOptions.IgnoreCase)); //time
            diffHelper.AddBlockExpression(new Regex(@"[0-9]/[0-9]{1,2}/20[0-9]{1,2}", RegexOptions.IgnoreCase)); //date
            string diffOutput = diffHelper.Build();
            //
            ViewBag.myData = diffOutput;
            return View();
        }

        [HttpGet]
        public ActionResult RunDiffQUery()
        {
            #region qry
            string Qry =string.Format(
    @"
SELECT
 NVL(WORKORDER.PARENT,WORKORDER.WONUM) HOOFDWONUM
,CASE NVL(WORKORDER.PARENT,'x')
   WHEN 'x' THEN NULL
   ELSE WORKORDER.WONUM END SUBWONUM
,WORKORDER.LOCATION
,WORKORDER.STATUS ACTSTATUS
,WORKORDER.DESCRIPTION
,WORKORDER.SCHEDSTART
,WORKORDER.STATUSDATE LASTCHANGED
,WORKORDER.WORKTYPE

,NVL(WORKORDER.CXINFOTOPROD,' ') 
 || NVL(WORKORDER.CXSHUTDOWNREMARKS,' ') 
 || NVL(WORKORDER.CXSHUTDOWNCONDITION,' ') Afstart

,'PLANNED1ST: ' || NVL((SELECT WPLABOR.LABORCODE   || ' H:' ||ROUND(WPLABOR.LABORHRS,1) FROM MAXIMO.WPLABOR WHERE (WPLABOR.WONUM = WORKORDER.WONUM) AND (ROWNUM = 1)),'') || '/ '||
 'ACTUAL1ST: ' || NVL((SELECT LABTRANS.LABORCODE || ' H:' || ROUND(LABTRANS.REGULARHRS,1) FROM MAXIMO.LABTRANS  WHERE (LABTRANS.REFWO = WORKORDER.WONUM) AND (ROWNUM = 1)),'/ ') Work
 
,WORKORDER.OWNERGROUP
,WORKORDER.CXMATERIALBOXID MatBox
,WORKORDER.ASSETNUM
,ROUND(WORKORDER.ESTDUR,2) ESTDUR 
,WORKORDER.JPNUM
,WORKORDER.ASSIGNEDOWNERGROUP

,ROW_NUMBER() OVER (PARTITION BY WOSTATUS.WONUM ORDER BY WOSTATUS.WOSTATUSID) WOSTATUSINST

FROM MAXIMO.WORKORDER 
--JOIN WORKORDER
LEFT JOIN MAXIMO.WOSTATUS ON  WORKORDER.WONUM = WOSTATUS.WONUM
AND WORKORDER.ORGID = WOSTATUS.ORGID
--JOIN LOCATION ANCESTOR TO GET WORKORDERS FROM SPECIFIC AREA
LEFT JOIN MAXIMO.LOCANCESTOR ON WORKORDER.LOCATION = LOCANCESTOR.LOCATION 
AND WORKORDER.ORGID = LOCANCESTOR.ORGID AND LOCANCESTOR.SYSTEMID = 'PRODMID'
AND LOCANCESTOR.ANCESTOR LIKE '{1}'
    --AND LOCANCESTOR.ANCESTOR IN ('A LIJN 331','A LIJN 334','A LIJN 336','A LIJN 337','A LIJN 338')
--GET WORKORDER STATUS THAT HAS CHANGES TO INPRG THIS WEEK
WHERE 
(( --STATUS CHANGES IN DT RANGE
WOSTATUS.CHANGEDATE BETWEEN
(SELECT (TRUNC ( TO_DATE (SUBSTR ('{0}', 1, 4) || '0131', 'YYYY'|| 'MMDD'), 'IYYY') + ( 7 * ( TO_NUMBER (SUBSTR ('{0}', 5)) - 1)) ) AS IW_MONDAY FROM DUAL)
AND 
(SELECT (TRUNC ( TO_DATE (SUBSTR ('{0}', 1, 4) || '0131', 'YYYY'|| 'MMDD'), 'IYYY') + ( 7 * ( TO_NUMBER (SUBSTR ('{0}', 5)) + 1)) ) AS IW_NEXTMONDAY FROM DUAL)
) OR ( --WO CHANGED IN DT RANGE
WORKORDER.CHANGEDATE BETWEEN
(SELECT (TRUNC ( TO_DATE (SUBSTR ('{0}', 1, 4) || '0131', 'YYYY'|| 'MMDD'), 'IYYY') + ( 7 * ( TO_NUMBER (SUBSTR ('{0}', 5)) - 1)) ) AS IW_MONDAY FROM DUAL)
AND 
(SELECT (TRUNC ( TO_DATE (SUBSTR ('{0}', 1, 4) || '0131', 'YYYY'|| 'MMDD'), 'IYYY') + ( 7 * ( TO_NUMBER (SUBSTR ('{0}', 5)) + 1)) ) AS IW_NEXTMONDAY FROM DUAL)
))
AND WOSTATUS.ORGID = 'VCCBE'
AND LOCANCESTOR.ANCESTOR IS NOT NULL 
AND WORKORDER.OWNERGROUP IN('AACOW','AAE1262W','AAENG','AAOND','AAWEO') --WVB MUST FILL IN OWNERGROUP
--AND WORKORDER.PARENT IS NULL --NO CHILD WORKORDERS
AND WOSTATUS.STATUS IN ('INPRG','EXEC' ) --WVB MUST SET TO ONE OF THESE STATUSES (WILL CREATE DUPS)
", "201738", "AAS%");
            #endregion
            GadataComm gadataComm = new GadataComm();
            MaximoComm maximoComm = new MaximoComm();

            DataTable dt = maximoComm.oracle_runQuery(Qry);
            string htmlResult = ConvertDataTableToHTML(dt);

            gadataComm.InsertSnaphotGadata("Testname", Qry, htmlResult);

            ViewBag.myData = FormatHTMLTable(htmlResult);

            return View();
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
                            table {
                                border-collapse: separate;
                                width: 100%;
                                border: 2px solid black;
                                table-layout: auto;
                                white-space:nowrap;}
                             tr, td { width: 100%;
                                    border: 1px solid black
}
                            table > thead > tr > th { width: auto; }
                            </style>");
            builder.Append("<body>");
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
    }
}