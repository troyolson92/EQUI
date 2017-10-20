using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
namespace EQUICommunictionLib
{
    public class StoComm
    {
        OracleConnection Stoconn = new OracleConnection(
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

        //debugger
        myDebugger Debugger = new myDebugger();

        public DataTable oracle_runQuery(string Query)
        {
            try
            {
                using (OracleDataAdapter dadapter = new OracleDataAdapter(Query, Stoconn))
                {
                    //get location and asset data from maximo
                    DataTable table = new DataTable();
                    dadapter.Fill(table);
                    return table;
                }
            }
            catch (Exception e)
            {
               Debugger.Exeption(e);
                DataTable table = new DataTable();
                return table;
            }

        }
        

    }
}
