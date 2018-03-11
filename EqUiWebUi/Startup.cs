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
            //setup hangfire options
            var HFoptions = new BackgroundJobServerOptions
            {
                //MUST BE LOWERCASE ONLY !!!!!!
                Queues = new[] { "critical", "default", "alertengine", "gadata" },
                //How many jobs run at the same time
                WorkerCount = 5 // Environment.ProcessorCount * 5
            };
            app.UseHangfireServer(HFoptions);
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