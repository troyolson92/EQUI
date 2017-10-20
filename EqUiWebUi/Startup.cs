using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
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

            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new MyAuthorizationFilter() }
            });

            app.UseHangfireServer();
        //
            //background work
            Backgroundwork backgroundwork = new Backgroundwork();
            //**********************************TipDresData table***************************************************
            //fire and forget to init
            BackgroundJob.Enqueue(() => backgroundwork.UpdateTipstatus());
            //set job to refresh every minute
            RecurringJob.AddOrUpdate(() => backgroundwork.UpdateTipstatus(), Cron.Minutely);
            //**********************************Ploegreport table***************************************************
            //fire and forget to init
            BackgroundJob.Enqueue(() => backgroundwork.UpdatePloegreport());
            //set job to refresh every minute
            RecurringJob.AddOrUpdate(() => backgroundwork.UpdatePloegreport(), Cron.Minutely);
            //**********************************Supervisie table***************************************************
            //fire and forget to init
            BackgroundJob.Enqueue(() => backgroundwork.UpdateSupervisie());
            //set job to refresh every minute
            RecurringJob.AddOrUpdate(() => backgroundwork.UpdateSupervisie(), Cron.Minutely);
            //**********************************snapshot system****************************************************
            //check every minute for new jobs 
            RecurringJob.AddOrUpdate(() => backgroundwork.HandleMaximoSnapshotWork(),Cron.Minutely);
            //**********************************STO****************************************************
            //check every minute for new data 
            RecurringJob.AddOrUpdate(() => backgroundwork.PushDatafromSTOtoGADATA(), Cron.Minutely);
        }

        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                // In case you need an OWIN context, use the next line, `OwinContext` class
                // is the part of the `Microsoft.Owin` package.
                var owinContext = new OwinContext(context.GetOwinEnvironment());

                // Allow all authenticated users to see the Dashboard (potentially dangerous).
                //return owinContext.Authentication.User.Identity.IsAuthenticated;
                return true;
            }
        }

    }
}