﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EqUiWebUi.Areas.Tiplife.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class GADATAEntitiesTiplife : DbContext
    {
        public GADATAEntitiesTiplife()
            : base("name=GADATAEntitiesTiplife")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TipDressLogFile> TipDressLogFile { get; set; }
        public virtual DbSet<TipMonitor> TipMonitor { get; set; }
        public virtual DbSet<TipwearBeforeChange> TipwearBeforeChange { get; set; }
        public virtual DbSet<TipwearLast> TipwearLast { get; set; }
    }
}
