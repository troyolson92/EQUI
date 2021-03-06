//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.PJV.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RobotRoutine
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RobotRoutine()
        {
            this.RobotPoint = new HashSet<RobotPoint>();
        }
    
        public int id { get; set; }
        public string s_name { get; set; }
        public Nullable<int> programLine { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<System.DateTime> C_lasttimestamp { get; set; }
        public Nullable<int> isDead { get; set; }
        public Nullable<int> RobotProgramId { get; set; }
        public Nullable<int> rev_created { get; set; }
        public Nullable<int> rev_modified { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RobotPoint> RobotPoint { get; set; }
        public virtual RobotProgram RobotProgram { get; set; }
    }
}
