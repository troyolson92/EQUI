using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Globalization;
using System.Data;
using System.Net;
using System.Windows.Forms;

namespace EQUICommunictionLib
{
    public class stw040Sync
    {
        myDebugger Debugger = new myDebugger();

       public void get_swt040data()
        {
            //download
            WebClient Client = new WebClient();

         
               Client.DownloadFile("http://biwebreports.got.volvocars.net:8080/reports/vcg/VCG_Manufacturing_And_Supply_Chain/Manufacturing/Disturbances_And_Maintenance/Storingsrapport%20GA_dumpfile%20sched%202.xlsx"
                    , @"C:\GADATA\IIS\Temp\Storingsrapport GA _dumpfile sched 2.xlsx");

            //read into dt 
            DataTable dt = LoadWorksheetInDataTable(@"C:\GADATA\IIS\Temp\Storingsrapport GA _dumpfile sched 2.xlsx", "Zone - Lijst - Laatste werkdag$");
                //check how many records are now in gadata.
                GadataComm lGadataComm = new GadataComm();
                string qryCount = @"select count(id) 'count' from gadata.equi.['Zone - Lijst - Laatste werkdag$']";
                DataTable countBefore = lGadataComm.RunQueryGadata(qryCount);
                //push to gadata
                lGadataComm.BulkCopyToGadata("EqUi", dt, "'Zone - Lijst - Laatste werkdag$'");
                //run script to remove duplicates
                string cmdRemoveDup = @"
                DELETE GADATA.EqUi.['Zone - Lijst - Laatste werkdag$']
                FROM GADATA.EqUi.['Zone - Lijst - Laatste werkdag$'] as lx 
                LEFT OUTER JOIN (
                   SELECT 
                   MIN(l.id) as 'id' 
                   ,l.[Begin storing]
                   ,l.Machines
                   FROM  GADATA.EqUi.['Zone - Lijst - Laatste werkdag$'] as l  
                   GROUP BY    
                    l.[Begin storing]
                   ,l.Machines
                ) as KeepRows ON
                   lx.Id = KeepRows.Id
                WHERE
                   KeepRows.Id IS NULL
                ";
                lGadataComm.RunCommandGadata(cmdRemoveDup);
                //check how many record now
                DataTable countAfter = lGadataComm.RunQueryGadata(qryCount);
                Debugger.Log(string.Format(@"
                Finished with operation
                before: {0}
                after: {1}
                new records ={2}", countBefore.Rows[0].Field<int>("count").ToString()
                                    , countAfter.Rows[0].Field<int>("count").ToString()
                                    , (countBefore.Rows[0].Field<int>("count") - countAfter.Rows[0].Field<int>("count")).ToString())
                                    );
        }

        DataTable LoadWorksheetInDataTable(string fileName, string sheetName)
        {
            DataTable sheetData = new DataTable();
            using (OleDbConnection conn = this.returnConnection(fileName))
            {
                conn.Open();
                // retrieve the data using data adapter
                OleDbDataAdapter sheetAdapter = new OleDbDataAdapter("select * from [" + sheetName + "]", conn);
                sheetAdapter.Fill(sheetData);
            }
            return sheetData;
        }

        private OleDbConnection returnConnection(string fileName)
        {
            return new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + "; Jet OLEDB:Engine Type=5;Extended Properties=\"Excel 12.0;\"");
        }

   
    }
}