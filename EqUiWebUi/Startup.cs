using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;
using System.Configuration;
using System.Web;

namespace EqUiWebUi
{
    public class Startup
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Configuration(Owin.IAppBuilder app)
        {
            //setup signal r
            app.MapSignalR();

            try
            {
                //setting up hangfire 
                //setup hangfire database config
                GlobalConfiguration.Configuration
                    .UseSqlServerStorage(
                        ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString, //we take this from web.config
                        new SqlServerStorageOptions { QueuePollInterval = TimeSpan.FromSeconds(1) });
                //set up hangefire dashboard
                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    Authorization = new[] { new MyAuthorizationFilter() }
                });

                //RUN TE FULL QUERY LIST ONLY ON THE PRODUCTION MACHINE! 
                if (Environment.MachineName.Contains("SVW"))
                {
                    //setup hangfire options
                    var HFoptions = new BackgroundJobServerOptions
                    {
                        //MUST BE LOWERCASE ONLY !!!!!!
                        Queues = new[] { "critical", "default", "alertengine", "gadata", "jobengine", "sto" },
                        //How many jobs run at the same time
                        WorkerCount = Environment.ProcessorCount * 6
                    };
                    app.UseHangfireServer(HFoptions);
                    log.Info("Hangfire startup in production mode (" + Environment.MachineName + ")");
                }
                else
                {
                    //setup hangfire options
                    var HFoptions = new BackgroundJobServerOptions
                    {
                        //MUST BE LOWERCASE ONLY !!!!!!
                        Queues = new[] { "debug", "gadata" },
                        // Queues = new[] { "critical", "default", "alertengine", "gadata", "jobengine", "sto" },
                        //How many jobs run at the same time
                        WorkerCount = Environment.ProcessorCount * 1
                    };
                    app.UseHangfireServer(HFoptions);
                    log.Info("Hangfire startup in DEBUG mode (" + Environment.MachineName + ")");

                }
            }
            catch (Exception ex)
            {
                log.Error("Failed to init hangfire",ex);
            }
        //
        }

        //handle hangfire authentication
        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                var owinContext = new OwinContext(context.GetOwinEnvironment());
                roleProvider equiRoleProvider = new roleProvider();
                if (equiRoleProvider.IsUserInRole(HttpContext.Current.User.Identity.Name , "Administrator") || equiRoleProvider.IsUserInRole(HttpContext.Current.User.Identity.Name, "HangFire"))
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