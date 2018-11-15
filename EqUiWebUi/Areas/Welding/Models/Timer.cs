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
    
    public partial class Timer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Timer()
        {
            this.L_operation = new HashSet<L_operation>();
            this.rt_active_info = new HashSet<rt_active_info>();
            this.rt_alarm = new HashSet<rt_alarm>();
            this.rt_communication_state = new HashSet<rt_communication_state>();
            this.rt_job = new HashSet<rt_job>();
            this.rt_job1 = new HashSet<rt_job>();
            this.rt_weldfault = new HashSet<rt_weldfault>();
            this.rt_weldmeasureprotddw = new HashSet<rt_weldmeasureprotddw>();
        }
    
        public int ID { get; set; }
        public string Name { get; set; }
        public Nullable<int> NptId { get; set; }
        public string Robot { get; set; }
        public string location { get; set; }
        public Nullable<int> c_timer_class_id { get; set; }
        public Nullable<int> enable_bit { get; set; }
    
        public virtual NPT NPT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<L_operation> L_operation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_active_info> rt_active_info { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_alarm> rt_alarm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_communication_state> rt_communication_state { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_job> rt_job { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_job> rt_job1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_weldfault> rt_weldfault { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_weldmeasureprotddw> rt_weldmeasureprotddw { get; set; }
    }
}
