//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.UltraLog.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class L_InspectionPlans
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public L_InspectionPlans()
        {
            this.h_CompletedPlans = new HashSet<h_CompletedPlans>();
        }
    
        public int id { get; set; }
        public string InspectionPlanname { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<h_CompletedPlans> h_CompletedPlans { get; set; }
    }
}
