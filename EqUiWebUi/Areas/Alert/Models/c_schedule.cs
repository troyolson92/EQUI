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
    
    public partial class c_schedule
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_schedule()
        {
            this.c_triggers = new HashSet<c_triggers>();
        }
    
        public int id { get; set; }
        public bool enabled { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string jcron { get; set; }
        public bool runContinues { get; set; }
        public int minRunInterval { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<c_triggers> c_triggers { get; set; }
    }
}
