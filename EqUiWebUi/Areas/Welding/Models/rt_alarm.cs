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
    
    public partial class rt_alarm
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public rt_alarm()
        {
            this.rt_job_breakdown = new HashSet<rt_job_breakdown1>();
        }
    
        public int id { get; set; }
        public Nullable<int> timerId { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<int> protRecord_ID { get; set; }
        public Nullable<System.DateTime> dateTime { get; set; }
        public Nullable<int> errorCode1 { get; set; }
        public string errorCode1_txt { get; set; }
        public Nullable<int> errorCode2 { get; set; }
        public string errorCode2_txt { get; set; }
        public Nullable<bool> isError { get; set; }
        public string isError_txt { get; set; }
        public string comment { get; set; }
    
        public virtual c_timer c_timer { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_job_breakdown1> rt_job_breakdown { get; set; }
    }
}
