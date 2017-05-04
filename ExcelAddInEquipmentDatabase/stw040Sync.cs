using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using System.Globalization;
using System.Data;
using System.Net;

namespace ExcelAddInEquipmentDatabase
{
    class stw040Sync
    {
        Debugger ldebugger = new Debugger();

       public void get_swt040data()
        {
            try
            {
                //download
                WebClient Client = new WebClient();
                Client.DownloadFile("http://biwebreports.got.volvocars.net:8080/reports/vcg/VCG_Manufacturing_And_Supply_Chain/Manufacturing/Disturbances_And_Maintenance/Storingsrapport GA _dumpfile sched.xlsx"
                    , @"C:\Temp\Storingsrapport GA _dumpfile sched.xlsx");
                //read into dt 
                DataTable dt = LoadWorksheetInDataTable(@"C:\Temp\Storingsrapport GA _dumpfile sched.xlsx", "Zone - Lijst - Laatste werkdag$");
                //push to gadata
                GadataComm lGadataComm = new GadataComm();
                lGadataComm.BulkCopyToGadata("EqUi", dt, "'Zone - Lijst - Laatste werkdag$'");
                //run script to remove duplicates

            }
           catch (Exception ex)
            {
                ldebugger.Exeption(ex);
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