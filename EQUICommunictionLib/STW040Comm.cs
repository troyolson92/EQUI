using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using log4net;

namespace EQUICommunictionLib
{
    public class STW040Commxx
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //NOT YET SET !!!
        OracleConnection STw040ReportingConn = new OracleConnection(
            @"Data Source= (DESCRIPTION=
    (ADDRESS=
      (COMMUNITY=tcp.world)
      (PROTOCOL=TCP)
      (HOST=SDBIGP)
      (PORT=49949)
    )
    (CONNECT_DATA=
      (SERVER=dedicated)
      (SID=dbi)
    )
  )
;User Id=DBI_TABLEAU;Password=dbi_tableau;");

        public DataTable Oracle_runQuery(string Query, bool enblExeptions = false, int maxEXECtime = 300)
        {
            OracleConnection activeConn = STw040ReportingConn;
            try
            {
                //SDB like this we alway close the connection ? should we try to keep it open like in GADATACOM???

                using (OracleDataAdapter adapter = new OracleDataAdapter(Query, activeConn))
                {
                    adapter.SelectCommand.CommandTimeout = maxEXECtime;
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                log.Error("Command Failed", ex);
                if (enblExeptions)
                {
                    throw ex;
                }
                DataTable table = new DataTable();
                return table;
            }

        }
    }

}
