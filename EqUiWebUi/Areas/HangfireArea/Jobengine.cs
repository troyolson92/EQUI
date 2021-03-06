﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using EQUICommunictionLib;
using EqUiWebUi.Models;
using Hangfire.Server;
using Hangfire.Console;
using System.Data.Entity;
using System.Diagnostics;
using Hangfire.Storage.Monitoring;

namespace EqUiWebUi.Areas.HangfireArea
{
    public class Jobengine
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private GADATAEntitiesEQUI dbEQUI = new GADATAEntitiesEQUI();
        private Alert.Models.GADATA_AlertModel dbALERT = new Alert.Models.GADATA_AlertModel();
        private Alert.AlertEngine AlertEngine = new Alert.AlertEngine();
        private EqUiWebUi.HouseKeepingengine HouseKeepingengine = new HouseKeepingengine();
        private PerformContext MainContext;

        public void configure_schedules(bool onlyStartrunContinues = false)
        {
            List<c_schedule> list = dbEQUI.c_schedule.ToList();
            foreach (c_schedule Schedule in list)
            {
                if (Schedule.enabled)
                {
                    if (Schedule.runContinues) // use endless loop (triggers here and than runs without intervention
                    {
                        //to handle switching from jcron to endless
                        Hangfire.RecurringJob.RemoveIfExists("sch_" + Schedule.name);
                        //this is dangerous! each time you do this you CREATE a new loop. 
                        BackgroundJob.Enqueue(() => Run_schedule(Schedule.id, null));
                    }
                    else if (onlyStartrunContinues == false) // use RecurringJob job
                    {
                        Hangfire.RecurringJob.AddOrUpdate("sch_" + Schedule.name, () => Run_schedule(Schedule.id, null), Schedule.jcron);
                    }
                }
                else
                {
                    Hangfire.RecurringJob.RemoveIfExists("sch_" + Schedule.name);
                }
            }
        }

        [Queue("jobengine")]
        [AutomaticRetry(Attempts = 0)] //no hang-fire retry 
        public void Run_schedule(int c_schedule_id, PerformContext context)
        {
            c_schedule schedule = dbEQUI.c_schedule.Find(c_schedule_id);
            if (schedule == null)
            {
                context.WriteLine("schedule not found");
                throw new NotImplementedException();
            }
            else if (schedule.enabled == false)
            {
                context.WriteLine("schedule is disabled");
                return;
            }
            else
            {
                List<c_job> jobs = dbEQUI.c_job.Where(c => c.c_schedule_id == c_schedule_id).OrderBy(c => c.ordinal).ToList();
                List<Alert.Models.c_triggers> triggers = dbALERT.c_triggers.Where(c => c.c_schedule_id == c_schedule_id).OrderBy(c => c.ordinal).ToList();
                List<EqUiWebUi.Models.c_housekeeping> housekeepings = dbEQUI.c_housekeeping.Where(c => c.c_schedule_id == c_schedule_id).OrderBy(c => c.Ordinal).ToList();

                string previousJobId = null;
                //handle jobs
                foreach (c_job job in jobs)
                {
                    context.WriteLine($"processing Job: {job.name} ordinal: {job.ordinal}");
                    if (previousJobId == null)
                    {
                        previousJobId = BackgroundJob.Enqueue(() => Run_job(job.name, job.id, context));
                    }
                    else
                    {
                        if (job.continueOnJobFailure)
                        {
                            previousJobId = BackgroundJob.ContinueWith(previousJobId, () => Run_job(job.name, job.id, context), JobContinuationOptions.OnAnyFinishedState);
                        }
                        else
                        {
                            previousJobId = BackgroundJob.ContinueWith(previousJobId, () => Run_job(job.name, job.id, context), JobContinuationOptions.OnlyOnSucceededState);
                        }
                    }
                    //add URL to created job.
                    context.WriteLine(System.Configuration.ConfigurationManager.AppSettings["HangfireDetailsBasepath"].ToString() + previousJobId);
                }
                //handle alerts
                foreach (Alert.Models.c_triggers trigger in triggers)
                {
                    context.WriteLine($"processing Alert: {trigger.alertType} ordinal: {trigger.ordinal}");
                    if (previousJobId == null)
                    {
                        previousJobId = BackgroundJob.Enqueue(() => AlertEngine.CheckForalerts(trigger.id, trigger.discription, context,false));
                    }
                    else
                    {
                        if (trigger.continueOnJobFailure)
                        {
                            previousJobId = BackgroundJob.ContinueWith(previousJobId, () => AlertEngine.CheckForalerts(trigger.id, trigger.discription, context, false), JobContinuationOptions.OnAnyFinishedState);
                        }
                        else
                        {
                            previousJobId = BackgroundJob.ContinueWith(previousJobId, () => AlertEngine.CheckForalerts(trigger.id, trigger.discription, context, false), JobContinuationOptions.OnlyOnSucceededState);
                        }
                    }
                    //add URL to created job.                 
                    context.WriteLine(System.Configuration.ConfigurationManager.AppSettings["HangfireDetailsBasepath"].ToString() + previousJobId);
                }
                //handle house keeping
                foreach (EqUiWebUi.Models.c_housekeeping trigger in housekeepings)
                {
                    context.WriteLine($"processing House keeping: {trigger.Name} ordinal: {trigger.Ordinal}");
                    if (previousJobId == null)
                    {
                        previousJobId = BackgroundJob.Enqueue(() => HouseKeepingengine.Run_houseKeeping(trigger.Name, trigger.id, context));
                    }
                    else
                    {
                        previousJobId = BackgroundJob.ContinueWith(previousJobId, () => HouseKeepingengine.Run_houseKeeping(trigger.Name, trigger.id, context), JobContinuationOptions.OnAnyFinishedState);
                    }
                    //add URL to created job.                 
                    context.WriteLine(System.Configuration.ConfigurationManager.AppSettings["HangfireDetailsBasepath"].ToString() + previousJobId);
                }

                //if job had to run continues. add schedule at last job.
                if (schedule.runContinues)
                {
                   if (schedule.minRunInterval < 10 )
                    {
                        schedule.minRunInterval = 10;
                        context.WriteLine("minRunInterval override to 10sec this is the minimum");
                    }
                   previousJobId = BackgroundJob.ContinueWith(previousJobId, () => Wait_job("Wait_job", schedule.minRunInterval, context), JobContinuationOptions.OnAnyFinishedState);
             
                    c_job lastjob = jobs.Last();
                    if (schedule.enabled)
                    {
                        previousJobId = BackgroundJob.ContinueWith(previousJobId, () => Run_schedule(schedule.id, context), JobContinuationOptions.OnAnyFinishedState);
                    }
                    else
                    {
                        context.WriteLine("endless loop stopped because schedule is disabled");
                    }
                }
            }
        }

        [Queue("jobengine")]
        [AutomaticRetry(Attempts = 0)]
        public void Run_job(string name, int c_job_id , PerformContext context)
        {
            c_job job = dbEQUI.c_job.Find(c_job_id);
            context.WriteLine($"running: {job.name}");
            if (job.enabled == false)
            {
                context.WriteLine($"job is disabled");
                return;
            }
            //if interval enabled show it
            if (job.interval != 0)
            {
                context.WriteLine($"jobinterval {job.intervalCounter}/{job.interval}");
            }
            //run job if no interval or interval condition OK
            if (job.interval == 0 || (job.intervalCounter >= job.interval))
            {
                EQUICommunictionLib.ConnectionManager connectionManager = new ConnectionManager();
                MainContext = context;
                connectionManager.InfoMessage += ConnectionManager_InfoMessage;

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                context.WriteLine($"Running sql command on {job.c_datasource.Name}");
                connectionManager.RunCommand(job.sqlCommand,dbID: job.c_datasource.Id, enblExeptions: true, maxEXECtime: job.maxRuntime.GetValueOrDefault(200),subscribeToMessages:true);
                stopwatch.Stop();

                if (stopwatch.Elapsed.TotalSeconds > job.warnRuntime.GetValueOrDefault(180))
                {
                    context.SetTextColor(ConsoleTextColor.Yellow);
                    context.WriteLine($"job overtime warning! {Math.Round(stopwatch.Elapsed.TotalSeconds,1)}/{job.warnRuntime}");
                    context.ResetTextColor();
                }
            }
            //if interval inc interval. and reset if needed
            if (job.interval != 0)
            {
                job.intervalCounter += 1;
                if (job.intervalCounter >= job.interval)
                {
                    job.intervalCounter = 0;
                }
                dbEQUI.Entry(job).State = EntityState.Modified;
                dbEQUI.SaveChanges();
            }
        }

        private void ConnectionManager_InfoMessage(string msg)
        {
            MainContext.SetTextColor(ConsoleTextColor.Yellow);
            MainContext.WriteLine(msg);
            MainContext.ResetTextColor();
        }

        [Queue("jobengine")]
        [AutomaticRetry(Attempts = 0)] //no hangfire retrys 
        public void Wait_job(string name, int waittime, PerformContext context)
        {
            context.WriteLine($"it is my job to just sit here and wait for: {waittime}sec");
            System.Threading.Thread.Sleep(waittime * 1000);
            context.WriteLine("done waiting les go...");
        }
  
    }
}