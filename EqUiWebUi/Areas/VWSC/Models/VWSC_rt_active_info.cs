//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.VWSC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class VWSC_rt_active_info
    {
        public int id { get; set; }
        public Nullable<int> timerId { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<bool> online { get; set; }
        public Nullable<byte> communicationState { get; set; }
        public Nullable<int> errorNumber { get; set; }
        public string errorText { get; set; }
        public Nullable<int> rt_alarm_id { get; set; }
        public Nullable<int> rt_job_id { get; set; }
        public Nullable<int> vwsc_state { get; set; }
        public Nullable<int> rt_weldfault_id { get; set; }
        public Nullable<int> rt_weldmeasureprotddw_id { get; set; }
        public Nullable<int> rt_datechangeprot_id { get; set; }
    
        public virtual VWSC_c_Timer Timer { get; set; }
    }
}