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
    public class mx7Sync
    {
        myDebugger Debugger = new myDebugger();

       public void get_mx7data()
        {
            try
            {
                //download
                WebClient Client = new WebClient();
                Client.DownloadFile("http://biwebreports.got.volvocars.net:8080/reports/vcg/VCG_Manufacturing_And_Supply_Chain/Manufacturing/Maximo7/Storingsrapport GA - x3A versie maximo 7 45dagen x3A 13653701.xlsx"
                    , @"C:\Temp\Storingsrapport GA - x3A versie maximo 7 45dagen x3A 13653701.xlsx");
                //read into dt 
                DataTable dt = LoadWorksheetInDataTable(@"C:\Temp\Storingsrapport GA - x3A versie maximo 7 45dagen x3A 13653701.xlsx", "Report 2$");
                //check how many records are now in gadata.
                GadataComm lGadataComm = new GadataComm();
                string qryCount = @"select count(id) 'count' from gadata.[EqUi].['Report 2$']";
                DataTable countBefore = lGadataComm.RunQueryGadata(qryCount);
                //push to gadata
                lGadataComm.BulkCopyToGadata("EqUi", dt, "'Report 2$'");
                //run script to remove duplicates
                string cmdRemoveDup = @"
                DELETE gadata.[EqUi].['Report 2$']
                FROM gadata.[EqUi].['Report 2$'] as lx 
                LEFT OUTER JOIN (
                   SELECT 
                   MIN(l.id) as 'id' 
                   ,l.[WorkOrder Number]
                   ,l.Changedate
                   FROM  gadata.[EqUi].['Report 2$'] as l  
                   GROUP BY    
                    l.[WorkOrder Number]
                   ,l.Changedate
                ) as KeepRows ON
                   lx.Id = KeepRows.Id
                WHERE
                   KeepRows.Id IS NULL
                ";
                lGadataComm.RunCommandGadata(cmdRemoveDup);
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