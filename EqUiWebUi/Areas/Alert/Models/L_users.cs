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
    
    public partial class L_users
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public L_users()
        {
            this.Changed = new HashSet<h_alert>();
            this.Close = new HashSet<h_alert>();
            this.AcceptUser = new HashSet<h_alert>();
            this.l_variants = new HashSet<l_variants>();
            this.l_controlLimits = new HashSet<l_controlLimits>();
            this.l_controlLimits1 = new HashSet<l_controlLimits>();
        }
    
        public int id { get; set; }
        public string username { get; set; }
        public string LocationRoot { get; set; }
        public string AssetRoot { get; set; }
        public bool Locked { get; set; }
        public bool Blocked { get; set; }
        public string SessionId { get; set; }
        public string Comment { get; set; }
        public string ResponsibleArea { get; set; }
        public string Team { get; set; }
        public string Culture { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<h_alert> Changed { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<h_alert> Close { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<h_alert> AcceptUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<l_variants> l_variants { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<l_controlLimits> l_controlLimits { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<l_controlLimits> l_controlLimits1 { get; set; }
    }
}
