using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class ErrorStats : Form
    {
        GadataComm lGdataComm = new GadataComm();
        DataTable dt;

        public ErrorStats(string Location, string Errornum)
        {
            InitializeComponent();
            this.Text = string.Format("ErrorStats tool Errornum: {0} Location: {1}", Errornum,Location);
            //init chart 
            chart1.Series.Add("ErrorCount");
            chart1.Series["ErrorCount"].XValueMember = "starttime";
            chart1.Series["ErrorCount"].YValueMembers = "count";
            chart1.Series["ErrorCount"].ChartType = SeriesChartType.Column;
            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            //query all instances of the error 
            string qry = string.Format(
    @"
DECLARE @Location as varchar(max) = '{0}'
DECLARE @ERRORNUM as int = {1} -- why not use the index of the l_error ? might be smarter 
DECLARE @controllerID as int = (select top 1 controller_id from gadata.equi.ASSETS where RTRIM(location) = @Location)
DECLARE @controllerTYPE as varchar(10) = (select top 1 controller_type from gadata.equi.ASSETS where RTRIM(location) = @Location)
DECLARE @First as datetime
if @controllerTYPE = 'c4g'
BEGIN
SET @First = 
(
select top 1 isnull(_timestamp,c_timestamp) FROM GADATA.c4g.h_alarm 
left join gadata.C4G.L_error on L_error.[error_number] = @Errornum and L_error.id = h_alarm.error_id
where h_alarm.controller_id = @controllerID
)
END
if @controllerTYPE = 'c3g'
BEGIN
SET @First = 
(
select top 1 isnull(_timestamp,c_timestamp) FROM GADATA.c3g.h_alarm 
left join gadata.C3G.L_error on L_error.[error_number] = @Errornum and L_error.id = h_alarm.error_id
where h_alarm.controller_id = @controllerID
)
END

print @First

if @controllerTYPE = 'c4g'
BEGIN
SELECT 
 t.starttime 
 ,count(x.ts) as 'count'
FROM GADATA.VOLVO.L_timeline as t 
LEFT JOIN 
(
SELECT ISNULL(h._timestamp,h.c_timestamp) as 'ts' FROM gadata.c4g.h_alarm as h 
LEFT JOIN gadata.c4g.l_error as l on 
l.id = h.error_id 
AND 
l.[error_number] = @Errornum
WHERE
ISNULL(h._timestamp,h.c_timestamp) between @first and getdate()
AND 
h.controller_id = @controllerID
AND 
l.[error_number] = @ERRORNUM
)  as x on x.ts between t.starttime and t.endtime

WHERE 
t.starttime between @first and getdate() 
group by t.starttime
END

if @controllerTYPE = 'c3g'
BEGIN
SELECT 
 t.starttime 
 ,count(x.ts) as 'count'
FROM GADATA.VOLVO.L_timeline as t 
LEFT JOIN 
(
SELECT ISNULL(h._timestamp,h.c_timestamp) as 'ts' FROM gadata.c3g.h_alarm as h 
LEFT JOIN gadata.c3g.l_error as l on 
l.id = h.error_id 
AND 
l.[error_number] = @Errornum
WHERE
ISNULL(h._timestamp,h.c_timestamp) between @first and getdate()
AND 
h.controller_id = @controllerID
AND 
l.[error_number] = @ERRORNUM
)  as x on x.ts between t.starttime and t.endtime

WHERE 
t.starttime between @first and getdate() 
group by t.starttime
END

", Location, Errornum);
            dt = lGdataComm.RunQueryGadata(qry);
            //figure out init mode
            var count3 = from a in dt.AsEnumerable()
                      where a.Field<DateTime>("starttime") >  DateTime.Now.AddDays(Convert.ToInt32(3) * -1)
                      select a;
            if (count3.Count() > 10) //more than 10 times in 3 days
            {
                numericUpDown1.Value = 3;
            }
            else
            {
                var count30 = from a in dt.AsEnumerable()
                             where a.Field<DateTime>("starttime") > DateTime.Now.AddDays(Convert.ToInt32(30) * -1)
                             select a;
                if (count30.Count() > 10) //more than 10 times in a month
                {
                    numericUpDown1.Value = 30;
                }
                else
                {
                    numericUpDown1.Value = 1000;

                }
            }
            //build trend chart in init mode. 
            buildTrendChart();
        }

private void buildTrendChart() 
        {
            decimal nDays = numericUpDown1.Value;
            DateTime GrapStart = DateTime.Now.AddDays(Convert.ToInt32(nDays) * -1);

            var ldt = from a in dt.AsEnumerable()
                      where a.Field<DateTime>("starttime") > GrapStart
                      select a;
            //
            chart1.DataSource = ldt;
            chart1.DataBind();
            //first error instance
            string FirstE = "";//dt.AsEnumerable().First()[0].ToString();
            //
            if (nDays < 4) //shift mode = min resulotion
            {
                //this will not work because we already group /shift on SQL side. fuck
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
                chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
                chart1.Series["ErrorCount"].BorderWidth = 8;
                label1.Text = string.Format("First error: {0}  Mode: {1}", FirstE, "ShiftMode");
                chart1.DataManipulator.Group("SUM", 1, IntervalType.Hours, "ErrorCount");
            }
            else if (nDays < 31) //day mode
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
                chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
                chart1.Series["ErrorCount"].BorderWidth = 6;
                label1.Text = string.Format("First error: {0}  Mode: {1}", FirstE, "Daymode");
                chart1.DataManipulator.Group("SUM", 1, IntervalType.Days, "ErrorCount");
            }
            else //week mode
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
                chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
                chart1.Series["ErrorCount"].BorderWidth = 4;
                label1.Text = string.Format("First error: {0}  Mode: {1}", FirstE, "Weekmode");
                chart1.DataManipulator.Group("SUM", 1, IntervalType.Weeks, "ErrorCount");
            }       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buildTrendChart();
        }
    }
}
