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
    
    public partial class rt_paramvalues
    {
        public int id { get; set; }
        public Nullable<int> timerId { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<int> subindex { get; set; }
        public string value { get; set; }
        public Nullable<int> c_bosch_param_id { get; set; }
        public Nullable<int> isDead { get; set; }
    
        public virtual c_bosch_param c_bosch_param { get; set; }
        public virtual c_timer c_timer { get; set; }
    }
}