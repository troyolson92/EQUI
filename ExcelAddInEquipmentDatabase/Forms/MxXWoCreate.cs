using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Forms;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class MxXWoCreate : MetroForm
    {
        //debugger
        Debugger Debugger = new Debugger();
        //
        MaximoComm lMaximoComm = new MaximoComm();
        //
        BackgroundWorker bw = new BackgroundWorker();
        //
        DataTable dt = new DataTable();
        //
        Forms.MXxWOdetails lMXxWOdetails;

        public MxXWoCreate()
        {
            InitializeComponent();
            //populate comboxes from mx using backgroundwork
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            cb_worktype.Enabled = false;
            cb_status.Enabled = false;
            cb_owner.Enabled = false;
            cb_ownergroup.Enabled = false;
            bw.RunWorkerAsync();
            //
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            cb_worktype.DisplayMember = "worktype";
            cb_worktype.ValueMember = "worktype";
            cb_status.DisplayMember = "status";
            cb_status.ValueMember = "status";
            cb_owner.DisplayMember = "personid";
            cb_owner.ValueMember = "personid";
            cb_ownergroup.DisplayMember = "persongroup";
            cb_ownergroup.ValueMember = "persongroup";
         cb_worktype.Enabled = true;
         cb_status.Enabled = true;
         cb_owner.Enabled = true;
         cb_ownergroup.Enabled = true;
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            cb_worktype.DataSource = lMaximoComm.oracle_runQuery("select worktype from maximo.worktype;");
            cb_status.DataSource = lMaximoComm.oracle_runQuery("select distinct status from maximo.wpeditsetting");
            cb_owner.DataSource = lMaximoComm.oracle_runQuery("select personid from maximo.person where locationsite = 'VCG'");
            cb_ownergroup.DataSource = lMaximoComm.oracle_runQuery("select persongroup from maximo.persongroup");
        }

        void getFromMx()
        {
            string cmdWO = string.Format(@"
select 
 to_number(WORKORDER.WONUM) WONUM
 ,WORKORDER.LOCATION
 ,WORKORDER.DESCRIPTION
 --
,WORKORDER.WORKTYPE
,WORKORDER.STATUS
,WORKORDER.STATUSDATE
,WORKORDER.REPORTEDBY
,WORKORDER.REPORTDATE
,WORKORDER.OWNER
,WORKORDER.OWNERGROUP
,WORKORDER.FNLCONSTRAINT
,WORKORDER.CXCBDDPROD
,WORKORDER.ESTDUR
from MAXIMO.WORKORDER WORKORDER  
where 
WORKORDER.WONUM LIKE  '{0}'
            ", tb_workorder.Text);
            //
            dt = lMaximoComm.oracle_runQuery(cmdWO);
            if (dt.Rows.Count == 0) { Debugger.Message("no result from query"); return; ; }
            //
            tb_location.Text = dt.Rows[0].Field<String>("LOCATION");
            tb_discription.Text = dt.Rows[0].Field<String>("DESCRIPTION");
            tb_locationDescription.Text = "not done ";
            cb_worktype.Text = dt.Rows[0].Field<String>("WORKTYPE");
            cb_status.Text = dt.Rows[0].Field<String>("STATUS");
            tb_statusdate.Text = dt.Rows[0].Field<DateTime>("STATUSDATE").ToString();
            //
            tb_reportedby.Text = dt.Rows[0].Field<String>("REPORTEDBY");
            tb_reportdate.Text = dt.Rows[0].Field<DateTime>("REPORTDATE").ToString();
            cb_owner.Text = dt.Rows[0].Field<String>("OWNER");
            cb_ownergroup.Text = dt.Rows[0].Field<String>("OWNERGROUP");
            //
            tb_completedby.Text = dt.Rows[0].Field<DateTime>("FNLCONSTRAINT").ToString();
            tb_duur.Text = dt.Rows[0].Field<decimal>("ESTDUR").ToString();
            if (dt.Rows[0].Field<String>("CXCBDDPROD") == "y")
            {cbx_WorkDuringproduc.Checked = true;}
            else
            {cbx_WorkDuringproduc.Checked = false;}
            cb_defectclass.Text = "not done";
            tb_defectremark.Text = "not done";
        }

        private void btn_test_Click(object sender, EventArgs e)
        {
            getFromMx();
        }

        private void btn_longdescription_Click(object sender, EventArgs e)
        {
            if (lMXxWOdetails != null)
            {
                lMXxWOdetails.Dispose();
                lMXxWOdetails = null; 
            }
            lMXxWOdetails = new Forms.MXxWOdetails(tb_workorder.Text);
            lMXxWOdetails.Show();
        }

        private void btn_defectlongdescription_Click(object sender, EventArgs e)
        {
            if (lMXxWOdetails != null)
            {
                lMXxWOdetails.Dispose();
                lMXxWOdetails = null;
            }
            lMXxWOdetails = new Forms.MXxWOdetails(tb_workorder.Text);
            lMXxWOdetails.Show();
        }


    }
}
