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
    
    public partial class rt_alarm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rt_alarm()
        {
            this.rt_job_breakdown = new HashSet<rt_job_breakdown>();
        }
    
        public int id { get; set; }
        public Nullable<int> controller_id { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<System.DateTime> error_timestamp { get; set; }
        public Nullable<int> sequenceNumber { get; set; }
        public Nullable<int> number { get; set; }
        public Nullable<int> categoryId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string consequences { get; set; }
        public string causes { get; set; }
        public string actions { get; set; }
        public string type { get; set; }
    
        public virtual c_controller c_controller { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_job_breakdown> rt_job_breakdown { get; set; }
    }
}
