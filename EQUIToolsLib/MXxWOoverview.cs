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
    public partial class MXxWOoverview : MetroFramework.Forms.MetroForm
    {
        MaximoComm lMaximocomm = new MaximoComm();
        DataTable tableFromMx7 = new DataTable();
        BackgroundWorker bwLongDescription = new BackgroundWorker();
        BackgroundWorker bwWorkorders = new BackgroundWorker();
        myDebugger Debugger = new myDebugger();
        //
        string Llocation;
        bool lPartmode;

        public MXxWOoverview(bool partmode)
        {
            //
            initMXxWOoverview();
            //
            tb_location.Text = "";
            tb_location.Enabled = true;
            btn_refresh.Enabled = true;
            //
            metroProgressSpinner1.Visible = false;
            //
            lPartmode = partmode;
            //
            this.Show();
            //
        }


        public MXxWOoverview(string location, bool partmode)
        {
            //store original query location
            Llocation = location;
            //
            initMXxWOoverview();
            //
            tb_location.Text = Llocation;
            tb_location.Enabled = false;
            btn_refresh.Enabled = false;
            //
            lPartmode = partmode;
            //
            this.Show();
            bwWorkorders.RunWorkerAsync();
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
            if (lPartmode)
            {
                this.Text = string.Format("Maximo Wo browser: 'Parts used'");
            }
            else
            {
                this.Text = string.Format("Maximo Wo browser: 'Workorders'");
            }
        }

        void bwWorkorders_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //string ancestor = (from a in tableFromMx7.AsEnumerable() select a.Field<string>("ANCESTOR")).FirstOrDefault();
            //
            dataGridView1.DataSource = tableFromMx7;
            apply_filter();
            //
            metroProgressSpinner1.Hide();
        }

        void bwWorkorders_DoWork(object sender, DoWorkEventArgs e)
        {
            getMaximoWorkorder(tb_location.Text, lPartmode);
        }

        private void getMaximoWorkorder(string location, bool partmode)
        {
            //get asset list from maximo M7 daily copy
            string strSqlGetFromMaximo = string.Format(@"
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

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            webBrowser1.DocumentText = lMaximocomm.StringToHTML_Table("....","");
        }
        //
#endregion

#region event calls 
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            this.Enabled = false;
            metroProgressSpinner1.Show();
            //store original query location
            Llocation = tb_location.Text; //schould store the ancestor 
            tb_location.Text = Llocation;
            //
            getMaximoWorkorder(tb_location.Text, lPartmode);
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
                    sb.Append(string.Format("LOCATION = '{0}'", Llocation));
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
