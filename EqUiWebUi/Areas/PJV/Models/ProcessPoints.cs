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
    
    public partial class ProcessPoints
    {
        public string robotname { get; set; }
        public string process_s { get; set; }
        public string p_name { get; set; }
        public string s_name { get; set; }
        public string v_name { get; set; }
        public Nullable<int> programLine { get; set; }
        public Nullable<System.DateTime> DetectTimestamp { get; set; }
        public Nullable<double> x { get; set; }
        public Nullable<double> y { get; set; }
        public Nullable<double> z { get; set; }
        public Nullable<int> isDead { get; set; }
        public Nullable<int> rev_modified { get; set; }
        public Nullable<int> previousRev { get; set; }
        public Nullable<long> RevisionCount { get; set; }
        public Nullable<System.DateTime> C_previoustimestamp { get; set; }
        public Nullable<double> previousX { get; set; }
        public Nullable<double> previousY { get; set; }
        public Nullable<double> previousZ { get; set; }
        public int refID { get; set; }
        public Nullable<double> Delta { get; set; }
        public Nullable<System.DateTime> FileTimestamp { get; set; }
        public string locationtree { get; set; }
    }
}
