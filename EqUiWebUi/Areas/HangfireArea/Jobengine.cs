using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using EQUICommunictionLib;

namespace EqUiWebUi.Areas.HangfireArea
{
    public class Jobengine
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void Makejob(string jobname, string command, string cron, int maxExectime, int maxRetry)
        {

            //SDB need to add max retry here but how? 

            log.Debug("Makejob: " + jobname);
            Hangfire.RecurringJob.AddOrUpdate("BGJ_"+jobname, () => Runjob(command,maxExectime), cron);
        }

        [Queue("jobengine")]
        [AutomaticRetry(Attempts = 0)] //no hangfire retrys 
      //  [DisableConcurrentExecution(60*10)] //max exec time 10 minutes no dual running
        public void Runjob(string command, int maxExectime = 300)
        {
            log.Debug("runjob: " + command);
            ConnectionManager connectionManager = new ConnectionManager();
            connectionManager.RunCommand(command, enblExeptions: true, maxEXECtime: maxExectime);
        }
    }
}