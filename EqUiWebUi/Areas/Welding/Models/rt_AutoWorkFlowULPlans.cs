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
    
    public partial class rt_AutoWorkFlowULPlans
    {
        public int ID { get; set; }
        public Nullable<System.DateTime> LastinspectionTime { get; set; }
        public string UL_Plan { get; set; }
        public Nullable<int> BuildBody { get; set; }
        public Nullable<System.TimeSpan> Worktime { get; set; }
        public Nullable<int> WorkGroup { get; set; }
        public string WorkLocation { get; set; }
        public Nullable<int> BodyLast30minStart { get; set; }
        public Nullable<int> BodyLast30minEnd { get; set; }
        public string Last30min_productionStatus { get; set; }
        public string onderhoudswerken { get; set; }
        public Nullable<int> LastnumberFromPlanBusy { get; set; }
        public Nullable<int> Planlenght { get; set; }
        public Nullable<int> PreviousPlace { get; set; }
        public Nullable<int> SpotsinExtraControleList { get; set; }
    }
}
