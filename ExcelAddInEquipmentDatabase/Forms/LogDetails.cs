using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class LogDetails : MetroFramework.Forms.MetroForm
    {
        //debugger
        Debugger Debugger = new Debugger();
        GadataComm lGdataComm = new GadataComm();
        DataTable dt;
        BackgroundWorker bw = new BackgroundWorker();

        public LogDetails(string Location, string Errornum)
        {
            InitializeComponent();
                        bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            this.Text = string.Format("Errornum: {0}", Errornum);
            
            if (Errornum != null)
            {
                tb_errorId.Text = Errornum;
                tb_errorId.Enabled = false;
                tb_location.Text = Location;
                tb_location.Enabled = false;
                btn_get.Visible = false;
                bw.RunWorkerAsync();
            }
            else
            {
                tb_errorId.Text = "xxxx";
                tb_errorId.Enabled = true;
                tb_location.Text = "xxxx";
                tb_location.Enabled = true;
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
'Will use this for user info later ... bye bye sdebeul' as 'INFO' 
,c.cause_text as 'Cause' 
,R.Remedy_text as 'Remedy'
from GADATA.ABB.h_alarm as h 
left join GADATA.ABB.L_Cause as c on c.id = h.cause_id
left join GADATA.ABB.L_Remedy as R on R.id = h.remedy_id
WHERE h.id = @ERRORNUM
END
", tb_location.Text, tb_errorId.Text);

            //fill dataset
            dt = lGdataComm.RunQueryGadata(qry);
            //check if the result was valid 
            if (dt.Rows.Count == 0) { Debugger.Message("The query for this errorcode did not return a valid result"); this.Dispose(); return; };

            StringBuilder sb = new StringBuilder();
            sb.AppendLine(
@"<div>            
<table bgcolor=#00FF00>
<tr>
<td style = white-space:PRE>INFO                                                                                                                                                                </td>
</tr>
</table>
</div>");
            sb.AppendLine(dt.Rows[0].Field<String>("INFO"));
            sb.AppendLine("<div>---------------------------------------------------------------</div>").AppendLine(
@"<div>            
<table bgcolor=#00FF00>
<tr>
<td style = white-space:PRE>Cause                                                                                                                                                                </td>
</tr>
</table>
</div>");
            sb.AppendLine(dt.Rows[0].Field<String>("Cause"));
            sb.AppendLine("<div>---------------------------------------------------------------</div>").AppendLine(
@"<div>            
<table bgcolor=#00FF00>
<tr>
<td style = white-space:PRE>Remedy                                                                                                                                                              </td>
</tr>
</table>
</div>");
            sb.AppendLine(dt.Rows[0].Field<String>("Remedy"));
            sb.AppendLine("<div>---------------------------------------------------------------</div>");

            webBrowser1.DocumentText = sb.ToString();
        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            if (bw.IsBusy) { return; }
            bw.RunWorkerAsync();        
        }

    }
}
