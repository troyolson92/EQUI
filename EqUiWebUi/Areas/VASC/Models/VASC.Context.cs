﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GADATAEntitiesVASC : DbContext
    {
        public GADATAEntitiesVASC()
            : base("name=GADATAEntitiesVASC")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<c_service_setup> c_service_setup { get; set; }
        public virtual DbSet<c_device_info> c_device_info { get; set; }
        public virtual DbSet<c_job> c_job { get; set; }
        public virtual DbSet<c_variable> c_variable { get; set; }
        public virtual DbSet<c_variable_search> c_variable_search { get; set; }
        public virtual DbSet<c_error> c_error { get; set; }
        public virtual DbSet<c_controller> c_controller { get; set; }
        public virtual DbSet<c_controller_class> c_controller_class { get; set; }
        public virtual DbSet<rt_active_info> rt_active_info { get; set; }
        public virtual DbSet<rt_device_info> rt_device_info { get; set; }
        public virtual DbSet<rt_event> rt_event { get; set; }
        public virtual DbSet<rt_value> rt_value { get; set; }
        public virtual DbSet<L_operation> L_operation { get; set; }
        public virtual DbSet<rt_controller> rt_controller { get; set; }
        public virtual DbSet<rt_search_value> rt_search_value { get; set; }
        public virtual DbSet<rt_job> rt_job { get; set; }
        public virtual DbSet<rt_job_breakdown> rt_job_breakdown { get; set; }
        public virtual DbSet<c_csv_log> c_csv_log { get; set; }
        public virtual DbSet<rt_csv_file> rt_csv_file { get; set; }
        public virtual DbSet<rt_alarm> rt_alarm { get; set; }
        public virtual DbSet<h_alarm> h_alarm { get; set; }
        public virtual DbSet<L_actions> L_actions { get; set; }
        public virtual DbSet<L_causes> L_causes { get; set; }
        public virtual DbSet<L_consequences> L_consequences { get; set; }
        public virtual DbSet<L_description> L_description { get; set; }
        public virtual DbSet<L_error> L_error { get; set; }
        public virtual DbSet<L_type> L_type { get; set; }
        public virtual DbSet<L_category> L_category { get; set; }
    }
}
