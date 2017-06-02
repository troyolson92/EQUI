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

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class SBCUStats : MetroFramework.Forms.MetroForm
    {
        //debugger
        Debugger Debugger = new Debugger();
        //communication module
        GadataComm lGdataComm = new GadataComm();
        //tabels
        DataTable dt_SBCU;
        DataTable dt_Cylinder;
        DataTable dt_MidAir;


        public SBCUStats(string Location)
        {
            InitializeComponent();
            cb_weldguns.Text = Location;
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
            //init 2nd serie short sbcu
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
            //
            chart1.ChartAreas[0].AxisX.Interval = 1;
            chart1.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart1.FormatNumber += chart1_FormatNumber;
            chart1.GetToolTipText += chart_GetToolTipText;
            //init chart Cylinder
            chart3.Series.Add("TotalTime");
            chart3.Series["TotalTime"].XValueMember = "_timestamp";
            chart3.Series["TotalTime"].YValueMembers = "TotalTime";
            chart3.Series["TotalTime"].ChartType = SeriesChartType.Line;
            chart3.Series[0].XValueType = ChartValueType.DateTime;
            chart3.Series["TotalTime"].BorderWidth = 3;
            chart3.ChartAreas[0].AxisX.Interval = 1;
            chart3.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chart3.FormatNumber += chart1_FormatNumber;
            chart3.GetToolTipText += chart_GetToolTipText;
            chart3.Series["TotalTime"].Color = System.Drawing.Color.Green;
            //init chart MidAir
            chart4.Series.Add("ResisActual");
            chart4.Series["ResisActual"].XValueMember = "timestamp";
            chart4.Series["ResisActual"].YValueMembers = "ResisActual";
            chart4.Series["ResisActual"].ChartType = SeriesChartType.Line;
            chart4.Series[0].XValueType = ChartValueType.DateTime;
            chart4.Series["ResisActual"].BorderWidth = 3;
            chart4.ChartAreas[0].AxisX.Interval = 1;
            chart4.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
            chart4.FormatNumber += chart1_FormatNumber;
            chart4.GetToolTipText += chart_GetToolTipText;
            chart4.Series["ResisActual"].Color = System.Drawing.Color.Black ;
            //get data to build the chart
          //  GetChartData();
            //build trend chart in init mode. 
          //  built_Chart(true);
            //
            cb_sortmode.SelectedValueChanged += new System.EventHandler(this.cb_sortmode_SelectedValueChanged);
            //
            this.Show();
        }

        private void GetChartData()
        {
            //query all instances of the error 
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

SELECT *, sbcu.Dsetup as 'LongDsetup', null as 'ShortDsetup' FROM gadata.c3g.sbcudata as sbcu 
WHERE
sbcu.Longcheck = 1
AND
sbcu.tool_timestamp between   @startdate and @EndDate 
AND
sbcu.Robotname LIKE @Robot
AND
sbcu.tool_id LIKE @Tool

UNION  
SELECT *, null as 'LongDsetup', sbcu.Dsetup as 'ShortDsetup' FROM gadata.c3g.sbcudata as sbcu 
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
,null,null,null,null,null,null,null,null,null,null,null,null
,null,null,null,null,null,null,null,null,null,null,null,null
UNION
SELECT 
@EndDate as 'tool_timestamp' 
,null,null,null,null,null,null,null,null,null,null,null,null
,null,null,null,null,null,null,null,null,null,null,null,null
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
            //fill dataset with all errors
            int Daysback = -80;
            DateTime StartDate = System.DateTime.Now.AddDays(Daysback);
            DateTime EndDate = System.DateTime.Now;
            //
            dt_SBCU = lGdataComm.RunQueryGadata(string.Format(qrySBCUShortLong, StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), cb_weldguns.Text.Trim()));
            dt_Cylinder = lGdataComm.RunQueryGadata(string.Format(qryCylinder, StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), cb_weldguns.Text.Trim()));
            dt_MidAir = lGdataComm.RunQueryGadata(string.Format(qryMidair,  StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), cb_weldguns.Text.Trim()));    
            //setup trackbar (trackbar maximum = first time error happend, minium = now)
            trackBar1.Minimum = (int)(StartDate - EndDate).TotalDays;
            if (trackBar1.Minimum > -3) { trackBar1.Minimum = -3; }
            trackBar1.Maximum = -1; //minimum display = 1 day 
            //set startup view to last 30 days of data.
            if (trackBar1.Minimum < -30) { trackBar1.Value = -30; }
            else {trackBar1.Value = trackBar1.Minimum; }
            // set up 2nd trackbar
            trackBar2.Minimum = trackBar1.Minimum;
            trackBar2.Maximum = trackBar1.Maximum;
            trackBar2.Value = trackBar2.Maximum;
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
            DateTime FirstError = (from a in dt_SBCU.AsEnumerable() select a.Field<DateTime>("tool_timestamp")).Min();
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
            //check that manual grouping is fesable
            if ((cb_sortmode.SelectedIndex < 2) && ((trackBar1.Value - trackBar2.Value) < -60))
            {
                Debugger.Message(string.Format("Its not a good idea to group in this way for '{0}' days of data", (trackBar1.Value - trackBar2.Value)));
                cb_sortmode.SelectedIndex = 3;
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
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "NoGrouping");
                    break;

                case "Hours":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    chart3.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    chart4.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart4.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupHourmode");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Hours, "LongSbcu");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Hours, "ShortSbcu");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Hours, "TotalTime");
                    chart4.DataManipulator.Group("AVE", 1, IntervalType.Hours, "ResisActual");
                    break;

                case "Days":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay"; 
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    chart3.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay";
                    chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    chart4.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay";
                    chart4.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupDaymode");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Days, "LongSbcu");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Days, "ShortSbcu");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Days, "TotalTime");
                    chart4.DataManipulator.Group("AVE", 1, IntervalType.Days, "ResisActual");
                    break;

                case "Weeks":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek"; 
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    chart3.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek";
                    chart3.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    chart4.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek";
                    chart4.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupWeekmode");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "LongSbcu");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "ShortSbcu");
                    chart3.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "TotalTime");
                    chart4.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "ResisActual");
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
           e.Text = string.Format("X:\t{0}\nY:\t{1}", dataPoint.XValue, dataPoint.YValues[0]);
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
                cb_weldguns.SelectedIndexChanged += new System.EventHandler(cb_weldguns_SelectedIndexChanged);
            }
        }

        private void cb_weldguns_SelectedIndexChanged(object sender, EventArgs e)
        {
            cb_weldguns.Enabled = false;
            //get data to build the chart
            GetChartData();
            //build trend chart in init mode. 
            built_Chart(true);
            cb_weldguns.Enabled = true;
        }

    }
}
