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
using EQUICommunictionLib;
using WPFChart3D;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class SBCUStats : MetroFramework.Forms.MetroForm
    {
        //debugger
        myDebugger Debugger = new myDebugger();
        //communication module
        GadataComm lGdataComm = new GadataComm();
        //tabels
        DataTable dt_SBCU;
        DataTable dt_Cylinder;
        DataTable dt_MidAir;
        //bw
        BackgroundWorker bw = new BackgroundWorker();
        //settings
        int Daysback = -200; //load 200 days data in buffer.
        int InitDays = -50; //show 50 days on startup.
        //
        string activeLocation;

        public SBCUStats(string Location)
        {
            InitializeComponent();
            cb_weldguns.Enabled = false;
            cb_weldguns.Items.Clear();
            cb_weldguns.Items.Insert(0, Location);
            cb_weldguns.SelectedIndex = 0;
            activeLocation = Location;
            //
            initchart();
            cb_sortmode.SelectedValueChanged += new System.EventHandler(this.cb_sortmode_SelectedValueChanged);
            //
            metroProgressSpinner1.Visible = true;
            bw.RunWorkerAsync();
            //
        }

        public SBCUStats()
        {
            InitializeComponent();
            cb_weldguns.Enabled = true;
            initchart();
            cb_sortmode.SelectedValueChanged += new System.EventHandler(this.cb_sortmode_SelectedValueChanged);
        }

        private void initchart()
        {
            //
            cb_sortmode.Items.Clear();
            cb_sortmode.Items.Insert(0, "None");
            cb_sortmode.Items.Insert(1, "Hours");
            cb_sortmode.Items.Insert(2, "Days");
            cb_sortmode.Items.Insert(3, "Weeks");
            cb_sortmode.SelectedIndex = 0; //default = no grouping 
            //
            //init chart long sbcu
            chart1.Series.Add("LongSbcu");
            chart1.Series["LongSbcu"].XValueMember = "tool_timestamp";
            chart1.Series["LongSbcu"].YValueMembers = "LongDsetup";
            chart1.Series["LongSbcu"].ChartType = SeriesChartType.Line;
            chart1.Series["LongSbcu"].XValueType = ChartValueType.DateTime;
            chart1.Series["LongSbcu"].BorderWidth = 3;
            chart1.Series["LongSbcu"].Color = Color.Red;
            chart1.Series["LongSbcu"].MarkerStyle = MarkerStyle.Circle;
            chart1.Series["LongSbcu"].MarkerColor = Color.Red;
            chart1.Series["LongSbcu"].MarkerSize = 8;
            chart1.Series["LongSbcu"].EmptyPointStyle.Color = Color.Red;
            chart1.Series["LongSbcu"].EmptyPointStyle.BorderWidth = 3;
            chart1.Series["LongSbcu"].EmptyPointStyle.AxisLabel = "Empty";
            //add series short sbcu
            chart1.Series.Add("ShortSbcu");
            chart1.Series["ShortSbcu"].XValueMember = "tool_timestamp";
            chart1.Series["ShortSbcu"].YValueMembers = "ShortDsetup";
            chart1.Series["ShortSbcu"].ChartType = SeriesChartType.Line;
            chart1.Series["ShortSbcu"].XValueType = ChartValueType.DateTime;
            chart1.Series["ShortSbcu"].BorderWidth = 3;
            chart1.Series["ShortSbcu"].Color = Color.Blue;
            chart1.Series["ShortSbcu"].MarkerStyle = MarkerStyle.Square;
            chart1.Series["ShortSbcu"].MarkerColor = Color.Blue;
            chart1.Series["ShortSbcu"].MarkerSize = 8;
            chart1.Series["ShortSbcu"].EmptyPointStyle.Color = Color.Blue;
            chart1.Series["ShortSbcu"].EmptyPointStyle.BorderWidth = 3;
            chart1.Series["ShortSbcu"].EmptyPointStyle.AxisLabel = "Empty";
            //long UCL
            chart1.Series.Add("LongUCL");
            chart1.Series["LongUCL"].XValueMember = "tool_timestamp";
            chart1.Series["LongUCL"].YValueMembers = "LongUCL";
            chart1.Series["LongUCL"].ChartType = SeriesChartType.Line;
            chart1.Series["LongUCL"].XValueType = ChartValueType.DateTime;
            chart1.Series["LongUCL"].BorderWidth = 2;
            chart1.Series["LongUCL"].Color = Color.Black;
            chart1.Series["LongUCL"].EmptyPointStyle.Color = Color.Black;
            chart1.Series["LongUCL"].EmptyPointStyle.BorderWidth = 2;
            chart1.Series["LongUCL"].EmptyPointStyle.AxisLabel = "Empty";
            //long LCL
            chart1.Series.Add("LongLCL");
            chart1.Series["LongLCL"].XValueMember = "tool_timestamp";
            chart1.Series["LongLCL"].YValueMembers = "LongLCL";
            chart1.Series["LongLCL"].ChartType = SeriesChartType.Line;
            chart1.Series["LongLCL"].XValueType = ChartValueType.DateTime;
            chart1.Series["LongLCL"].BorderWidth = 2;
            chart1.Series["LongLCL"].Color = Color.Black;
            chart1.Series["LongLCL"].EmptyPointStyle.Color = Color.Black;
            chart1.Series["LongLCL"].EmptyPointStyle.BorderWidth = 2;
            chart1.Series["LongLCL"].EmptyPointStyle.AxisLabel = "Empty";
            //
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart1.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart1.FormatNumber += chart1_FormatNumber;
            chart1.GetToolTipText += chart_GetToolTipText;
            //init chart Cylinder
            chart3.Series.Add("TotalTime");
            chart3.Series["TotalTime"].XValueMember = "_timestamp";
            chart3.Series["TotalTime"].YValueMembers = "TotalTime";
            chart3.Series["TotalTime"].ChartType = SeriesChartType.Line;
            chart3.Series["TotalTime"].XValueType = ChartValueType.DateTime;
            chart3.Series["TotalTime"].BorderWidth = 3;
            chart3.Series["TotalTime"].Color = System.Drawing.Color.Green;
            chart3.Series["TotalTime"].MarkerStyle = MarkerStyle.Circle;
            chart3.Series["TotalTime"].MarkerColor = Color.Green;
            chart3.Series["TotalTime"].MarkerSize = 8;
            //add series for UCL
            chart3.Series.Add("UCL");
            chart3.Series["UCL"].XValueMember = "_timestamp";
            chart3.Series["UCL"].YValueMembers = "UCL";
            chart3.Series["UCL"].ChartType = SeriesChartType.Line;
            chart3.Series["UCL"].XValueType = ChartValueType.DateTime;
            chart3.Series["UCL"].BorderWidth = 2;
            chart3.Series["UCL"].Color = System.Drawing.Color.Black;
            //add series for LCL
            chart3.Series.Add("LCL");
            chart3.Series["LCL"].XValueMember = "_timestamp";
            chart3.Series["LCL"].YValueMembers = "LCL";
            chart3.Series["LCL"].ChartType = SeriesChartType.Line;
            chart3.Series["LCL"].XValueType = ChartValueType.DateTime;
            chart3.Series["LCL"].BorderWidth = 2;
            chart3.Series["LCL"].Color = System.Drawing.Color.Black;
            //
            chart3.ChartAreas[0].AxisX.Interval = 1;
            chart3.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart3.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart3.FormatNumber += chart1_FormatNumber;
            chart3.GetToolTipText += chart_GetToolTipText;
            //init chart MidAir
            chart4.Series.Add("ResisActual");
            chart4.Series["ResisActual"].XValueMember = "timestamp";
            chart4.Series["ResisActual"].YValueMembers = "ResisActual";
            chart4.Series["ResisActual"].ChartType = SeriesChartType.Line;
            chart4.Series["ResisActual"].XValueType = ChartValueType.DateTime;
            chart4.Series["ResisActual"].BorderWidth = 3;
            chart4.Series["ResisActual"].Color = System.Drawing.Color.Green;
            //add series for reference value
            chart4.Series.Add("ResisRef");
            chart4.Series["ResisRef"].XValueMember = "timestamp";
            chart4.Series["ResisRef"].YValueMembers = "ResisRef";
            chart4.Series["ResisRef"].ChartType = SeriesChartType.Line;
            chart4.Series["ResisRef"].XValueType = ChartValueType.DateTime;
            chart4.Series["ResisRef"].BorderWidth = 2;
            chart4.Series["ResisRef"].Color = System.Drawing.Color.Black;
            //
            chart4.ChartAreas[0].AxisX.Interval = 1;
            chart4.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            chart4.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart4.FormatNumber += chart1_FormatNumber;
            chart4.GetToolTipText += chart_GetToolTipText;
            //
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            //
            this.Show();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            set_trackbars();
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            GetChartData();
        }

        private void GetChartData()
        {
            #region Query
            string qrySBCUShortLong = @"
   DECLARE 
   @StartDate as DATETIME = '{0}',
   @EndDate as DATETIME = '{1}',
--Filterparameters.
   @Robot as varchar(25) = null,
   @Tool as varchar(25) = null,
   @Weldgunname as varchar(25) = '{2}'

---------------------------------------------------------------------------------------
--Als er een weldgun name word gebruikt zoeken we de juiste robot en tool id op..
---------------------------------------------------------------------------------------

if (@Weldgunname is not null)
BEGIN
SET @Robot = (SELECT TOP 1 '%' + rws.Robot + '%' from GADATA.volvo.RobotWeldGunRelation as rws where rws.WeldgunName LIKE @Weldgunname)
SET @Tool = (SELECT TOP 1 '%Tool: ' + CAST(rws.ElectrodeNbr as varchar(2)) + '%' from GADATA.volvo.RobotWeldGunRelation as rws where rws.WeldgunName LIKE @Weldgunname)
END

SELECT 
*
, sbcu.Dsetup as 'LongDsetup'
, null as 'ShortDsetup' 
, sbcu.UCL as 'LongUCL'
, sbcu.LCL as 'LongLCL'
,null as 'ShortUCL'
,null as 'ShortLCL'
FROM gadata.c3g.sbcudata as sbcu 
WHERE
sbcu.Longcheck = 1
AND
sbcu.tool_timestamp between   @startdate and @EndDate 
AND
sbcu.Robotname LIKE @Robot
AND
sbcu.tool_id LIKE @Tool

UNION  
SELECT 
*
, null as 'LongDsetup'
, sbcu.Dsetup as 'ShortDsetup' 
, null as 'LongUCL'
, null as 'LongLCL'
,sbcu.UCL  as 'ShortUCL'
,sbcu.LCL  as 'ShortLCL'

FROM gadata.c3g.sbcudata as sbcu 
WHERE
sbcu.Longcheck = 0
AND
sbcu.tool_timestamp between   @startdate and @EndDate 
AND
sbcu.Robotname LIKE @Robot
AND
sbcu.tool_id LIKE @Tool  


UNION
SELECT 
@StartDate as 'tool_timestamp' 
,null,null,null,null,null,null,null,null,null,null,null,null,null,null
,null,null,null,null,null,null,null,null,null,null,null,null,null,null
UNION
SELECT 
@EndDate as 'tool_timestamp' 
,null,null,null,null,null,null,null,null,null,null,null,null,null,null
,null,null,null,null,null,null,null,null,null,null,null,null,null,null
                ";
            string qryCylinder = @"
   DECLARE 
   @StartDate as DATETIME = '{0}',
   @EndDate as DATETIME = '{1}',
--Filterparameters.
   @Robot as varchar(25) = null,
   @Tool as varchar(25) = null,
   @Weldgunname as varchar(25) = '{2}'

---------------------------------------------------------------------------------------
--Als er een weldgun name word gebruikt zoeken we de juiste robot en tool id op..
---------------------------------------------------------------------------------------

if (@Weldgunname is not null)
BEGIN
SET @Robot = (SELECT TOP 1 '%' + rws.Robot + '%' from GADATA.volvo.RobotWeldGunRelation as rws where rws.WeldgunName LIKE @Weldgunname)
SET @Tool = (SELECT TOP 1 '%Tool: ' + CAST(rws.ElectrodeNbr as varchar(2)) + '%' from GADATA.volvo.RobotWeldGunRelation as rws where rws.WeldgunName LIKE @Weldgunname)
END

SELECT * FROM [GADATA].[C3G].[WeldGunCylinder]
WHERE
[WeldGunCylinder]._timestamp between   @startdate and @EndDate 
AND
[WeldGunCylinder].[controller_name] LIKE @Robot
AND
'%Tool: ' + CAST([WeldGunCylinder].tool_id as varchar(2)) LIKE @Tool  
UNION
SELECT 
null,null,null
,@StartDate as '_timestamp'
,null,null,null,null,null,null,null,null,null
,null,null,null,null,null,null,null  
UNION
SELECT 
null,null,null
,@EndDate as '_timestamp'
,null,null,null,null,null,null,null,null,null
,null,null,null,null,null,null,null  
";

            string qryMidair = @"
   DECLARE 
   @StartDate as DATETIME = '{0}',
   @EndDate as DATETIME = '{1}',
--Filterparameters.
   @Robot as varchar(25) = null,
   @Tool as varchar(25) = null,
   @Weldgunname as varchar(25) = '{2}'


---------------------------------------------------------------------------------------
--Als er een weldgun name word gebruikt zoeken we de juiste robot en tool id op..
---------------------------------------------------------------------------------------

if (@Weldgunname is not null)
BEGIN
SET @Robot = (SELECT TOP 1 '%' + rws.Robot + '%' from GADATA.volvo.RobotWeldGunRelation as rws where rws.WeldgunName LIKE @Weldgunname)
SET @Tool = (SELECT TOP 1 '%Tool: ' + CAST(rws.ElectrodeNbr as varchar(2)) + '%' from GADATA.volvo.RobotWeldGunRelation as rws where rws.WeldgunName LIKE @Weldgunname)
END

SELECT [Robotname]
      ,[Tool]
      ,[timestamp]
      ,[SpotId]
      ,[ResisActual]
      ,[ResisRef]
  FROM [GADATA].[dbo].[MidairRef]

WHERE
[MidairRef].[timestamp] between   @startdate and @EndDate 
AND
[MidairRef].[Robotname] LIKE @Robot
AND
[MidairRef].[Tool] LIKE @Tool 

UNION
SELECT null ,null
      , @EndDate as 'timestamp'
      ,null, null, null
UNION
SELECT null ,null
      , @StartDate as 'timestamp'
      ,null, null, null
         
";
            #endregion
            //
            DateTime StartDate = System.DateTime.Now.AddDays(Daysback);
            DateTime EndDate = System.DateTime.Now;
            //
            dt_SBCU = lGdataComm.RunQueryGadata(string.Format(qrySBCUShortLong, StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), activeLocation.Trim()));
            dt_Cylinder = lGdataComm.RunQueryGadata(string.Format(qryCylinder, StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), activeLocation.Trim()));
            dt_MidAir = lGdataComm.RunQueryGadata(string.Format(qryMidair, StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), activeLocation.Trim()));    
            //
        }

        private void set_trackbars()
        {
            //
            DateTime StartDate = System.DateTime.Now.AddDays(Daysback);
            DateTime EndDate = System.DateTime.Now;
            //setup trackbar (trackbar maximum = first time error happend, minium = now)
            trackBar1.Minimum = (int)(StartDate - EndDate).TotalDays;
            if (trackBar1.Minimum > -3) { trackBar1.Minimum = -3; }
            trackBar1.Maximum = -1; //minimum display = 1 day 
            //set startup view to last 30 days of data.
            if (trackBar1.Minimum < InitDays) { trackBar1.Value = InitDays; }
            else { trackBar1.Value = trackBar1.Minimum; }
            // set up 2nd trackbar
            trackBar2.Minimum = trackBar1.Minimum;
            trackBar2.Maximum = trackBar1.Maximum;
            trackBar2.Value = trackBar2.Maximum;
            //
            trackBar1.ValueChanged += new System.EventHandler(trackBar1_ValueChanged);
            trackBar2.ValueChanged += new System.EventHandler(trackBar1_ValueChanged);

        }

        private void built_Chart(Boolean noAutoGrouping) 
        {
            //use the trackbar to calculate the starting point of the graph
            DateTime GrapStart = DateTime.Now.AddDays(Convert.ToInt32(trackBar1.Value));
            DateTime GrapEnd = DateTime.Now.AddDays(Convert.ToInt32(trackBar2.Value));
            //
            var ldt_SBCU = from a in dt_SBCU.AsEnumerable()
                               where a.Field<DateTime>("tool_timestamp") > GrapStart && a.Field<DateTime>("tool_timestamp") < GrapEnd
                               orderby a.Field<DateTime>("tool_timestamp")
                               select a;
            chart1.DataSource = ldt_SBCU;
            chart1.DataBind();
            //
                var ldt_Cylinder = from a in dt_Cylinder.AsEnumerable()
                                   where a.Field<DateTime>("_timestamp") > GrapStart && a.Field<DateTime>("_timestamp") < GrapEnd
                                   orderby a.Field<DateTime>("_timestamp")
                                   select a;
                chart3.DataSource = ldt_Cylinder;
                chart3.DataBind();
            //
                var ldt_Midair = from a in dt_MidAir.AsEnumerable()
                                 where a.Field<DateTime>("timestamp") > GrapStart && a.Field<DateTime>("timestamp") < GrapEnd
                                 orderby a.Field<DateTime>("timestamp")
                                 select a;
                chart4.DataSource = ldt_Midair;
                chart4.DataBind();
            //
            if (noAutoGrouping == false)
            {
                if ((trackBar1.Value - trackBar2.Value) > -10)
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

            switch (cb_sortmode.Text)
            {
                case "None":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatTimestamp"; 
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.NotSet;   
                    chart3.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatTimestamp"; 
                    chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.NotSet; 
                    chart4.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatTimestamp"; 
                    chart4.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.NotSet; 
                    break;

                case "Hours":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    chart3.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    chart4.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart4.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Hours, "LongSbcu");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Hours, "ShortSbcu");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Hours, "LongUCL");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Hours, "LongLCL");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Hours, "TotalTime");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Hours, "UCL");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Hours, "LCL");
                    chart4.DataManipulator.Group("AVE", 1, IntervalType.Hours, "ResisActual");
                    chart4.DataManipulator.Group("AVE", 1, IntervalType.Hours, "ResisRef");
                    break;

                case "Days":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay"; 
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    chart3.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay";
                    chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    chart4.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay";
                    chart4.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Days, "LongSbcu");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Days, "ShortSbcu");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Days, "LongUCL");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Days, "LongLCL");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Days, "TotalTime");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Days, "UCL");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Days, "LCL");
                    chart4.DataManipulator.Group("AVE", 1, IntervalType.Days, "ResisActual");
                    chart4.DataManipulator.Group("AVE", 1, IntervalType.Days, "ResisRef");
                    break;

                case "Weeks":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek"; 
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    chart3.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek";
                    chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    chart4.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek";
                    chart4.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "LongSbcu");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "ShortSbcu");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "LongUCL");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "LongLCL");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "TotalTime");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "UCL");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "LCL");
                    chart4.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "ResisActual");
                    chart4.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "ResisRef");
                    break;

                default:
                    break;
            }
            //here because of cross threding
            metroProgressSpinner1.Visible = false;
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
                        DateTime.FromOADate(e.Value).Year %100,
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
                        DateTime.FromOADate(e.Value).Year %100,
                        currentCalendar.GetWeekOfYear(DateTime.FromOADate(e.Value),System.Globalization.CalendarWeekRule.FirstFourDayWeek,CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek),
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

        private void chart_GetToolTipText(object sender, ToolTipEventArgs e)
  {
     // Check selected chart element and set tooltip text for it
     switch (e.HitTestResult.ChartElementType)
     {
        case ChartElementType.DataPoint:
           var dataPoint = e.HitTestResult.Series.Points[e.HitTestResult.PointIndex];

           e.Text = string.Format(
@"Datatype: {0}
Timestamp: {1}
Value: {2:0.00}"
            , e.HitTestResult.Series.Name, DateTime.FromOADate(dataPoint.XValue).ToString("yyyy-MM-dd HH:mm:ss"), dataPoint.YValues[0]);
           break;
     }
  }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            built_Chart(false);
        }

        private void cb_sortmode_SelectedValueChanged(object sender, EventArgs e)
        {
            built_Chart(true);
        }

        private void btn_dataview_Click(object sender, EventArgs e)
        {
            DataSet ldataset = new DataSet();
            try
            {
                if (dt_SBCU != null) { dt_SBCU.TableName = "dt_SBCU"; ldataset.Tables.Add(dt_SBCU); }
                if (dt_Cylinder != null) { dt_Cylinder.TableName = "dt_Cylinder"; ldataset.Tables.Add(dt_Cylinder); }
                if (dt_MidAir != null) { dt_MidAir.TableName = "dt_MidAir"; ldataset.Tables.Add(dt_MidAir); }
            }
            catch (Exception ex)
            {
                Debugger.Exeption(ex);
            }
            Forms.DataDisplay ldatadisplay = new Forms.DataDisplay(ldataset);
        }

        private void cb_weldguns_DropDown(object sender, EventArgs e)
        {
            string orgSelection = cb_weldguns.Text;
            if (cb_weldguns.DataSource == null)
            {
                string QryGetguns = "select  WeldgunName from GADATA.volvo.RobotWeldGunRelation where RobotType = 'c3g'";
                cb_weldguns.DataSource = lGdataComm.RunQueryGadata(QryGetguns);
                cb_weldguns.ValueMember = "WeldgunName"; 
                cb_weldguns.DisplayMember = "WeldgunName";
                cb_weldguns.Text = orgSelection;
            }
        }

        private void cb_weldguns_DropDownClosed(object sender, EventArgs e)
        {
            cb_weldguns.Enabled = false;
            activeLocation = cb_weldguns.Text;
            //get data to build the chart
            metroProgressSpinner1.Visible = true;
            bw.RunWorkerAsync();
            //
            cb_weldguns.Enabled = true;
        }

        private void metroLabel1_DoubleClick(object sender, EventArgs e)
        {
            if (dt_SBCU == null) { Debugger.Message("dt_sbcu is null"); return; }
            DateTime GrapStart = DateTime.Now.AddDays(Convert.ToInt32(trackBar1.Value));
            DateTime GrapEnd = DateTime.Now.AddDays(Convert.ToInt32(trackBar2.Value));

            var ldt = dt_SBCU.Select(string.Format("[ToolX] is not null AND [ToolY] is not null AND [ToolZ] is not null AND [tool_timestamp] > '{0}' AND [tool_timestamp] < '{1}'", GrapStart, GrapEnd)).CopyToDataTable();

            if (ldt.Rows.Count < 1) { Debugger.Message("ldt has no data"); return; }

            WPFChart3D.Window1 lChar = new Window1(ldt);
            lChar.Show();
        }

    }
}
