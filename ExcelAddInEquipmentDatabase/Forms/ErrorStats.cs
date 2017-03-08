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
    public partial class ErrorStats : Form
    {
        GadataComm lGdataComm = new GadataComm();

        public ErrorStats(string Location, string Errornum)
        {
            InitializeComponent();
            this.Text = string.Format("ErrorStats tool Errornum: {0} Location: {1}", Errornum,Location);

            string qry = string.Format(
                @"
select t.starttime , SUM(ISNULL(l.id,0)) count
FROM  gadata.volvo.l_timeline as t 
left join  gadata.c4g.h_alarm as h on h.c_timestamp between t.starttime and t.endtime 
and 
h.controller_id = (select top 1 c.id from gadata.c4g.c_controller as c where c.controller_name = '{0}')

left join gadata.c4g.l_error as l on l.id = h.error_id and l.error_number = {1}

where t.starttime between getdate() - 30 and getdate()
group by t.starttime
",Location,Errornum);
            DataTable dt = lGdataComm.RunQueryGadata(qry);
            chart1.Series.Add("ErrorCount");
            chart1.Series["ErrorCount"].XValueMember = "starttime";
            chart1.Series["ErrorCount"].YValueMembers = "count";
            chart1.Series["ErrorCount"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["ErrorCount"].BorderWidth = 2;
            chart1.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
            chart1.DataSource = dt;
            chart1.DataBind();

      
        }

        private void chart1_SizeChanged(object sender, EventArgs e)
        {
            chart1.Update();
           // chart1.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Auto;
        }
    }
}
