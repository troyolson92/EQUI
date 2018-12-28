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
using EQUICommunictionLib;

namespace ExcelAddInEquipmentDatabase
{
    public partial class ExcelConnectionManager : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //connection to database
        ConnectionManager connectionManager = new ConnectionManager();
        // to GADATA for maximo querys
        OracleQuery lOracleQuery = new OracleQuery();
        //Query edit box instance
        Forms.MXxQueryEdit QEdit;

        public string DsnMX7 { get { return "MX7"; } }
        public string DsnGADATA { get { return "GADATA"; } }
        public string GADATAconnectionString
        {
            get { return @"ODBC;DSN=" + DsnGADATA + ";Description= GADATA;UID=EqUi;PWD=EqUi;APP=SQLFront;WSID=GNL1004ZCBQC2\\EQUI;DATABASE=GADATA"; }
        }
        public string MX7connectionString
        {
            get { return @"ODBC;DSN=" + DsnMX7 + ";Description= MAXIMO7;UID=ARCTVCG;PWD=vcg$tokfeb2017;"; }
        }


        public ExcelConnectionManager()
        {
            InitializeComponent();
            //lv init 
            lv_GADATA_procParms.Columns.Add("ParmName", -2, HorizontalAlignment.Left);
            lv_GADATA_procParms.Columns.Add("DefaultValue", -2, HorizontalAlignment.Left);
            lv_MX7_procParms.Columns.Add("ParmName", -2, HorizontalAlignment.Left);
            lv_MX7_procParms.Columns.Add("DefaultValue", -2, HorizontalAlignment.Left);
            //
            Lb_get_connections();
        }

        private void ConnectionManger_Shown(object sender, EventArgs e)
        {
            var _point = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            Top = _point.Y;
            Left = _point.X;
        }

        private void Create_ODBC_connection(string QueryCmd, string ODBCconn, string connectionName)
        {
            Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            Excel.Worksheet activeWorksheet = Globals.ThisAddIn.Application.ActiveSheet as Excel.Worksheet;

            Excel.Worksheet oTemplateSheet;
            Excel.Sheets oSheets;
            Excel.Range oRng;

            Excel.QueryTables oTables;
            Excel.QueryTable oTable;
            try
            {
                // get existing sheets
                oSheets = activeWorkbook.Sheets;
                // add new sheet to sheets
                oTemplateSheet = oSheets.Add();
                //select first cell of new sheet 
                oRng = oTemplateSheet.get_Range("A1");
                //set name of new sheet
                oTemplateSheet.Name = connectionName;
                // get the QueryTables collection
                oTables = oTemplateSheet.QueryTables;
                // create the workbook connection
                activeWorkbook.Connections.Add2(connectionName, connectionName, ODBCconn, QueryCmd);

                //add the query table
                foreach (Excel.WorkbookConnection lconn in activeWorkbook.Connections)
                {
                    if (lconn.Name == connectionName)
                    {
                        lconn.ODBCConnection.SavePassword = true;
                        oTable = oTables.Add(lconn, oRng);
                        oTable.RefreshStyle = Excel.XlCellInsertionMode.xlInsertEntireRows;
                        oTable.Name = connectionName;
                        oTable.Refresh(false); //this failes but everthing worked? 
                    }
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        //existing connections
        public Excel.WorkbookConnection Get_Connection(string connectionname)
        {
            Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            foreach (var connection in activeWorkbook.Connections.Cast<Excel.WorkbookConnection>())
            {
                if (connection.Name == connectionname)
                {
                    return connection;
                }
            }
            return null;
        }
        private void Lb_get_connections()
        {
            lb_connections.Items.Clear();
            Excel._Workbook activeWorkbook = Globals.ThisAddIn.Application.ActiveWorkbook as Excel.Workbook;
            foreach (var connection in activeWorkbook.Connections.Cast<Excel.WorkbookConnection>())
            {
                try
                {
                    switch (connection.Type)
                    {
                        case Excel.XlConnectionType.xlConnectionTypeODBC:
                            var ODBCconString = connection.ODBCConnection.Connection.ToString();
                            lb_connections.Items.Add(connection.Name);
                            break;
                        default:
                            break;
                    }
                }
                catch (Exception ex )
                {
                    log.Error(ex);
                }
            }

        }
        private void Btn_Delete_Click(object sender, EventArgs e)
        {
            Excel.WorkbookConnection connection = Get_Connection(lb_connections.GetItemText(lb_connections.SelectedItem));
            connection.Delete();
            Lb_get_connections();
        }
        private void Btn_Edit_Click(object sender, EventArgs e)
        {
            Excel.WorkbookConnection connection = Get_Connection(lb_connections.GetItemText(lb_connections.SelectedItem));
            if (connection == null) return;
            string sNewName = Microsoft.VisualBasic.Interaction.InputBox("Change connection name", string.Format("Edit: {0}", connection.Name), connection.Name, -1, -1);
            if (sNewName != "")
            {
                try //catch because name might already exist
                {
                    connection.Name = sNewName;
                }
                catch (Exception ex)
                {
                    log.Error(ex);
                }
            }
            Lb_get_connections();
        }
        //GADATA link
        #region GADATA
        private void Tp_GADATA_Enter(object sender, EventArgs e)
        {
            using (applData.StoredProceduresDataTable lPROCEDURES = new applData.StoredProceduresDataTable())
            {
                using (applDataTableAdapters.StoredProceduresTableAdapter adapter = new applDataTableAdapters.StoredProceduresTableAdapter())
                {
                    adapter.Fill(lPROCEDURES);
                }
                var data = from a in lPROCEDURES
                           orderby a.ROUTINE_NAME descending
                           select a.SPECIFIC_CATALOG + '.' + a.SPECIFIC_SCHEMA + '.' + a.ROUTINE_NAME;

                cb_GADTA_procedures.DataSource = data.Distinct().ToList();
            }
        }

        private void Btn_GADATA_Create_Click(object sender, EventArgs e)
        {
            if (cb_GADTA_procedures.Text != "")
            {
               // lGadataComm.Make_DSN();
                string Query = "use gadata EXEC " + cb_GADTA_procedures.Text.Trim();
                string ODBCconn = GADATAconnectionString; 
                string ConnectionName = cb_GADTA_procedures.Text.Split('.')[2].Trim();
                Create_ODBC_connection(Query, ODBCconn, ConnectionName);
            }
            Lb_get_connections();
            this.Hide();
            this.Dispose();
        }
        private void Lb_GADATA_get_SpParams(SqlCommand cmd)
        {
            lv_GADATA_procParms.Items.Clear();
            foreach (SqlParameter p in cmd.Parameters)
            {
                ListViewItem item = new ListViewItem(p.ParameterName);
                item.SubItems.Add(p.SqlDbType.ToString());
                lv_GADATA_procParms.Items.Add(item);
            }
        }
        private void Cb_GADATA_procedures_SelectedIndexChanged(object sender, EventArgs e)
        {
            Lb_GADATA_get_SpParams(connectionManager.GetSpParms(cb_GADTA_procedures.Text));
        }
        #endregion

        #region Maximo 7
        //Maximo7 link
        private void Tp_MX7_Enter(object sender, EventArgs e)
        {
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Fill(lQUERYS);
                }
                var data = from a in lQUERYS
                           where a.SYSTEM == DsnMX7
                           orderby a.NAME descending
                           select a.NAME;
                cb_MX7_QueryNames.DataSource = data.Distinct().ToList();
            }
        }

        private void Btn_MX7_create_Click(object sender, EventArgs e)
        {
            string Query;
            using (Forms.ProcedureManager  ProcMngr = new Forms.ProcedureManager(DsnMX7))
          {
              ProcMngr.MX7_ActiveConnectionToProcMngr(lOracleQuery.oracle_get_QueryParms_from_GADATA(cb_MX7_QueryNames.Text, DsnMX7), "It does not exist");
              Query = ProcMngr.MX7_BuildQuery_ProcMngrToActiveConnection(lOracleQuery.oracle_get_QueryTemplate_from_GADATA(cb_MX7_QueryNames.Text, DsnMX7));
          }
           // lMaximoComm.Make_DSN(lMaximoComm.SystemMX7);
            string ODBCconn = MX7connectionString;
            string ConnectionName = cb_MX7_QueryNames.Text;
            Create_ODBC_connection(Query, ODBCconn, ConnectionName);
            this.Hide();
            this.Dispose();
        }

        private void Cb_MX7_QueryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Fill(lQUERYS);
                }
                lbl_MX7_procDiscription.Text = (from a in lQUERYS
                                                where a.SYSTEM == DsnMX7 && a.NAME == cb_MX7_QueryNames.Text
                                                select a.DISCRIPTION).First().ToString();
            }
            lv_MX7_procParms.Items.Clear();
            foreach (OracleQueryParm Parm in lOracleQuery.oracle_get_QueryParms_from_GADATA(cb_MX7_QueryNames.Text, DsnMX7))
            {
                ListViewItem item = new ListViewItem(Parm.ParameterName);
                item.SubItems.Add(Parm.Defaultvalue);
                lv_MX7_procParms.Items.Add(item);
            }
        }

        private void Btn_MX7_new_Click(object sender, EventArgs e)
        {
            if (QEdit != null) QEdit.Dispose();
            QEdit = new Forms.MXxQueryEdit
            {
                TargetSystem = DsnMX7
            };
            QEdit.Show();
        }

        private void Btn_MX7_edit_Click(object sender, EventArgs e)
        {
            if (QEdit != null) QEdit.Dispose();
            QEdit = new Forms.MXxQueryEdit
            {
                TargetSystem = DsnMX7,
                QueryName = cb_MX7_QueryNames.Text,
                QueryDiscription = lbl_MX7_procDiscription.Text,
                Query = lOracleQuery.oracle_get_QueryTemplate_from_GADATA(cb_MX7_QueryNames.Text, DsnMX7)
            };
            QEdit.Show();
        }
        #endregion

    }
}
