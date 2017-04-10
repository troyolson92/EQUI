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
                    webBrowser1.DocumentText = lMaximocomm.getMaximoDetails(row.Cells[0].Value.ToString());
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
