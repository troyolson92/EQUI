//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.Alert.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class c_triggers
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_triggers()
        {
            this.h_alert = new HashSet<h_alert>();
            this.l_controlLimits = new HashSet<l_controlLimits>();
        }
    
        public int id { get; set; }
        public bool enabled { get; set; }
        public string discription { get; set; }
        public int RunAgainst { get; set; }
        public string sqlStqStatement { get; set; }
        public Nullable<int> smsSystem { get; set; }
        public int initial_state { get; set; }
        public string alertType { get; set; }
        public bool AutoSetStateTechComp { get; set; }
        public bool smsOnRetrigger { get; set; }
        public bool enableSMS { get; set; }
        public string Cron { get; set; }
        public bool isDowntime { get; set; }
        public bool isInReport { get; set; }
    
        public virtual c_smsSystem c_smsSystem { get; set; }
        public virtual c_state c_state { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<h_alert> h_alert { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<l_controlLimits> l_controlLimits { get; set; }
    }
}
