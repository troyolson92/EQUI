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
    
    public partial class c_CPT600
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_CPT600()
        {
            this.c_SMSconfig = new HashSet<c_SMSconfig>();
        }
    
        public int id { get; set; }
        public string Discription { get; set; }
        public string System { get; set; }
        public string LocationTree { get; set; }
        public string AssetRoot { get; set; }
        public string ActivePloeg { get; set; }
        public Nullable<System.TimeSpan> StartTime { get; set; }
        public Nullable<System.TimeSpan> EndTime { get; set; }
        public Nullable<int> SMSlimit { get; set; }
        public Nullable<int> SMSsend { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<c_SMSconfig> c_SMSconfig { get; set; }
    }
}
