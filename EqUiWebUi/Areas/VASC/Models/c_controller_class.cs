//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.VASC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class c_controller_class
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public c_controller_class()
        {
            this.c_controller = new HashSet<c_controller>();
        }
    
        public int id { get; set; }
        public string name { get; set; }
        public Nullable<bool> doConnect { get; set; }
        public Nullable<int> evStateChange { get; set; }
        public Nullable<int> evOperatingModeChange { get; set; }
        public Nullable<int> evConnectionChange { get; set; }
        public Nullable<int> evExecutionStatus { get; set; }
        public Nullable<int> evExecutionStatusTRob1 { get; set; }
        public Nullable<int> evBackupCompleted { get; set; }
        public Nullable<int> evDataResolveChange { get; set; }
        public Nullable<int> evExecutionCycleChange { get; set; }
        public Nullable<int> evTaskEnabledChange { get; set; }
        public Nullable<int> evMasterChange { get; set; }
        public Nullable<int> evMotionPointerTRob1Change { get; set; }
        public Nullable<int> evProgramPointerTRob1Change { get; set; }
        public Nullable<int> evMotionPointerTRob1ManualChange { get; set; }
        public Nullable<int> evProgramPointerTRob1ManualChange { get; set; }
        public Nullable<int> cVariableMask { get; set; }
        public Nullable<int> cVariableSearchMask { get; set; }
        public Nullable<int> cDeviceInfoMask { get; set; }
        public Nullable<int> cCSVLogMask { get; set; }
        public Nullable<int> cJobMask { get; set; }
        public Nullable<int> logCategoryMask { get; set; }
        public Nullable<bool> handleHSocket { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<int> setClock { get; set; }
        public Nullable<int> evLogMessageAction { get; set; }
        public Nullable<int> cPJVEventMask { get; set; }
        public Nullable<int> cErrorNoLogMask { get; set; }
        public Nullable<int> cAlarmIgnoreMask { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<c_controller> c_controller { get; set; }
    }
}
