//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.Tiplife.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VWSC_TipMonitor
    {
        public string Robot { get; set; }
        public int Tool_Nr { get; set; }
        public System.DateTime Date_time { get; set; }
        public Nullable<int> nDress { get; set; }
        public Nullable<int> nWelds { get; set; }
        public Nullable<int> WearRatio { get; set; }
        public Nullable<int> pWear { get; set; }
        public Nullable<int> nRspots { get; set; }
        public Nullable<int> nRcars { get; set; }
        public Nullable<int> MaxBodyEndOfLife { get; set; }
        public Nullable<int> SortCol { get; set; }
        public Nullable<int> TipAge_h_ { get; set; }
        public Nullable<int> LastTipchange { get; set; }
        public Nullable<int> Time_DressCycleTime { get; set; }
        public int id { get; set; }
        public string LocationTree { get; set; }
        public Nullable<int> hasTipchanger { get; set; }
        public Nullable<int> MagicFiXedWear { get; set; }
        public string Status { get; set; }
        public Nullable<int> RobotWear { get; set; }
    }
}