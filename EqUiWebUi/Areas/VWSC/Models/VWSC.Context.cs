﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.VWSC.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GADATAEntitiesVWSC : DbContext
    {
        public GADATAEntitiesVWSC()
            : base("name=GADATAEntitiesVWSC")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<VWSC_c_error> c_error { get; set; }
        public virtual DbSet<VWSC_c_service_setup> c_service_setup { get; set; }
        public virtual DbSet<VWSC_c_severity> c_severity { get; set; }
        public virtual DbSet<VWSC_c_timer_class> c_timer_class { get; set; }
        public virtual DbSet<VWSC_rt_active_info> rt_active_info { get; set; }
        public virtual DbSet<VWSC_rt_alarm> rt_alarm { get; set; }
        public virtual DbSet<VWSC_rt_communication_state> rt_communication_state { get; set; }
        public virtual DbSet<VWSC_rt_job> rt_job { get; set; }
        public virtual DbSet<VWSC_rt_job_breakdown> rt_job_breakdown { get; set; }
        public virtual DbSet<VWSC_rt_weldfault> rt_weldfault { get; set; }
        public virtual DbSet<VWSC_c_NPT> c_NPT { get; set; }
        public virtual DbSet<VWSC_c_timer> c_timer { get; set; }
        public virtual DbSet<VWSC_rt_weldmeasureprotddw> rt_weldmeasureprotddw { get; set; }
        public virtual DbSet<VWSC_c_bosch_view> c_bosch_view { get; set; }
        public virtual DbSet<VWSC_L_operation> L_operation { get; set; }
    }
}
