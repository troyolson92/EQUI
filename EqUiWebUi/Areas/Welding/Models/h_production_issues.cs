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
    
    public partial class h_production_issues
    {
        public int id { get; set; }
        public int spotid { get; set; }
        public System.DateTime startdate { get; set; }
        public System.DateTime enddate { get; set; }
        public int reporterId { get; set; }
        public int issueId { get; set; }
        public Nullable<int> quantity { get; set; }
        public string lastBodyNbr { get; set; }
        public string remarks { get; set; }
    
        public virtual c_production_issue c_production_issue { get; set; }
        public virtual c_reporter c_reporter { get; set; }
        public virtual rt_spottable rt_spottable { get; set; }
    }
}
