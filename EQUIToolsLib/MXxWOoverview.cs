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
using EQUICommunictionLib;

namespace EQUIToolsLib
{
    public class MxxOverviewMode
    {
        string sDataGridQuery;
        string sDiscription;
        string sLocation;

        public string DataGridQuery 
        {
            get { return sDataGridQuery; }
            set { sDataGridQuery = value; }    
        }

        public string Discription
        {
            get { return sDiscription; }
            set { sDiscription = value; }
        }

        public string Location
        {
            get { return sLocation; }
            set { sLocation = value; }
        }
    }

    public partial class MXxWOoverview : MetroFramework.Forms.MetroForm
    {
        MaximoComm lMaximocomm = new MaximoComm();
        DataTable tableFromMx7 = new DataTable();
        BackgroundWorker bwLongDescription = new BackgroundWorker();
        BackgroundWorker bwWorkorders = new BackgroundWorker();
        myDebugger Debugger = new myDebugger();
        //
        MxxOverviewMode viewmode = new MxxOverviewMode();
        //

        //constructor only mode
        public MXxWOoverview(string mode)
        {
            //getviewmode
            viewmode = DefaultViewmodes(mode);
            //
            initMXxWOoverview();
            //
            tb_location.Text = "";
            tb_location.Enabled = true;
            btn_refresh.Enabled = true;
            //
            metroProgressSpinner1.Visible = false;
            //
            this.Show();
            //
        }

        //constructor with location and mode
        public MXxWOoverview(string location, string mode)
        {
            //getviewmode
            viewmode = DefaultViewmodes(mode);
            //store original query location
            viewmode.Location = location;
            //
            initMXxWOoverview();
            //
            tb_location.Text = viewmode.Location;
            tb_location.Enabled = false;
            btn_refresh.Enabled = false;
            //
            this.Show();
            bwWorkorders.RunWorkerAsync();
        }

        //containts al standard viewmodes
        MxxOverviewMode DefaultViewmodes(string mode)
        {
            MxxOverviewMode lvm = new MxxOverviewMode();
            //
            switch (mode)
            {
//view mode parts on location ****************************************************************
                case "vm_PartsOnLocation":
                    lvm.Discription = "Maximo Wo browser: 'Parts used'";
                    lvm.DataGridQuery = @"
SELECT 
 WORKORDER.WONUM
,WORKORDER.REPORTDATE
,WORKORDER.WORKTYPE
,WORKORDER.DESCRIPTION
,WPITEM.ITEMNUM
,WPITEM.DESCRIPTION PARTDESCRIPTION
,WORKORDER.LOCATION
,WPITEM.REQUESTBY
,locancestor.ANCESTOR
FROM MAXIMO.WORKORDER WORKORDER  
JOIN MAXIMO.WPITEM WPITEM ON WPITEM.WONUM = WORKORDER.WONUM
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
ORDER BY WORKORDER.STATUSDATE";
                    break;
//view workorders on location ****************************************************************
                case "vm_workordersOnLocation":
            lvm.Discription = "Maximo Wo browser: 'Workorders'";
            lvm.DataGridQuery = @"
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
            ";
            break;
 //view Qtaakplan ****************************************************************
                case "vm_QualityOnLocation":
            lbl_default:
            lvm.Discription = "Maximo Wo browser: 'Quality'";
            lvm.DataGridQuery = @"
select 
 WORKORDER.WONUM WONUM
,WORKORDER.STATUS
,WORKORDER.STATUSDATE
,WORKORDER.WORKTYPE
,WORKORDER.DESCRIPTION
,WORKORDER.LOCATION
,WORKORDER.REPORTEDBY
,WORKORDER.REPORTDATE
,WORKORDER.JPNUM 
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
WHERE 
WORKORDER.JPNUM = 'PQAGEO'

ORDER BY WORKORDER.STATUSDATE DESC
            ";
            break;

            default:
                goto lbl_default;

            }
            //
            return lvm;
            //
        }

        void initMXxWOoverview()
        {
            InitializeComponent();
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            //
            bwLongDescription.DoWork += bwLongDescription_DoWork;
            bwWorkorders.DoWork += bwWorkorders_DoWork;
            bwWorkorders.RunWorkerCompleted += bwWorkorders_RunWorkerCompleted;
            //
            this.Text = viewmode.Discription;
        }

        void bwWorkorders_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //string ancestor = (from a in tableFromMx7.AsEnumerable() select a.Field<string>("ANCESTOR")).FirstOrDefault();
            //
            dataGridView1.DataSource = tableFromMx7;
            apply_filter();
            //
            //if no rows are visible because of the active filter
            if (dataGridView1.DisplayedRowCount(true) == 0)
            {
                cb_ciblings.Checked = true;
                apply_filter();
                               if (dataGridView1.DisplayedRowCount(true) == 0)
                               {
                                   cb_preventive.Checked = true;
                                   apply_filter();
                               }
            }
            //
            metroProgressSpinner1.Hide();
        }

        void bwWorkorders_DoWork(object sender, DoWorkEventArgs e)
        {
            tableFromMx7 =  lMaximocomm.oracle_runQuery(string.Format(viewmode.DataGridQuery, viewmode.Location));
            if (tableFromMx7.Rows.Count == 0) { Debugger.Message("no result from maximo"); return; };
        }


#region wo details
        //wo details
        //8936762 ref wo 
        void bwLongDescription_DoWork(object sender, DoWorkEventArgs e)
        {
            webBrowser1.DocumentText = lMaximocomm.StringToHTML_Table("Getting data....", "");
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells[0].Value != null)
                {
                    webBrowser1.DocumentText = lMaximocomm.getMaximoDetails(row.Cells[0].Value.ToString());
                }
            }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (!bwLongDescription.IsBusy)
            {
                bwLongDescription.RunWorkerAsync();
            }
        }

        //
#endregion

#region event calls 
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            metroProgressSpinner1.Show();
            //store original query location
            viewmode.Location = tb_location.Text; //schould store the ancestor 
            tb_location.Text = viewmode.Location;
            //
            tableFromMx7 = lMaximocomm.oracle_runQuery(string.Format(viewmode.DataGridQuery, viewmode.Location));
            if (tableFromMx7.Rows.Count == 0) { Debugger.Message("no result from maximo"); return; };
            dataGridView1.DataSource = tableFromMx7;
            //
            apply_filter();
            //
            metroProgressSpinner1.Hide();
            this.Enabled = true;
            //
        }

        private void apply_filter()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            //set up filter for worktype
            if (tableFromMx7.Columns.Contains("WORKTYPE"))
            {
                if (!cb_preventive.Checked)
                {
                    sb.Append("WORKTYPE not in('PP','PCI','WSCH')");
                }
                cb_preventive.Enabled = true;
            }
            else
            {
                cb_preventive.Enabled = false;
            }

            //set up filter for ancestor 
            if (tableFromMx7.Columns.Contains("LOCATION"))
            {
                if (!cb_ciblings.Checked)
                {
                    if(sb.Length > 0){ sb.Append(" AND ");}
                    sb.Append(string.Format("LOCATION = '{0}'", viewmode.Location));
                }
                cb_ciblings.Enabled = true;
            }
            else
            {
                cb_ciblings.Enabled = false;
            }
            //
            tableFromMx7.DefaultView.RowFilter = sb.ToString();
            //
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
