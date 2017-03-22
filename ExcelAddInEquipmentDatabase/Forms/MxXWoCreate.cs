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
        DataTable dtWO = new DataTable();
        DataTable dtLabor = new DataTable();
        //
        Forms.MXxWOdetails lMXxWOdetails;

        public MxXWoCreate(string location, string logtype, string logtekst,int downtime, int refId)
        {
            initform();
            //qyr db 


            //setup the basic WO
            tb_workorder.Text = null;
            tb_location.Text = location;
            tb_discription.Text = "ATW: " + logtekst;
            tb_locationDescription.Text = "not done ";
            cb_worktype.Items.Insert(0, "CI");
            cb_worktype.SelectedIndex = 0;
            cb_status.Items.Insert(0, "COMP");
            cb_status.SelectedIndex = 0;
            tb_statusdate.Text = null;
            //
            tb_reportedby.Text = Environment.UserName;
            tb_reportdate.Text = DateTime.Now.ToString();
            cb_owner.Items.Insert(0, Environment.UserName);
            cb_owner.SelectedIndex = 0;
            cb_ownergroup.Items.Insert(0, "AAOSR");
            cb_ownergroup.SelectedIndex = 0;
            //
            tb_completedby.Text = DateTime.Now.ToString();
            if (downtime > 0)
            {
                tb_duur.Text = downtime.ToString();
            }
            else
            {
                tb_duur.Text = "0:15";
            }
            //
            cbx_WorkDuringproduc.Checked = false; 
            cb_defectclass.Items.Insert(0, "Robots");
            cb_defectclass.SelectedIndex = 0;
            tb_defectremark.Text = null;
        }

        public MxXWoCreate()
        {
            initform();
            //
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            bw.RunWorkerAsync();
        }

        void initform()
        {
            InitializeComponent();
          /*  cb_worktype.Enabled = false;
            cb_status.Enabled = false;
            cb_owner.Enabled = false;
            cb_ownergroup.Enabled = false;*/
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
            dtWO = lMaximoComm.oracle_runQuery(cmdWO);
            if (dtWO.Rows.Count == 0) { Debugger.Message("no result from query"); return; ; }
            //
            tb_location.Text = dtWO.Rows[0].Field<String>("LOCATION");
            tb_discription.Text = dtWO.Rows[0].Field<String>("DESCRIPTION");
            tb_locationDescription.Text = "not done ";
            cb_worktype.Text = dtWO.Rows[0].Field<String>("WORKTYPE");
            cb_status.Text = dtWO.Rows[0].Field<String>("STATUS");
            tb_statusdate.Text = dtWO.Rows[0].Field<DateTime>("STATUSDATE").ToString();
            //
            tb_reportedby.Text = dtWO.Rows[0].Field<String>("REPORTEDBY");
            tb_reportdate.Text = dtWO.Rows[0].Field<DateTime>("REPORTDATE").ToString();
            cb_owner.Text = dtWO.Rows[0].Field<String>("OWNER");
            cb_ownergroup.Text = dtWO.Rows[0].Field<String>("OWNERGROUP");
            //
            tb_completedby.Text = dtWO.Rows[0].Field<DateTime>("FNLCONSTRAINT").ToString();
            tb_duur.Text = dtWO.Rows[0].Field<decimal>("ESTDUR").ToString();
            if (dtWO.Rows[0].Field<String>("CXCBDDPROD") == "y")
            {cbx_WorkDuringproduc.Checked = true;}
            else
            {cbx_WorkDuringproduc.Checked = false;}
            cb_defectclass.Text = "not done";
            tb_defectremark.Text = "not done";
            //
            string cmdLabor = string.Format(@"
select 
 LABORCODE
,CRAFT
,PAYRATE
,REGULARHRS
from MAXIMO.LABTRANS  LABTRANS 
where LABTRANS.REFWO  = '{0}'
            ", tb_workorder.Text);
            dtLabor = lMaximoComm.oracle_runQuery(cmdLabor);;
            dg_labact.DataSource = dtLabor;
            //
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
