﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.Welding.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class GADATAEntitiesWelding : DbContext
    {
        public GADATAEntitiesWelding()
            : base("name=GADATAEntitiesWelding")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<rt_AutoWorkFlowULPlans> rt_AutoWorkFlowULPlans { get; set; }
        public virtual DbSet<ULRapportering> ULRapporterings { get; set; }
        public virtual DbSet<DoubleSpotCheck> DoubleSpotCheck { get; set; }
        public virtual DbSet<CheckDubbelPrograms> CheckDubbelPrograms { get; set; }
        public virtual DbSet<BosProgramAvailable> BosProgramAvailable { get; set; }
        public virtual DbSet<dressrequired> dressrequired { get; set; }
        public virtual DbSet<WeldtimeSpotsSetup> WeldtimeSpotsSetup { get; set; }
        public virtual DbSet<StabilityInDicationV316> StabilityInDicationV316 { get; set; }
        public virtual DbSet<ConnectionState> ConnectionState { get; set; }
        public virtual DbSet<LastWelds> LastWelds { get; set; }
        public virtual DbSet<QISViewer> QISViewer { get; set; }
        public virtual DbSet<TimerBreakdowns_busy> TimerBreakdowns_busy { get; set; }
        public virtual DbSet<TimerBreakdownDatachange> TimerBreakdownDatachange { get; set; }
        public virtual DbSet<TDTResults> TDTResults { get; set; }
        public virtual DbSet<ToDoList> ToDoList { get; set; }
        public virtual DbSet<ToDoList_Remark> ToDoList_Remark { get; set; }
        public virtual DbSet<ComparePitchV316> ComparePitchV316 { get; set; }
        public virtual DbSet<WeldFaultProtocol> WeldFaultProtocol { get; set; }
        public virtual DbSet<WeldfaultCount> WeldfaultCount { get; set; }
        public virtual DbSet<AutomaticPlanControleWeldBolt> AutomaticPlanControleWeldBolt { get; set; }
        public virtual DbSet<c_faultcode> c_faultcode { get; set; }
        public virtual DbSet<c_Job> c_Job { get; set; }
        public virtual DbSet<c_Picture> c_Picture { get; set; }
        public virtual DbSet<c_Teardown> c_Teardown { get; set; }
        public virtual DbSet<rt_Job1> rt_Job1Set { get; set; }
        public virtual DbSet<rt_Teardown> rt_Teardown { get; set; }
        public virtual DbSet<AlertsAASPOT> AlertsAASPOT { get; set; }
        public virtual DbSet<AlertsQteam> AlertsQteam { get; set; }
        public virtual DbSet<AlertsUsers> AlertsUsers { get; set; }
        public virtual DbSet<ActualSpatter_> ActualSpatter_ { get; set; }
        public virtual DbSet<rt_ExtraControles> rt_ExtraControles { get; set; }
        public virtual DbSet<c_bosch_param> c_bosch_param { get; set; }
        public virtual DbSet<c_error> c_error { get; set; }
        public virtual DbSet<c_NPT> c_NPT { get; set; }
        public virtual DbSet<c_timer> c_timer { get; set; }
        public virtual DbSet<c_timer_class> c_timer_class { get; set; }
        public virtual DbSet<c_user> c_user { get; set; }
        public virtual DbSet<h_Midair> h_Midair { get; set; }
        public virtual DbSet<h_weldmeasure> h_weldmeasure { get; set; }
        public virtual DbSet<L_operation> L_operation { get; set; }
        public virtual DbSet<rt_active_info> rt_active_info { get; set; }
        public virtual DbSet<rt_alarm> rt_alarm { get; set; }
        public virtual DbSet<rt_communication_state> rt_communication_state { get; set; }
        public virtual DbSet<rt_datachangeprot> rt_datachangeprot { get; set; }
        public virtual DbSet<rt_job11> rt_job11 { get; set; }
        public virtual DbSet<rt_job_breakdown1> rt_job_breakdown1 { get; set; }
        public virtual DbSet<rt_paramvalues> rt_paramvalues { get; set; }
        public virtual DbSet<rt_spottable> rt_spottable { get; set; }
        public virtual DbSet<rt_user> rt_user { get; set; }
        public virtual DbSet<rt_weldfault> rt_weldfault { get; set; }
        public virtual DbSet<rt_weldmeasureprotddw> rt_weldmeasureprotddw { get; set; }
        public virtual DbSet<h_Nut_Bolt_Measure> h_Nut_Bolt_Measure { get; set; }
    
        public virtual ObjectResult<Lastwelds_Result> Lastwelds(string timer, Nullable<int> spot)
        {
            var timerParameter = timer != null ?
                new ObjectParameter("Timer", timer) :
                new ObjectParameter("Timer", typeof(string));
    
            var spotParameter = spot.HasValue ?
                new ObjectParameter("spot", spot) :
                new ObjectParameter("spot", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Lastwelds_Result>("Lastwelds", timerParameter, spotParameter);
        }
    }
}
