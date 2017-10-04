﻿using Hangfire;
using Hangfire.SqlServer;
using System;

namespace EqUiWebUi
{
    public class Startup
    {
        public void Configuration(Owin.IAppBuilder app)
        {
        //setting up hangfire 
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(
                    "GADATAConnectionString",
                    new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) });

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        //


            //background work
            Backgroundwork backgroundwork = new Backgroundwork();
            //**********************************TipDresData table***************************************************
            //fire and forget to init
            BackgroundJob.Enqueue(() => backgroundwork.UpdateTipstatus());
            //set job to refresh every minute
            RecurringJob.AddOrUpdate(() => backgroundwork.UpdateTipstatus(), Cron.Minutely);
            //**********************************snapshot system****************************************************
            //check every minute for new jobs 
            RecurringJob.AddOrUpdate(() => backgroundwork.HandleMaximoSnapshotWork(),Cron.Minutely);

        }
    }
}