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
    
    public partial class VWSC_rt_datachangeprot
    {
        public int id { get; set; }
        public Nullable<int> timerId { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<int> protRecord_ID { get; set; }
        public Nullable<System.DateTime> dateTime { get; set; }
        public Nullable<int> param_ID { get; set; }
        public string param_status_txt { get; set; }
        public Nullable<int> subIndex { get; set; }
        public string oldValue { get; set; }
        public string oldValue_txt { get; set; }
        public string newValue { get; set; }
        public string newValue_txt { get; set; }
        public string oldNormValue { get; set; }
        public string newNormValue { get; set; }
        public Nullable<int> physicalUnitId { get; set; }
        public string physicalUnitId_txt { get; set; }
        public string computerName { get; set; }
        public string userName { get; set; }
        public string comment { get; set; }
    
        public virtual VWSC_c_timer c_timer { get; set; }
        public virtual VWSC_c_timer_class c_timer_class { get; set; }
    }
}
