﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using EQUICommunictionLib;

namespace EqUiWebUi.Areas.HanfireArea
{
    public class Jobengine
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Makejob(string jobname, string command, string cron)
        {
            log.Debug("Makejob: " + jobname);
            Hangfire.RecurringJob.AddOrUpdate("BGJ_"+jobname, () => Runjob(command), cron);
        }

        [Queue("jobengine")]
        [AutomaticRetry(Attempts = 0)] //no hangfire retrys 
        [DisableConcurrentExecution(60*10)] //max exec time 10 minutes no dual running
        public void Runjob(string command)
        {
            log.Debug("runjob: " + command);
            GadataComm gadataComm = new GadataComm();
            gadataComm.RunCommandGadata(command,enblExeptions:true,runAsAdmin:true,maxEXECtime:300);
        }
    }
}