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
    
    public partial class ULRapportering
    {
        public int id { get; set; }
        public Nullable<System.DateTime> starttimePlan { get; set; }
        public Nullable<System.DateTime> EndTimePlan { get; set; }
        public string name { get; set; }
        public Nullable<int> buildbody { get; set; }
        public string CDSID { get; set; }
        public string Ullaptop { get; set; }
        public Nullable<int> bodynr { get; set; }
        public Nullable<int> Worklocation { get; set; }
        public Nullable<int> DurationStoring { get; set; }
        public string userComment { get; set; }
        public Nullable<int> Vyear { get; set; }
        public Nullable<int> Vweek { get; set; }
        public Nullable<int> vday { get; set; }
        public string Shift { get; set; }
        public Nullable<int> spot { get; set; }
        public Nullable<bool> loose_ { get; set; }
    }
}
