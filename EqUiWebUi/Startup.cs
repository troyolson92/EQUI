using Hangfire;
using Hangfire.Dashboard;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;
using System;
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

            //setting up hangfire 
            //setup hangfire database config
            GlobalConfiguration.Configuration
                .UseSqlServerStorage(
                    "GADATAConnectionString",
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
                    Queues = new[] { "critical", "default", "alertengine", "gadata", "jobengine" },
                    //How many jobs run at the same time
                    WorkerCount = Environment.ProcessorCount * 5
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
                    Queues = new[] {"debug","gadata"},
                    //How many jobs run at the same time
                    WorkerCount = Environment.ProcessorCount * 1
                };
                app.UseHangfireServer(HFoptions);
                log.Info("Hangfire startup in DEBUG mode (" + Environment.MachineName + ")");

            }
        //
        }

        //handle hangfire authentication
        public class MyAuthorizationFilter : IDashboardAuthorizationFilter
        {
            public bool Authorize(DashboardContext context)
            {
                // In case you need an OWIN context, use the next line, `OwinContext` class
                // is the part of the `Microsoft.Owin` package.
                var owinContext = new OwinContext(context.GetOwinEnvironment());

                // Allow all authenticated users to see the Dashboard (potentially dangerous).
                //return owinContext.Authentication.User.Identity.IsAuthenticated;  
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