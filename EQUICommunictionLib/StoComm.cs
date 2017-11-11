using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using Oracle.ManagedDataAccess.Client;
namespace EQUICommunictionLib
{
    public class StoComm
    {
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


        /*
                @"Data Source=(DESCRIPTION=
            (ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)
            (HOST=nvr.gent.vcc.ford.com)
            (PORT=49970)))
            (CONNECT_DATA=(SERVER=DEDICATED)
            (SERVICE_NAME=DST)));
            User Id=STO_SYS_READONLY;
            Password=sto_sys_readonly1;");

    */

        public DataTable oracle_runQuery(string Query)
        {
          try
            {
                using (OracleDataAdapter dadapter = new OracleDataAdapter(Query, StoconnPROD))
                {
                    //get location and asset data from maximo
                    DataTable table = new DataTable();
                    dadapter.Fill(table);
                    return table;
                }
            }
            catch (Exception ex)
            {
                throw new NotSupportedException("Failed to run sto query " + Query, ex);
            } 

        }
        

    }
}
