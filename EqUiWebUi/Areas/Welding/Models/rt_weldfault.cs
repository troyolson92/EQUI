//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.Welding.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class rt_weldfault
    {
        public int id { get; set; }
        public Nullable<int> timerId { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<int> protRecord_ID { get; set; }
        public Nullable<System.DateTime> dateTime { get; set; }
        public Nullable<int> progNo { get; set; }
        public Nullable<int> monitorState { get; set; }
        public string monitorState_txt { get; set; }
        public Nullable<int> regulationState { get; set; }
        public string regulationState_txt { get; set; }
        public Nullable<int> measureState { get; set; }
        public string measureState_txt { get; set; }
        public Nullable<float> weldProgValue { get; set; }
        public Nullable<float> weldActualValue { get; set; }
        public Nullable<decimal> wear { get; set; }
        public Nullable<int> rt_spot_id { get; set; }
        public Nullable<bool> isError { get; set; }
        public string WMComment { get; set; }
    
        public virtual c_timer c_timer { get; set; }
    }
}
