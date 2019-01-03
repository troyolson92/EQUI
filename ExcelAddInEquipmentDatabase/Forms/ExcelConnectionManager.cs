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
using System.Configuration;

namespace ExcelAddInEquipmentDatabase
{
    public partial class ExcelConnectionManager : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //connection to database
        ConnectionManager connectionManager = new ConnectionManager();
        EquiEntities db = new EquiEntities();
        //Query edit box instance
        Forms.MXxQueryEdit QEdit;

        public string EquiODBCconnectionString
        {
            get {
                System.Data.SqlClient.SqlConnectionStringBuilder sqlconnection = new System.Data.SqlClient.SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString);
                return $"ODBC;DSN={DsnNames.DsnGADATA}; UID={sqlconnection.UserID}; PWD={sqlconnection.Password}";
            }
        }
        public string MX7ODBCconnectionString
        {
            get { return $"ODBC;DSN={DsnNames.DsnMX7}; UID=ARCTVCG; PWD=vcg$tokfeb2017"; }
        }

        public ExcelConnectionManager()
        {
            InitializeComponent();
            lv_GADATA_procParms.Columns.Add("ParmName", -2, HorizontalAlignment.Left);
            lv_GADATA_procParms.Columns.Add("DefaultValue", -2, HorizontalAlignment.Left);
            lv_MX7_procParms.Columns.Add("ParmName", -2, HorizontalAlignment.Left);
            lv_MX7_procParms.Columns.Add("DefaultValue", -2, HorizontalAlignment.Left);
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
           cb_GADTA_procedures.DataSource = db.Database.SqlQuery<string>($"select SPECIFIC_CATALOG + '.' + SPECIFIC_SCHEMA + '.' + ROUTINE_NAME  from information_schema.routines where routine_type = 'PROCEDURE' AND SPECIFIC_SCHEMA = '{Properties.Settings.Default.TemplateShema.ToString()}'").ToList();
        }

        private void Btn_GADATA_Create_Click(object sender, EventArgs e)
        {
            if (cb_GADTA_procedures.Text != "")
            {
                Create_ODBC_connection("EXEC " + cb_GADTA_procedures.Text.Trim(), EquiODBCconnectionString, cb_GADTA_procedures.Text.Split('.')[2].Trim());
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
            cb_MX7_QueryNames.DataSource = db.QUERYS.Where(c => c.SYSTEM == DsnNames.DsnMX7).Select(c => c.NAME).ToList();
        }

        private void Btn_MX7_create_Click(object sender, EventArgs e)
        {        
            using (Forms.ProcedureManager  ProcMngr = new Forms.ProcedureManager(DsnNames.DsnMX7))
            {
                QUERYS Qry = db.QUERYS.Where(c => c.NAME == cb_MX7_QueryNames.Text && c.SYSTEM == DsnNames.DsnMX7).First();
                ProcMngr.MX7_ActiveConnectionToProcMngr(Qry.OracleQueryParms, "It does not exist");
                Create_ODBC_connection(ProcMngr.MX7_BuildQuery_ProcMngrToActiveConnection(Qry.QueryBody), MX7ODBCconnectionString, cb_MX7_QueryNames.Text);
            }
            this.Hide();
            this.Dispose();
        }

        private void Cb_MX7_QueryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            QUERYS Qry = db.QUERYS.Where(c => c.NAME == cb_MX7_QueryNames.Text && c.SYSTEM == DsnNames.DsnMX7).First();
            lbl_MX7_procDiscription.Text = Qry.DISCRIPTION;

            lv_MX7_procParms.Items.Clear();
            foreach (OracleQueryParm Parm in Qry.OracleQueryParms)
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
                TargetSystem = DsnNames.DsnMX7
            };
            QEdit.Show();
        }

        private void Btn_MX7_edit_Click(object sender, EventArgs e)
        {
            if (QEdit != null) QEdit.Dispose();
            QUERYS Qry = db.QUERYS.Where(c => c.NAME == cb_MX7_QueryNames.Text && c.SYSTEM == DsnNames.DsnMX7).First();
            QEdit = new Forms.MXxQueryEdit
            {
                TargetSystem = DsnNames.DsnMX7,
                QueryName = cb_MX7_QueryNames.Text,
                QueryDiscription = lbl_MX7_procDiscription.Text,
                Query = Qry.QUERY
            };
            QEdit.Show();
        }
        #endregion

    }
}
