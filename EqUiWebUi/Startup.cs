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