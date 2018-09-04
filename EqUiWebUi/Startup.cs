using Hangfire;
using Hangfire.Console;
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
            //setting up hangfire if enabled
            if (Convert.ToBoolean(System.Configuration.ConfigurationManager.AppSettings["EnableHangfire"]) == true)
            {
                log.Warn("Hangfire_Start");
                //setup hangfire database config
                GlobalConfiguration.Configuration
                    .UseSqlServerStorage(
                        ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString, //we take this from web.config
                        new SqlServerStorageOptions {
                            QueuePollInterval = TimeSpan.FromSeconds(1),
                            PrepareSchemaIfNecessary = Convert.ToBoolean(ConfigurationManager.AppSettings["HangfirePrepareSchemas"]) // auto build hangfire tabels (if true it fails when we debloy empty tables)
                        }
                        )
                        .UseConsole(); //extension to hangefire for better logging https://github.com/pieceofsummer/Hangfire.Console
               //set up hangefire dashboard
                app.UseHangfireDashboard("/hangfire", new DashboardOptions
                {
                    Authorization = new[] { new MyAuthorizationFilter() }
                });
                //setup hangfire options
                BackgroundJobServerOptions HFoptions = new BackgroundJobServerOptions
                {
                    //MUST BE LOWERCASE ONLY !!!!!!
                    Queues = ConfigurationManager.AppSettings["HangfireQueues"].Split(';'),
                    //How many jobs run at the same time
                    WorkerCount = Environment.ProcessorCount * 6
                };
                app.UseHangfireServer(HFoptions);
                log.Info("Hangfire startup with HangfireQueues (" + ConfigurationManager.AppSettings["HangfireQueues"] + ")");
            }
            else
            {
                log.Warn("Hangfire not enabled in web.config");
            }
            //
            //things to do if in debug mode.
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