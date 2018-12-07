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
    
    public partial class c_timer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_timer()
        {
            this.L_operation = new HashSet<L_operation>();
            this.rt_active_info = new HashSet<rt_active_info>();
            this.rt_alarm = new HashSet<rt_alarm>();
            this.rt_communication_state = new HashSet<rt_communication_state>();
            this.rt_datachangeprot = new HashSet<rt_datachangeprot>();
            this.rt_job1 = new HashSet<rt_job11>();
            this.rt_paramvalues = new HashSet<rt_paramvalues>();
            this.rt_spottable = new HashSet<rt_spottable>();
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
        public string LocationTree { get; set; }
        public string Assetnum { get; set; }
        public string ResponsibleWeldMaster { get; set; }
        public string Station { get; set; }
        public string Line { get; set; }
    
        public virtual c_NPT c_NPT { get; set; }
        public virtual c_timer_class c_timer_class { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<L_operation> L_operation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_active_info> rt_active_info { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_alarm> rt_alarm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_communication_state> rt_communication_state { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_datachangeprot> rt_datachangeprot { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_job11> rt_job1 { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_paramvalues> rt_paramvalues { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_spottable> rt_spottable { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_weldfault> rt_weldfault { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_weldmeasureprotddw> rt_weldmeasureprotddw { get; set; }
    }
}
