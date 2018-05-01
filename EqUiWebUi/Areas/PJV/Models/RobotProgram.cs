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
    
    public partial class RobotProgram
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public RobotProgram()
        {
            this.RobotRoutine = new HashSet<RobotRoutine>();
        }
    
        public int id { get; set; }
        public string p_name { get; set; }
        public Nullable<int> isDead { get; set; }
        public Nullable<System.DateTime> C_timestamp { get; set; }
        public Nullable<System.DateTime> C_lasttimestamp { get; set; }
        public string filename { get; set; }
        public Nullable<int> robotDetailId { get; set; }
        public Nullable<int> size { get; set; }
        public Nullable<System.DateTime> fileDateTime { get; set; }
        public string fullFilename { get; set; }
        public string MD5 { get; set; }
        public Nullable<int> rev_created { get; set; }
        public Nullable<int> rev_modified { get; set; }
    
        public virtual RobotDetail RobotDetail { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RobotRoutine> RobotRoutine { get; set; }
    }
}
