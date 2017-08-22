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

namespace EQUIToolsLib
{
	public partial class ErrorStats : MetroFramework.Forms.MetroForm
	{
		//debugger
		myDebugger Debugger = new myDebugger();

		GadataComm lGdataComm = new GadataComm();
		DataTable dt;

		public ErrorStats(string Location, string Logtype, string Errornum, string logtekst)
		{
			InitializeComponent();
			this.Text = string.Format("ErrorStats tool Errornum: {0} <{2}> Location: {1}", Errornum,Location,logtekst.Trim());
			//
			cb_sortmode.Items.Clear();
			cb_sortmode.Items.Insert(0, "None");
			cb_sortmode.Items.Insert(1, "Hours");
			cb_sortmode.Items.Insert(2, "Days");
			cb_sortmode.Items.Insert(3, "Weeks");
			//init chart 
			chart1.Series.Add("ErrorCount");
			chart1.Series["ErrorCount"].XValueMember = "starttime";
			chart1.Series["ErrorCount"].YValueMembers = "count";
			chart1.Series["ErrorCount"].ChartType = SeriesChartType.Column;
			chart1.Series["ErrorCount"]["PixelPointWidth"] = "15"; //makes the with of the bars stable
			chart1.Series["ErrorCount"].XValueType = ChartValueType.DateTime;
			chart1.ChartAreas[0].AxisX.Interval = 1;
			chart1.FormatNumber += chart1_FormatNumber;
			//query all instances of the error 
			string qry = string.Format(
			@"EXEC [EqUi].[GetErrorTrentData] @Location = '{0}' ,@ERRORNUM = {1} ,@Logtext = '{2}' ,@logType = '{3}'"
			, Location, Errornum, logtekst,Logtype);
			//fill dataset with all errors
			dt = lGdataComm.RunQueryGadata(qry);
			//check if the result was valid 
			if (dt.Rows.Count == 0) { Debugger.Message("The query for this errorcode did not return a valid result"); this.Dispose(); return; };
			//setup trackbar (trackbar maximum = first time error happend, minium = now)
			DateTime FirstError = (from a in dt.AsEnumerable() select a.Field<DateTime>("starttime")).Min();
			DateTime LastError = (from a in dt.AsEnumerable() select a.Field<DateTime>("starttime")).Max();
			trackBar1.Minimum = (int)(FirstError - LastError).TotalDays;
			if (trackBar1.Minimum > -3) {trackBar1.Minimum = -3;}
			trackBar1.Maximum = -1; //minimum display = 1 day 
			//figure out init mode
			/*this will figure out of "active" the error is, 
			 * if it happens more than 10 times in the last 3 days => set graph to 3 day range (show data in hours)
			 *               more than 10 times in the last 30 days => set graph to 30 day range (show data in days)
			 *               else set to full 'lifecycle' of error and set graph in week mode
			 */
			var count3 = from a in dt.AsEnumerable()
					  where a.Field<DateTime>("starttime") >  DateTime.Now.AddDays(Convert.ToInt32(3) * -1)
					  select a;
			if (count3.Count() > 10) //more than 10 times in 3 days
			{
				if (trackBar1.Minimum > -3)
				{
					trackBar1.Value = -3;
				}
				else
				{
					trackBar1.Value = trackBar1.Minimum;
				}
				chart1.Series["ErrorCount"].Color = System.Drawing.Color.Red;
			}
			else
			{
				var count30 = from a in dt.AsEnumerable()
							 where a.Field<DateTime>("starttime") > DateTime.Now.AddDays(Convert.ToInt32(30) * -1)
							 select a;
				if (count30.Count() > 10) //more than 10 times in a month
				{
					if (trackBar1.Minimum > -30)
					{
						trackBar1.Value = -30;
					}
					else
					{
						trackBar1.Value = trackBar1.Minimum;
					}
					chart1.Series["ErrorCount"].Color = System.Drawing.Color.DarkOrange;
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
					chart1.Series["ErrorCount"].Color = System.Drawing.Color.Blue;
				}
			}
			//build trend chart in init mode. 
			buildTrendChart();
			this.Show();
		}

		private void buildTrendChart()
		{ buildTrendChart(false); }

		private void buildTrendChart(bool noAutoGrouping) 
		{
			//use the trackbar to calculate the starting point of the graph
			DateTime GrapStart = DateTime.Now.AddDays(Convert.ToInt32(trackBar1.Value));
			//
			var ldt = from a in dt.AsEnumerable()
					  where a.Field<DateTime>("starttime") > GrapStart
					  select a;
			chart1.DataSource = ldt;
			chart1.DataBind();
			//
			DateTime FirstError = (from a in dt.AsEnumerable() select a.Field<DateTime>("starttime")).Min();
			//
			if (noAutoGrouping == false)
			{
				if ((trackBar1.Value - 0) > -10)
				{
					cb_sortmode.SelectedIndex = 1;
				}
				else if ((trackBar1.Value - 0) > -100)
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
					chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
					label1.Text = string.Format("First error: {0} ", FirstError);
					label2.Text = string.Format("'Now'  DisplayMode:{0}", "NoGrouping");
					break;

				case "Hours":
					chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
					chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
					chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
					label1.Text = string.Format("First error: {0} ", FirstError);
					label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupHourmode");
					chart1.DataManipulator.Group("SUM", 1, IntervalType.Hours, "ErrorCount");
					break;

				case "Days":
					chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay"; 
					chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
					chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
					label1.Text = string.Format("First error: {0} ", FirstError);
					label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupDaymode");
					chart1.DataManipulator.Group("SUM", 1, IntervalType.Days, "ErrorCount");
					break;

				case "Weeks":
					chart1.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek"; 
					chart1.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
					chart1.ChartAreas[0].AxisX.IntervalOffset = 1;
					label1.Text = string.Format("First error: {0} ", FirstError);
					label2.Text = string.Format("'Now'  DisplayMode:{0}", "GroupWeekmode");
					chart1.DataManipulator.Group("SUM", 1, IntervalType.Weeks, "ErrorCount");
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
			buildTrendChart();
		}

		private void cb_sortmode_SelectedIndexChanged(object sender, EventArgs e)
		{
			buildTrendChart(true);
		}

	}
}
