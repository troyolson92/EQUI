using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Globalization;
using System.Data;
using System.Net;
using System.Windows.Forms;

namespace EQUICommunictionLib
{
    public class stoSync
    {
        myDebugger Debugger = new myDebugger();

       public void get_stodata()
        {
            try
            {
                //read into dt 
               StoComm lStoComm = new StoComm();
               string qry = @"SELECT * FROM STO_SYS.ALARM_DATA";

               DataTable dt = lStoComm.oracle_runQuery(qry);
                //check how many records are now in gadata.
               GadataComm lGadataComm = new GadataComm();
               string qryCount = @"select count(*) 'count' from gadata.sto.rt_breakdown";
               DataTable countBefore = lGadataComm.RunQueryGadata(qryCount);
                
                //clean out the table 
                lGadataComm.RunCommandGadata("DELETE gadata.sto.rt_breakdown FROM gadata.sto.rt_breakdown");
                //push to gadata
                lGadataComm.BulkCopyToGadata("STO", dt, "rt_breakdown");

                //run script to remove duplicates
                /*string cmdRemoveDup = @"";
                lGadataComm.RunCommandGadata(cmdRemoveDup);*/
 
                //check how many record now
                DataTable countAfter = lGadataComm.RunQueryGadata(qryCount);
                MessageBox.Show(string.Format(@"
Finished with operation
before: {0}
after: {1}
new records ={2}"   , countBefore.Rows[0].Field<int>("count").ToString()
                    , countAfter.Rows[0].Field<int>("count").ToString()
                    , (countBefore.Rows[0].Field<int>("count")-countAfter.Rows[0].Field<int>("count")).ToString())
                    , "Confirmation", MessageBoxButtons.OK);
            }
           catch (Exception ex)
            {
                Debugger.Exeption(ex);
                Debugger.Message(ex.Message);
            }
        }
    }
}