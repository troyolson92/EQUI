using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using EQUICommunictionLib;

namespace EQUIToolsLib
{
    public partial class LogDetails : MetroFramework.Forms.MetroForm
    {
        //debugger
        myDebugger Debugger = new myDebugger();
        GadataComm lGdataComm = new GadataComm();
        DataTable dt;
        BackgroundWorker bw = new BackgroundWorker();
        MaximoComm lMaximoComm = new MaximoComm();
        string slogtype = "";

        public LogDetails(string Location, string LogType, string Errornum, int RefId)
        {
            InitializeComponent();
                        bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            this.Text = string.Format("Errornum: {0}", Errornum);
            slogtype = LogType;

            if (Errornum != null)
            {
                tb_errorId.Text = Errornum;
                tb_errorId.Enabled = false;
                tb_location.Text = Location;
                tb_location.Enabled = false;
                tb_refid.Text = RefId.ToString();
                tb_refid.Enabled = false;
                btn_get.Visible = false;
                bw.RunWorkerAsync();
            }
            else
            {
                tb_errorId.Text = "xxxx";
                tb_errorId.Enabled = true;
                tb_location.Text = "xxxx";
                tb_location.Enabled = true;
                tb_refid.Text = "xxxx";
                tb_refid.Enabled = true;
                btn_get.Visible = true;
            }
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            metroProgressSpinner1.Hide();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            getErrorDetails();
        }

        private void getErrorDetails()
        {
            metroProgressSpinner1.Show();

            //query all instances of the error 
            string qry = string.Format(
    @"
DECLARE @Location as varchar(max) = '{0}'
DECLARE @ERRORNUM as int = {1} 
DECLARE @Refid as int = {2}
DECLARE @logtype as varchar(max) = '{3}'
DECLARE @controllerTYPE as varchar(10) = (select top 1 controller_type from gadata.equi.ASSETS where RTRIM(location) = @Location)
--voor c3g
if @controllerTYPE = 'c3g'
BEGIN
SELECT TOP 1 
 GADATA.[dbo].[fn_QinfoFormatString]([Info]) as 'INFO' 
,GADATA.[dbo].[fn_QinfoFormatString]([Cause]) as 'Cause' 
,GADATA.[dbo].[fn_QinfoFormatString]([Remedy]) as 'Remedy' 
FROM [GADATA].[Volvo].[FaultInfo] 
WHERE ErrorNbr = @ERRORNUM
END

--voor c4g
if @controllerTYPE = 'c4g'
BEGIN
SELECT TOP 1 
 GADATA.[dbo].[fn_QinfoFormatString]([Info]) as 'INFO' 
,GADATA.[dbo].[fn_QinfoFormatString]([Cause]) as 'Cause' 
,GADATA.[dbo].[fn_QinfoFormatString]([Remedy]) as 'Remedy' 
FROM [GADATA].[Volvo].[FaultInfo] 
WHERE ErrorNbr = @ERRORNUM
END

--voor IRC5
if @controllerTYPE = 'IRC5'
BEGIN
select top 1 
 l.error_text as 'INFO' 
,c.cause_text as 'Cause' 
,R.Remedy_text as 'Remedy'
from GADATA.ABB.h_alarm as h 
left join GADATA.ABB.L_Cause as c on c.id = h.cause_id
left join GADATA.ABB.L_Remedy as R on R.id = h.remedy_id
left join GADATA.ABB.L_error as l on l.id = h.error_id
WHERE h.id = @Refid
END

--voor NGAC
if @controllerTYPE = 'NGAC' and @logtype = 'ControllerEvent'
BEGIN
select top 1 
 le.Title
,ld.Description
,lco.Consequences
,lca.Causes
,lac.Actions
from GADATA.NGAC.h_alarm as h 
left join GADATA.NGAC.L_error as le on le._id = h.L_error_id
left join GADATA.NGAC.L_description as ld on ld.id = le.l_description_id
left join GADATA.NGAC.L_consequences as lco on lco.id = le.l_consequences_id
left join GADATA.NGAC.L_causes as lca on lca.id = le.l_causes_id
left join GADATA.NGAC.L_actions as lac on lac.id = le.l_actions_id
WHERE h.id = @Refid
END

--voor NGAC
if @controllerTYPE = 'NGAC' and @logtype = 'ErrDispLog'
BEGIN
select top 1 
 h.FullLogtext
from GADATA.NGAC.ErrDispLog as h 
WHERE h.refId = @Refid
END
", tb_location.Text, tb_errorId.Text, tb_refid.Text, slogtype);

            //fill dataset
            dt = lGdataComm.RunQueryGadata(qry);
            //check if the result was valid 

            StringBuilder sb = new StringBuilder();
            string newline = "<p></p>";
            if (dt.Rows.Count != 0)
            {
                DataRow myRow = dt.Rows[0];
                foreach (DataColumn dc in myRow.Table.Columns)
                {
                    sb.AppendLine(lMaximoComm.StringToHTML_Table( dc.ColumnName,myRow.Field<string>(dc.ColumnName).ToString() )).AppendLine(newline);
                }
            }
            else
            {
                sb.AppendLine("No valid result from query").AppendLine(newline);
            }
            webBrowser1.DocumentText = sb.ToString();
        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy) { return; }
            bw.RunWorkerAsync();        
        }


    }
}
