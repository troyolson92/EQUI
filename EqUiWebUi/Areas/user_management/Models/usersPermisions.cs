//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.user_management.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class usersPermisions
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string Role { get; set; }
        public int GrantedBy { get; set; }
        public Nullable<System.DateTime> GrantedAt { get; set; }
    
        public virtual userRoles c_userRoles { get; set; }
        public virtual users L_users { get; set; }
        public virtual users L_users1 { get; set; }
    }
}