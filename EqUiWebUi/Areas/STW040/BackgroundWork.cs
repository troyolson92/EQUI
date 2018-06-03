using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EQUICommunictionLib;
using System.Data;
using Hangfire;

namespace EqUiWebUi.Areas.STW040
{
    public class BackgroundWork
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void configHangfireJobs()
        {
            //background work
            BackgroundWork backgroundwork = new BackgroundWork();
            //**********************************STW040 => GADATA sunc***************************************************
            //set job to refresh every 5 minutes
            RecurringJob.AddOrUpdate("STW40=>GADATA", () => backgroundwork.STW040ToGadataSync(), Cron.HourInterval(1));
        }

        [Queue("gadata")]
        [AutomaticRetry(Attempts = 0)]
        public void STW040ToGadataSync()
        {
            string STW040qry = @"
select 
 null id
,EMPLOYEE.OPERSFN LastName
,EMPLOYEE.OPERSVN SurName 
,STW040.IMACHINE
,STW040.KPLOEG
,STW040.DBSTASTO 
,STW040.DBSTOSTO 
,STW040.DBSTOSTIL
--,CAST (STW040.DBSTASTO AS TIMESTAMP) DBSTASTO
--,CAST (STW040.DBSTOSTO AS TIMESTAMP) DBSTOSTO
--,CAST (STW040.DBSTOSTIL AS TIMESTAMP) DBSTOSTIL
,STW040.OAKTIE 
,STW040.KSHIFT
,STW040.HAMSTOR
,STW040.HOSVLT
,STW040.HOSSTT
,STW040.KMTBF
,STW040.IMZONE
,STW040.NPLC
,STW040.DBCYCLE
--,CAST (STW040.DBCYCLE AS TIMESTAMP) DBCYCLE
,STW040.KFAB
,STW040.KEIG
,STW040.OPREVAKTIE
,STW040.KMANUEEL
,OORZAAK.OOORZAAK
,FOUT.OFOUTKODE
,MACHINE.IGEBD
,MACHINE.OGEBD
,MACHINE.ISBGEBD
,MACHINE.OZONE40
,MACHINE.ISTOSTAT
FROM APPLICATION.TBI200 STW040
LEFT JOIN APPLICATION.TBI060 MACHINE on STW040.IMACHINE = MACHINE.IMACHINE
LEFT JOIN APPLICATION.TBI290 EMPLOYEE on  STW040.IPERSMEL = EMPLOYEE.IPERS
LEFT JOIN APPLICATION.TBI270 FOUT on STW040.KFOUT5 = FOUT.KFOUT5 and STW040.KEIG = FOUT.KFOUTGRP
LEFT JOIN APPLICATION.TBI280 OORZAAK on STW040.KOORZAAK = OORZAAK.KOORZAAK and STW040.KEIG = OORZAAK.KOORZGRP
WHERE 
STW040.KFAB = 'GA'
AND STW040.DBSTASTO > sysdate-((1*24*60)/1440) --work with last 1 days of data.
AND STW040.KMANUEEL <> 'N'
--AND STW040.IMACHINE like '336060%'
";

            //get data from stw040 database.
            //This should be realtime but is not! Still a copy that runs every end of shift. 
            STW040Comm sTW040Comm = new STW040Comm();
            DataTable dt = new DataTable();
            dt = sTW040Comm.Oracle_runQuery(STW040qry, enblExeptions: true, maxEXECtime: 600);

            GadataComm gadataComm = new GadataComm();
            //delete data from server
            //string STW040deleteQry = "delete GADATA.STW040.STW040 from GADATA.STW040.STW040";
            //gadataComm.RunCommandGadata(STW040deleteQry, runAsAdmin: true, enblExeptions: true, maxEXECtime: 30);
            //push data to server
            gadataComm.BulkCopyToGadata("STW040", dt, "STW040",runAsAdmin: true, enblExeptions: true, maxEXECtime: 300);
            //remove duplicates
            string STW040RemoveDups = @"
DELETE GADATA.STW040.STW040 
--select lx.*
FROM GADATA.STW040.STW040 as lx 
LEFT OUTER JOIN (
   SELECT 
   MIN(l.id) as 'id' 
   ,L.DBSTASTO
   ,L.IMACHINE
   ,L.DBCYCLE
   FROM  GADATA.STW040.STW040 as l  
   GROUP BY    
    L.DBSTASTO
   ,L.IMACHINE
   ,L.DBCYCLE
) as KeepRows ON
   lx.Id = KeepRows.Id
WHERE
   KeepRows.Id IS NULL";
            gadataComm.RunCommandGadata(STW040RemoveDups, runAsAdmin: true, enblExeptions: true, maxEXECtime: 30);
            return;
        }

    }
}