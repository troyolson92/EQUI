//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class L_pannel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public L_pannel()
        {
            this.L_link = new HashSet<L_link>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool collapsed { get; set; }
        public string HeaderCss { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<L_link> L_link { get; set; }
    }
}
