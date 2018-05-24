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
    
    public partial class c_controller
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_controller()
        {
            this.rt_active_info = new HashSet<rt_active_info>();
            this.rt_device_info = new HashSet<rt_device_info>();
            this.rt_value = new HashSet<rt_value>();
            this.rt_event = new HashSet<rt_event>();
            this.rt_controller = new HashSet<rt_controller>();
            this.L_operation = new HashSet<L_operation>();
            this.rt_search_value = new HashSet<rt_search_value>();
            this.rt_job = new HashSet<rt_job>();
            this.rt_csv_file = new HashSet<rt_csv_file>();
            this.rt_alarm = new HashSet<rt_alarm>();
            this.h_alarm = new HashSet<h_alarm>();
        }
    
        public int id { get; set; }
        public string controller_name { get; set; }
        public int enable_bit { get; set; }
        public string systemId { get; set; }
        public string ip { get; set; }
        public int class_id { get; set; }
        public int flags { get; set; }
        public string LocationTree { get; set; }
        public string Assetnum { get; set; }
        public string ProductionTeam { get; set; }
        public string ResponsiblePloeg { get; set; }
        public string ClassificationTree { get; set; }
        public string CLassificationId { get; set; }
        public Nullable<bool> hasRackidAsBodynum { get; set; }
    
        public virtual c_controller_class c_controller_class { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_active_info> rt_active_info { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_device_info> rt_device_info { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_value> rt_value { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_event> rt_event { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_controller> rt_controller { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<L_operation> L_operation { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_search_value> rt_search_value { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_job> rt_job { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_csv_file> rt_csv_file { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_alarm> rt_alarm { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<h_alarm> h_alarm { get; set; }
    }
}
