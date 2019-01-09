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
        public static bool _isRunningUpdateTipstatus;
        public static int TipWearbeforechangeCounter { get; set; }
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
            //I call this now at the end of nom_NGAC
        }


        //update the local datatable with tipstatus called every minute #hangfire
        [AutomaticRetry(Attempts = 0)]
        [Queue("gadata")]
        public void UpdateTipstatus(PerformContext context)
        {
            try
            {
                if (DataBuffer._isRunningUpdateTipstatus)
                {
                    log.Error("job was already running CANCEL job");
                    context.WriteLine("job was already running CANCEL job");
                    return;
                }
                DataBuffer._isRunningUpdateTipstatus = true;

                            
                GADATAEntitiesTiplife db = new GADATAEntitiesTiplife();
                db.Database.CommandTimeout = 45; // default 30 

                //get data for NGAC  
                context.WriteLine("Get gADATAEntities.TipMonitor (NGAC)");
                List<TipMonitor> data = db.TipMonitor.ToList();

                //ADD comau data if area is enabled
                if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled("Welding"))
                {
                    context.WriteLine("Get gADATAEntities.TipMonitor (VWSC)");
                    List<VWSC_TipMonitor> VWSC_data = db.TipMonitor1.ToList();
                    if (VWSC_data.Count != 0)
                    {
                   //     data.AddRange(VWSC_data);
                    }
                }

                //check data valid and update if needed
                if (data.Count != 0)
                {
                    DataBuffer.Tipstatus = data;

                    DateTime maxDate = data
                                .Where(r => r != null)
                                .Select(r => r.Date_time)
                                .Max();

                    DataBuffer.TipstatusLastDt = maxDate;
                    context.WriteLine(string.Format("Buffer table updated ({0} rows)", data.Count));
                    //notify clients
                    var SignalRcontext = Microsoft.AspNet.SignalR.GlobalHost.ConnectionManager.GetHubContext<Areas.Supervision.DataRefreshHub>();
                    SignalRcontext.Clients.Group("TipstatusGrid").newData();
                }
                else
                {
                    context.WriteLine("UpdateTipstatus did not return any data");
                    log.Error("UpdateTipstatus did not return any data");
                }

                //update tipwear before change every 5 minutes
                int TipWearbeforechangeInterval = 5;
                if (DataBuffer.TipWearbeforechangeCounter >= TipWearbeforechangeInterval)
                {
                    context.WriteLine("TipWearbeforechange update START");
                    db.Database.CommandTimeout = 120;
                    db.Database.ExecuteSqlCommand("exec [NGAC].[sp_CalcTipWearBeforeChange]");
                    DataBuffer.TipWearbeforechangeCounter = 0;
                    context.WriteLine("Done");
                }
                else
                {
                    DataBuffer.TipWearbeforechangeCounter += 1;
                    context.WriteLine(string.Format("TipWearbeforechange update SKIPPED (interval:{0}/{1})",DataBuffer.TipWearbeforechangeCounter, TipWearbeforechangeInterval));
                }

            }
            finally
            {
                DataBuffer._isRunningUpdateTipstatus = false;
            }
        }

    }
}