﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GADATAEntitiesEQUI : DbContext
    {
        public GADATAEntitiesEQUI()
            : base("name=GADATAEntitiesEQUI")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<c_datasource> c_datasource { get; set; }
        public virtual DbSet<c_LogClassRules> c_LogClassRules { get; set; }
        public virtual DbSet<c_logClassSystem> c_logClassSystem { get; set; }
        public virtual DbSet<c_Classification> c_Classification { get; set; }
        public virtual DbSet<c_Subgroup> c_Subgroup { get; set; }
        public virtual DbSet<l_dummyLogClassResult> l_dummyLogClassResult { get; set; }
        public virtual DbSet<ASSETS> ASSETS { get; set; }
        public virtual DbSet<L_link> L_link { get; set; }
        public virtual DbSet<L_pannel> L_pannel { get; set; }
        public virtual DbSet<c_job> c_job { get; set; }
        public virtual DbSet<c_schedule> c_schedule { get; set; }
        public virtual DbSet<L_housekeeping> L_housekeeping { get; set; }
        public virtual DbSet<c_housekeeping> c_housekeeping { get; set; }
        public virtual DbSet<Wiki> Wiki { get; set; }
    }
}
