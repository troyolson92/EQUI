using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;
namespace EQUICommunictionLib
{
    public class StoCommXXX
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        OracleConnection StoconnQI = new OracleConnection(
            @"Data Source= (DESCRIPTION=
    (ADDRESS=
      (COMMUNITY=tcp.world)
      (PROTOCOL=TCP)
      (HOST=SDSTGD)
      (PORT=49970)
    )
    (CONNECT_DATA=
      (SID=DST)
    )
  )
;User Id=STO_SYS_READONLY;Password=sto_sys_readonly1;");

        OracleConnection StoconnPROD = new OracleConnection(
           @"Data Source= (DESCRIPTION=
            (ADDRESS=
              (COMMUNITY=tcp.world)
              (PROTOCOL=TCP)
              (HOST=nvr.gent.vcc.ford.com)
              (PORT=49970)
            )
            (CONNECT_DATA=
              (SID=DST)
            )
          )
        ;User Id=STO_SYS_READONLY;Password=sto_sys_readonly1;");


        public string STOconnectionString
        {
            get { return @"ODBC;DSN=" + DsnSTO + ";Description= STO;UID=STO_SYS_READONLY;PWD=sto_sys_readonly1;"; }
        }
        public string SystemSTO { get { return "STO"; } }
        public string DsnSTO { get { return "STO"; } }

        public void Make_DSN(string System)
        {
            if (System == SystemSTO)
            {
                ODBCManager.CreateDSN(DsnSTO, "odbc link STO", "nvr.gent.vcc.ford.com"
                    , "STO ODBC for oracle", @"C:\windows\system32\msorcl32.dll", true, "STO_SYS");
            }

        }

        public DataTable Oracle_runQuery(string Query, bool enblExeptions = false, int maxEXECtime = 300)
        {
            OracleConnection activeConn = StoconnPROD;


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
