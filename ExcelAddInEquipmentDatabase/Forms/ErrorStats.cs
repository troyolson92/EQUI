﻿using System;
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
        DataTable dt;

        public ErrorStats(string Location, string Errornum)
        {
            InitializeComponent();
            this.Text = string.Format("ErrorStats tool Errornum: {0} Location: {1}", Errornum,Location);
            //init chart 
            chart1.Series.Add("ErrorCount");
            chart1.Series["ErrorCount"].XValueMember = "starttime";
            chart1.Series["ErrorCount"].YValueMembers = "count";
            chart1.Series["ErrorCount"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Column;
            chart1.Series[0].XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            //query all instances of the error 
            string qry = string.Format(
    @"
DECLARE @Location as varchar(max) = '{0}'
DECLARE @ERRORNUM as int = {1} -- why not use the index of the l_error ? might be smarter 
DECLARE @controllerID as int = (select top 1 controller_id from gadata.equi.ASSETS where RTRIM(location) = @Location)
DECLARE @controllerTYPE as varchar(10) = (select top 1 controller_type from gadata.equi.ASSETS where RTRIM(location) = @Location)
DECLARE @First as datetime = 
(
select top 1 isnull(_timestamp,c_timestamp) FROM GADATA.c4g.h_alarm 
left join gadata.C4G.L_error on L_error.[error_number] = @Errornum and L_error.id = h_alarm.error_id
where h_alarm.controller_id = @controllerID
)
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
h._timestamp between @first and getdate()
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
h._timestamp between @first and getdate()
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
                // should query all instanace of the error. (from first time it happend)
                //than figer out what mode I should use. if happend more that 10 times in last 3 days = hourmode if 10 in last week day mode ...
                //numericUpDown1.Value := 10;
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

            chart1.DataSource = ldt;
            chart1.DataBind();

            if (nDays < 3) //shift mode = min resulotion
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
                chart1.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Hours;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
                chart1.Series["ErrorCount"].BorderWidth = 8;
            }
            else if (nDays < 31) //day mode
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
                chart1.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
                chart1.Series["ErrorCount"].BorderWidth = 6;
            }
            else //week mode
            {
                chart1.ChartAreas[0].AxisX.LabelStyle.Format = "yyyy-MM-dd";
                chart1.ChartAreas[0].AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Weeks;
                chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
                chart1.Series["ErrorCount"].BorderWidth = 4;
            }       
        }

        private void button1_Click(object sender, EventArgs e)
        {
            buildTrendChart();
        }
    }
}
