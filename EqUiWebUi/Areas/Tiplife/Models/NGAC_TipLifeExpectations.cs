//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.Tiplife.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class NGAC_TipLifeExpectations
    {
        public string controller_name { get; set; }
        public string LocationTree { get; set; }
        public short Tool_Nr { get; set; }
        public Nullable<double> avgESTnSpotsFixedWearBefore100 { get; set; }
        public Nullable<double> avgESTnSpotsMoveWearBefore100 { get; set; }
        public Nullable<int> avgWeldPerDress { get; set; }
        public Nullable<int> TotWearComponent { get; set; }
        public Nullable<double> maxWearInCalc { get; set; }
        public Nullable<double> minWearInCalc { get; set; }
        public Nullable<int> countWearInCalc { get; set; }
        public Nullable<System.DateTime> LastTipchange { get; set; }
        public Nullable<double> Last_FixedWearBeforeChange { get; set; }
        public Nullable<double> Last_MovWearBeforeChange { get; set; }
        public Nullable<double> avgDeltaNomAfterchange { get; set; }
        public Nullable<double> avgPartsFixedBefore100 { get; set; }
        public Nullable<double> avgPartsMoveBefore100 { get; set; }
        public int refid { get; set; }
    }
}
