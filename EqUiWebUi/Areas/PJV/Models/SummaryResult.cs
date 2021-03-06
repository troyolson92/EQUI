//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.PJV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SummaryResult
    {
        public int id { get; set; }
        public Nullable<int> isDead { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<System.DateTime> C_lasttimestamp { get; set; }
        public Nullable<int> numberPrograms { get; set; }
        public Nullable<int> numberRoutines { get; set; }
        public int numberPointInstructions { get; set; }
        public Nullable<double> AvgDX { get; set; }
        public Nullable<double> AvgDY { get; set; }
        public Nullable<double> AvgDZ { get; set; }
        public Nullable<double> AvgDelta { get; set; }
        public Nullable<double> StdDelta { get; set; }
        public Nullable<int> numberOfWarnings { get; set; }
        public Nullable<int> numberOfProcessDataWarnings { get; set; }
        public Nullable<int> robotDetailId { get; set; }
        public string comment { get; set; }
        public Nullable<int> rev_created { get; set; }
        public Nullable<int> rev_modified { get; set; }
        public Nullable<int> numberMatchingJPDPP { get; set; }
        public Nullable<double> maxDX { get; set; }
        public Nullable<double> maxDY { get; set; }
        public Nullable<double> maxDZ { get; set; }
    
        public virtual RobotDetail RobotDetail { get; set; }
    }
}
