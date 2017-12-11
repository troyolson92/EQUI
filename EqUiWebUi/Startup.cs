using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using System;
using System.Web;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]

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
            //**********************************STO****************************************************************
            //check every minute for new data (hystorian)
            RecurringJob.AddOrUpdate(() => backgroundwork.PushDatafromSTOtoGADATA(), Cron.MinuteInterval(2));
            //**********************************STW040 BI rapoort****************************************************
            RecurringJob.AddOrUpdate(() => backgroundwork.PushDatafromSTW040toGADATA(), Cron.HourInterval(1));
            //**********************************MX7 *****************************************************************
            //BI rapport
            //RecurringJob.AddOrUpdate(() => backgroundwork.PushDatafromMX7toGADATA(), Cron.HourInterval(1));
            //reporting DB 
            RecurringJob.AddOrUpdate(() => backgroundwork.PushDatafromMAXIMOtoGADATA(), Cron.HourInterval(1));
            //**********************************Tableau buffers******************************************************
            RecurringJob.AddOrUpdate(() => backgroundwork.UpdateTableauBuffers(), Cron.MinuteInterval(20));


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

                EquiRoleProvider equiRoleProvider = new EquiRoleProvider();

                if (equiRoleProvider.IsUserInRole(HttpContext.Current.User.Identity.Name , "Administrator"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

    }
}