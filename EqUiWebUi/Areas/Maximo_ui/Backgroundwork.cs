using EQUICommunictionLib;
using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.Maximo_ui
{
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
            //check every minute for new data (hystorian)
            //RecurringJob.AddOrUpdate(() => backgroundwork.PushDatafromSTOtoGADATA(), Cron.MinuteInterval(5));
            //**********************************MX7 *****************************************************************
            //reporting DB 
            RecurringJob.AddOrUpdate("RT_MX=>GADATA", () => backgroundwork.PushDatafromMAXIMOtoGADATA(null), Cron.HourInterval(1));
        }

        //update new data from STO to gadata. called every minute #hangfire
        [AutomaticRetry(Attempts = 0)]
        [Queue("gadata")]
        public void PushDatafromMAXIMOtoGADATA(PerformContext context)
        {
            //delete data in now in maximo.
            ConnectionManager connectionManager = new ConnectionManager();
            context.WriteLine(" Delete workorders in gadata started");
            connectionManager.RunCommand("DELETE GADATA.MAXIMO.WORKORDERS FROM GADATA.MAXIMO.WORKORDERS");
            context.WriteLine(" Done");

            //get new records from STO
            string MaximoQry = string.Format(@"
select 
 WORKORDER.WONUM WONUM
,WORKORDER.STATUS
,WORKORDER.STATUSDATE
,WORKORDER.WORKTYPE
,WORKORDER.DESCRIPTION
,WORKORDER.LOCATION
,WORKORDER.REPORTEDBY
,WORKORDER.REPORTDATE
,locancestor.ANCESTOR
from MAXIMO.WORKORDER WORKORDER
join MAXIMO.locancestor locancestor on 
locancestor.LOCATION = WORKORDER.LOCATION
and 
locancestor.ORGID = 'VCCBE'
and
locancestor.ANCESTOR LIKE 'A'
WHERE
workorder.status not in ('CLOSE','CLOSEVOID') --they changes something where they set a shitload of workorders in this state. (the changedate was updated)
and
workorder.changedate >= sysdate - 100
"
);

            context.WriteLine(" Get workorders on MAXIMOrt started");
            DataTable newMaximoDt = connectionManager.RunQuery(MaximoQry,dbName: "MAXIMOrt", maxEXECtime: 120, enblExeptions: true);
            context.WriteLine(" Done");
            //push to gadata
            context.WriteLine(" Push workorders to gadata started");
            connectionManager.BulkCopy(newMaximoDt, "[MAXIMO].[WORKORDERS]");
            context.WriteLine(" Done");
        }

    }
}