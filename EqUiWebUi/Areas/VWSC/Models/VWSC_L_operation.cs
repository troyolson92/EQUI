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
    
    public partial class VWSC_L_operation
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<int> code { get; set; }
        public string Vwsc_name { get; set; }
        public Nullable<int> controller_id { get; set; }
        public string Description { get; set; }
        public Nullable<int> npt_id { get; set; }
        public Nullable<int> timer_id { get; set; }
    
        public virtual VWSC_c_timer c_timer { get; set; }
        public virtual VWSC_c_timer c_timer1 { get; set; }
    }
}
