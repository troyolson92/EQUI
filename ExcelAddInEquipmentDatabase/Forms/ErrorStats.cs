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

        public ErrorStats(string Errornum)
        {
            InitializeComponent();
            this.Text = string.Format("Errornum: {0}", Errornum);

            string qry =
                @"
select count(h.id) as count ,t.vyear,t.vweek
FROM  gadata.volvo.l_timeline as t 
left join  gadata.c4g.h_alarm as h on h.c_timestamp between t.starttime and t.endtime
left join gadata.c4g.l_error as l on l.id = h.error_id
where t.starttime between getdate()-200 and getdate()
and h.controller_id = 5 and error_number = 4098
group by t.vyear,t.vweek
";
            DataTable dt = lGdataComm.RunQueryGadata(qry);
            chart1.Series.Add("ErrorCount");
            chart1.Series["ErrorCount"].XValueMember = "vweek";
            chart1.Series["ErrorCount"].YValueMembers = "count";
            chart1.Series["ErrorCount"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            chart1.Series["ErrorCount"].BorderWidth = 2;
            chart1.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
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
