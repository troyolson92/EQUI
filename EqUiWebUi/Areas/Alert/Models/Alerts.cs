//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.Alert.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Alerts
    {
        public string Location { get; set; }
        public string AssetID { get; set; }
        public string Logtype { get; set; }
        public System.DateTime timestamp { get; set; }
        public string Logcode { get; set; }
        public string Severity { get; set; }
        public string Logtext { get; set; }
        public string FullLogtext { get; set; }
        public Nullable<int> Response { get; set; }
        public Nullable<int> Downtime { get; set; }
        public string Classification { get; set; }
        public string Subgroup { get; set; }
        public string Category { get; set; }
        public int refId { get; set; }
        public string LocationTree { get; set; }
        public Nullable<int> ClassTree { get; set; }
        public string controller_name { get; set; }
        public string controller_type { get; set; }
        public Nullable<int> Vyear { get; set; }
        public Nullable<int> Vweek { get; set; }
        public Nullable<int> Vday { get; set; }
        public string shift { get; set; }
    }
}
