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

        GadataComm lGdataComm = new GadataComm();
        DataTable dt_longSBCU;
        DataTable dt_shortSBCU;

        public SBCUStats(string Location)
        {
            InitializeComponent();

            Location = "32070ws02a";

            this.Text = string.Format("SBCUStats Location: {0}",Location);
            //
            cb_sortmode.Items.Clear();
            cb_sortmode.Items.Insert(0, "None");
            cb_sortmode.Items.Insert(1, "Hours");
            cb_sortmode.Items.Insert(2, "Days");
            cb_sortmode.Items.Insert(3, "Weeks");
            cb_sortmode.SelectedIndex = 0; //default = no grouping 
            //
            //init chart long sbcu
            chart1.Series.Add("DeltaSetup");
            chart1.Series["DeltaSetup"].XValueMember = "tool_timestamp";
            chart1.Series["DeltaSetup"].YValueMembers = "Dsetup";
            chart1.Series["DeltaSetup"].ChartType = SeriesChartType.Line;
            chart1.Series[0].XValueType = ChartValueType.DateTime;
            chart1.Series["DeltaSetup"].BorderWidth = 3;
            chart1.ChartAreas[0].AxisX.Interval = 1;
            
            chart1.FormatNumber += chart1_FormatNumber;
            chart1.Series["DeltaSetup"].Color = System.Drawing.Color.Red;
            //init chart short sbcu
            chart2.Series.Add("DeltaSetup");
            chart2.Series["DeltaSetup"].XValueMember = "tool_timestamp";
            chart2.Series["DeltaSetup"].YValueMembers = "Dsetup";
            chart2.Series["DeltaSetup"].ChartType = SeriesChartType.Line;
            chart2.Series[0].XValueType = ChartValueType.DateTime;
            chart2.Series["DeltaSetup"].BorderWidth = 3;
            chart2.ChartAreas[0].AxisX.Interval = 1;
            chart2.FormatNumber += chart1_FormatNumber;
            chart2.Series["DeltaSetup"].Color = System.Drawing.Color.Blue;

            //query all instances of the error 
            #region Query
            string qrySBCUShortLong =  @"
--timeparameters
   DECLARE 
   @StartDate as DATETIME = null,
   @EndDate as DATETIME = null,
--Filterparameters.
   @Robot as varchar(25) = '57050r03%',
   @Tool as varchar(25) = 'Tool: 1',
   @Weldgunname as varchar(25) = '{1}'


---------------------------------------------------------------------------------------
--Als er een weldgun name word gebruikt zoeken we de juiste robot en tool id op..
---------------------------------------------------------------------------------------

if (@Weldgunname is not null)
BEGIN
SET @Robot = (SELECT TOP 1 '%' + rws.Robot + '%' from GADATA.volvo.RobotWeldGunRelation as rws where rws.WeldgunName LIKE @Weldgunname)
SET @Tool = (SELECT TOP 1 '%Tool: ' + CAST(rws.ElectrodeNbr as varchar(2)) + '%' from GADATA.volvo.RobotWeldGunRelation as rws where rws.WeldgunName LIKE @Weldgunname)
END

---------------------------------------------------------------------------------------
--Set default values of start and end date
---------------------------------------------------------------------------------------
if ((@StartDate is null) OR (@StartDate = '1900-01-01 00:00:00:000'))
BEGIN
SET @StartDate = GETDATE()-300
END

if ((@EndDate is null) OR (@EndDate = '1900-01-01 00:00:00:000'))
BEGIN
SET @EndDate = GETDATE()
END


SELECT 
 sbcu.RobotName
,sbcu.tool_id
,sbcu.tool_timestamp 
,sbcu.Dsetup 

FROM gadata.c3g.sbcudata as sbcu 
WHERE
sbcu.Longcheck = {0}
AND
sbcu.tool_timestamp between   @startdate and @EndDate 
AND
sbcu.Robotname LIKE @Robot
AND
sbcu.tool_id LIKE @Tool  

UNION
SELECT 
 null
,null
,getdate() as 'tool_timestamp' 
,null
                ";
            #endregion
            //fill dataset with all errors
            dt_longSBCU = lGdataComm.RunQueryGadata(string.Format(qrySBCUShortLong,1,Location.Trim()));
            dt_shortSBCU = lGdataComm.RunQueryGadata(string.Format(qrySBCUShortLong, 0,Location.Trim()));
            //check if the result was valid 
            if (dt_longSBCU.Rows.Count == 0) { Debugger.Message("The query long did not return a valid result"); this.Dispose(); return; };
            if (dt_shortSBCU.Rows.Count == 0) { Debugger.Message("The query short did not return a valid result"); this.Dispose(); return; };
            //setup trackbar (trackbar maximum = first time error happend, minium = now)
            DateTime First = (from a in dt_longSBCU.AsEnumerable() select a.Field<DateTime>("tool_timestamp")).Min();
            DateTime Last = (from a in dt_longSBCU.AsEnumerable() select a.Field<DateTime>("tool_timestamp")).Max();
            trackBar1.Minimum = (int)(First - Last).TotalDays;
            if (trackBar1.Minimum > -3) {trackBar1.Minimum = -3;}
            trackBar1.Maximum = -1; //minimum display = 1 day 
            trackBar1.Value = trackBar1.Minimum;
            //set startup view to last 30 days of data.
            if (trackBar1.Minimum < -30) { trackBar1.Value = -30; }
            //
            trackBar2.Minimum = trackBar1.Minimum;
            trackBar2.Maximum = trackBar1.Maximum;
            trackBar2.Value = trackBar2.Maximum;
            //build trend chart in init mode. 
            built_Chart(true);
            //
            cb_sortmode.SelectedValueChanged += new System.EventHandler(this.cb_sortmode_SelectedValueChanged);
            //
            this.Show();
        }

        private void built_Chart(Boolean noAutoGrouping) 
        {
            //use the trackbar to calculate the starting point of the graph
            //use the trackbar to calculate the starting point of the graph
            DateTime GrapStart = DateTime.Now.AddDays(Convert.ToInt32(trackBar1.Value));
            DateTime GrapEnd = DateTime.Now.AddDays(Convert.ToInt32(trackBar2.Value));
            //
            var ldt_longSBCU = from a in dt_longSBCU.AsEnumerable()
                               where a.Field<DateTime>("tool_timestamp") > GrapStart && a.Field<DateTime>("tool_timestamp") < GrapEnd
                               orderby a.Field<DateTime>("tool_timestamp")
                               select a;
            chart1.DataSource = ldt_longSBCU;
            chart1.DataBind();
            //
            var ldt_ShortSBCU = from a in dt_shortSBCU .AsEnumerable()
                                where a.Field<DateTime>("tool_timestamp") > GrapStart && a.Field<DateTime>("tool_timestamp") < GrapEnd
                               orderby  a.Field<DateTime>("tool_timestamp")
                               select a;
            chart2.DataSource = ldt_ShortSBCU;
            chart2.DataBind();
            //
            DateTime FirstError = (from a in dt_longSBCU.AsEnumerable() select a.Field<DateTime>("tool_timestamp")).Min();
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
            //check that manual grouping is fasable
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
                    chart2.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatTimestamp"; 
                    chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.NotSet;                    
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "NoGrouping");
                    break;

                case "Hours":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    chart2.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupHourmode");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Hours, "DeltaSetup");
                    chart2.DataManipulator.Group("AVE", 1, IntervalType.Hours, "DeltaSetup");
                    break;

                case "Days":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay"; 
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    chart2.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay"; 
                    chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupDaymode");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Days, "DeltaSetup");
                    chart2.DataManipulator.Group("AVE", 1, IntervalType.Days, "DeltaSetup");
                    break;

                case "Weeks":
                    chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek"; 
                    chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    chart2.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek"; 
                    chart2.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    label1.Text = string.Format("First error: {0} ", FirstError);
                    label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupWeekmode");
                    chart1.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "DeltaSetup");
                    chart2.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "DeltaSetup");
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

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            built_Chart(false);
        }

        private void cb_sortmode_SelectedValueChanged(object sender, EventArgs e)
        {
            built_Chart(true);
        }

    }
}
