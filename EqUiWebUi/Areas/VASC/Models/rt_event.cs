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
    
    public partial class rt_event
    {
        public int id { get; set; }
        public Nullable<int> event_enum { get; set; }
        public Nullable<int> c_controller_id { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<System.DateTime> abbDateTime { get; set; }
        public string description { get; set; }
        public Nullable<bool> isEvent { get; set; }
        public Nullable<int> event_value { get; set; }
    
        public virtual c_controller c_controller { get; set; }
    }
}
