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
    
    public partial class c_controller
    {
        public int id { get; set; }
        public string controller_name { get; set; }
        public Nullable<int> enable_bit { get; set; }
        public string systemId { get; set; }
        public string ip { get; set; }
        public Nullable<int> class_id { get; set; }
        public Nullable<int> flags { get; set; }
        public string LocationTree { get; set; }
        public string Assetnum { get; set; }
        public string ProductionTeam { get; set; }
        public string ResponsiblePloeg { get; set; }
        public string ClassificationTree { get; set; }
        public string CLassificationId { get; set; }
    
        public virtual c_controller_class c_controller_class { get; set; }
    }
}
