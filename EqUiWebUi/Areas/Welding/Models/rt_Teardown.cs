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
    
    public partial class rt_Teardown
    {
        public int id { get; set; }
        public int c_Teardown_id { get; set; }
        public Nullable<System.DateTime> measdate { get; set; }
        public string Comment { get; set; }
        public Nullable<decimal> measdiameter { get; set; }
        public Nullable<int> rt_job_id { get; set; }
        public string NuggetDemand { get; set; }
        public string TearDownNugget { get; set; }
        public string TeardownStatus { get; set; }
        public string TeardownEvalution { get; set; }
        public string TDTComment { get; set; }
        public Nullable<int> c_faultcode_id { get; set; }
    
        public virtual c_faultcode c_faultcode { get; set; }
        public virtual c_Teardown c_Teardown { get; set; }
        public virtual rt_Job1 rt_Job { get; set; }
    }
}
