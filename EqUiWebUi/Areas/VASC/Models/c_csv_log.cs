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
    
    public partial class c_csv_log
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_csv_log()
        {
            this.rt_csv_file = new HashSet<rt_csv_file>();
        }
    
        public int id { get; set; }
        public int enable_bit { get; set; }
        public string csv_filename { get; set; }
        public string logcount_variable { get; set; }
        public string rt_table { get; set; }
        public int poll_rate { get; set; }
        public string comment { get; set; }
        public string Tempnote { get; set; }
        public int flags { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_csv_file> rt_csv_file { get; set; }
    }
}
