using EqUiWebUi.Models;
using Hangfire;
using Hangfire.Server;
using Hangfire.Console;
using System.Data;
using System;

namespace EqUiWebUi
{
    public class HouseKeepingengine
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private GADATAEntitiesEQUI dbEQUI = new GADATAEntitiesEQUI();

        [Queue("jobengine")]
        [AutomaticRetry(Attempts = 0)] //no hangfire retry 
        public void Run_houseKeeping(string name, int id, PerformContext context)
        {
            c_housekeeping housekeeping = dbEQUI.c_housekeeping.Find(id);
            context.WriteLine($"housekeeping: {housekeeping.Name} table: {housekeeping.TableName}");

            EQUICommunictionLib.ConnectionManager connectionManager = new EQUICommunictionLib.ConnectionManager();
            string cmd = $@"
              EXEC [EqUi].[sp_runHouseKeepingOnTable]  
                   @nDaysKeepHistory = {housekeeping.nDaysKeepHistory},
	               @nDeleteBatchSize  = {housekeeping.nDeleteBatchSize},
	               @SchemaName = 'WELDING2',
	               @TableName  = '{housekeeping.TableName}',
	               @IdColName  = '{housekeeping.IdColName}',
	               @DateTimeColName  = '{housekeeping.DateTimeColName}'

            ";
            //
            DataTable result = connectionManager.RunQuery(cmd,enblExeptions:true,maxEXECtime:housekeeping.nMaxRunTime);
            L_housekeeping housekeepingResult = new L_housekeeping();
            housekeepingResult.nJobID = Convert.ToInt32(context.BackgroundJob.Id);
            housekeepingResult.nRowCountStart = result.Rows[1].Field<int>("StartCount");
            housekeepingResult.nRowCountEnd = result.Rows[1].Field<int>("EndCount");
            //
            dbEQUI.L_housekeeping.Add(housekeepingResult);
            dbEQUI.SaveChanges();
        }
    }
}