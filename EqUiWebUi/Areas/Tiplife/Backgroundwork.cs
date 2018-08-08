using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQUICommunictionLib;
using EqUiWebUi.Areas.Tiplife.Models;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;

namespace EqUiWebUi.Areas.Tiplife
{
    public static class DataBuffer
    {
        //
        public static List<TipMonitor> Tipstatus { get; set; }
        public static DateTime TipstatusLastDt { get; set; }
    }

    public class Backgroundwork
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// <summary>
        /// configure standard jobs to hangfire
        /// </summary>
        public void configHangfireJobs()
        {
            //background work
            Backgroundwork backgroundwork = new Backgroundwork();
            //**********************************TipDresData table***************************************************
            //set job to refresh every minute
            RecurringJob.AddOrUpdate("BT_NGAC_tipSTS", () => backgroundwork.UpdateTipstatus(null), Cron.Minutely);
        }


        //update the local datatable with tipstatus called every minute #hangfire
        [AutomaticRetry(Attempts = 0)]
        [Queue("gadata")]
        public void UpdateTipstatus(PerformContext context)
        {
            ConnectionManager connectionManager = new ConnectionManager();
            string[] cmds = { "exec GADATA.[NGAC].[sp_CalcTipWearBeforeChange]"
            };

            foreach (string cmd in cmds)
            {
                context.WriteLine(" " + cmd);
                connectionManager.RunCommand(cmd, enblExeptions: true, maxEXECtime: 300);
                context.WriteLine(" Done");
            }

            context.WriteLine(" Get gADATAEntities.TipMonitor");
            GADATAEntitiesTiplife gADATAEntities = new GADATAEntitiesTiplife();
            gADATAEntities.Database.CommandTimeout = 60; //override default 30 seconds timeout. 
            List<TipMonitor> data = (from tipstatus in gADATAEntities.TipMonitor
                                     select tipstatus).ToList();

            if (data.Count != 0)
            {
                DataBuffer.Tipstatus = data;

                DateTime maxDate = data
                            .Where(r => r != null)
                            .Select(r => r.Date_time)
                            .Max();

                DataBuffer.TipstatusLastDt = maxDate;
                context.WriteLine(string.Format("UpdateTipstatus {0} records", data.Count));

                //add singal R 
                var SignalRcontext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Areas.Gadata.DataRefreshHub>();
                SignalRcontext.Clients.Group("TipstatusGrid").newData();
            }
            else
            {
                log.Error("UpdateTipstatus did not return any data");
            }
        }

    }
}