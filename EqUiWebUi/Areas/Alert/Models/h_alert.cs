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
    
    public partial class h_alert
    {
        public int id { get; set; }
        public int c_tirgger_id { get; set; }
        public System.DateTime C_timestamp { get; set; }
        public string info { get; set; }
        public int state { get; set; }
        public string location { get; set; }
        public string comments { get; set; }
        public Nullable<int> acceptUserID { get; set; }
        public Nullable<System.DateTime> acceptTimestamp { get; set; }
        public Nullable<int> closeUserID { get; set; }
        public Nullable<System.DateTime> closeTimestamp { get; set; }
        public Nullable<int> lastChangedUserID { get; set; }
        public Nullable<System.DateTime> lastChangedTimestamp { get; set; }
<<<<<<< HEAD
        public int triggerCount { get; set; }
        public System.DateTime lastTriggerd { get; set; }
    
        public virtual c_state c_state { get; set; }
        public virtual c_triggers c_triggers { get; set; }
        public virtual L_users ChangedUser { get; set; }
        public virtual L_users CloseUser { get; set; }
        public virtual L_users AcceptUser { get; set; }
=======
    
        public virtual c_triggers c_triggers { get; set; }
        public virtual L_users acceptUser { get; set; }
        public virtual L_users closeUser { get; set; }
        public virtual L_users lastChangeUser { get; set; }
>>>>>>> 5db2146... start implementing new alert system (and sms controller)
    }
}
