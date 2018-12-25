using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.Alert.Models
{
    public class ControlChartMetadata
    {
    }

    public partial class ControlchartResult
    {
        public int id { get; set; }
        public string alarmobject { get; set; }
        public System.DateTime timestamp { get; set; }
        public double value { get; set; }
        public Nullable<double> UpperLimit { get; set; }
        public Nullable<double> LowerLimit { get; set; }
        public Nullable<int> l_controlLimits_id { get; set; }
        public string Comment { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<double> OptValue { get; set; } //optvalue gets rendered in an extra chart
        public Nullable<double> RefValue { get; set; } //Refvalue gets rendered in the same chart / same axis.

        public virtual l_controlLimits l_controlLimits { get; set; }
    }
}