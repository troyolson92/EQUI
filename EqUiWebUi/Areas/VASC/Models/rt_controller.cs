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
    
    public partial class rt_controller
    {
        public int id { get; set; }
        public int c_controller_id { get; set; }
        public string Availability { get; set; }
        public string BaseDirectory { get; set; }
        public string ControllerName { get; set; }
        public string HostName { get; set; }
        public string Controller_Id { get; set; }
        public string IPaddress { get; set; }
        public Nullable<int> IsVirtual { get; set; }
        public string MacAddress { get; set; }
        public string Name { get; set; }
        public string RunLevel { get; set; }
        public string SystemId { get; set; }
        public string SystemName { get; set; }
        public string Version { get; set; }
        public string VersionName { get; set; }
        public Nullable<int> WebServicesPort { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
    
        public virtual c_controller c_controller { get; set; }
    }
}
