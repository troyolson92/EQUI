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
    
    public partial class rt_device_info
    {
        public int id { get; set; }
        public Nullable<int> c_controller_id { get; set; }
        public Nullable<int> c_device_info_id { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public string value { get; set; }
    
        public virtual c_controller c_controller { get; set; }
        public virtual c_device_info c_device_info { get; set; }
    }
}
