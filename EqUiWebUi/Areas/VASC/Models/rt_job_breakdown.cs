//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.VASC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class rt_job_breakdown
    {
        public int id { get; set; }
        public Nullable<int> rt_job_active_id { get; set; }
        public Nullable<int> h_alarm_id { get; set; }
        public Nullable<System.DateTime> ts_breakdownStart { get; set; }
        public Nullable<System.DateTime> ts_breakdownEnd { get; set; }
        public Nullable<System.DateTime> ts_breakdownAck { get; set; }
        public Nullable<int> ev_breakdownStart { get; set; }
        public Nullable<int> val_breakdownStart { get; set; }
        public Nullable<int> programPointer { get; set; }
        public Nullable<int> motionPointer { get; set; }
        public Nullable<int> ev_breakdownAck { get; set; }
        public Nullable<int> val_breakdownAck { get; set; }
        public string index { get; set; }
        public string phase1 { get; set; }
        public string phase2 { get; set; }
        public string phase3 { get; set; }
        public string phase4 { get; set; }
        public string phase5 { get; set; }
        public string phase6 { get; set; }
        public string phase7 { get; set; }
        public string phase8 { get; set; }
        public Nullable<int> rt_alarm_id { get; set; }
    
        public virtual rt_job rt_job { get; set; }
        public virtual h_alarm h_alarm { get; set; }
        public virtual rt_alarm rt_alarm { get; set; }
    }
}
