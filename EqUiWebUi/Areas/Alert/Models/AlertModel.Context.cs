﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GADATA_AlertModel : DbContext
    {
        public GADATA_AlertModel()
            : base("name=GADATA_AlertModel")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<c_smsSystem> c_smsSystem { get; set; }
        public virtual DbSet<c_state> c_state { get; set; }
        public virtual DbSet<h_alert> h_alert { get; set; }
        public virtual DbSet<L_users> L_users { get; set; }
        public virtual DbSet<c_triggers> c_triggers { get; set; }
        public virtual DbSet<c_CPT600> c_CPT600 { get; set; }
        public virtual DbSet<c_SMSconfig> c_SMSconfig { get; set; }
        public virtual DbSet<l_variants> l_variants { get; set; }
        public virtual DbSet<c_datasource> c_datasource { get; set; }
        public virtual DbSet<l_controlLimits> l_controlLimits { get; set; }
        public virtual DbSet<l_dummyControlchartResult> l_dummyControlchartResult { get; set; }
        public virtual DbSet<c_schedule> c_schedule { get; set; }
    }
}
