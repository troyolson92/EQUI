using EQUICommunictionLib;
using Hangfire;
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
            RecurringJob.AddOrUpdate("RT_MX=>GADATA", () => backgroundwork.PushDatafromMAXIMOtoGADATA(), Cron.HourInterval(1));
        }

        //update new data from STO to gadata. called every minute #hangfire
        [AutomaticRetry(Attempts = 0)]
        [Queue("gadata")]
        public void PushDatafromMAXIMOtoGADATA()
        {
            //delete data in now in maximo.
            GadataComm lGadataComm = new GadataComm();
            lGadataComm.RunCommandGadata("DELETE GADATA.MAXIMO.WORKORDERS FROM GADATA.MAXIMO.WORKORDERS", true);

            //get new records from STO
            MaximoComm maximoComm = new MaximoComm();
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
workorder.changedate >= sysdate - 100
and
--workorder.worktype = 'CI'
--and 
locancestor.ANCESTOR LIKE 'A'

ORDER BY WORKORDER.STATUSDATE DESC
"
);

            DataTable newMaximoDt = maximoComm.Oracle_runQuery(MaximoQry, RealtimeConn: true, maxEXECtime: 120, enblExeptions: true);
            //push to gadata
            lGadataComm.BulkCopyToGadata("MAXIMO", newMaximoDt, "WORKORDERS");
        }

    }
}