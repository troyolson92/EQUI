using System;
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
        Debugger Debugger = new Debugger();
        string Llocation;

        public MXxWOoverview(string location, bool partmode)
        {
            //
            if (location == null) { Debugger.Message("no location found"); return; }
            //
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            btn_refresh.Enabled = false;
            cb_preventive.Enabled = false;
            cb_ciblings.Enabled = false;
            //
            bwLongDescription.DoWork += bwLongDescription_DoWork;
            bwWorkorders.DoWork += bwWorkorders_DoWork;
            bwWorkorders.RunWorkerCompleted += bwWorkorders_RunWorkerCompleted;
            //store original query location
            Llocation = location;
            tb_location.Text = Regex.Replace(location, @"[A-Za-z\s]", "%") + "%";
            //
            lPartmode = partmode;
            if (partmode)
            {
                this.Text = string.Format("Maximo Wo browser: 'Parts used on location' <{0}>", location);
            }
            else
            {
                this.Text = string.Format("Maximo Wo browser: 'Workorders on location' <{0}>", location);
            }
            //
            this.Show();
            bwWorkorders.RunWorkerAsync();
        }

        #region backgroundworkers
        void bwWorkorders_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            dataGridView1.DataSource = tableFromMx7;
            //by default filer out worktype PP & WSCH
            apply_filter();
            //
            metroProgressSpinner1.Hide();
            btn_refresh.Enabled = true;
            cb_preventive.Enabled = true;
            cb_ciblings.Enabled = true;
        }


        void bwWorkorders_DoWork(object sender, DoWorkEventArgs e)
        {
            getMaximoWorkorder(tb_location.Text, lPartmode);
        }

        void bwLongDescription_DoWork(object sender, DoWorkEventArgs e)
        {
            webBrowser1.DocumentText = "Getting data....";
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells[0].Value != null)
                {
                    getMaximoDetails(row.Cells[0].Value.ToString());
                }
            }
        }
        #endregion

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
            if (tableFromMx7 == null) { Debugger.Message("no result from maximo"); return; };
        }

#region wo details
        //wo details
        //8936762 ref wo 

        private void getMaximoDetails(string wonum)
        {
            string cmdFAILUREREMARK = (@"
                select  NVL2(LD.LDTEXT, LD.LDTEXT, '') LDTEXT
                from MAXIMO.FAILUREREMARK FM 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = FM.FAILUREREMARKID AND LD.LDOWNERTABLE = 'FAILUREREMARK'
                where fm.wonum = '{0}'
            ");
            cmdFAILUREREMARK = string.Format(cmdFAILUREREMARK, wonum);
            //
            string cmdLONGDESCRIPTION = (@"
                select NVL2(LD.LDTEXT, LD.LDTEXT, '') LDTEXT
                from MAXIMO.WORKORDER WO 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = WO.WORKORDERID AND LD.LDOWNERTABLE = 'WORKORDER'
                where WO.wonum = '{0}'
            ");
            cmdLONGDESCRIPTION = string.Format(cmdLONGDESCRIPTION, wonum);
            //
            string cmdLabor = (@"
            select 
             LABORCODE
            ,PERSON.DISPLAYNAME
            ,CRAFT
            ,PAYRATE
            ,PERSON.SUPERVISOR
            ,LABTRANS.ENTERDATE
            ,REGULARHRS
            ,to_timestamp('12/30/1899 00:00:00', 'MM/DD/YYYY  hh24:mi:ss') + REGULARHRS / 24 Converted
            from MAXIMO.LABTRANS  LABTRANS 
            left join MAXIMO.PERSON ON PERSON.PERSONID = LABTRANS.LABORCODE
            where LABTRANS.REFWO  = '{0}'
            ");
            cmdLabor = string.Format(cmdLabor, wonum);
            //
            string cmdWorkLog = (@"
            select 
            wl.logtype
            ,wl.CREATEBY
            ,wl.CREATEDATE
            ,wl.CLIENTVIEWABLE
            ,wl.DESCRIPTION
            ,ld.LDTEXT 
            from maximo.worklog wl
            left join maximo.longdescription ld  on 
            ld.ldownertable = 'WORKLOG'  
            AND  ld.ldownercol = 'DESCRIPTION'
            AND  ld.LDKEY = wl.WORKLOGID
            where
            wl.RECORDKEY = '{0}'
            ");
            cmdWorkLog = string.Format(cmdWorkLog, wonum);
            //
            StringBuilder sb = new StringBuilder();
            string newline = "<p></p>";
            sb.AppendLine(StringToHTML_Table("LONGDESCRIPTION", lMaximocomm.GetClobMaximo7(cmdLONGDESCRIPTION))).AppendLine(newline);
            sb.AppendLine(StringToHTML_Table("FAILUREREMARK", lMaximocomm.GetClobMaximo7(cmdFAILUREREMARK))).AppendLine(newline);
            sb.AppendLine(DtToHTML_Table(lMaximocomm.oracle_runQuery(cmdLabor))).AppendLine(newline);
            sb.AppendLine(DtToHTML_Table(lMaximocomm.oracle_runQuery(cmdWorkLog))).AppendLine(newline);

            DataTable dt = lMaximocomm.oracle_runQuery(cmdWorkLog);

            //
            webBrowser1.DocumentText = sb.ToString();
        }

        public static string DtToHTML_Table(DataTable dt)
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
            builder.Append("<table border='3px' cellpadding='5' cellspacing='0' ");
            builder.Append("style='border: solid 2px Silver; font-size: x-small;'>");
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

        public static string StringToHTML_Table(string header, string input)
        {

            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add(header);
            DataRow rw = dt.NewRow();
            if (input == null || input == "")
            {
                rw[header] = "null (no data)";
            }
            else
            {
                rw[header] = input;
            }
            dt.Rows.Add(rw);
            return DtToHTML_Table(dt);
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (bwLongDescription.IsBusy) { return; }
            bwLongDescription.RunWorkerAsync();
        }
        //
#endregion

#region event calls 
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            btn_refresh.Enabled = false;
            cb_preventive.Enabled = false;
            cb_ciblings.Enabled = false;
            metroProgressSpinner1.Show();
            //store original query location
            Llocation = tb_location.Text;
            tb_location.Text = Regex.Replace(Llocation, @"[A-Za-z\s]", "%") + "%";
            //
            getMaximoWorkorder(tb_location.Text, lPartmode);
            dataGridView1.DataSource = tableFromMx7;
            //
            cb_ciblings.Checked = false;
            cb_preventive.Checked = false;
            apply_filter();
            //
            metroProgressSpinner1.Hide();
            btn_refresh.Enabled = true;
            cb_preventive.Enabled = true;
            cb_ciblings.Enabled = true;
        }

        private void apply_filter()
        {
            if (cb_ciblings.Checked)
            {
                if (cb_preventive.Checked)
                {
                    tableFromMx7.DefaultView.RowFilter = ""; //all assets with preventive
                }
                else
                {
                    tableFromMx7.DefaultView.RowFilter = "WORKTYPE not in('PP','PCI','WSCH')"; //all assets no preventive
                }
                tb_location.Text = Regex.Replace(Llocation, @"[A-Za-z\s]", "%") + "%";
            }
            else
            {
                if (cb_preventive.Checked)
                {
                    tableFromMx7.DefaultView.RowFilter = string.Format("LOCATION = '{0}'", Llocation); //only target asset with preventive
                }
                else
                {
                    tableFromMx7.DefaultView.RowFilter = string.Format("LOCATION = '{0}' AND WORKTYPE not in('PP','PCI','WSCH') ", Llocation); //only target asset no preventive
                }
                tb_location.Text = Llocation;
            }
        }

        private void cb_ciblings_CheckedChanged(object sender, EventArgs e)
        {
            apply_filter();
        }

        private void cb_preventive_CheckedChanged(object sender, EventArgs e)
        {
            apply_filter();

        }
 #endregion
    }
}
