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
    
    public partial class rt_csv_file
    {
        public int id { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<int> c_controller_id { get; set; }
        public Nullable<int> c_csv_log_id { get; set; }
        public Nullable<System.DateTime> lastDateRecord { get; set; }
        public Nullable<int> lastLineRead { get; set; }
        public Nullable<int> lastFileSize { get; set; }
        public Nullable<int> lastStreamPos { get; set; }
        public Nullable<int> lastLineRead_2 { get; set; }
        public Nullable<int> lastFileSize_2 { get; set; }
        public Nullable<int> lastStreamPos_2 { get; set; }
    
        public virtual c_controller c_controller { get; set; }
        public virtual c_csv_log c_csv_log { get; set; }
    }
}
