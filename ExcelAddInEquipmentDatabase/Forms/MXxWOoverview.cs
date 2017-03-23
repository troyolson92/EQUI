﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class MXxWOoverview : MetroFramework.Forms.MetroForm
    {
        MaximoComm lMaximocomm = new MaximoComm();
        bool lPartmode;
        DataTable tableFromMx7 = new DataTable();
        BackgroundWorker bwLongDescription = new BackgroundWorker();
        BackgroundWorker bwWorkorders = new BackgroundWorker();
        string Llocation;

        public MXxWOoverview(string location, bool partmode)
        {
            InitializeComponent();
            bwLongDescription.DoWork += bwLongDescription_DoWork;
            bwWorkorders.DoWork += bwWorkorders_DoWork;
            bwWorkorders.RunWorkerCompleted += bwWorkorders_RunWorkerCompleted;
            //store original query location
            Llocation = location;
            tb_location.Text = location;
            //
            lPartmode = partmode;
            if (partmode)
            {
                this.Text = string.Format("Maximo Wo browser: 'Parts on location' <{0}>", location);
            }
            else
            {
                this.Text = string.Format("Maximo Wo browser: 'Workorder on location' <{0}>", location);
            }

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            this.Show();
            btn_refresh.Enabled = false;
            bwWorkorders.RunWorkerAsync();
            dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
        }

        void bwWorkorders_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.DataSource = tableFromMx7;
            metroProgressSpinner1.Hide();
            btn_refresh.Enabled = true;
        }

        void bwWorkorders_DoWork(object sender, DoWorkEventArgs e)
        {
            getMaximoWorkorder(tb_location.Text, lPartmode);
        }

        void bwLongDescription_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                getMaximoDetails(row.Cells[0].Value.ToString());
            }
        }

        private void getMaximoWorkorder(string location, bool partmode)
        {
            //get asset list from maximo M7 daily copy
            string strSqlGetFromMaximo = string.Format(@"
SELECT 
 TO_NUMBER(WORKORDER.WONUM) WONUM
,WORKORDER.STATUS
,WORKORDER.STATUSDATE
,WORKORDER.WORKTYPE
,WORKORDER.DESCRIPTION
,WORKORDER.LOCATION
,WORKORDER.REPORTEDBY
,WORKORDER.REPORTDATE
FROM MAXIMO.WORKORDER WORKORDER  
WHERE WORKORDER.LOCATION LIKE '{0}'
ORDER BY WORKORDER.STATUSDATE DESC
            ",location);

            string strSqlGetPartsMaximo = string.Format(@"

SELECT 
 WORKORDER.WONUM
,WORKORDER.REPORTDATE
,WORKORDER.WORKTYPE
,WORKORDER.DESCRIPTION
,WPITEM.ITEMNUM
,WPITEM.DESCRIPTION PARTDESCRIPTION
,WORKORDER.LOCATION
,WPITEM.REQUESTBY
FROM MAXIMO.WORKORDER WORKORDER  
JOIN MAXIMO.WPITEM WPITEM ON WPITEM.WONUM = WORKORDER.WONUM
WHERE WORKORDER.LOCATION LIKE '{0}'
ORDER BY WORKORDER.STATUSDATE
            ", location);

            if (partmode)
            {
                tableFromMx7 = lMaximocomm.oracle_runQuery(strSqlGetPartsMaximo);
            }
            else
            {
                tableFromMx7 = lMaximocomm.oracle_runQuery(strSqlGetFromMaximo);
            }
        }

        private void getMaximoDetails(string wonum)
        {
            string cmdFAILUREREMARK = (@"
                select  NVL2(LD.LDTEXT, LD.LDTEXT, '') LDTEXT
                from MAXIMO.FAILUREREMARK FM 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = FM.FAILUREREMARKID AND LD.LDOWNERTABLE = 'FAILUREREMARK'
                where fm.wonum = '{0}'
            ");
            string cmdLONGDESCRIPTION = (@"
                select NVL2(LD.LDTEXT, LD.LDTEXT, '') LDTEXT
                from MAXIMO.WORKORDER WO 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = WO.WORKORDERID AND LD.LDOWNERTABLE = 'WORKORDER'
                where WO.wonum = '{0}'
            ");
            string cmdLabor = (@"
            select 
             LABORCODE
            ,CRAFT
            ,PAYRATE
            ,REGULARHRS
            from MAXIMO.LABTRANS  LABTRANS 
            where LABTRANS.REFWO  = '{0}'
            ");

            cmdFAILUREREMARK = string.Format(cmdFAILUREREMARK, wonum);
            cmdLONGDESCRIPTION = string.Format(cmdLONGDESCRIPTION, wonum);
            cmdLabor = string.Format(cmdLabor, wonum);

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(
@"<div>            
<table bgcolor=#00FF00>
<tr>
<td style = white-space:PRE>LONGDESCRIPTION</td>
</tr>
</table>
</div>");
            sb.AppendLine(lMaximocomm.GetClobMaximo7(cmdLONGDESCRIPTION));
            sb.AppendLine("<div>---------------------------------------------------------------</div>").AppendLine(
@"<div>            
<table bgcolor=#00FF00>
<tr>
<td style = white-space:PRE>FAILUREREMARK</td>
</tr>
</table>
</div>");
            sb.AppendLine(lMaximocomm.GetClobMaximo7(cmdFAILUREREMARK));
            sb.AppendLine("<div>---------------------------------------------------------------</div>").AppendLine(
@"<div>            
<table bgcolor=#00FF00>
<tr>
<td style = white-space:PRE>LABTRANS</td>
</tr>
</table>
</div>");
            sb.AppendLine(toHTML_Table(lMaximocomm.oracle_runQuery(cmdLabor)));
            webBrowser1.DocumentText = sb.ToString();
        }

        public static string toHTML_Table(DataTable dt)
        {
            if (dt.Rows.Count == 0) return ""; // enter code here

            StringBuilder builder = new StringBuilder();
            builder.Append("<html>");
            builder.Append("<head>");
            builder.Append("<title>");
            builder.Append("Page-");
            builder.Append(Guid.NewGuid());
            builder.Append("</title>");
            builder.Append("</head>");
            builder.Append("<body>");
            builder.Append("<table border='1px' cellpadding='5' cellspacing='0' ");
            builder.Append("style='border: solid 1px Silver; font-size: x-small;'>");
            builder.Append("<tr align='left' valign='top'>");
            foreach (DataColumn c in dt.Columns)
            {
                builder.Append("<td align='left' valign='top'><b>");
                builder.Append(c.ColumnName);
                builder.Append("</b></td>");
            }
            builder.Append("</tr>");
            foreach (DataRow r in dt.Rows)
            {
                builder.Append("<tr align='left' valign='top'>");
                foreach (DataColumn c in dt.Columns)
                {
                    builder.Append("<td align='left' valign='top'>");
                    builder.Append(r[c.ColumnName]);
                    builder.Append("</td>");
                }
                builder.Append("</tr>");
            }
            builder.Append("</table>");
            builder.Append("</body>");
            builder.Append("</html>");
            return builder.ToString();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (bwLongDescription.IsBusy) { return; }
            bwLongDescription.RunWorkerAsync();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        { // if i run it in the bw is this out on cross threath 
            refresh();
        }
        
        private void refresh()
        {
            btn_refresh.Enabled = false;
            metroProgressSpinner1.Show();
            getMaximoWorkorder(tb_location.Text, lPartmode);
            dataGridView1.DataSource = tableFromMx7;
            metroProgressSpinner1.Hide();
            btn_refresh.Enabled = true;
        }

        private void cb_ciblings_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_ciblings.Checked)
            {
                tb_location.Text = Regex.Replace(tb_location.Text, @"[A-Za-z\s]", "%") + "%";
            }
            else
            {
                tb_location.Text = Llocation;
            }
            refresh();
        }

        private void cb_preventive_CheckedChanged(object sender, EventArgs e)
        {
            if (cb_preventive.Checked)
            {
   
            }
            else
            {

            }

        }
    }
}
