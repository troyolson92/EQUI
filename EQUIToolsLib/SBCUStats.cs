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
using WPFChart3D;
using EQUIToolsLib;
using EQUICommunictionLib;

namespace EQUIToolsLib
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
        DataTable dt_DressData;
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
            //
            //
            cb_weldguns.Enabled = false;
            cb_weldguns.Items.Clear();
            cb_weldguns.Items.Insert(0, Location);
            cb_weldguns.SelectedIndex = 0;
            activeLocation = Location;
            //
            initchart();
            toolStripComboBoxSortMode.SelectedIndexChanged += new System.EventHandler(this.cb_sortmode_SelectedValueChanged);
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
            toolStripComboBoxSortMode.SelectedIndexChanged  += new System.EventHandler(this.cb_sortmode_SelectedValueChanged);
        }

        private void initchart()
        {
            //init cb for sort mode
            toolStripComboBoxSortMode.Items.Clear();
            toolStripComboBoxSortMode.Items.Insert(0, "Groupmode: None");
            toolStripComboBoxSortMode.Items.Insert(1, "Groupmode: Hours");
            toolStripComboBoxSortMode.Items.Insert(2, "Groupmode: Days");
            toolStripComboBoxSortMode.Items.Insert(3, "Groupmode: Weeks");
            toolStripComboBoxSortMode.SelectedIndex = 0; //default = no grouping 
            //************************************************************************************
            //init chart long sbcu
            //************************************************************************************
            chart_sbcu.Series.Add("LongSbcu");
            chart_sbcu.Series["LongSbcu"].XValueMember = "timestamp";
            chart_sbcu.Series["LongSbcu"].YValueMembers = "LongDsetup";
            chart_sbcu.Series["LongSbcu"].ChartType = SeriesChartType.Line;
            chart_sbcu.Series["LongSbcu"].XValueType = ChartValueType.DateTime;
            chart_sbcu.Series["LongSbcu"].BorderWidth = 3;
            chart_sbcu.Series["LongSbcu"].Color = Color.Red;
            chart_sbcu.Series["LongSbcu"].MarkerStyle = MarkerStyle.Circle;
            chart_sbcu.Series["LongSbcu"].MarkerColor = Color.Red;
            chart_sbcu.Series["LongSbcu"].MarkerSize = 8;
            chart_sbcu.Series["LongSbcu"].EmptyPointStyle.Color = Color.Red;
            chart_sbcu.Series["LongSbcu"].EmptyPointStyle.BorderWidth = 3;
            chart_sbcu.Series["LongSbcu"].EmptyPointStyle.AxisLabel = "Empty";
            //add series short sbcu
            chart_sbcu.Series.Add("ShortSbcu");
            chart_sbcu.Series["ShortSbcu"].XValueMember = "timestamp";
            chart_sbcu.Series["ShortSbcu"].YValueMembers = "ShortDsetup";
            chart_sbcu.Series["ShortSbcu"].ChartType = SeriesChartType.Line;
            chart_sbcu.Series["ShortSbcu"].XValueType = ChartValueType.DateTime;
            chart_sbcu.Series["ShortSbcu"].BorderWidth = 3;
            chart_sbcu.Series["ShortSbcu"].Color = Color.Blue;
            chart_sbcu.Series["ShortSbcu"].MarkerStyle = MarkerStyle.Square;
            chart_sbcu.Series["ShortSbcu"].MarkerColor = Color.Blue;
            chart_sbcu.Series["ShortSbcu"].MarkerSize = 8;
            chart_sbcu.Series["ShortSbcu"].EmptyPointStyle.Color = Color.Blue;
            chart_sbcu.Series["ShortSbcu"].EmptyPointStyle.BorderWidth = 3;
            chart_sbcu.Series["ShortSbcu"].EmptyPointStyle.AxisLabel = "Empty";
            //long UCL
            chart_sbcu.Series.Add("LongUCL");
            chart_sbcu.Series["LongUCL"].XValueMember = "timestamp";
            chart_sbcu.Series["LongUCL"].YValueMembers = "LongUCL";
            chart_sbcu.Series["LongUCL"].ChartType = SeriesChartType.Line;
            chart_sbcu.Series["LongUCL"].XValueType = ChartValueType.DateTime;
            chart_sbcu.Series["LongUCL"].BorderWidth = 2;
            chart_sbcu.Series["LongUCL"].Color = Color.Black;
            chart_sbcu.Series["LongUCL"].EmptyPointStyle.Color = Color.Black;
            chart_sbcu.Series["LongUCL"].EmptyPointStyle.BorderWidth = 2;
            chart_sbcu.Series["LongUCL"].EmptyPointStyle.AxisLabel = "Empty";
            //long LCL
            chart_sbcu.Series.Add("LongLCL");
            chart_sbcu.Series["LongLCL"].XValueMember = "timestamp";
            chart_sbcu.Series["LongLCL"].YValueMembers = "LongLCL";
            chart_sbcu.Series["LongLCL"].ChartType = SeriesChartType.Line;
            chart_sbcu.Series["LongLCL"].XValueType = ChartValueType.DateTime;
            chart_sbcu.Series["LongLCL"].BorderWidth = 2;
            chart_sbcu.Series["LongLCL"].Color = Color.Black;
            chart_sbcu.Series["LongLCL"].EmptyPointStyle.Color = Color.Black;
            chart_sbcu.Series["LongLCL"].EmptyPointStyle.BorderWidth = 2;
            chart_sbcu.Series["LongLCL"].EmptyPointStyle.AxisLabel = "Empty";
            //
            //chart_sbcu.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            chart_sbcu.ChartAreas[0].AxisX.Interval = 1;
            chart_sbcu.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart_sbcu.FormatNumber += chart_FormatNumberXaxis;
            chart_sbcu.GetToolTipText += chart_GetToolTipText;
            //************************************************************************************
            //init chart Cylinder
            //************************************************************************************
            chart_cilinder.Series.Add("TotalTime");
            chart_cilinder.Series["TotalTime"].XValueMember = "timestamp";
            chart_cilinder.Series["TotalTime"].YValueMembers = "TotalTime";
            chart_cilinder.Series["TotalTime"].ChartType = SeriesChartType.Line;
            chart_cilinder.Series["TotalTime"].XValueType = ChartValueType.DateTime;
            chart_cilinder.Series["TotalTime"].BorderWidth = 3;
            chart_cilinder.Series["TotalTime"].Color = System.Drawing.Color.Green;
            chart_cilinder.Series["TotalTime"].MarkerStyle = MarkerStyle.Circle;
            chart_cilinder.Series["TotalTime"].MarkerColor = Color.Green;
            chart_cilinder.Series["TotalTime"].MarkerSize = 8;
            //add series for UCL
            chart_cilinder.Series.Add("UCL");
            chart_cilinder.Series["UCL"].XValueMember = "timestamp";
            chart_cilinder.Series["UCL"].YValueMembers = "UCL";
            chart_cilinder.Series["UCL"].ChartType = SeriesChartType.Line;
            chart_cilinder.Series["UCL"].XValueType = ChartValueType.DateTime;
            chart_cilinder.Series["UCL"].BorderWidth = 2;
            chart_cilinder.Series["UCL"].Color = System.Drawing.Color.Black;
            //add series for LCL
            chart_cilinder.Series.Add("LCL");
            chart_cilinder.Series["LCL"].XValueMember = "timestamp";
            chart_cilinder.Series["LCL"].YValueMembers = "LCL";
            chart_cilinder.Series["LCL"].ChartType = SeriesChartType.Line;
            chart_cilinder.Series["LCL"].XValueType = ChartValueType.DateTime;
            chart_cilinder.Series["LCL"].BorderWidth = 2;
            chart_cilinder.Series["LCL"].Color = System.Drawing.Color.Black;
            //
            //chart_cilinder.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            chart_cilinder.ChartAreas[0].AxisX.Interval = 1;
            chart_cilinder.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart_cilinder.FormatNumber += chart_FormatNumberXaxis;
            chart_cilinder.GetToolTipText += chart_GetToolTipText;
            //************************************************************************************
            //init chart MidAir
            //************************************************************************************
            chart_midair.Series.Add("ResisActual");
            chart_midair.Series["ResisActual"].XValueMember = "timestamp";
            chart_midair.Series["ResisActual"].YValueMembers = "ResisActual";
            chart_midair.Series["ResisActual"].ChartType = SeriesChartType.Line;
            chart_midair.Series["ResisActual"].XValueType = ChartValueType.DateTime;
            chart_midair.Series["ResisActual"].BorderWidth = 3;
            chart_midair.Series["ResisActual"].Color = System.Drawing.Color.Green;
            //add series for reference value
            chart_midair.Series.Add("ResisRef");
            chart_midair.Series["ResisRef"].XValueMember = "timestamp";
            chart_midair.Series["ResisRef"].YValueMembers = "ResisRef";
            chart_midair.Series["ResisRef"].ChartType = SeriesChartType.Line;
            chart_midair.Series["ResisRef"].XValueType = ChartValueType.DateTime;
            chart_midair.Series["ResisRef"].BorderWidth = 2;
            chart_midair.Series["ResisRef"].Color = System.Drawing.Color.Black;
            //
            //chart_midair.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            chart_midair.ChartAreas[0].AxisX.Interval = 1;
            chart_midair.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart_midair.FormatNumber += chart_FormatNumberXaxis;
            chart_midair.GetToolTipText += chart_GetToolTipText;
            //************************************************************************************
            //init chart DressData
            //************************************************************************************
            //fixed wear
            chart_DressData.Series.Add("WearFixed");
            chart_DressData.Series["WearFixed"].XValueMember = "timestamp";
            chart_DressData.Series["WearFixed"].YValueMembers = "Wear_Fixed";
            chart_DressData.Series["WearFixed"].ChartType = SeriesChartType.StackedColumn;
            chart_DressData.Series["WearFixed"].XValueType = ChartValueType.DateTime;
            chart_DressData.Series["WearFixed"].BorderWidth = 3;
            chart_DressData.Series["WearFixed"].Color = Color.DarkOrange;
            chart_DressData.Series["WearFixed"].EmptyPointStyle.Color = Color.DarkOrange;
            chart_DressData.Series["WearFixed"].EmptyPointStyle.BorderWidth = 3;
            chart_DressData.Series["WearFixed"].EmptyPointStyle.AxisLabel = "Empty";
            //mov wear
            chart_DressData.Series.Add("WearMove");
            chart_DressData.Series["WearMove"].XValueMember = "timestamp";
            chart_DressData.Series["WearMove"].YValueMembers = "Wear_Move";
            chart_DressData.Series["WearMove"].ChartType = SeriesChartType.StackedColumn;
            chart_DressData.Series["WearMove"].XValueType = ChartValueType.DateTime;
            chart_DressData.Series["WearMove"].BorderWidth = 3;
            chart_DressData.Series["WearMove"].Color = Color.DarkMagenta;
            chart_DressData.Series["WearMove"].EmptyPointStyle.Color = Color.DarkMagenta;
            chart_DressData.Series["WearMove"].EmptyPointStyle.BorderWidth = 3;
            chart_DressData.Series["WearMove"].EmptyPointStyle.AxisLabel = "Empty";
            //nr dress
            chart_DressData.Series.Add("Dress_Num");
            chart_DressData.Series["Dress_Num"].XValueMember = "timestamp";
            chart_DressData.Series["Dress_Num"].YValueMembers = "Dress_Num";
            chart_DressData.Series["Dress_Num"].ChartType = SeriesChartType.Line;
            chart_DressData.Series["Dress_Num"].XValueType = ChartValueType.DateTime;
            chart_DressData.Series["Dress_Num"].BorderWidth = 3;
            chart_DressData.Series["Dress_Num"].Color = Color.Gray;
            chart_DressData.Series["Dress_Num"].EmptyPointStyle.Color = Color.Gray;
            chart_DressData.Series["Dress_Num"].EmptyPointStyle.BorderWidth = 3;
            chart_DressData.Series["Dress_Num"].EmptyPointStyle.AxisLabel = "Empty";
            chart_DressData.Series["Dress_Num"].YAxisType = AxisType.Secondary;
            //nr of welds
            /*
            chart_DressData.Series.Add("Weld_Counter");
            chart_DressData.Series["Weld_Counter"].XValueMember = "timestamp";
            chart_DressData.Series["Weld_Counter"].YValueMembers = "Weld_Counter";
            chart_DressData.Series["Weld_Counter"].ChartType = SeriesChartType.Line;
            chart_DressData.Series["Weld_Counter"].XValueType = ChartValueType.DateTime;
            chart_DressData.Series["Weld_Counter"].BorderWidth = 3;
            chart_DressData.Series["Weld_Counter"].Color = Color.Blue;
            chart_DressData.Series["Weld_Counter"].EmptyPointStyle.Color = Color.Blue;
            chart_DressData.Series["Weld_Counter"].EmptyPointStyle.BorderWidth = 3;
            chart_DressData.Series["Weld_Counter"].EmptyPointStyle.AxisLabel = "Empty";
            chart_DressData.Series["Weld_Counter"].YAxisType = AxisType.Secondary;
             */

            //
            chart_DressData.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
            chart_DressData.ChartAreas[0].AxisX.Interval = 1;
            chart_DressData.ChartAreas[0].AxisY.IsStartedFromZero = false;
            chart_DressData.FormatNumber += chart_FormatNumberXaxis;
            chart_DressData.GetToolTipText += chart_GetToolTipText;
            //************************************************************************************
            //
            bw.DoWork += bw_DoWork;
            bw.RunWorkerCompleted += bw_RunWorkerCompleted;
            //
            this.Show();
        }

        void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            set_trackbars();
            EnableEvents(true);
            trackBar1.Value = trackBar1.Value; //to trigger chart initalisation (I know its stupid) 
            /*
             * in standonlone oke maar in build in Excel crossthreading 
             mogelijks better om al de tools te launchen via    Application.Run(new SBCUStats(col["target"]));...
             * * */
        }

        void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            EnableEvents(false);
            GetChartData();
        }

        private void EnableEvents(Boolean enbl)
        {
           if (enbl)
           {
               trackBar1.ValueChanged += new System.EventHandler(trackBar_ValueChanged);
               trackBar2.ValueChanged += new System.EventHandler(trackBar_ValueChanged);
               toolStripComboBoxSortMode.SelectedIndexChanged += new System.EventHandler(this.cb_sortmode_SelectedValueChanged);
           }
           else
           {
               trackBar1.ValueChanged -= new System.EventHandler(trackBar_ValueChanged);
               trackBar2.ValueChanged -= new System.EventHandler(trackBar_ValueChanged);
               toolStripComboBoxSortMode.SelectedIndexChanged -= new System.EventHandler(this.cb_sortmode_SelectedValueChanged);
           }
        }


        private void GetChartData()
        {
            string qrySBCUShortLong = @"exec [EqUi].[GetSbcuData] @StartDate = '{0}',  @EndDate = '{1}', @Weldgunname = '{2}'";
            string qryCylinder =  @"exec [EqUi].[GetCilinderData] @StartDate = '{0}',  @EndDate = '{1}', @Weldgunname = '{2}'";
            string qryMidair =      @"exec [EqUi].[GetMidairData] @StartDate = '{0}',  @EndDate = '{1}', @Weldgunname = '{2}'";
            string qryDressData = @"exec [EqUi].[GetTipDressData] @StartDate = '{0}',  @EndDate = '{1}', @Weldgunname = '{2}'";
            //
            DateTime StartDate = System.DateTime.Now.AddDays(Daysback);
            DateTime EndDate = System.DateTime.Now;
            //
           // activeLocation = "321030WS01a"; //DEBUGGG
            //
            dt_SBCU = lGdataComm.RunQueryGadata(string.Format(qrySBCUShortLong, StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), activeLocation.Trim()));
            dt_SBCU = add_dummyTimestampRow(dt_SBCU, StartDate);
            dt_SBCU = add_dummyTimestampRow(dt_SBCU, EndDate);
            //
            dt_Cylinder = lGdataComm.RunQueryGadata(string.Format(qryCylinder, StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), activeLocation.Trim()));
            dt_Cylinder = add_dummyTimestampRow(dt_Cylinder, StartDate);
            dt_Cylinder = add_dummyTimestampRow(dt_Cylinder, EndDate);            
            //
            dt_MidAir = lGdataComm.RunQueryGadata(string.Format(qryMidair, StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), activeLocation.Trim()));
            dt_MidAir = add_dummyTimestampRow(dt_MidAir, StartDate);
            dt_MidAir = add_dummyTimestampRow(dt_MidAir, EndDate);           
            //
            dt_DressData = lGdataComm.RunQueryGadata(string.Format(qryDressData, StartDate.ToString("yyyy-MM-dd HH:mm:ss"), EndDate.ToString("yyyy-MM-dd HH:mm:ss"), activeLocation.Trim()));
            dt_DressData = add_dummyTimestampRow(dt_DressData, StartDate);
            dt_DressData = add_dummyTimestampRow(dt_DressData, EndDate);
            //
        }

        private DataTable add_dummyTimestampRow(DataTable dt, DateTime timestamp)
        {
            foreach (DataColumn col in dt.Columns)
            {
                col.AllowDBNull = true;
            }
            DataRow newrow = dt.NewRow();
            newrow["timestamp"] = timestamp;
            dt.Rows.Add(newrow);
            return dt;
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
        }

        private void link_chart(Chart lchart, DataTable ldt)
        {
            if (ldt == null) return;
            //use the trackbar to calculate the starting point of the graph
            DateTime GrapStart = DateTime.Now.AddDays(Convert.ToInt32(trackBar1.Value));
            DateTime GrapEnd = DateTime.Now.AddDays(Convert.ToInt32(trackBar2.Value));
            //
            var vldt = from a in ldt.AsEnumerable()
                           where a.Field<DateTime>("timestamp") > GrapStart && a.Field<DateTime>("timestamp") < GrapEnd
                           orderby a.Field<DateTime>("timestamp")
                           select a;
            lchart.DataSource = vldt;
            try
            {
                lchart.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        private void built_Chart(Boolean noAutoGrouping, Boolean bHideEmptyCharts) 
        {
            //disable events
            EnableEvents(false);
            //link datatbels to charts
            link_chart(chart_sbcu, dt_SBCU);
            link_chart(chart_cilinder, dt_Cylinder);
            link_chart(chart_midair, dt_MidAir);
            link_chart(chart_DressData, dt_DressData);
            //set autogrouping
            if (noAutoGrouping == false)
            {
                if ((trackBar1.Value - trackBar2.Value) > -10)
                {
                    toolStripComboBoxSortMode.SelectedIndex = 1;
                }
                else if ((trackBar1.Value - trackBar2.Value) > -100)
                {
                    toolStripComboBoxSortMode.SelectedIndex = 2;
                }
                else
                {
                    toolStripComboBoxSortMode.SelectedIndex = 3;
                }
            }

            switch (toolStripComboBoxSortMode.Text)
            {
                case "Groupmode: None":
                    chart_sbcu.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatTimestamp"; 
                    chart_sbcu.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.NotSet;  
                    
                    chart_cilinder.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatTimestamp"; 
                    chart_cilinder.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.NotSet; 
                    
                    chart_midair.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatTimestamp"; 
                    chart_midair.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.NotSet; 

                    chart_DressData.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatTimestamp";
                    chart_DressData.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.NotSet; 
                 
                    break;

                case "Groupmode: Hours":
                    chart_sbcu.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart_sbcu.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    
                    chart_cilinder.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart_cilinder.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                   
                    chart_midair.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour"; 
                    chart_midair.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;

                    chart_DressData.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatHour";
                    chart_DressData.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Hours, "LongSbcu");
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Hours, "ShortSbcu");
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Hours, "LongUCL");
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Hours, "LongLCL");
                    
                    chart_cilinder.DataManipulator.Group("AVE", 1, IntervalType.Hours, "TotalTime");
                    chart_cilinder.DataManipulator.Group("AVE", 1, IntervalType.Hours, "UCL");
                    chart_cilinder.DataManipulator.Group("AVE", 1, IntervalType.Hours, "LCL");
                    
                    chart_midair.DataManipulator.Group("AVE", 1, IntervalType.Hours, "ResisActual");
                    chart_midair.DataManipulator.Group("AVE", 1, IntervalType.Hours, "ResisRef");

                    chart_DressData.DataManipulator.Group("AVE", 1, IntervalType.Hours, "WearFixed");
                    chart_DressData.DataManipulator.Group("AVE", 1, IntervalType.Hours, "WearMove");

                    chart_DressData.DataManipulator.Group("MAX", 1, IntervalType.Hours, "Dress_Num");
                   // chart_DressData.DataManipulator.Group("MAX", 1, IntervalType.Hours, "Weld_Counter");
                    break;

                case "Groupmode: Days":
                    chart_sbcu.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay"; 
                    chart_sbcu.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    
                    chart_cilinder.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay";
                    chart_cilinder.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;
                    
                    chart_midair.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay";
                    chart_midair.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;

                    chart_DressData.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatDay";
                    chart_DressData.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Days;

                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Days, "LongSbcu");
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Days, "ShortSbcu");
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Days, "LongUCL");
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Days, "LongLCL");
                    
                    chart_cilinder.DataManipulator.Group("AVE", 1, IntervalType.Days, "TotalTime");
                    chart_cilinder.DataManipulator.Group("AVE", 1, IntervalType.Days, "UCL");
                    chart_cilinder.DataManipulator.Group("AVE", 1, IntervalType.Days, "LCL");
                    
                    chart_midair.DataManipulator.Group("AVE", 1, IntervalType.Days, "ResisActual");
                    chart_midair.DataManipulator.Group("AVE", 1, IntervalType.Days, "ResisRef");

                    chart_DressData.DataManipulator.Group("AVE", 1, IntervalType.Days, "WearFixed");
                    chart_DressData.DataManipulator.Group("AVE", 1, IntervalType.Days, "WearMove");

                    chart_DressData.DataManipulator.Group("MAX", 1, IntervalType.Days, "Dress_Num");
                    //chart_DressData.DataManipulator.Group("MAX", 1, IntervalType.Days, "Weld_Counter");
                    break;

                case "Groupmode: Weeks":
                    chart_sbcu.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek"; 
                    chart_sbcu.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    
                    chart_cilinder.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek";
                    chart_cilinder.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    
                    chart_midair.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek";
                    chart_midair.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;

                    chart_DressData.ChartAreas[0].AxisX.LabelStyle.Format = "CustomAxisXFormatWeek";
                    chart_DressData.ChartAreas[0].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "LongSbcu");
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "ShortSbcu");
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "LongUCL");
                    chart_sbcu.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "LongLCL");
                    
                    chart_cilinder.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "TotalTime");
                    chart_cilinder.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "UCL");
                    chart_cilinder.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "LCL");
                    
                    chart_midair.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "ResisActual");
                    chart_midair.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "ResisRef");

                    chart_DressData.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "WearFixed");
                    chart_DressData.DataManipulator.Group("AVE", 1, IntervalType.Weeks, "WearMove");

                    chart_DressData.DataManipulator.Group("MAX", 1, IntervalType.Weeks, "Dress_Num");
                    //chart_DressData.DataManipulator.Group("MAX", 1, IntervalType.Weeks, "Weld_Counter");
                    break;

                default:
                    break;
            }
            //
            if (bHideEmptyCharts) hideEmptyCharts();
            //
            SetXlabelOnForBottumChart();
            //
            metroProgressSpinner1.Visible = false;
            //
            EnableEvents(true);
            //
        }

        private void chart_FormatNumberXaxis(object sender, FormatNumberEventArgs e)
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

        private void trackBar_ValueChanged(object sender, EventArgs e)
        {
            built_Chart(false,true);
        }

        private void cb_sortmode_SelectedValueChanged(object sender, EventArgs e)
        {
            built_Chart(true,false);
        }

        private void btn_dataview_Click(object sender, EventArgs e)
        {
            DataSet ldataset = new DataSet();
            try
            {
                if (dt_SBCU != null) { dt_SBCU.TableName = "dt_SBCU"; ldataset.Tables.Add(dt_SBCU); }
                if (dt_Cylinder != null) { dt_Cylinder.TableName = "dt_Cylinder"; ldataset.Tables.Add(dt_Cylinder); }
                if (dt_MidAir != null) { dt_MidAir.TableName = "dt_MidAir"; ldataset.Tables.Add(dt_MidAir); }
                if (dt_DressData != null) { dt_DressData.TableName = "dt_DressData"; ldataset.Tables.Add(dt_DressData); }
            }
            catch (Exception ex)
            {
                Debugger.Exeption(ex);
            }
            DataDisplay ldatadisplay = new DataDisplay(ldataset);
        }

        private void cb_weldguns_DropDown(object sender, EventArgs e)
        {
            if (cb_weldguns.DataSource != null) return;
            string orgSelection = cb_weldguns.Text;
            string QryGetguns = "SELECT [WeldgunName] FROM [GADATA].[Volvo].[RobotWeldGunRelation] ";
                    cb_weldguns.DataSource = lGdataComm.RunQueryGadata(QryGetguns);
                    cb_weldguns.ValueMember = "WeldgunName";
                    cb_weldguns.DisplayMember = "WeldgunName";
                    cb_weldguns.Text = orgSelection;
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

 //launch SBCU 3D tool 
        private void btn_3dTemp_Click(object sender, EventArgs e)
        {
            //SBCU (dsetup in mm | LongSbcu = Red | ShortScbu = Blue)
            if (dt_SBCU == null) { Debugger.Message("dt_sbcu is null"); return; }
            DateTime GrapStart = DateTime.Now.AddDays(Convert.ToInt32(trackBar1.Value));
            DateTime GrapEnd = DateTime.Now.AddDays(Convert.ToInt32(trackBar2.Value));

            var ldt = dt_SBCU.Select(string.Format("[ToolX] is not null AND [ToolY] is not null AND [ToolZ] is not null AND [tool_timestamp] > '{0}' AND [tool_timestamp] < '{1}'", GrapStart, GrapEnd)).CopyToDataTable();

            if (ldt.Rows.Count < 1) { Debugger.Message("ldt has no data"); return; }

            WPFChart3D.Window1 lChar = new Window1(ldt);
            lChar.Show();
        }

//show hide charts 
        private void btn_chart_sbcu_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.RowStyles[2].Height == 0)
            {
                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[2].Height = 25;  //size = %   
            }
            else
            {
                tableLayoutPanel1.RowStyles[2].Height = 0;
            }
            built_Chart(false,false);
        }

        private void Btn_CharCilinder_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.RowStyles[4].Height == 0)
            {
                tableLayoutPanel1.RowStyles[4].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[4].Height = 25;  //size = %   
            }
            else
            {
                tableLayoutPanel1.RowStyles[4].Height = 0;
            }
            built_Chart(false,false);
        }

        private void Btn_ChartMidair_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.RowStyles[6].Height == 0)
            {
                tableLayoutPanel1.RowStyles[6].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[6].Height = 25;  //size = %   
            }
            else
            {
                tableLayoutPanel1.RowStyles[6].Height = 0;
            }
            built_Chart(false,false);
        }

        private void btn_ChartDressData_Click(object sender, EventArgs e)
        {
            if (tableLayoutPanel1.RowStyles[8].Height == 0)
            {
                tableLayoutPanel1.RowStyles[8].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[8].Height = 25;  //size = %   
            }
            else
            {
                tableLayoutPanel1.RowStyles[8].Height = 0;
            }
            built_Chart(false,false);
        }

        private void hideEmptyCharts()
        {
            if (chart_sbcu.Series[0].Points.Count == 0)
            {
                tableLayoutPanel1.RowStyles[2].Height = 0;
            }
            else
            {
                tableLayoutPanel1.RowStyles[2].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[2].Height = 25;  //size = %   
            }

            if (chart_cilinder.Series[0].Points.Count == 0)
            {
                tableLayoutPanel1.RowStyles[4].Height = 0;
            }
            else
            {
                tableLayoutPanel1.RowStyles[4].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[4].Height = 25;  //size = %   
            }

            if (chart_midair.Series[0].Points.Count == 0)
            {
                tableLayoutPanel1.RowStyles[6].Height = 0;
            }
            else
            {
                tableLayoutPanel1.RowStyles[6].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[6].Height = 25;  //size = %   
            }

          
            if (chart_DressData.Series[0].Points.Count == 0) 
            {
                tableLayoutPanel1.RowStyles[8].Height = 0;
            }
            else
            {
                tableLayoutPanel1.RowStyles[8].SizeType = SizeType.Percent;
                tableLayoutPanel1.RowStyles[8].Height = 25;  //size = %   
            }

        }

        private void SetXlabelOnForBottumChart()
        {
             chart_sbcu.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
             chart_cilinder.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
             chart_midair.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
             chart_DressData.ChartAreas[0].AxisX.LabelStyle.Enabled = false;

            if (tableLayoutPanel1.RowStyles[8].Height != 0)
            {
                chart_DressData.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
                return;
            }
            if (tableLayoutPanel1.RowStyles[6].Height != 0)
            {
                chart_midair.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
                return;
            }
            if (tableLayoutPanel1.RowStyles[4].Height != 0)
            {
                chart_cilinder.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
                return;
            }
            if (tableLayoutPanel1.RowStyles[2].Height != 0)
            {
                chart_sbcu.ChartAreas[0].AxisX.LabelStyle.Enabled = true;
                return;
            }
        }

        private void showHideLedendeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

    }
}
