﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;
using System.Data.SqlClient;
using Microsoft.VisualBasic;


namespace ExcelAddInEquipmentDatabase
{
    public partial class ConnectionManger : Form
    {
        //connection to gadata
        GadataComm lGadataComm = new GadataComm();

        public ConnectionManger()
        {
            InitializeComponent();
            lb_get_connections();
            cb_GADATA_procedures_fill();
        }

        private void create_ODBC_connection(string QueryCmd, string ODBCconn, string connectionName )
        {
            Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            Excel.Worksheet activeWorksheet = Globals.ThisAddIn.Application.ActiveSheet as Excel.Worksheet;

            Excel.Worksheet oTemplateSheet;
            Excel.Sheets oSheets;
            Excel.Range oRng;

            /*
            Excel.Connections oConnections;
            Excel.XlConnectionType oXLConnection;
            Excel.ODBCConnection oODBCconnection;

            Excel.ListObjects oListObjects;
            Excel.ListObject oListObject;
            */
            Excel.QueryTables oTables;
            Excel.QueryTable oTable;
            try
            {
                //create Query string 
                string SQLStr;
                SQLStr = QueryCmd;
                object aStrSQL = SQLStr;
                //create connection string 

                //conn.Name = storedProc;
                object connection = ODBCconn;

                // get existing sheets
                oSheets = activeWorkbook.Sheets;
                // add new sheet to sheets
                oTemplateSheet = oSheets.Add();
                //select first cell of new sheet 
                oRng = oTemplateSheet.get_Range("A1");
                //set name of new sheet
                oTemplateSheet.Name = connectionName;

                // Get the listobjects 
                /*
                    oListObjects = activeWorksheet.ListObjects;
                    oListObject = oListObjects.AddEx();
                    oListObject.Name = storedProc;
                    oListObject.TableStyle = "";
                  */

                // get the QueryTables collection
                oTables = oTemplateSheet.QueryTables;
                // create a query table with the connection and SQL command
                oTable = oTables.Add(connection, oRng, aStrSQL);
                oTable.RefreshStyle = Excel.XlCellInsertionMode.xlInsertEntireRows;
                oTable.Name = connectionName;
                oTable.Refresh(false); //this failes but everthing worked? 
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        //existing connections
        public Excel.WorkbookConnection get_Connection(string connectionname)
        { 
            Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            foreach (var connection in activeWorkbook.Connections.Cast<Excel.WorkbookConnection>())
            {
                if (connection.Name == connectionname)
                {
                    return connection;
                }
                Debug.WriteLine(connection.Name);
            }
            return null;
        }
        private void lb_get_connections()
        {
            lb_connections.Items.Clear();
            Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            foreach (var connection in activeWorkbook.Connections.Cast<Excel.WorkbookConnection>())
            {
                switch (connection.Type)
                {
                    case Excel.XlConnectionType.xlConnectionTypeODBC:
                        var ODBCconString = connection.ODBCConnection.Connection.ToString();
                        Debug.WriteLine("ODBC Name: {0}",connection.Name);
                        lb_connections.Items.Add(connection.Name);
                        break;
                    default :
                        Debug.WriteLine("connection tpye not supported");
                        break;
                }
            }

        }
        private void btn_Delete_Click(object sender, EventArgs e)
        {
            Excel.WorkbookConnection connection = get_Connection(lb_connections.GetItemText(lb_connections.SelectedItem));
            connection.Delete();
            lb_get_connections();
        }
        private void btn_Edit_Click(object sender, EventArgs e)
        {
            Excel.WorkbookConnection connection = get_Connection(lb_connections.GetItemText(lb_connections.SelectedItem));
            connection.Name = "Newname";
            lb_get_connections();
        }
        //GADATA link
        private void btn_GADATA_Create_Click(object sender, EventArgs e)
        {
            if (cb_procedures.Text != "")
            {
                string Query = "use gadata EXEC Volvo." + cb_procedures.Text.Trim();
                string ODBCconn = @"ODBC;DSN=GADATA;Description= GADATA;UID=GADATA;PWD=GADATA987;APP=SQLFront;WSID=GNL1004ZCBQC2\\SDEBEUL;DATABASE=GADATA";
                string ConnectionName = cb_procedures.Text.Trim();
                create_ODBC_connection(Query, ODBCconn, ConnectionName);
            }
            lb_get_connections();
        }
        private void cb_GADATA_procedures_fill()
        {
            try
            {
                applData.StoredProceduresDataTable lPROCEDURES = new applData.StoredProceduresDataTable();
                using (applDataTableAdapters.StoredProceduresTableAdapter adapter = new applDataTableAdapters.StoredProceduresTableAdapter())
                {
                    adapter.Fill(lPROCEDURES);
                }
                var data = from a in lPROCEDURES
                           orderby a.ROUTINE_NAME descending
                           select a.ROUTINE_NAME;

                cb_procedures.DataSource = data.Distinct().ToList();
            }
            catch (Exception ex)
            {
                cb_procedures.Text = "%";
                Debug.WriteLine(ex.Message);
            }
        }
        private void lb_GADATA_get_SpParams(SqlCommand cmd)
        {
            lb_procParms.Items.Clear();
            string header = string.Format("{0,-50}   {1,30}\n", "pName", "pSqlDbType");
            lb_procParms.Items.Add(header);
            foreach (SqlParameter p in cmd.Parameters)
            {
                try
                {
                    string item = string.Format("{0,-50}   {1,30}", p.ParameterName, p.SqlDbType);
                    lb_procParms.Items.Add(item);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }
        private void cb_GADATA_procedures_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_GADATA_get_SpParams(lGadataComm.get_GADATA_sp_parameters(cb_procedures.Text));
        }
        //Maximo7 link
        private void btn_MX7_create_Click(object sender, EventArgs e)
        {
            string Query = @"
                Select STATUS, WORKTYPE, DESCRIPTION, LOCATION, WONUM, REPORTDATE 
                from MAXIMO.WORKORDER WORKORDER 
                where 
                (WORKORDER.location LIKE '5310%') 
                order by REPORTDATE
                    ";
            string ODBCconn = @"ODBC;DSN=Max;Description= MX7;UID=BGASTHUY;PWD=BGASTHUY$123;";
            string ConnectionName = "MX7Test";
            create_ODBC_connection(Query, ODBCconn, ConnectionName);
        }
        //Maximo3 link
        private void btn_MX3_create_Click(object sender, EventArgs e)
        {

            // NOT WORKING YET 
            string Query = @"
                Select LDKEY, STATUS, WORKTYPE, DESCRIPTION, LOCATION, WONUM, WOPM1, REPORTDATE 
                from MAXIMO.WORKORDER WORKORDER 
                where 
                (WORKORDER.location LIKE '%99070R01%') 
                order by REPORTDATE
                    ";
            string ODBCconn = @"ODBC;DSN=MVCGP2;Description= MVCGP2;UID=maximo_ro;PWD=maximo_ro;";
            string ConnectionName = "MX3Test";
            create_ODBC_connection(Query, ODBCconn, ConnectionName);
        }
    }
}
