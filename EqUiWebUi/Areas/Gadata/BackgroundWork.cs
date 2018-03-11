using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EqUiWebUi.Areas.Gadata.Models;
using Hangfire;

namespace EqUiWebUi.Areas.Gadata
{
    public static class DataBuffer
    {
        //
        public static List<Supervisie> Supervisie { get; set; }
        public static DateTime SupervisieLastDt { get; set; }
        //
        public static List<AAOSR_PloegRaport_Result> Ploegreport { get; set; }
        public static DateTime PloegreportLastDt { get; set; }
        //
        public static List<EQpluginDefaultNGAC_Result> EQpluginDefaultNGAC { get; set; }
        public static DateTime EQpluginDefaultNGAC_DT { get; set; }
    }


    public class BackgroundWork
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

            public void configHangfireJobs()
            {
                //background work
                BackgroundWork backgroundwork = new BackgroundWork();
                //**********************************Ploegreport table***************************************************
                //set job to refresh every 5 minutes
                RecurringJob.AddOrUpdate(() => backgroundwork.UpdatePloegreport(), Cron.MinuteInterval(5));
                //**********************************Ploegreport table***************************************************
                //set job to refresh every 5 minutes
                RecurringJob.AddOrUpdate(() => backgroundwork.UpdateEQpluginDefaultNGAC(), Cron.MinuteInterval(5));
                //**********************************Supervisie table***************************************************
                //set job to refresh every minute
                RecurringJob.AddOrUpdate(() => backgroundwork.UpdateSupervisie(), Cron.Minutely);
            }

            //update the local datatable with ploeg rapport called every minute #hangfire
            [Queue("gadata")]
            [AutomaticRetry(Attempts = 0)]
            [DisableConcurrentExecution(50)] //timeout
            public void UpdatePloegreport()
            {
                GADATAEntities2 gADATAEntities = new GADATAEntities2();

                List<AAOSR_PloegRaport_Result> data = (from ploegrapport in gADATAEntities.AAOSR_PloegRaport
                                    (startDate: null,
                                       endDate: null,
                                       daysBack: null,
                                       assets: "%",
                                       locations: "%",
                                       lochierarchy: "%",
                                       minDowntime: 20,
                                       minCountOfDowtime: 3,
                                       minCountofWarning: 4,
                                       getAlerts: false,
                                       getShifbook: true)
                                                       select ploegrapport).ToList();

                if (data.Count != 0)
                {
                    DataBuffer.Ploegreport = data;

                    DateTime maxDate = data
                        .Where(r => Convert.ToDateTime(r.timestamp) < System.DateTime.Now)
                        // .Where(r => r.Logtype.Contains("Ruleinfo") == false)
                        .Select(r => Convert.ToDateTime(r.timestamp))
                        .Max();

                    DataBuffer.PloegreportLastDt = maxDate;

                    //add singal R 
                    var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<DataRefreshHub>();
                    context.Clients.Group("Ploegrapport").newData();
                }
                else
                {
                    log.Error("UpdatePloegreport did not return any data");
                }
            }

            //update the local datatable with supervisie called every minute #hangfire
            [Queue("gadata")]
            [AutomaticRetry(Attempts = 0)]
            [DisableConcurrentExecution(50)] //timeout 3minutes
            public void UpdateSupervisie()
            {
                GADATAEntities2 gADATAEntities = new GADATAEntities2();
                List<Supervisie> data = (from supervis in gADATAEntities.Supervisie
                                         select supervis).ToList();

                if (data.Count != 0)
                {
                    DataBuffer.Supervisie = data;

                    DateTime maxDate = data
                                .Where(r => r != null)
                                .Select(r => r.timestamp.Value)
                                .Max();

                    DataBuffer.SupervisieLastDt = maxDate;

                    //add singal R 
                    var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<DataRefreshHub>();
                    context.Clients.Group("Supervisie").newData();
                }
                else
                {
                    log.Error("UpdateSupervisie did not return any data");
                }
            }

            //update the local datatable with detail robot data called every minute #hangfire
            [Queue("gadata")]
            [AutomaticRetry(Attempts = 0)]
            [DisableConcurrentExecution(50)] //timeout
            public void UpdateEQpluginDefaultNGAC()
            {
                GADATAEntities2 gADATAEntities = new GADATAEntities2();

            List<EQpluginDefaultNGAC_Result> data = (from DefaultNgac in gADATAEntities.EQpluginDefaultNGAC      
                                  (startDate: null,
                                   endDate: null,
                                   daysBack: 1,
                                   assets: "%",
                                   locations: "%",
                                   lochierarchy: "%",
                                   timeline: true,
                                   controllerEventLog: false,
                                   errDispLog: true,
                                   errDispLogS4C: true,
                                   variableLog: false,
                                   deviceProperty: false,
                                   breakdown: true,
                                   breakdownStart: false,
                                   jobs: false,
                                   displayLevel: 0,
                                   displayFullLogtext: true,
                                   excludeOperational: true
                                       )
                                                         select DefaultNgac).ToList();

                if (data.Count != 0)
                {
                    DataBuffer.EQpluginDefaultNGAC = data;

                    DateTime maxDate = data
                        .Where(r => Convert.ToDateTime(r.timestamp) < System.DateTime.Now)
                        // .Where(r => r.Logtype.Contains("Ruleinfo") == false)
                        .Select(r => Convert.ToDateTime(r.timestamp))
                        .Max();

                    DataBuffer.EQpluginDefaultNGAC_DT = maxDate;

                    //add singal R 
                    var context = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<DataRefreshHub>();
                    context.Clients.Group("EQpluginDefaultNGAC").newData();
                }
                else
                {
                    log.Error("UpdateEQpluginDefaultNGAC did not return any data");
                }
            }

        }
}