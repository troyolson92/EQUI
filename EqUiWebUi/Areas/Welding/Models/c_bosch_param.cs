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
    
    public partial class c_bosch_param
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_bosch_param()
        {
            this.rt_paramvalues = new HashSet<rt_paramvalues>();
        }
    
        public int ID { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<int> param_id { get; set; }
        public Nullable<int> flag { get; set; }
        public string paramName { get; set; }
        public string text { get; set; }
        public Nullable<int> enable_bit { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<rt_paramvalues> rt_paramvalues { get; set; }
    }
}