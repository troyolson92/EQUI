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
        private PerformContext MainContext;

        [Queue("jobengine")]
        [AutomaticRetry(Attempts = 0)] //no hangfire retry 
        public void Run_houseKeeping(string name, int id, PerformContext context)
        {
            c_housekeeping housekeeping = dbEQUI.c_housekeeping.Find(id);
            context.WriteLine($"housekeeping: {housekeeping.Name} table: [{housekeeping.SchemaName}].[{ housekeeping.TableName}]");

            DateTime sarttime = System.DateTime.Now;
            DataTable result = new DataTable();
            try
            {
                EQUICommunictionLib.ConnectionManager connectionManager = new EQUICommunictionLib.ConnectionManager();
                MainContext = context;
                connectionManager.InfoMessage += ConnectionManager_InfoMessage;

                string cmd = $@"EXEC [EqUi].[sp_runHouseKeepingOnTable]
@nDaysKeepHistory = {housekeeping.nDaysKeepHistory},
@nDeleteBatchSize  = {housekeeping.nDeleteBatchSize},
@SchemaName = '{housekeeping.SchemaName.Trim()}',
@TableName  = '{housekeeping.TableName.Trim()}',
@IdColName  = '{housekeeping.IdColName}',
@DateTimeColName  = '{housekeeping.DateTimeColName}'

            ";
                context.WriteLine(cmd);
                result = connectionManager.RunQuery(cmd, enblExeptions: true, maxEXECtime: housekeeping.nMaxRunTime,subscribeToMessages:true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //
                L_housekeeping housekeepingResult = new L_housekeeping();
                housekeepingResult.nJobID = Convert.ToInt32(context.BackgroundJob.Id);
                housekeepingResult.timestamp = sarttime;
                housekeepingResult.c_housekeeping_id = id;
                if (result.Rows.Count == 1)
                {
                    housekeepingResult.nRowCountStart = result.Rows[0].Field<int>("StartCount");
                    housekeepingResult.nRowCountEnd = result.Rows[0].Field<int>("EndCount");
                    housekeepingResult.nLoopCount = result.Rows[0].Field<int>("LoopCount");
                    housekeepingResult.nDeleteCount = result.Rows[0].Field<int>("DeleteCount");
                }
               dbEQUI.L_housekeeping.Add(housekeepingResult);
               dbEQUI.SaveChanges();
            }
        }

        private void ConnectionManager_InfoMessage(string msg)
        {
            MainContext.SetTextColor(ConsoleTextColor.Yellow);
            MainContext.WriteLine(msg);
            MainContext.ResetTextColor();
        }
    }
}