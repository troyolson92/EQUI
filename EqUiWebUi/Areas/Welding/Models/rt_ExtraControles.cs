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
    
    public partial class rt_ExtraControles
    {
        public int ID { get; set; }
        public string statusadaptieve { get; set; }
        public string model { get; set; }
        public string robot { get; set; }
        public Nullable<int> spot { get; set; }
        public string plan { get; set; }
        public Nullable<int> plannummer { get; set; }
        public string typecontrole { get; set; }
        public Nullable<System.DateTime> time_uitadaptief { get; set; }
        public Nullable<System.TimeSpan> durationOFF { get; set; }
        public Nullable<System.DateTime> endtime { get; set; }
        public Nullable<System.DateTime> lastinspectiontime { get; set; }
        public Nullable<int> planid { get; set; }
        public string NPTOwner { get; set; }
        public string NPT { get; set; }
    }
}