﻿//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Core.Objects;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    
    public partial class GADATAEntities : DbContext
    {
        public GADATAEntities()
            : base("name=GADATAEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<QUERYParameters> QUERYParameters { get; set; }
        public DbSet<QUERYS> QUERYS { get; set; }
        public DbSet<c_querySnapshots> c_querySnapshots { get; set; }
        public DbSet<h_querySnapshots> h_querySnapshots { get; set; }
        public DbSet<l_querySnapshots> l_querySnapshots { get; set; }
        public DbSet<Supervisie> Supervisie { get; set; }
        public DbSet<TipMonitor> TipMonitor { get; set; }
        public DbSet<Breakdown> Breakdown { get; set; }
        public DbSet<logDetails> logDetails { get; set; }
        public DbSet<TipwearBeforeChange> TipwearBeforeChange { get; set; }
        public DbSet<ia_Alert> ia_Alert { get; set; }
    
        public virtual ObjectResult<AAOSR_PloegRaport_Result> AAOSR_PloegRaport(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, Nullable<int> daysBack, string assets, string locations, string lochierarchy, Nullable<int> minDowntime, Nullable<int> minCountOfDowtime, Nullable<int> minCountofWarning, Nullable<bool> getAlerts, Nullable<bool> getShifbook)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            var daysBackParameter = daysBack.HasValue ?
                new ObjectParameter("daysBack", daysBack) :
                new ObjectParameter("daysBack", typeof(int));
    
            var assetsParameter = assets != null ?
                new ObjectParameter("assets", assets) :
                new ObjectParameter("assets", typeof(string));
    
            var locationsParameter = locations != null ?
                new ObjectParameter("locations", locations) :
                new ObjectParameter("locations", typeof(string));
    
            var lochierarchyParameter = lochierarchy != null ?
                new ObjectParameter("lochierarchy", lochierarchy) :
                new ObjectParameter("lochierarchy", typeof(string));
    
            var minDowntimeParameter = minDowntime.HasValue ?
                new ObjectParameter("minDowntime", minDowntime) :
                new ObjectParameter("minDowntime", typeof(int));
    
            var minCountOfDowtimeParameter = minCountOfDowtime.HasValue ?
                new ObjectParameter("minCountOfDowtime", minCountOfDowtime) :
                new ObjectParameter("minCountOfDowtime", typeof(int));
    
            var minCountofWarningParameter = minCountofWarning.HasValue ?
                new ObjectParameter("minCountofWarning", minCountofWarning) :
                new ObjectParameter("minCountofWarning", typeof(int));
    
            var getAlertsParameter = getAlerts.HasValue ?
                new ObjectParameter("getAlerts", getAlerts) :
                new ObjectParameter("getAlerts", typeof(bool));
    
            var getShifbookParameter = getShifbook.HasValue ?
                new ObjectParameter("getShifbook", getShifbook) :
                new ObjectParameter("getShifbook", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AAOSR_PloegRaport_Result>("AAOSR_PloegRaport", startDateParameter, endDateParameter, daysBackParameter, assetsParameter, locationsParameter, lochierarchyParameter, minDowntimeParameter, minCountOfDowtimeParameter, minCountofWarningParameter, getAlertsParameter, getShifbookParameter);
        }
    
        public virtual ObjectResult<EQpluginDefaultNGAC_Result> EQpluginDefaultNGAC(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, Nullable<int> daysBack, string assets, string locations, string lochierarchy, Nullable<bool> timeline, Nullable<bool> controllerEventLog, Nullable<bool> errDispLog, Nullable<bool> variableLog, Nullable<bool> deviceProperty, Nullable<bool> breakdown, Nullable<bool> breakdownStart, Nullable<int> displayLevel, Nullable<bool> displayFullLogtext, Nullable<bool> excludeOperational)
        {
            var startDateParameter = startDate.HasValue ?
                new ObjectParameter("StartDate", startDate) :
                new ObjectParameter("StartDate", typeof(System.DateTime));
    
            var endDateParameter = endDate.HasValue ?
                new ObjectParameter("EndDate", endDate) :
                new ObjectParameter("EndDate", typeof(System.DateTime));
    
            var daysBackParameter = daysBack.HasValue ?
                new ObjectParameter("daysBack", daysBack) :
                new ObjectParameter("daysBack", typeof(int));
    
            var assetsParameter = assets != null ?
                new ObjectParameter("assets", assets) :
                new ObjectParameter("assets", typeof(string));
    
            var locationsParameter = locations != null ?
                new ObjectParameter("locations", locations) :
                new ObjectParameter("locations", typeof(string));
    
            var lochierarchyParameter = lochierarchy != null ?
                new ObjectParameter("lochierarchy", lochierarchy) :
                new ObjectParameter("lochierarchy", typeof(string));
    
            var timelineParameter = timeline.HasValue ?
                new ObjectParameter("timeline", timeline) :
                new ObjectParameter("timeline", typeof(bool));
    
            var controllerEventLogParameter = controllerEventLog.HasValue ?
                new ObjectParameter("ControllerEventLog", controllerEventLog) :
                new ObjectParameter("ControllerEventLog", typeof(bool));
    
            var errDispLogParameter = errDispLog.HasValue ?
                new ObjectParameter("ErrDispLog", errDispLog) :
                new ObjectParameter("ErrDispLog", typeof(bool));
    
            var variableLogParameter = variableLog.HasValue ?
                new ObjectParameter("VariableLog", variableLog) :
                new ObjectParameter("VariableLog", typeof(bool));
    
            var devicePropertyParameter = deviceProperty.HasValue ?
                new ObjectParameter("DeviceProperty", deviceProperty) :
                new ObjectParameter("DeviceProperty", typeof(bool));
    
            var breakdownParameter = breakdown.HasValue ?
                new ObjectParameter("Breakdown", breakdown) :
                new ObjectParameter("Breakdown", typeof(bool));
    
            var breakdownStartParameter = breakdownStart.HasValue ?
                new ObjectParameter("BreakdownStart", breakdownStart) :
                new ObjectParameter("BreakdownStart", typeof(bool));
    
            var displayLevelParameter = displayLevel.HasValue ?
                new ObjectParameter("DisplayLevel", displayLevel) :
                new ObjectParameter("DisplayLevel", typeof(int));
    
            var displayFullLogtextParameter = displayFullLogtext.HasValue ?
                new ObjectParameter("DisplayFullLogtext", displayFullLogtext) :
                new ObjectParameter("DisplayFullLogtext", typeof(bool));
    
            var excludeOperationalParameter = excludeOperational.HasValue ?
                new ObjectParameter("ExcludeOperational", excludeOperational) :
                new ObjectParameter("ExcludeOperational", typeof(bool));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<EQpluginDefaultNGAC_Result>("EQpluginDefaultNGAC", startDateParameter, endDateParameter, daysBackParameter, assetsParameter, locationsParameter, lochierarchyParameter, timelineParameter, controllerEventLogParameter, errDispLogParameter, variableLogParameter, devicePropertyParameter, breakdownParameter, breakdownStartParameter, displayLevelParameter, displayFullLogtextParameter, excludeOperationalParameter);
        }
    }
}
