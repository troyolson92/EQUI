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
    
    public partial class Inspectionplan
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Inspectionplan()
        {
            this.UltralogInspections = new HashSet<UltralogInspections>();
        }
    
        public int ID { get; set; }
        public Nullable<int> CreatorID { get; set; }
        public Nullable<short> Lenght { get; set; }
        public string Name { get; set; }
        public Nullable<System.DateTime> Date { get; set; }
        public Nullable<bool> PlanActive { get; set; }
        public Nullable<int> SpotIdent { get; set; }
        public Nullable<System.TimeSpan> WorkTime { get; set; }
        public Nullable<int> WorkGroup { get; set; }
        public string WorkLocation { get; set; }
        public Nullable<int> SpotBefore { get; set; }
        public Nullable<int> SpotAfter { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UltralogInspections> UltralogInspections { get; set; }
    }
}
