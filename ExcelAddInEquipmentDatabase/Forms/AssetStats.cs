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
using System.Text.RegularExpressions;
using System.Diagnostics;
using EQUICommunictionLib;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class AssetStats : MetroFramework.Forms.MetroForm
    {
        //debugger
        myDebugger Debugger = new myDebugger();

        GadataComm lGdataComm = new GadataComm();
        DataTable dtGadata;

        MaximoComm lMaximoComm = new MaximoComm();
        DataTable dtMaximoGraph;
        DataTable dtMaximoGrid;

        Point? prevPosition = null; 
        ToolTip tooltip = new ToolTip();

        BackgroundWorker bwInit = new BackgroundWorker();
        BackgroundWorker bwLongDescription = new BackgroundWorker();

        string lLocation;

        public AssetStats(string Location)
        {
            InitializeComponent();
            lLocation = Location;
           
            //to play nice with gb locations need to clean this up 
            if ((!String.IsNullOrEmpty(lLocation) && Char.IsLetter(lLocation[0])) == false)
            {
                tb_location.Text = lLocation;
            }
            else
            {
                tb_location.Text = Regex.Replace(lLocation, @"[A-Za-z\s]", "%") + "%";
            }
            this.Text = string.Format("AssetStats tool Location: {0}",Location);
            //
            cb_sortmode.Items.Clear();
            cb_sortmode.Items.Insert(0, "None");
            cb_sortmode.Items.Insert(1, "Hours");
            cb_sortmode.Items.Insert(2, "Days");
            cb_sortmode.Items.Insert(3, "Weeks");
            //init chart common
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.IsMarginVisible = false;
            chart1.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            chart1.ChartAreas[0].AxisY.Title = "Count of downtime/wo";
            chart1.ChartAreas[0].AxisY.IsMarginVisible = false;
            chart1.ChartAreas[0].AxisY2.Title = "Sum of downtime (min.)";
            chart1.ChartAreas[0].AxisY2.IsMarginVisible = false;
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
            //init chart 'ResolveTime'
            chart1.Series.Add("ResolveTime");
            chart1.Series["ResolveTime"].XValueMember = "starttime";
            chart1.Series["ResolveTime"].YValueMembers = "ResolveTime";
            chart1.Series["ResolveTime"].XValueType = ChartValueType.DateTime;
            chart1.Series["ResolveTime"].ChartType = SeriesChartType.Line;
            chart1.Series["ResolveTime"].BorderWidth = 4;
            chart1.Series["ResolveTime"].Color = System.Drawing.Color.LawnGreen;
            chart1.Series["ResolveTime"].YAxisType = AxisType.Secondary;
            //init chat 'maximo'
            chart1.Series.Add("Maximo");
            chart1.Series["Maximo"].XValueMember = "starttime";
            chart1.Series["Maximo"].YValueMembers = "countWO";
            chart1.Series["Maximo"].XValueType = ChartValueType.DateTime;
            chart1.Series["Maximo"].ChartType = SeriesChartType.Point;
            chart1.Series["Maximo"].BorderWidth = 8;
            chart1.Series["Maximo"].Color = System.Drawing.Color.ForestGreen;
            //
            bwInit.DoWork += bwInit_DoWork;
            bwInit.RunWorkerCompleted += bwInit_RunWorkerCompleted;
            bwInit.WorkerSupportsCancellation = true;
            bwInit.RunWorkerAsync();
            //
            this.Show();
        }

        void bwInit_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //check if the result was valid 
            if (dtGadata.Rows.Count == 0) { Debugger.Message("The query for this errorcode did not return a valid result"); this.Dispose(); return; };
            //setup trackbar (trackbar maximum = first time error happend, minium = now)
            DateTime FirstError = (from a in dtGadata.AsEnumerable() select a.Field<DateTime>("starttime")).Min();
            DateTime LastError = (from a in dtGadata.AsEnumerable() select a.Field<DateTime>("starttime")).Max();
            trackBar1.Minimum = (int)(FirstError - LastError).TotalDays;
            if (trackBar1.Minimum > -3) { trackBar1.Minimum = -3; }
            trackBar1.Maximum = -1; //minimum display = 1 day 
            trackBar2.Minimum = trackBar1.Minimum;
            trackBar2.Maximum = trackBar1.Maximum;
            trackBar2.Value = trackBar2.Maximum;
            //figure out init mode
            /*this will figure out of "active" the error is, 
             * if it happens more than 10 times in the last 3 days => set graph to 3 day range (show data in hours)
             *               more than 10 times in the last 30 days => set graph to 30 day range (show data in days)
             *               else set to full 'lifecycle' of error and set graph in week mode
             */
            var count3 = from a in dtGadata.AsEnumerable()
                         where a.Field<DateTime>("starttime") > DateTime.Now.AddDays(Convert.ToInt32(3) * -1)
                         select a;
            if (count3.Count() > 20) //more than 10 times in 3 days
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
                if (count30.Count() > 20) //more than 10 times in a month
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
            bwLongDescription.DoWork += bwLongDescription_DoWork;
            //
            metroProgressSpinner1.Hide();
        }

        void bwInit_DoWork(object sender, DoWorkEventArgs e)
        {
            //query all instances of the error 
            #region Query
            string strSqlGetFromGadata = string.Format(
    @"
USE GADATA
DECLARE @Location as varchar(max) = '{0}'
DECLARE @controllerTYPE as varchar(10) = (select top 1 controller_type from gadata.equi.ASSETS where RTRIM(Location) LIKE @Location and controller_type is not null)

if @controllerTYPE = 'c3g'
BEGIN
SELECT 
 breakdown.Location as 'Location'
,breakdown.timestamp as 'starttime'
,breakdown.[Response(s)]/60 as 'Responsetime'
,(breakdown.[Downtime(s)] - breakdown.[Response(s)]) / 60 as 'ResolveTime'
,breakdown.[Downtime(s)]/60 as 'Downtime'
, 1 as 'countDT'
, null as 'countWO'
, null as 'WONUM'
, null as 'WORKTYPE'
from GADATA.c3g.breakdown
where 
breakdown.Location like @Location
AND 
breakdown.Subgroup not in('EO Maint','Operational**','Operational')

UNION
SELECT 
  null
 ,getdate() as 'starttime'
 ,null   
 ,null
, null
 , 0
 ,null
 ,null
 ,null
END

if @controllerTYPE = 'c4g'
BEGIN
SELECT
 breakdown.Location as 'Location'
,breakdown.timestamp as 'starttime'
,breakdown.[Response(s)]/60 as 'Responsetime'
,(breakdown.[Downtime(s)] - breakdown.[Response(s)]) / 60 as 'ResolveTime'
,breakdown.[Downtime(s)]/60 as 'Downtime'
, 1 as 'countDT'
, null as 'countWO'
, null as 'WONUM'
, null as 'WORKTYPE'
from GADATA.c4g.breakdown
where 
breakdown.Location like @Location
AND 
breakdown.Subgroup not in('EO Maint','Operational**','Operational')

UNION
SELECT 
  null
 ,getdate() as 'starttime'
 ,null   
 ,null
, null
 , 0
 ,null
 ,null
 ,null
END", tb_location.Text);


            string strSqlGetFromMaximoGraph = string.Format(@"
SELECT
  WORKORDER.LOCATION Location
 ,WORKORDER.STATUSDATE starttime 
 ,null Responsetime
 ,null Resolvetime
 ,null Downtime
 ,null countDT
 ,1 countWO
 ,TO_NUMBER(WORKORDER.WONUM) WONUM
 ,WORKTYPE
FROM MAXIMO.WORKORDER WORKORDER  
WHERE WORKORDER.LOCATION LIKE '{0}'
            ", tb_location.Text);

            string strSqlGetFromMaximoDataGrid = string.Format(@"
SELECT 
 TO_NUMBER(WORKORDER.WONUM) WONUM
,WORKORDER.STATUS
,WORKORDER.STATUSDATE
,WORKORDER.WORKTYPE
,WORKORDER.DESCRIPTION
,WORKORDER.LOCATION
,WORKORDER.REPORTEDBY
,WORKORDER.REPORTDATE
,WPITEM.ITEMNUM
,WPITEM.DESCRIPTION PARTDESCRIPTION
,WPITEM.REQUESTBY
FROM MAXIMO.WORKORDER WORKORDER  
LEFT JOIN MAXIMO.WPITEM WPITEM ON WPITEM.WONUM = WORKORDER.WONUM
WHERE WORKORDER.LOCATION LIKE '{0}'
ORDER BY WORKORDER.STATUSDATE DESC
            ", tb_location.Text);
            #endregion
            //fill dataset with all errors
            dtGadata = lGdataComm.RunQueryGadata(strSqlGetFromGadata);
            dtMaximoGraph = lMaximoComm.oracle_runQuery(strSqlGetFromMaximoGraph);
            dtMaximoGrid = lMaximoComm.oracle_runQuery(strSqlGetFromMaximoDataGrid);
        }

        private void built_Chart() { built_Chart(false); }
        private void built_Chart(Boolean noAutoGrouping) 
        {
            //use the trackbar to calculate the starting point of the graph
            DateTime GrapStart = DateTime.Now.AddDays(Convert.ToInt32(trackBar1.Value));
            DateTime GrapEnd = DateTime.Now.AddDays(Convert.ToInt32(trackBar2.Value));
            //
               var  ldt = from a in dtGadata.AsEnumerable()
                 .Union(dtMaximoGraph.AsEnumerable())
                          where 
                          (
                          (
                          a.Field<DateTime>("starttime") > GrapStart && a.Field<DateTime>("starttime") < GrapEnd
                          && cb_preventive.Checked == false
                          && a.Field<string>("Location") == lLocation && cb_incCiblings.Checked == false
                          )
                          ||
                          (
                          a.Field<DateTime>("starttime") > GrapStart && a.Field<DateTime>("starttime") < GrapEnd
                          && cb_preventive.Checked == true
                          && a.Field<string>("WORKTYPE") != "PP"
                          && a.Field<string>("WORKTYPE") != "PCI"
                          && a.Field<string>("WORKTYPE") != "WSCH"
                          && a.Field<string>("Location") == lLocation && cb_incCiblings.Checked == false
                          )
                          ||
                                                    (
                          a.Field<DateTime>("starttime") > GrapStart && a.Field<DateTime>("starttime") < GrapEnd
                          && cb_preventive.Checked == false
                          && cb_incCiblings.Checked == true
                          )
                          ||
                          (
                          a.Field<DateTime>("starttime") > GrapStart && a.Field<DateTime>("starttime") < GrapEnd
                          && cb_preventive.Checked == true
                          && a.Field<string>("WORKTYPE") != "PP"
                          && a.Field<string>("WORKTYPE") != "PCI"
                          && a.Field<string>("WORKTYPE") != "WSCH"
                          && cb_incCiblings.Checked == true
                          )
                          )
                          orderby a.Field<DateTime>("starttime")
                          select a;
            //this is shit 
               

               chart1.DataSource = ldt;
               chart1.DataBind();
            //
               if (dtMaximoGrid.Rows.Count != 0)
               {
                if (cb_incCiblings.Checked) // with cilds
                {
                    if (cb_preventive.Checked) //hide preventive
                    {

                        dtMaximoGrid.DefaultView.RowFilter = string.Format("STATUSDATE > '{0}' AND STATUSDATE < '{1}' AND WORKTYPE not in('PP','PCI','WSCH')", GrapStart, GrapEnd);

                    }
                    else
                    {
                        dtMaximoGrid.DefaultView.RowFilter = string.Format("STATUSDATE > '{0}' AND STATUSDATE < '{1}' ", GrapStart, GrapEnd);
                    }
                }
                else // no childs
                {
                    if (cb_preventive.Checked) //hide preventive
                    {

                        dtMaximoGrid.DefaultView.RowFilter = string.Format("STATUSDATE > '{0}' AND STATUSDATE < '{1}' AND WORKTYPE not in('PP','PCI','WSCH') AND LOCATION = '{2}'", GrapStart, GrapEnd,lLocation);

                    }
                    else
                    {
                        dtMaximoGrid.DefaultView.RowFilter = string.Format("STATUSDATE > '{0}' AND STATUSDATE < '{1}'  AND LOCATION = '{2}'", GrapStart, GrapEnd, lLocation);
                    }
                }


                   dtMaximoGrid.DefaultView.Sort = "STATUSDATE desc";
                   dataGridView1.DataSource = dtMaximoGrid;
               }
            //
            if (cb_spltDt.Checked)
            {
                chart1.Series["DownTime"].Enabled = false;
                chart1.Series["ResolveTime"].Enabled = true;
                chart1.Series["ResponseTime"].Enabled = true;
            }
            else
            {
                chart1.Series["DownTime"].Enabled = true;
                chart1.Series["ResolveTime"].Enabled = false;
                chart1.Series["ResponseTime"].Enabled = false;
            }

            //
            DateTime FirstError = (from a in dtGadata.AsEnumerable() select a.Field<DateTime>("starttime")).Min();
            //
            if (noAutoGrouping == false)
            {
                if ((trackBar1.Value -  trackBar2.Value) > -10)
                {
                    cb_sortmode.SelectedIndex = 1;
                }
                else if ((trackBar1.Value - trackBar2.Value) > -100)
                {
                    cb_sortmode.SelectedIndex = 2;
                }
                else
                {
                    cb_sortmode.SelectedIndex = 3;
                }
            }
            //check that manual grouping is fasable
            if ((cb_sortmode.SelectedIndex < 2) && ((trackBar1.Value - trackBar2.Value) < -20))
            {
                    Debugger.Message(string.Format("Its not a good idea to group in this way for '{0}' days of data",(trackBar1.Value-trackBar2.Value)));
                    cb_sortmode.SelectedIndex = 3;
            }

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
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Hours, "ResolveTime");
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
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Days, "ResolveTime");
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
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Weeks, "ResolveTime");
                    chart1.DataManipulator.Group("SUM", 1, IntervalType.Weeks, "Maximo");
                    break;

                default:
                    break;
            }
            //test
            chart1.Series["cDownTime"].BorderWidth = 8;
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
            try
            {
                if ((trackBar1.Value - trackBar2.Value) > -1)
                {
                    trackBar2.ValueChanged -= new System.EventHandler(trackBar2_ValueChanged);
                    if (trackBar1.Maximum < trackBar1.Value - 3)
                    {
                        trackBar1.ValueChanged -= new System.EventHandler(trackBar1_ValueChanged);
                        trackBar1.Value = trackBar1.Maximum;
                        trackBar1.ValueChanged += new System.EventHandler(trackBar1_ValueChanged);
                        trackBar2.Value = trackBar2.Maximum - 3;
                    }
                    else
                    {
                        trackBar2.Value = trackBar1.Value + 3;
                    }
                    trackBar2.ValueChanged += new System.EventHandler(trackBar2_ValueChanged);
                }
                else
                {
                      built_Chart();
                }
            }
            catch (System.ArgumentOutOfRangeException) 
            {
            }
        }

        private void trackBar2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if ((trackBar2.Value - trackBar1.Value) < -1)
                {
                    trackBar1.ValueChanged -= new System.EventHandler(trackBar1_ValueChanged);
                    if (trackBar2.Minimum > (trackBar2.Value - 3))
                    {
                        trackBar2.ValueChanged -= new System.EventHandler(trackBar2_ValueChanged);
                        trackBar2.Value = trackBar2.Minimum;
                        trackBar2.ValueChanged += new System.EventHandler(trackBar2_ValueChanged);
                        trackBar1.Value = trackBar1.Minimum + 3;
                    }
                    else
                    {
                        trackBar1.Value = trackBar2.Value - 3;
                    }
                    trackBar1.ValueChanged += new System.EventHandler(trackBar1_ValueChanged);
                }
                else
                {
                     built_Chart();
                }
            }
            catch (System.ArgumentOutOfRangeException)
            {
            }
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

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (bwLongDescription.IsBusy) { return; }
            bwLongDescription.RunWorkerAsync();
        }

        void bwLongDescription_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (row.Cells[0].Value != null)
                {
                    webBrowser1.DocumentText = lMaximoComm.getMaximoDetails(row.Cells["WONUM"].Value.ToString());
                }
            }
        }

        private void cb_preventive_CheckedChanged(object sender, EventArgs e)
        {
            built_Chart();
        }

        private void cb_spltDt_CheckedChanged(object sender, EventArgs e)
        {
            built_Chart();
        }

        private void cb_incCiblings_CheckedChanged(object sender, EventArgs e)
        {
            built_Chart();
        }

        private void cb_sortmode_SelectedValueChanged(object sender, EventArgs e)
        {
            built_Chart(true);
        }

        private void AssetStats_FormClosing(object sender, FormClosingEventArgs e)
        {
            chart1.FormatNumber -= chart1_FormatNumber;
            cb_sortmode.SelectedValueChanged -= new System.EventHandler(this.cb_sortmode_SelectedValueChanged);
            dataGridView1.RowEnter -= new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            bwInit.CancelAsync();
        }

        private void btn_viewData_Click(object sender, EventArgs e)
        {
            DataSet ldataset = new DataSet();
            try 
            {

                    if (dtGadata != null) { ldataset.Tables.Add(dtGadata); }
                    if (dtMaximoGraph != null) { ldataset.Tables.Add(dtMaximoGraph); }
                    if (dtMaximoGrid != null) { ldataset.Tables.Add(dtMaximoGrid); }
            }
            catch( Exception ex)
            {
                Debugger.Exeption(ex);
            }
            Forms.DataDisplay ldatadisplay = new Forms.DataDisplay(ldataset);
        }


    }
}
