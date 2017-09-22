using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EQUICommunictionLib;
using HtmlDiff;

namespace EQUIToolsLib
{
    public partial class QueryDiffer : MetroFramework.Forms.MetroForm
    {
        MaximoComm lmaxCom = new MaximoComm();
        GadataComm lgadataCom = new GadataComm();

        public QueryDiffer()
        {
            InitializeComponent();
            Show();
        }

        private void btn_snapshot_Click(object sender, EventArgs e)
        {
            string Qry =
                @"
SELECT  
 WORKORDER.WONUM
,WORKORDER.LOCATION
,LOCANCESTOR.ANCESTOR LINE
,WORKORDER.STATUS ACTSTATUS
,WORKORDER.DESCRIPTION
,WORKORDER.SCHEDSTART
,ROUND(WORKORDER.ESTDUR,2) ESTDUR 
,WORKORDER.STATUSDATE LASTCHANGED
,WORKORDER.OWNERGROUP
,WORKORDER.JPNUM
,WORKORDER.ASSETNUM
,WORKORDER.ASSIGNEDOWNERGROUP
,WORKORDER.CXMATERIALBOXID
,WORKORDER.CXINFOTOPROD
,WORKORDER.CXSHUTDOWNREMARKS
,WORKORDER.CXSHUTDOWNCONDITION
,WORKORDER.WORKTYPE
,(SELECT WPLABOR.LABORCODE   || ' H:' ||ROUND(WPLABOR.LABORHRS,1) FROM MAXIMO.WPLABOR WHERE (WPLABOR.WONUM = WORKORDER.WONUM) AND (ROWNUM = 1)) PLANNED1ST
,(SELECT LABTRANS.LABORCODE || ' H:' || ROUND(LABTRANS.REGULARHRS,1) FROM MAXIMO.LABTRANS  WHERE (LABTRANS.REFWO = WORKORDER.WONUM) AND (ROWNUM = 1)) ACTUAL1ST
,ROW_NUMBER() OVER (PARTITION BY WOSTATUS.WONUM ORDER BY WOSTATUS.WOSTATUSID) WOSTATUSINST
,WOSTATUS.STATUS
FROM MAXIMO.WORKORDER 
--JOIN WORKORDER
LEFT JOIN MAXIMO.WOSTATUS ON  WORKORDER.WONUM = WOSTATUS.WONUM
AND WORKORDER.ORGID = WOSTATUS.ORGID
--JOIN LOCATION ANCESTOR TO GET WORKORDERS FROM SPECIFIC AREA
LEFT JOIN MAXIMO.LOCANCESTOR ON WORKORDER.LOCATION = LOCANCESTOR.LOCATION 
AND WORKORDER.ORGID = LOCANCESTOR.ORGID AND LOCANCESTOR.SYSTEMID = 'PRODMID'
AND LOCANCESTOR.ANCESTOR LIKE 'AAS%'
    --AND LOCANCESTOR.ANCESTOR IN ('A LIJN 331','A LIJN 334','A LIJN 336','A LIJN 337','A LIJN 338')
--GET WORKORDER STATUS THAT HAS CHANGES TO INPRG THIS WEEK
WHERE 
(( --STATUS CHANGES IN DT RANGE
WOSTATUS.CHANGEDATE BETWEEN
(SELECT (TRUNC ( TO_DATE (SUBSTR ('201738', 1, 4) || '0131', 'YYYY'|| 'MMDD'), 'IYYY') + ( 7 * ( TO_NUMBER (SUBSTR ('201738', 5)) - 1)) ) AS IW_MONDAY FROM DUAL)
AND 
(SELECT (TRUNC ( TO_DATE (SUBSTR ('201738', 1, 4) || '0131', 'YYYY'|| 'MMDD'), 'IYYY') + ( 7 * ( TO_NUMBER (SUBSTR ('201738', 5)) + 1)) ) AS IW_NEXTMONDAY FROM DUAL)
) OR ( --WO CHANGED IN DT RANGE
WORKORDER.CHANGEDATE BETWEEN
(SELECT (TRUNC ( TO_DATE (SUBSTR ('201738', 1, 4) || '0131', 'YYYY'|| 'MMDD'), 'IYYY') + ( 7 * ( TO_NUMBER (SUBSTR ('201738', 5)) - 1)) ) AS IW_MONDAY FROM DUAL)
AND 
(SELECT (TRUNC ( TO_DATE (SUBSTR ('201738', 1, 4) || '0131', 'YYYY'|| 'MMDD'), 'IYYY') + ( 7 * ( TO_NUMBER (SUBSTR ('201738', 5)) + 1)) ) AS IW_NEXTMONDAY FROM DUAL)
))
AND WOSTATUS.ORGID = 'VCCBE'
AND LOCANCESTOR.ANCESTOR IS NOT NULL 
AND WORKORDER.OWNERGROUP IN('AACOW','AAE1262W','AAENG','AAOND','AAWEO') --WVB MUST FILL IN OWNERGROUP
AND WORKORDER.PARENT IS NULL --NO CHILD WORKORDERS
AND WOSTATUS.STATUS IN ('INPRG','EXEC' ) --WVB MUST SET TO ONE OF THESE STATUSES (WILL CREATE DUPS)
";

            DataTable dt = lmaxCom.oracle_runQuery(Qry);
            string htmlResult = ConvertDataTableToHTML(dt);

            lgadataCom.InsertSnaphotGadata("Testname",Qry,htmlResult);

            webBrowser1.DocumentText = FormatHTMLTable(htmlResult);

        }

        private void btn_compare_Click(object sender, EventArgs e)
        {
            DataTable dt = lgadataCom.RunQueryGadata("SELECT TOP 1 * FROM GADATA.EQUI.QuerySnapshots order by id desc ");

            string orgHtmlResult = dt.Rows[0].Field<string>("htmlResult");
            DataTable dtNew = lmaxCom.oracle_runQuery(dt.Rows[0].Field<string>("query"));
            string newHtmlReulst = ConvertDataTableToHTML(dtNew);

            //test 
            //newHtmlReulst = newHtmlReulst.Replace("INPRG", "dingens");

            HtmlDiff.HtmlDiff diffHelper = new HtmlDiff.HtmlDiff(orgHtmlResult, newHtmlReulst);
            string diffOutput = diffHelper.Build();

            webBrowser1.DocumentText = FormatHTMLTable(diffOutput);

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
