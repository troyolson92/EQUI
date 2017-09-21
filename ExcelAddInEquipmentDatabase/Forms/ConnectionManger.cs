using System;
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
    public partial class ConnectionManger : Form
    {
        //debugger
        myDebugger Debugger = new myDebugger();
        //connection to gadata
        GadataComm lGadataComm = new GadataComm();
        //connection to maximo 
        MaximoComm lMaximoComm = new MaximoComm();
        //connection to GADATA for maximo querys
        MaximoQuery lMaximoQuery = new MaximoQuery();
        //Query edit box instance
        Forms.MXxQueryEdit QEdit;

        public ConnectionManger()
        {
            InitializeComponent();
            //lv init 
            lv_GADATA_procParms.Columns.Add("ParmName", -2, HorizontalAlignment.Left);
            lv_GADATA_procParms.Columns.Add("DefaultValue", -2, HorizontalAlignment.Left);
            lv_MX7_procParms.Columns.Add("ParmName", -2, HorizontalAlignment.Left);
            lv_MX7_procParms.Columns.Add("DefaultValue", -2, HorizontalAlignment.Left);
            lv_MX3_procParms.Columns.Add("ParmName", -2, HorizontalAlignment.Left);
            lv_MX3_procParms.Columns.Add("DefaultValue", -2, HorizontalAlignment.Left);
            //
            lb_get_connections();
        }

        private void ConnectionManger_Shown(object sender, EventArgs e)
        {
            var _point = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            Top = _point.Y;
            Left = _point.X;
        }

        private void create_ODBC_connection(string QueryCmd, string ODBCconn, string connectionName)
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
            catch (Exception e)
            {
               Debugger.Exeption(e);
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
            }
            return null;
        }
        private void lb_get_connections()
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
                catch (Exception e )
                {
                 Debugger.Exeption(e);
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
                   Debugger.Exeption(ex);
                }
            }
            lb_get_connections();
        }
        //GADATA link
        #region GADATA
        private void tp_GADATA_Enter(object sender, EventArgs e)
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

        private void btn_GADATA_Create_Click(object sender, EventArgs e)
        {
            if (cb_GADTA_procedures.Text != "")
            {
                lGadataComm.make_DSN();
                string Query = "use gadata EXEC " + cb_GADTA_procedures.Text.Trim();
                string ODBCconn = lGadataComm.GADATAconnectionString; 
                string ConnectionName = cb_GADTA_procedures.Text.Split('.')[2].Trim();
                create_ODBC_connection(Query, ODBCconn, ConnectionName);
            }
            lb_get_connections();
            this.Hide();
            this.Dispose();
        }
        private void lb_GADATA_get_SpParams(SqlCommand cmd)
        {
            lv_GADATA_procParms.Items.Clear();
            foreach (SqlParameter p in cmd.Parameters)
            {
                ListViewItem item = new ListViewItem(p.ParameterName);
                item.SubItems.Add(p.SqlDbType.ToString());
                lv_GADATA_procParms.Items.Add(item);
            }
        }
        private void cb_GADATA_procedures_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_GADATA_get_SpParams(lGadataComm.get_GADATA_sp_parameters(cb_GADTA_procedures.Text));
        }
        #endregion

        #region Maximo 7
        //Maximo7 link
        private void tp_MX7_Enter(object sender, EventArgs e)
        {
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Fill(lQUERYS);
                }
                var data = from a in lQUERYS
                           where a.SYSTEM == lMaximoComm.SystemMX7
                           orderby a.NAME descending
                           select a.NAME;
                cb_MX7_QueryNames.DataSource = data.Distinct().ToList();
            }
        }

        private void btn_MX7_create_Click(object sender, EventArgs e)
        {
            string Query;
            using (Forms.ProcedureManeger  ProcMngr = new Forms.ProcedureManeger(lMaximoComm.SystemMX7))
          {
              ProcMngr.MX7_ActiveConnectionToProcMngr(lMaximoQuery.oracle_get_QueryParms_from_GADATA(cb_MX7_QueryNames.Text, lMaximoComm.SystemMX7), "It does not exist");
              Query = ProcMngr.MX7_BuildQuery_ProcMngrToActiveConnection(lMaximoQuery.oracle_get_QueryTemplate_from_GADATA(cb_MX7_QueryNames.Text, lMaximoComm.SystemMX7));
          }
            lMaximoComm.make_DSN(lMaximoComm.SystemMX7);
            string ODBCconn = lMaximoComm.MX7connectionString;
            string ConnectionName = cb_MX7_QueryNames.Text;
            create_ODBC_connection(Query, ODBCconn, ConnectionName);
            this.Hide();
            this.Dispose();
        }

        private void cb_MX7_QueryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Fill(lQUERYS);
                }
                lbl_MX7_procDiscription.Text = (from a in lQUERYS
                                                where a.SYSTEM == lMaximoComm.SystemMX7 && a.NAME == cb_MX7_QueryNames.Text
                                                select a.DISCRIPTION).First().ToString();
            }
            lv_MX7_procParms.Items.Clear();
            foreach (OracleQueryParm Parm in lMaximoQuery.oracle_get_QueryParms_from_GADATA(cb_MX7_QueryNames.Text, lMaximoComm.SystemMX7))
            {
                ListViewItem item = new ListViewItem(Parm.ParameterName);
                item.SubItems.Add(Parm.Defaultvalue);
                lv_MX7_procParms.Items.Add(item);
            }
        }

        private void btn_MX7_new_Click(object sender, EventArgs e)
        {
            if (QEdit != null) QEdit.Dispose();
            QEdit = new Forms.MXxQueryEdit();
            QEdit.TargetSystem = lMaximoComm.SystemMX7;
            QEdit.Show();
        }

        private void btn_MX7_edit_Click(object sender, EventArgs e)
        {
            if (QEdit != null) QEdit.Dispose();
            QEdit = new Forms.MXxQueryEdit();
            QEdit.TargetSystem = lMaximoComm.SystemMX7;
            QEdit.QueryName = cb_MX7_QueryNames.Text;
            QEdit.QueryDiscription =  lbl_MX7_procDiscription.Text;
            QEdit.Query = lMaximoQuery.oracle_get_QueryTemplate_from_GADATA(cb_MX7_QueryNames.Text, lMaximoComm.SystemMX7);
            QEdit.Show();
        }
        #endregion

        #region MAXIMO3
        //Maximo3 link
        private void tp_MX3_Enter(object sender, EventArgs e)
        {
            applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable();
            using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
            {
                adapter.Fill(lQUERYS);
            }
            var data = from a in lQUERYS
                       where a.SYSTEM == lMaximoComm.SystemMX3
                       orderby a.NAME descending
                       select a.NAME;
            cb_MX3_QueryNames.DataSource = data.Distinct().ToList();
        }

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
            lMaximoComm.make_DSN(lMaximoComm.SystemMX3);
            string ODBCconn = @"ODBC;DSN=MVCGP2;Description= MVCGP2;UID=maximo_ro;PWD=maximo_ro;";
            string ConnectionName = "MX3Test";
            create_ODBC_connection(Query, ODBCconn, ConnectionName);

            this.Hide();
            this.Dispose();
        }

        private void cb_MX3_QueryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
         using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Fill(lQUERYS);
                }
                lbl_MX3_procDiscription.Text = (from a in lQUERYS
                           where a.SYSTEM == lMaximoComm.SystemMX3 && a.NAME == cb_MX3_QueryNames.Text
                           select a.DISCRIPTION).First().ToString();
           }
            lv_MX3_procParms.Items.Clear();
            foreach (OracleQueryParm Parm in lMaximoQuery.oracle_get_QueryParms_from_GADATA(cb_MX3_QueryNames.Text, lMaximoComm.SystemMX3))
            {
                ListViewItem item = new ListViewItem(Parm.ParameterName);
                item.SubItems.Add(Parm.Defaultvalue);
                lv_MX3_procParms.Items.Add(item);
            }
        }

        private void btn_MX3_edit_Click(object sender, EventArgs e)
        {
            if (QEdit != null) QEdit.Dispose();
            QEdit = new Forms.MXxQueryEdit();
            QEdit.TargetSystem = lMaximoComm.SystemMX3;
            QEdit.Show();
        }

        private void btn_MX3_new_Click(object sender, EventArgs e)
        {
            if (QEdit != null) QEdit.Dispose();
            QEdit = new Forms.MXxQueryEdit();
            QEdit.TargetSystem = lMaximoComm.SystemMX3;
            QEdit.QueryName = cb_MX7_QueryNames.Text;
            QEdit.QueryDiscription = lbl_MX3_procDiscription.Text;
            QEdit.Query = lMaximoQuery.oracle_get_QueryTemplate_from_GADATA(cb_MX3_QueryNames.Text, lMaximoComm.SystemMX3);
            QEdit.Show();
        }
        #endregion

    }
}
