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
    
        public virtual ObjectResult<GetErrorInfoData_Result> GetErrorInfoData(string location, Nullable<int> eRRORNUM, Nullable<int> refid, string logtype)
        {
            var locationParameter = location != null ?
                new ObjectParameter("Location", location) :
                new ObjectParameter("Location", typeof(string));
    
            var eRRORNUMParameter = eRRORNUM.HasValue ?
                new ObjectParameter("ERRORNUM", eRRORNUM) :
                new ObjectParameter("ERRORNUM", typeof(int));
    
            var refidParameter = refid.HasValue ?
                new ObjectParameter("Refid", refid) :
                new ObjectParameter("Refid", typeof(int));
    
            var logtypeParameter = logtype != null ?
                new ObjectParameter("logtype", logtype) :
                new ObjectParameter("logtype", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<GetErrorInfoData_Result>("GetErrorInfoData", locationParameter, eRRORNUMParameter, refidParameter, logtypeParameter);
        }
    
        public virtual ObjectResult<AAOSR_PloegRaportV2_Result> AAOSR_PloegRaportV2(Nullable<System.DateTime> startDate, Nullable<System.DateTime> endDate, Nullable<int> daysBack, string assets, string locations, string lochierarchy, Nullable<int> minDowntime, Nullable<int> minCountOfDowtime, Nullable<int> minCountofWarning, Nullable<bool> getAlerts, Nullable<bool> getShifbook)
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
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AAOSR_PloegRaportV2_Result>("AAOSR_PloegRaportV2", startDateParameter, endDateParameter, daysBackParameter, assetsParameter, locationsParameter, lochierarchyParameter, minDowntimeParameter, minCountOfDowtimeParameter, minCountofWarningParameter, getAlertsParameter, getShifbookParameter);
        }
    }
}
