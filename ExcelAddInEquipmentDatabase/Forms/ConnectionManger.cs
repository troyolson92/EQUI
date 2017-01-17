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
        }

        private void ConnectionManger_Shown(object sender, EventArgs e)
        {
            var _point = new System.Drawing.Point(Cursor.Position.X, Cursor.Position.Y);
            Top = _point.Y;
            Left = _point.X;
        }

        private void create_ODBC_connection(string QueryCmd, string ODBCconn, string connectionName )
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
                        oTable = oTables.Add(lconn, oRng);
                        oTable.RefreshStyle = Excel.XlCellInsertionMode.xlInsertEntireRows;
                        oTable.Name = connectionName;
                        oTable.Refresh(false); //this failes but everthing worked? 

                        /*
                        Excel.ListObject oListobject = (Excel.ListObject)oTemplateSheet.ListObjects.AddEx(
                        SourceType: Excel.XlListObjectSourceType.xlSrcRange,
                        Source: oRng,
                        XlListObjectHasHeaders: Excel.XlYesNoGuess.xlYes)
                        ;

                        oListobject.Name = "Test";
                         */
                    }
                }
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
            if (connection == null) return;
            string sNewName = Microsoft.VisualBasic.Interaction.InputBox("Change connection name", string.Format("Edit: {0}",connection.Name), connection.Name, -1, -1);
            if (sNewName != "") 
            {
                try //catch because name might already exist
                {
                    connection.Name = sNewName;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
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
                string Query = "use gadata EXEC Volvo." + cb_GADTA_procedures.Text.Trim();
                string ODBCconn = @"ODBC;DSN=GADATA;Description= GADATA;UID=GADATA;PWD=GADATA987;APP=SQLFront;WSID=GNL1004ZCBQC2\\SDEBEUL;DATABASE=GADATA";
                string ConnectionName = cb_GADTA_procedures.Text.Trim();
                create_ODBC_connection(Query, ODBCconn, ConnectionName);
            }
            lb_get_connections();
        }
        private void lb_GADATA_get_SpParams(SqlCommand cmd)
        {
            lb_GADATA_procParms.Items.Clear();
            lb_GADATA_procParms.Items.AddRange(new object[] { "ParameterNam","SqlDbType" });
            foreach (SqlParameter p in cmd.Parameters)
            {
                lb_GADATA_procParms.Items.AddRange(new object[] { p.ParameterName, p.SqlDbType });
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
                           where a.SYSTEM == "MX7"
                           orderby a.NAME descending
                           select a.NAME;
                cb_MX7_QueryNames.DataSource = data.Distinct().ToList();
            }
        }

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

        private void cb_MX7_QueryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_MX7_QueryDetails.Items.Clear();
            lb_MX7_QueryDetails.Items.Add(String.Format("{0}     {1}", "ParmName", "DefaultValue"));
            foreach (OracleQueryParms Parm in oracle_get_QueryParms_from_GADATA(cb_MX7_QueryNames.Text, "MX7"))
            {
                lb_MX7_QueryDetails.Items.Add(String.Format("{0}     {1}", Parm.name, Parm.value));
            }
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
                       where a.SYSTEM == "MX3"
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
            string ODBCconn = @"ODBC;DSN=MVCGP2;Description= MVCGP2;UID=maximo_ro;PWD=maximo_ro;";
            string ConnectionName = "MX3Test";
            create_ODBC_connection(Query, ODBCconn, ConnectionName);
        }

        private void cb_MX3_QueryNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            lb_MX3_QueryDetails.Items.Clear();
            lb_MX3_QueryDetails.Items.Add(String.Format("{0}     {1}", "ParmName", "DefaultValue"));
            foreach (OracleQueryParms Parm in oracle_get_QueryParms_from_GADATA(cb_MX3_QueryNames.Text, "MX3"))
            {
                lb_MX3_QueryDetails.Items.Add(String.Format("{0}     {1}", Parm.name, Parm.value));
            }
        }

        #endregion
        private List<OracleQueryParms> oracle_get_QueryParms_from_GADATA(string QueryName, string System)
        {
            string Query;
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Fill(lQUERYS);
                }
                Query = (from a in lQUERYS
                         where a.SYSTEM == System && a.NAME == QueryName
                         select a.QUERY).First().ToString();
            }
            //gets part of the query containing the params
            List<string> ParmLines = Query.ToUpper().Split(new string[] { "SELECT" }, StringSplitOptions.None)[0].Trim()
                                                    .Split(new string[] { "DEFINE" }, StringSplitOptions.None).ToList();

            List<OracleQueryParms> _parmList = new List<OracleQueryParms>();
            foreach (string parm in ParmLines)
            {
                if (parm.Contains("="))
                {
                    string ParmName = parm.Split('=')[0].Trim();
                    string ParmValue = parm.Split('=')[1].Trim().Split('\'')[1];
                    _parmList.Add(new OracleQueryParms { name = ParmName, value = ParmValue });
                }
            }
            return _parmList;
        }

    }
    public class OracleQueryParms
    {
        public string name { get; set; }
        public string value { get; set; }
    }
}
