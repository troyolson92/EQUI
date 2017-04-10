using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Globalization;

using System.Diagnostics;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class AssetStats : MetroFramework.Forms.MetroForm
    {
        //debugger
        Debugger Debugger = new Debugger();

        GadataComm lGdataComm = new GadataComm();
        DataTable dtGadata;

        MaximoComm lMaximoComm = new MaximoComm();
        DataTable dtMaximo;

        Point? prevPosition = null; 
        ToolTip tooltip = new ToolTip(); 
 
        BackgroundWorker bwLongDescription = new BackgroundWorker();

        public AssetStats(string Location)
        {
            InitializeComponent();
         //.   Location = "59090GH01F"; //test
            this.Text = string.Format("AssetStats tool Location: {0}",Location);
            //
            cb_sortmode.Items.Clear();
            cb_sortmode.Items.Insert(0, "None");
            cb_sortmode.Items.Insert(1, "Hours");
            cb_sortmode.Items.Insert(2, "Days");
            cb_sortmode.Items.Insert(3, "Weeks");
            //init chart common
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            chart1.FormatNumber += chart1_FormatNumber;
            //init chart 'count of dt'
            chart1.Series.Add("cDownTime");
            chart1.Series["cDownTime"].XValueMember = "starttime";
            chart1.Series["cDownTime"].YValueMembers = "countDT";
            chart1.Series["cDownTime"].XValueType = ChartValueType.DateTime;
            chart1.Series["cDownTime"].ChartType = SeriesChartType.Column;
            chart1.Series["cDownTime"].BorderWidth = 8;
            //init chart 'downtime'
            chart1.Series.Add("DownTime");
            chart1.Series["DownTime"].XValueMember = "starttime";
            chart1.Series["DownTime"].YValueMembers = "Downtime";
            chart1.Series["DownTime"].XValueType = ChartValueType.DateTime;
            chart1.Series["DownTime"].ChartType = SeriesChartType.Line;
            chart1.Series["DownTime"].BorderWidth = 4;
            chart1.Series["DownTime"].Color = System.Drawing.Color.Purple;
            chart1.Series["DownTime"].YAxisType = AxisType.Secondary;
            //init chart 'reponseTime'
            chart1.Series.Add("ResponseTime");
            chart1.Series["ResponseTime"].XValueMember = "starttime";
            chart1.Series["ResponseTime"].YValueMembers = "Responsetime";
            chart1.Series["ResponseTime"].XValueType = ChartValueType.DateTime;
            chart1.Series["ResponseTime"].ChartType = SeriesChartType.Line;
            chart1.Series["ResponseTime"].BorderWidth = 4;
            chart1.Series["ResponseTime"].Color = System.Drawing.Color.Tomato;
            chart1.Series["ResponseTime"].YAxisType = AxisType.Secondary;
            //init chat 'maximo'
            chart1.Series.Add("Maximo");
            chart1.Series["Maximo"].XValueMember = "starttime";
            chart1.Series["Maximo"].YValueMembers = "countWO";
            chart1.Series["Maximo"].XValueType = ChartValueType.DateTime;
            chart1.Series["Maximo"].ChartType = SeriesChartType.Point;
            chart1.Series["Maximo"].BorderWidth = 8;
            chart1.Series["Maximo"].Color = System.Drawing.Color.ForestGreen;
            //query all instances of the error 
            #region Query
            string strSqlGetFromGadata = string.Format(
    @"
USE GADATA
DECLARE @Location as varchar(max) = '{0}'
DECLARE @controllerTYPE as varchar(10) = (select top 1 controller_type from gadata.equi.ASSETS where RTRIM(location) = @Location)

if @controllerTYPE = 'c3g'
BEGIN
SELECT 		 
  h.StartOfBreakdown as 'starttime' 
, DATEDIFF(second,'1900-01-01 00:00:00', H.Rt)		AS 'Responsetime' 
, DATEDIFF(second, H.StartOfBreakdown, H.EndOfBreakdown)AS 'Downtime'
, 1 as 'countDT'
, null as 'countWO'
, null as 'WONUM'
FROM  C3G.h_breakdown AS H 
LEFT OUTER JOIN C3G.L_error AS L ON L.id = H.error_id 

LEFT OUTER JOIN VOLVO.c_Classification as cc on cc.id = L.c_ClassificationId
LEFT OUTER JOIN VOLVO.c_Subgroup as cs on cs.id = L.c_SubgroupId

--joining of the RIGHT ASSET
LEFT OUTER JOIN equi.ASSETS as A on 
A.controller_type = 'c3g' --join the right 'data controller type'
AND
A.controller_id = h.controller_id --join the right 'data controller id'
AND 
A.CLassificationId LIKE '%' + ISNULL(RTRIM(cc.Classification),'UR') + '%' 

where 
a.LOCATION like @Location


UNION
SELECT 
  getdate() as 'starttime'
 ,null   
 ,null
 , 0
 ,null
 ,null
END

if @controllerTYPE = 'c4g'
BEGIN
SELECT 		 
  h.StartOfBreakdown as 'starttime' 
, DATEDIFF(second,'1900-01-01 00:00:00', H.Rt)		AS 'Responsetime' 
, DATEDIFF(second, H.StartOfBreakdown, H.EndOfBreakdown)AS 'Downtime'
, 1 as 'countDT'
, null as 'countWO'
, null as 'WONUM'
FROM  C4G.h_breakdown AS H 
LEFT OUTER JOIN C4G.L_error AS L ON L.id = H.error_id 

LEFT OUTER JOIN VOLVO.c_Classification as cc on cc.id = L.c_ClassificationId
LEFT OUTER JOIN VOLVO.c_Subgroup as cs on cs.id = L.c_SubgroupId

--joining of the RIGHT ASSET
LEFT OUTER JOIN equi.ASSETS as A on 
A.controller_type = 'c4g' --join the right 'data controller type'
AND
A.controller_id = h.controller_id --join the right 'data controller id'
AND 
A.CLassificationId LIKE '%' + ISNULL(RTRIM(cc.Classification),'UR') + '%' 

where 
a.LOCATION like @Location


UNION
SELECT 
  getdate() as 'starttime'
 ,null   
 ,null
 , 0
 ,null
 ,null
END",Location);


            string strSqlGetFromMaximo = string.Format(@"
SELECT
  WORKORDER.STATUSDATE starttime 
 ,null Responsetime
 ,null Downtime
, null countDT
 ,1 countWO
 ,TO_NUMBER(WORKORDER.WONUM) WONUM
FROM MAXIMO.WORKORDER WORKORDER  
WHERE WORKORDER.LOCATION LIKE '{0}'
            ", Location);
            #endregion
            //fill dataset with all errors
            dtGadata = lGdataComm.RunQueryGadata(strSqlGetFromGadata);
            dtMaximo = lMaximoComm.oracle_runQuery(strSqlGetFromMaximo);
            //check if the result was valid 
            if (dtGadata.Rows.Count == 0) { Debugger.Message("The query for this errorcode did not return a valid result"); this.Dispose(); return; };
            //setup trackbar (trackbar maximum = first time error happend, minium = now)
            DateTime FirstError = (from a in dtGadata.AsEnumerable() select a.Field<DateTime>("starttime")).Min();
            DateTime LastError = (from a in dtGadata.AsEnumerable() select a.Field<DateTime>("starttime")).Max();
            trackBar1.Minimum = (int)(FirstError - LastError).TotalDays;
            if (trackBar1.Minimum > -3) {trackBar1.Minimum = -3;}
            trackBar1.Maximum = -1; //minimum display = 1 day 
            //figure out init mode
            /*this will figure out of "active" the error is, 
             * if it happens more than 10 times in the last 3 days => set graph to 3 day range (show data in hours)
             *               more than 10 times in the last 30 days => set graph to 30 day range (show data in days)
             *               else set to full 'lifecycle' of error and set graph in week mode
             */
            var count3 = from a in dtGadata.AsEnumerable()
                      where a.Field<DateTime>("starttime") >  DateTime.Now.AddDays(Convert.ToInt32(3) * -1)
                      select a;
            if (count3.Count() > 10) //more than 10 times in 3 days
            {
                trackBar1.Value = -3;
                cb_sortmode.SelectedIndex = 1;
                chart1.Series["cDownTime"].Color = System.Drawing.Color.Red;
            }
            else
            {
                var count30 = from a in dtGadata.AsEnumerable()
                             where a.Field<DateTime>("starttime") > DateTime.Now.AddDays(Convert.ToInt32(30) * -1)
                             select a;
                if (count30.Count() > 10) //more than 10 times in a month
                {
                    trackBar1.Value = -30;
                    cb_sortmode.SelectedIndex = 2;
                    chart1.Series["cDownTime"].Color = System.Drawing.Color.DarkOrange;
                }
                else
                {
                    if (trackBar1.Minimum < -360)
                    {
                        trackBar1.Value = -360; //set last running year as a max for 'init' mode
                    }
                    else
                    {
                        trackBar1.Value = trackBar1.Minimum;
                    }
                    cb_sortmode.SelectedIndex = 3;
                    chart1.Series["cDownTime"].Color = System.Drawing.Color.Blue;
                }
            }
            //build trend chart in init mode. 
            cb_sortmode.SelectedValueChanged += new System.EventHandler(this.cb_sortmode_SelectedValueChanged);
            chart1.GetToolTipText += new System.EventHandler<System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs>(Chart1_GetToolTipText);
            bwLongDescription.DoWork += bwLongDescription_DoWork;
            this.Show();
        }


        private void built_Chart() { built_Chart(false); }
        private void built_Chart(Boolean noAutoGrouping) 
        {
            //use the trackbar to calculate the starting point of the graph
            DateTime GrapStart = DateTime.Now.AddDays(Convert.ToInt32(trackBar1.Value));
            //
            var ldt = from a in dtGadata.AsEnumerable()
                      .Union(dtMaximo.AsEnumerable())
                      where a.Field<DateTime>("starttime") > GrapStart
                      orderby a.Field<DateTime>("starttime")
                      select a;
            chart1.DataSource = ldt;
            chart1.DataBind();
            //
            dtMaximo.DefaultView.RowFilter = string.Format("starttime > '{0}'", GrapStart);
            dtMaximo.DefaultView.Sort = "starttime desc";
            dataGridView1.DataSource = dtMaximo; 
            //
            DateTime FirstError = (from a in dtGadata.AsEnumerable() select a.Field<DateTime>("starttime")).Min();
            //
            if (noAutoGrouping == false)
            {
                if (trackBar1.Value > -10)
                {
                    cb_sortmode.SelectedIndex = 1;
                }
                else if (trackBar1.Value > -100)
                {
                    cb_sortmode.SelectedIndex = 2;
                }
                else
                {
                    cb_sortmode.SelectedIndex = 3;
                }
            }
            //
            switch (cb_sortmode.Text)
            {
                case "None":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatTimestamp";
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.NotSet;
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "NoGrouping");
                    break;

                case "Hours":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour";
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupHourmode");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Hours, "cDownTime");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Hours, "DownTime");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Hours, "ResponseTime");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Hours, "Maximo");
                    break;

                case "Days":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay";
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupDaymode");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Days, "cDownTime");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Days, "DownTime");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Days, "ResponseTime");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Days, "Maximo");
                    break;

                case "Weeks":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek";
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupWeekmode");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Weeks, "cDownTime");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Weeks, "DownTime");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Weeks, "ResponseTime");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Weeks, "Maximo");
                    break;

                default:
                    break;
            }   
        }

        void chart1_FormatNumber(object sender, FormatNumberEventArgs e)
{
    if (e.ElementType == ChartElementType.AxisLabels && e.Format == "CustomAxisXFormatWeek")
    {
        if (e.ValueType == ChartValueType.DateTime)
        {
            var currentCalendar = CultureInfo.CurrentCulture.Calendar;

            e.LocalizedValue = string.Format("{0}W{1}",
                DateTime.FromOADate(e.Value).Year % 100,
                currentCalendar.GetWeekOfYear(DateTime.FromOADate(e.Value), System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek)
                );
        }
    }

    if (e.ElementType == ChartElementType.AxisLabels && e.Format == "CustomAxisXFormatDay")
    {
        if (e.ValueType == ChartValueType.DateTime)
        {
            var currentCalendar = CultureInfo.CurrentCulture.Calendar;

            e.LocalizedValue = string.Format("{0}W{1}D{2}",
                DateTime.FromOADate(e.Value).Year % 100,
                currentCalendar.GetWeekOfYear(DateTime.FromOADate(e.Value), System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek),
                (int)DateTime.FromOADate(e.Value).DayOfWeek
                );
        }
    }

    if (e.ElementType == ChartElementType.AxisLabels && e.Format == "CustomAxisXFormatHour")
    {
        if (e.ValueType == ChartValueType.DateTime)
        {
            var currentCalendar = CultureInfo.CurrentCulture.Calendar;

            e.LocalizedValue = string.Format("{0}W{1}D{2} h{3}",
                DateTime.FromOADate(e.Value).Year % 100,
                currentCalendar.GetWeekOfYear(DateTime.FromOADate(e.Value), System.Globalization.CalendarWeekRule.FirstFourDayWeek, CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek),
                (int)DateTime.FromOADate(e.Value).DayOfWeek,
                DateTime.FromOADate(e.Value).Hour
                );
        }
    }

    if (e.ElementType == ChartElementType.AxisLabels && e.Format == "CustomAxisXFormatTimestamp")
    {
        if (e.ValueType == ChartValueType.DateTime)
        {
            e.LocalizedValue = DateTime.FromOADate(e.Value).ToString("yyyy-MM-dd hh:mm:ss");
        }
    }
}

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            built_Chart();
        }

        private void cb_sortmode_SelectedValueChanged(object sender, EventArgs e)
        {
            built_Chart(true);
        }

        private void chart1_MouseMove(object sender, MouseEventArgs e)
        {
        var pos = e.Location;
        if (prevPosition.HasValue && pos == prevPosition.Value) { return; };     
        tooltip.RemoveAll();     
        prevPosition = pos;     
         var results = chart1.HitTest(pos.X, pos.Y, false, ChartElementType.PlottingArea);     
            foreach (var result in results)     
            {         
                if (result.ChartElementType == ChartElementType.PlottingArea)         
                {
                    chart1.Series["Maximo"].ToolTip = "X=#VALX, Y=#VALY";
                }    
            } 
        }

        private void Chart1_GetToolTipText(object sender, System.Windows.Forms.DataVisualization.Charting.ToolTipEventArgs e)
        {

           // Check selevted chart element and set tooltip text
           if (e.HitTestResult.ChartElementType == ChartElementType.DataPoint)
           {
              int i = e.HitTestResult.PointIndex;
              DataPoint dp = e.HitTestResult.Series.Points[i];
              e.Text = string.Format("{0:F1}, {1:F1},  {2}", dp.XValue, dp.YValues[0], DateTime.FromOADate(dp.XValue).ToString());
           }
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (bwLongDescription.IsBusy) { return; }
            bwLongDescription.RunWorkerAsync();
        }

        void bwLongDescription_DoWork(object sender, DoWorkEventArgs e)
        {
           // webBrowser1.DocumentText = "Getting data....";
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells[0].Value != null)
                {
                    webBrowser1.DocumentText = lMaximoComm.getMaximoDetails(row.Cells["WONUM"].Value.ToString());
                }
            }
        }

    }
}
