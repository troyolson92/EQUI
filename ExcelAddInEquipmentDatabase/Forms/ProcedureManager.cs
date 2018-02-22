using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using Excel = Microsoft.Office.Interop.Excel;
using EQUICommunictionLib;

namespace ExcelAddInEquipmentDatabase.Forms
{

    public partial class ProcedureManager : UserControl
    {
        //debugger
        myDebugger Debugger = new myDebugger();
        //connection to gadata
        GadataComm lGadataComm = new GadataComm();
        //connection to maximo 
        MaximoComm lMaximoComm = new MaximoComm();
        //connection to GADATA maximo querys 
        MaximoQuery lMaximoQuery = new MaximoQuery();
        //active connection in active workbook
        ActiveConnection lActConn = new ActiveConnection();

        #region parameters to the ribbon
        Forms.uc_Datebox _startDate = new Forms.uc_Datebox();
        Forms.uc_Datebox _endDate = new Forms.uc_Datebox();
        Forms.uc_Inputbox _assets = new Forms.uc_Inputbox();
        Forms.uc_Inputbox _lochierarchy = new Forms.uc_Inputbox();
        Forms.uc_Inputbox _locations = new Forms.uc_Inputbox();
        Forms.uc_Inputbox _daysBack = new Forms.uc_Inputbox();
        public Forms.uc_Inputbox daysBack
        {
            get
            {
                if (_daysBack.input == "") { _daysBack.input = "1"; }
                return _daysBack;
            }
            set { _daysBack = value; }
        }
        public Forms.uc_Inputbox assets
        {
            get { return _assets; }
            set { _assets = value; }
        }
        public Forms.uc_Inputbox lochierarchy
        {
            get { return _lochierarchy; }
            set { _lochierarchy = value; }
        }
        public Forms.uc_Inputbox locations
        {
            get { return _locations; }
            set { _locations = value; }
        }
        public Forms.uc_Datebox startDate
        {
            get { return _startDate; }
            set { _startDate = value; }
        }
        public Forms.uc_Datebox endDate
        {
            get { return _endDate; }
            set { _endDate = value; }
        }
        public string activeconnection
        {
            get { return lActConn.Name; }
        }
        public string activeSystem
        {
            get { return lActConn.System; }
        }
        public string ProcedureName
        {
            get { return lActConn.ProcedureName; }
        }
        #endregion

        public ProcedureManager(string activeconnection)
        {
            InitializeComponent();
            //
            if (Properties.Settings.Default.userlevel >= 10) 
            { Btn_saveSet.Visible = true; }
            else 
            { Btn_saveSet.Visible = false; }
            //
            if (activeconnection == lMaximoComm.SystemMX7) //CREATE NEW MX7connections
            {
                assets.Name = "_assets".ToUpper();
                lochierarchy.Name = "_lochierarchy".ToUpper();
                locations.Name = "_locations".ToUpper();
                startDate.Name = "_StartDate".ToUpper();
                endDate.Name = "_EndDate".ToUpper();
                daysBack.Name = "_nDays".ToUpper();
                return;
            }
            //
            lActConn.Name = activeconnection;
            lActConn.ODBCconnString = lActConn.get_ODBCconnString_from_activeconnection();
            //
            if (lActConn.ODBCconnString.Contains(lMaximoComm.DsnMX7)) //existing MAXIMO7 connection
            {
                assets.Name = "_assets".ToUpper();
                lochierarchy.Name = "_lochierarchy".ToUpper();
                locations.Name = "_locations".ToUpper();
                startDate.Name = "_StartDate".ToUpper();
                endDate.Name = "_EndDate".ToUpper();
                daysBack.Name = "_nDays".ToUpper();
                //
                lActConn.System = lMaximoComm.DsnMX7;
                lActConn.ProcedureName = lActConn.Name; //maximo system dont have a stored proc name so we take the name of the Query template
                MX7_ActiveConnectionToProcMngr(lMaximoQuery.oracle_get_QueryParms_from_GADATA(activeconnection, lMaximoComm.SystemMX7)
                    , lMaximoQuery.oracle_get_QueryTemplate_from_GADATA(activeconnection, lMaximoComm.SystemMX7));
            }
            else if (lActConn.ODBCconnString.Contains(lGadataComm.DsnGADATA)) //existing GADATAconnections
            {
                assets.Name = "@assets";
                lochierarchy.Name = "@lochierarchy";
                locations.Name = "@locations";
                startDate.Name = "@StartDate";
                endDate.Name = "@EndDate";
                daysBack.Name = "@daysBack";
                //
                lActConn.System = lGadataComm.DsnGADATA;
                lActConn.ProcedureName = lActConn.get_storedProcedureFromQuery(lActConn.Query);
                GADATA_ActiveConnectionToProcMngr(lGadataComm.Get_GADATA_sp_parameters(lActConn.ProcedureName), lActConn.Query);
            }
            else
            {
                //SDEBEUL 18w4d5 uit dienst gezet omdat dit irritant voor Freidant.
                //Debugger.Message(string.Format("Unable to read connection DSN must be {0} or {1}",lGadataComm.DsnGADATA,lMaximoComm.DsnMX7));
            }
        }

        //guess the parameter type based on its value
        private TypeCode guess_typecode(string sValue)
        {
            try { if (DateTime.ParseExact(sValue, "yyyy-MM-dd hh:mm:ss", CultureInfo.InvariantCulture) != null) return TypeCode.DateTime; }
            catch { }
            try { if (DateTime.ParseExact(sValue, "yyyy-MM-dd", CultureInfo.InvariantCulture) != null) return TypeCode.DateTime; }
            catch { }
            //try { if (int.Parse(sValue.Trim(), CultureInfo.InvariantCulture) > -1) return TypeCode.Int32; }
            //catch { }
            //try { if (bool.Parse(sValue.Trim()) != null) return TypeCode.Boolean; }
            //catch { }
            return TypeCode.String;
        }

        /*
         * populates the procmanger with the query of the active connection
         * FOR STORED PROCEDURES FROM GADATA
         *  and links the default ribbon controls if needed
         */
        public void GADATA_ActiveConnectionToProcMngr(SqlCommand cmd, string Query)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = true;

            foreach (SqlParameter p in cmd.Parameters)
            {
                if (p.ParameterName == "@RETURN_VALUE") continue;
                try
                {
                    switch (p.SqlDbType)
                    {
                        case SqlDbType.DateTime:  //create datetime pickers 
                            //get dt value from Query string 
                            DateTime dtSelectedTimestamp = Convert.ToDateTime("1900-01-01 00:00:00");
                            if (Query.Contains(p.ParameterName)) dtSelectedTimestamp = Convert.ToDateTime(Query.Substring(Query.IndexOf(p.ParameterName) + p.ParameterName.Length).Split('\'')[1]);

                            if (p.ParameterName == _startDate.Name) //handel default ribbon controls. "@startdate"
                            {
                                _startDate.Name = p.ParameterName;
                                _startDate.label = p.ParameterName;
                                if (Query.Contains(p.ParameterName))
                                {
                                    _startDate.active = true;
                                    _startDate.input = dtSelectedTimestamp;
                                }
                                else
                                {
                                    _startDate.active = false;
                                }
                                flowLayoutPanel1.Controls.Add(_startDate);
                            }
                            else if (p.ParameterName == _endDate.Name) //handel default ribbon controls. "@enddate"
                            {
                                _endDate.Name = p.ParameterName;
                                _endDate.label = p.ParameterName;
                                if (Query.Contains(p.ParameterName))
                                {
                                    _endDate.active = true;
                                    _endDate.input = dtSelectedTimestamp;
                                }
                                else
                                {
                                    _endDate.active = false;
                                }
                                flowLayoutPanel1.Controls.Add(_endDate);
                            }
                            else
                            { //in case of an other timestamp create a datetimepicker for it
                                Forms.uc_Datebox nDateb = new Forms.uc_Datebox();
                                nDateb.Name = p.ParameterName;
                                nDateb.label = p.ParameterName;
                                if (Query.Contains(p.ParameterName))
                                {
                                    nDateb.active = true;
                                    nDateb.input = dtSelectedTimestamp;
                                }
                                else
                                {
                                    nDateb.active = false;
                                }
                                flowLayoutPanel1.Controls.Add(nDateb);
                            }
                            break;

                        case SqlDbType.Bit: //create checkboxes for bits
                            Forms.uc_Checkbox nCheckb = new Forms.uc_Checkbox();
                            nCheckb.Name = p.ParameterName;
                            nCheckb.label = p.ParameterName;
                            nCheckb.active = false;
                            nCheckb.input = false;
                            if (Query.Contains(p.ParameterName))
                            {
                                nCheckb.active = true;
                                string sValuefirst = Query.Substring(Query.IndexOf(p.ParameterName) + p.ParameterName.Length).Split('=')[1].Trim();
                                if (sValuefirst.StartsWith("true", StringComparison.InvariantCultureIgnoreCase) || sValuefirst.StartsWith("1", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    nCheckb.input = true;
                                }
                                else
                                {
                                    nCheckb.input = false;
                                }
                            }
                            flowLayoutPanel1.Controls.Add(nCheckb);
                            break;

                        case SqlDbType.Int: // in case of bit create a editbox for it
                            if (p.ParameterName == _daysBack.Name)  //handel default ribbon controls. @daysback
                            {
                                _daysBack.Name = p.ParameterName;
                                _daysBack.label = p.ParameterName;
                                _daysBack.intOnly = true;
                                _daysBack.active = true;
                                if (Query.Contains(p.ParameterName))
                                {
                                    _daysBack.active = true;
                                    string sValuefirst = Query.Substring(Query.IndexOf(p.ParameterName) + p.ParameterName.Length).Split('=')[1].Trim();
                                    if (sValuefirst.Contains(','))
                                    {
                                        sValuefirst = sValuefirst.Split(',')[0].Trim();
                                    }
                                    _daysBack.input = sValuefirst.Split(' ')[0].Trim();
                                }
                                flowLayoutPanel1.Controls.Add(_daysBack);
                            }
                            else
                            {
                                Forms.uc_Inputbox nInputbInt = new Forms.uc_Inputbox();
                                nInputbInt.Name = p.ParameterName;
                                nInputbInt.label = p.ParameterName;
                                nInputbInt.intOnly = true;
                                nInputbInt.active = false;
                                nInputbInt.input = "0";
                                if (Query.Contains(p.ParameterName))
                                {
                                    nInputbInt.active = true;
                                    string sValuefirst = Query.Substring(Query.IndexOf(p.ParameterName) + p.ParameterName.Length).Split('=')[1].Trim();
                                    if (sValuefirst.Contains(','))
                                    {
                                        sValuefirst = sValuefirst.Split(',')[0].Trim();
                                    }
                                    nInputbInt.input = sValuefirst.Split(' ')[0].Trim();
                                }
                                flowLayoutPanel1.Controls.Add(nInputbInt);
                            }
                            break;

                        case SqlDbType.VarChar: // in case of bit create a editbox for it
                            //get current value from querystring
                            string sInput = "";
                            if (Query.Contains(p.ParameterName)) sInput = Query.Substring(Query.IndexOf(p.ParameterName) + p.ParameterName.Length).Split('\'')[1];

                            if (p.ParameterName == _assets.Name) //handel default ribbon controls. @Assets
                            {
                                _assets.Name = p.ParameterName;
                                _assets.label = p.ParameterName;
                                _assets.intOnly = false;
                                if (Query.Contains(p.ParameterName))
                                {
                                    _assets.active = true;
                                    _assets.input = sInput;
                                }
                                else
                                {
                                    _assets.active = false;
                                }
                                flowLayoutPanel1.Controls.Add(_assets);
                            }
                            else if (p.ParameterName == _locations.Name) //handel default ribbon controls. @Locations
                            {
                                _locations.Name = p.ParameterName;
                                _locations.label = p.ParameterName;
                                _locations.intOnly = false;
                                if (Query.Contains(p.ParameterName))
                                {
                                    _locations.active = true;
                                    _locations.input = sInput;
                                }
                                else
                                {
                                    _locations.active = false;
                                }
                                flowLayoutPanel1.Controls.Add(_locations);
                            }
                            else if (p.ParameterName == _lochierarchy.Name)  //handel default ribbon controls. @Lochierarchy
                            {
                                _lochierarchy.Name = p.ParameterName;
                                _lochierarchy.label = p.ParameterName;
                                _lochierarchy.intOnly = false;
                                if (Query.Contains(p.ParameterName))
                                {
                                    _lochierarchy.active = true;
                                    _lochierarchy.input = sInput;
                                }
                                else
                                {
                                    _lochierarchy.active = false;
                                }
                                flowLayoutPanel1.Controls.Add(_lochierarchy);
                            }
                            else  // in case of other create control for it
                            {
                                Forms.uc_Inputbox nInputbChar = new Forms.uc_Inputbox();
                                nInputbChar.Name = p.ParameterName;
                                nInputbChar.label = p.ParameterName;
                                nInputbChar.intOnly = false;
                                if (Query.Contains(p.ParameterName))
                                {
                                    nInputbChar.active = true;
                                    nInputbChar.input = sInput;
                                }
                                else
                                {
                                    nInputbChar.active = false;
                                }
                                flowLayoutPanel1.Controls.Add(nInputbChar);
                            }
                            break;

                        default:
                            Debugger.Message(string.Format("Type not handeld: pName:{0} pSqlDbType: {1}", p.ParameterName, p.SqlDbType));
                            break;
                    }

                }
                catch (Exception e)
                {
                    Debugger.Exeption(e);
                }
            }
        }
        //builds to query command from the procmanger instance and puts it in the active connection
        public void GADATA_ProcMngrToActiveConnection()
        {
            System.Text.StringBuilder sbQuery = new System.Text.StringBuilder();
            sbQuery.Append("USE GADATA EXEC ").Append(lActConn.ProcedureName).Append(" ");
            foreach (var control in flowLayoutPanel1.Controls)
            {
                if (control is Forms.uc_Datebox)
                {
                    Forms.uc_Datebox nDateb = control as Forms.uc_Datebox;
                    //Debug.WriteLine("name: {0}  enbled: {1}  value: {2}", nDateb.Name, nDateb.active, nDateb.input.ToString("yyyy-MM-dd hh:mm:ss"));
                    if (nDateb.active)
                    {
                        sbQuery.Append(string.Format(" {0} = '{1}' ,", nDateb.Name, nDateb.input.ToString("yyyy-MM-dd hh:mm:ss")));
                    }
                }
                else if (control is Forms.uc_Checkbox)
                {
                    Forms.uc_Checkbox nCheckb = control as Forms.uc_Checkbox;
                    //Debug.WriteLine("name: {0}  enbled: {1}  value: {2}", nCheckb.Name, nCheckb.active, nCheckb.input);
                    if (nCheckb.active)
                    {
                        sbQuery.Append(string.Format(" {0} = {1} ,", nCheckb.Name, nCheckb.input));
                    }
                }
                else if (control is Forms.uc_Inputbox)
                {
                    Forms.uc_Inputbox nInputb = control as Forms.uc_Inputbox;
                    //Debug.WriteLine("name: {0}  enbled: {1}  value: {2}", nInputb.Name, nInputb.active, nInputb.input);
                    if (nInputb.active)
                    {
                        if (nInputb.intOnly == true) // integer box 
                        {
                            sbQuery.Append(string.Format(" {0} = {1} ,", nInputb.Name, Int32.Parse(nInputb.input)));
                        }
                        else // varchar box
                        {
                            sbQuery.Append(string.Format(" {0} = '{1}' ,", nInputb.Name, nInputb.input));
                        }
                    }
                }
            }
            string Query = sbQuery.ToString();
            //because I add a "," afther each parm and the last should not have one
            if (Query.Trim().EndsWith(","))
            {
                lActConn.Query = Query.TrimEnd(',');
            }
            else
            {
                lActConn.Query = Query;
            }
        }

        /*
          * populates the procmanger with the query of the active connection
          * FOR MX7 QUERYS
          *  and links the default ribbon controls if needed
          */
        public void MX7_ActiveConnectionToProcMngr(List<OracleQueryParm> Parms, string Query)
        {
            flowLayoutPanel1.Controls.Clear();
            flowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel1.WrapContents = true;
            flowLayoutPanel1.AutoScroll = true;

            string sValuefirst;
            foreach (OracleQueryParm p in Parms)
            {
                try
                {
                    //need to find out the datetype 
                    TypeCode pType = guess_typecode(p.Defaultvalue);
                    // Debugger.Exeption("P:{0} dft:{1} detected:{2}", p.ParameterName, p.Defaultvalue, pType.ToString());
                    switch (pType)
                    {
                        case TypeCode.DateTime:  //create datetime pickers 
                            //get dt value from Query string 
                            DateTime dtSelectedTimestamp = Convert.ToDateTime(p.Defaultvalue);
                            //if (Query.Contains(p.ParameterName)) dtSelectedTimestamp = Convert.ToDateTime(Query.Substring(Query.IndexOf(p.ParameterName) + p.ParameterName.Length).Split('\'')[1]);

                            if (p.ParameterName == _startDate.Name) //handel default ribbon controls. "@startdate"
                            {
                                _startDate.Name = p.ParameterName;
                                _startDate.label = p.ParameterName;
                                _startDate.active = true;
                                _startDate.hide_active = true;
                                _startDate.input = dtSelectedTimestamp;
                                flowLayoutPanel1.Controls.Add(_startDate);
                            }
                            else if (p.ParameterName == _endDate.Name) //handel default ribbon controls. "@enddate"
                            {
                                _endDate.Name = p.ParameterName;
                                _endDate.label = p.ParameterName;
                                _endDate.active = true;
                                _endDate.hide_active = true;
                                _endDate.input = dtSelectedTimestamp;
                                flowLayoutPanel1.Controls.Add(_endDate);
                            }
                            else
                            { //in case of an other timestamp create a datetimepicker for it
                                Forms.uc_Datebox nDateb = new Forms.uc_Datebox();
                                nDateb.Name = p.ParameterName;
                                nDateb.label = p.ParameterName;
                                nDateb.active = true;
                                nDateb.hide_active = true;
                                nDateb.input = dtSelectedTimestamp;
                                flowLayoutPanel1.Controls.Add(nDateb);
                            }
                            break;

                        case TypeCode.Boolean: //create checkboxes for bits
                            Forms.uc_Checkbox nCheckb = new Forms.uc_Checkbox();
                            nCheckb.Name = p.ParameterName;
                            nCheckb.label = p.ParameterName;
                            nCheckb.active = true;
                            nCheckb.hide_active = true;
                            nCheckb.input = false;
                            sValuefirst = Query.Substring(Query.IndexOf(p.ParameterName) + p.ParameterName.Length).Split('=')[1].Trim();
                            if (sValuefirst.StartsWith("true", StringComparison.InvariantCultureIgnoreCase) || sValuefirst.StartsWith("1", StringComparison.InvariantCultureIgnoreCase))
                            {
                                nCheckb.input = true;
                            }
                            else
                            {
                                nCheckb.input = false;
                            }
                            flowLayoutPanel1.Controls.Add(nCheckb);
                            break;

                        case TypeCode.Int32: // in case of bit create a editbox for it
                            Forms.uc_Inputbox nInputbInt = new Forms.uc_Inputbox();
                            nInputbInt.Name = p.ParameterName;
                            nInputbInt.label = p.ParameterName;
                            nInputbInt.intOnly = true;
                            nInputbInt.active = true;
                            nInputbInt.hide_active = true;
                            nInputbInt.input = "0";
                            sValuefirst = Query.Substring(Query.IndexOf(p.ParameterName) + p.ParameterName.Length).Split('=')[1].Trim();
                            nInputbInt.input = sValuefirst.Split(' ')[0].Trim();
                            flowLayoutPanel1.Controls.Add(nInputbInt);
                            break;

                        case TypeCode.String: // in case of varchar create a editbox for it
                            //get current value from querystring
                            string sInput = p.Defaultvalue;
                            //if (Query.Contains(p.ParameterName)) sInput = Query.Substring(Query.IndexOf(p.ParameterName) + p.ParameterName.Length).Split('\'')[1];

                            if (p.ParameterName == _assets.Name) //handel default ribbon controls. @Assets
                            {
                                _assets.Name = p.ParameterName;
                                _assets.label = p.ParameterName;
                                _assets.intOnly = false;
                                _assets.active = true;
                                _assets.hide_active = true;
                                _assets.input = sInput;
                                flowLayoutPanel1.Controls.Add(_assets);
                            }
                            else if (p.ParameterName == _locations.Name) //handel default ribbon controls. @Locations
                            {
                                _locations.Name = p.ParameterName;
                                _locations.label = p.ParameterName;
                                _locations.intOnly = false;
                                _locations.active = true;
                                _locations.hide_active = true;
                                _locations.input = sInput;
                                flowLayoutPanel1.Controls.Add(_locations);
                            }
                            else if (p.ParameterName == _lochierarchy.Name)  //handel default ribbon controls. @Lochierarchy
                            {
                                _lochierarchy.Name = p.ParameterName;
                                _lochierarchy.label = p.ParameterName;
                                _lochierarchy.intOnly = false;
                                _lochierarchy.active = true;
                                _lochierarchy.hide_active = true;
                                _lochierarchy.input = sInput;
                                flowLayoutPanel1.Controls.Add(_lochierarchy);
                            }
                            else if (p.ParameterName == _daysBack.Name)  //handel default ribbon controls. @daysback
                            {
                                _daysBack.Name = p.ParameterName;
                                _daysBack.label = p.ParameterName;
                                _daysBack.intOnly = true;
                                _daysBack.active = true;
                                _daysBack.hide_active = true;
                                _daysBack.input = sInput;
                                flowLayoutPanel1.Controls.Add(_daysBack);
                            }
                            else  // in case of other create control for it
                            {
                                Forms.uc_Inputbox nInputbChar = new Forms.uc_Inputbox();
                                nInputbChar.Name = p.ParameterName;
                                nInputbChar.label = p.ParameterName;
                                nInputbChar.intOnly = false;
                                nInputbChar.active = true;
                                nInputbChar.hide_active = true;
                                nInputbChar.input = sInput;
                                flowLayoutPanel1.Controls.Add(nInputbChar);
                            }
                            break;

                        default:
                            Debugger.Message(string.Format("Type not handeld: pName:{0} pDftValue: {1}", p.ParameterName, p.Defaultvalue));
                            break;
                    }

                }
                catch (Exception e)
                {
                    Debugger.Exeption(e);
                }
            }
        }
        //builds to query command from the procmanger instance and puts it in the active connection
        public void MX7_ProcMngrToActiveConnection(String QueryTemplate)
        {
            lActConn.Query = MX7_BuildQuery_ProcMngrToActiveConnection(QueryTemplate);
        }
        public string MX7_BuildQuery_ProcMngrToActiveConnection(String QueryTemplate)
        {
            System.Text.StringBuilder sbQuery = new System.Text.StringBuilder();
            //gives issue when users uses nested querys 
            //sbQuery.Append("SELECT ").Append(QueryTemplate.Split(new string[] { "SELECT" }, StringSplitOptions.None)[1]);
            sbQuery.Append("SELECT ").Append(QueryTemplate.Substring(QueryTemplate.IndexOf("SELECT") + 6));

            foreach (var control in flowLayoutPanel1.Controls)
            {
                if (control is Forms.uc_Datebox)
                {
                    Forms.uc_Datebox nDateb = control as Forms.uc_Datebox;
                    //        Debugger.Exeption("name: {0}  enbled: {1}  value: {2}", nDateb.Name, nDateb.active, nDateb.input.ToString("yyyy-MM-dd hh:mm:ss"));
                    sbQuery.Replace("&" + nDateb.Name, nDateb.input.ToString("yyyy-MM-dd hh:mm:ss"));
                }
                else if (control is Forms.uc_Checkbox)
                {
                    Forms.uc_Checkbox nCheckb = control as Forms.uc_Checkbox;
                    //       Debugger.Exeption("name: {0}  enbled: {1}  value: {2}", nCheckb.Name, nCheckb.active, nCheckb.input);
                    sbQuery.Replace("&" + nCheckb.Name, nCheckb.input.ToString());
                }
                else if (control is Forms.uc_Inputbox)
                {
                    Forms.uc_Inputbox nInputb = control as Forms.uc_Inputbox;
                    //        Debugger.Exeption("name: {0}  enbled: {1}  value: {2}", nInputb.Name, nInputb.active, nInputb.input);
                    if (nInputb.intOnly == true) // integer box 
                    {
                        sbQuery.Replace("&" + nInputb.Name, nInputb.input);
                    }
                    else // varchar box
                    {
                        sbQuery.Replace("&" + nInputb.Name, nInputb.input);
                    }
                }
            }
            return sbQuery.ToString();
        }

        #region From controls
        private void StoredProcedureManger_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void Btn_saveSet_Click(object sender, EventArgs e)
        {
            if (cb_ParmSetNames.Items.Count == 0)
            {
                cb_ParmSetNames.DataSource = GADATA_Select_ParmSet_list(lActConn.System, lActConn.ProcedureName);
                cb_ParmSetNames.Visible = true;
                btn_SaveSetConfirm.Visible = true;
                btn_SetDelete.Visible = true;
            }
            else
            {
                cb_ParmSetNames.Visible = false;
                btn_SaveSetConfirm.Visible = false;
                btn_SetDelete.Visible = false;
                cb_ParmSetNames.DataSource = null;
            }
        }
        private void btn_SaveSetConfirm_Click(object sender, EventArgs e)
        {
            if (cb_ParmSetNames.Text.Length < 5)
            {
                Debugger.Message("The name is to short");
            }
            else
            {
                Save_ParmSet(lActConn.System, lActConn.ProcedureName, cb_ParmSetNames.Text);
                cb_ParmSetNames.Visible = false;
                btn_SaveSetConfirm.Visible = false;
                btn_SetDelete.Visible = false;
            }

        }
        private void btn_SetDelete_Click(object sender, EventArgs e)
        {
            try
            {
                GADATA_delete_ParmSet(lActConn.System, lActConn.ProcedureName, cb_ParmSetNames.Text);
            }
            catch { }
            cb_ParmSetNames.Visible = false;
            btn_SaveSetConfirm.Visible = false;
            btn_SetDelete.Visible = false;
        }
        private void cb_ParmSetNames_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_ParmSet(lActConn.System, lActConn.ProcedureName, cb_ParmSetNames.Text);
        }
        #endregion

    

        /*
         *Parameters sets or values for Query parameters that the user can store in the GADATA db 
         *they can be shared and managed.
         *It is a way of not having to manualy parameters a Query each time.
         *ie: you can make a parameter set to get all the gripper data in a specific area for the last week
         */
        #region ParmSets
        private void Save_ParmSet(string System, string Procname, string Setname)
        {
            GADATA_delete_ParmSet(System, Procname, Setname);

            foreach (var control in flowLayoutPanel1.Controls)
            {
                if (control is Forms.uc_Datebox)
                {
                    Forms.uc_Datebox nDateb = control as Forms.uc_Datebox;
                    if (nDateb.active)
                    {
                        GADATA_insert_ParmSet(System, Procname, Setname, nDateb.Name, nDateb.input.ToString("yyyy-MM-dd hh:mm:ss"));
                    }
                }
                else if (control is Forms.uc_Checkbox)
                {
                    Forms.uc_Checkbox nCheckb = control as Forms.uc_Checkbox;
                    if (nCheckb.active)
                    {
                        GADATA_insert_ParmSet(System, Procname, Setname, nCheckb.Name, nCheckb.input.ToString());
                    }
                }
                else if (control is Forms.uc_Inputbox)
                {
                    Forms.uc_Inputbox nInputb = control as Forms.uc_Inputbox;
                    if (nInputb.active)
                    {
                        GADATA_insert_ParmSet(System, Procname, Setname, nInputb.Name, nInputb.input.ToString());
                    }
                }
            }

        }
        private void Load_ParmSet(string System, string Procname, string Setname)
        {
            foreach (var control in flowLayoutPanel1.Controls)
            {
                if (control is Forms.uc_Datebox)
                {
                    Forms.uc_Datebox nDateb = control as Forms.uc_Datebox;
                    string Value = GADATA_Select_ParmSet_value(System, Procname, Setname, nDateb.Name);
                    if (Value != null)
                    {
                        nDateb.active = true;
                        nDateb.input = Convert.ToDateTime(Value);
                    }
                    else
                    {
                        nDateb.active = false;
                    }
                }
                else if (control is Forms.uc_Checkbox)
                {
                    Forms.uc_Checkbox nCheckb = control as Forms.uc_Checkbox;
                    string Value = GADATA_Select_ParmSet_value(System, Procname, Setname, nCheckb.Name);
                    if (Value != null)
                    {
                        nCheckb.active = true;
                        nCheckb.input = Convert.ToBoolean(Value);
                    }
                    else
                    {
                        nCheckb.active = false;
                    }
                }
                else if (control is Forms.uc_Inputbox)
                {
                    Forms.uc_Inputbox nInputb = control as Forms.uc_Inputbox;
                    string Value = GADATA_Select_ParmSet_value(System, Procname, Setname, nInputb.Name);
                    //should I try handle int / varchar ? 
                    if (Value != null)
                    {
                        nInputb.active = true;
                        nInputb.input = Value;
                    }
                    else
                    {
                        nInputb.active = false;
                    }
                }
            }
        }
        public void Load_ParmsSet(string Setname)
        {
            Load_ParmSet(lActConn.System, lActConn.ProcedureName, Setname);
        }

        //ProcManagerSets
        //parm set calls to gadata 

        public void GADATA_delete_ParmSet(string System, string Procname, string Setname)
        {
            using (applDataTableAdapters.QUERYParametersTableAdapter adapter = new applDataTableAdapters.QUERYParametersTableAdapter())
            {
                adapter.DeleteSet(System, Procname, Setname);
            }
        }

        public void GADATA_insert_ParmSet(string System, string Procname, string Setname, string Parm, string Value)
        {

            using (applDataTableAdapters.QUERYParametersTableAdapter adapter = new applDataTableAdapters.QUERYParametersTableAdapter())
            {
                adapter.Insert(System, Procname, Setname, "", Parm, Value);
            }
        }

        public string GADATA_Select_ParmSet_value(string System, string Procname, string Setname, string Parm)
        {
            using (applDataTableAdapters.QUERYParametersTableAdapter adapter = new applDataTableAdapters.QUERYParametersTableAdapter())
            {
                applData.QUERYParametersDataTable lDT = new applData.QUERYParametersDataTable();
                adapter.Fill(lDT, System, Procname, Setname, Parm);
                if (lDT.Count() == 0) return null;
                return (from a in lDT select a.Value).First();
            }
        }

        public List<string> GADATA_Select_ParmSet_list(string System, string Procname)
        {
            using (applDataTableAdapters.QUERYParametersTableAdapter adapter = new applDataTableAdapters.QUERYParametersTableAdapter())
            {
                applData.QUERYParametersDataTable lDT = new applData.QUERYParametersDataTable();
                adapter.FillByProcname(lDT, System, Procname);
                if (lDT.Count() == 0) return new List<string>();
                return (from a in lDT select a.SETNAME).Distinct().ToList();
            }
        }

        //Parm set calls to gadata for MAXIMO
        //maximo comm TO GADATA

        public void oracle_update_Query_to_GADATA(string System, string Queryname, string QueryDiscription, string Query)
        {
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    var ds = from a in lQUERYS
                             where a.SYSTEM == System && a.NAME == Queryname
                             select a;
                    //adapter.Update()

                }
            }
        }

        public void oracle_delete_Query_GADATA(string System, string Queryname)
        {
            using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
            {
                adapter.Delete(System, Queryname);
            }
        }

        public void oracle_send_new_Query_to_GADATA(string System, string Queryname, string QueryDiscription, string Query)
        {

            using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
            {
                adapter.Insert(System, Queryname, QueryDiscription, Query);
            }
        }

        public string oracle_get_QueryTemplate_from_GADATA(string QueryName, string System)
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
            return Query.Trim().TrimEnd(';').ToUpper();
        }

        public string oracle_get_QueryDiscription_from_GADATA(string QueryName, string System)
        {
            string Disciption;
            using (applData.QUERYSDataTable lQUERYS = new applData.QUERYSDataTable())
            {
                using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                {
                    adapter.Fill(lQUERYS);
                }
                Disciption = (from a in lQUERYS
                              where a.SYSTEM == System && a.NAME == QueryName
                              select a.DISCRIPTION).First().ToString();
            }
            return Disciption.Trim();
        }

        public List<OracleQueryParm> oracle_get_QueryParms_from_GADATA(string QueryName, string System)
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

            List<OracleQueryParm> _parmList = new List<OracleQueryParm>();
            foreach (string parm in ParmLines)
            {
                if (parm.Contains("="))
                {
                    string ParmName = parm.Split('=')[0].Trim();
                    string ParmValue = parm.Split('=')[1].Trim().Split('\'')[1];
                    _parmList.Add(new OracleQueryParm { ParameterName = ParmName, Defaultvalue = ParmValue });
                }
            }
            return _parmList;
        }

        #endregion


        //set with of flowlayout pannel according to biggest component.
        public int MaxWith()
        {
            int biggest = 0;
            foreach(UserControl uc in flowLayoutPanel1.Controls)
            {
                if (uc != null)
                {
                    if(uc.Width > biggest)
                    {
                        biggest = uc.Width;
                    }
                }
            }
            return biggest;

        }
    }

    public class ActiveConnection
    {
        string lname;
        public string Name
        {
            get { return lname; }
            set { lname = value; }
        }
        string lSystem;
        public string System
        {
            get { return lSystem; }
            set { lSystem = value; }
        }
        string lProcedureName;
        public string ProcedureName
        {
            get { return lProcedureName; }
            set { lProcedureName = value; }
        }
        //retruns the stored procedure name from a Query
        public string get_storedProcedureFromQuery(string Query)
        {
            string Procfirst = Query.Substring(Query.IndexOf("EXEC") + "EXEC".Length);
            if (Procfirst.Contains("@"))
            {
                return Procfirst.Split('@')[0].Trim();
            }
            else
            {
                return Procfirst.Trim();
            }
        }
        string lODBCconnString;
        public string ODBCconnString
        {
            get { return lODBCconnString; }
            set { lODBCconnString = value; }
        }
        public string get_ODBCconnString_from_activeconnection()
        {
            using (ConnectionManger connMngr = new ConnectionManger())
            {
                Excel.WorkbookConnection connection = connMngr.get_Connection(lname);
                connMngr.Dispose();
                return connection.ODBCConnection.Connection;
            }
        }
        public string Query
        {
            get
            {
                using (ConnectionManger connMngr = new ConnectionManger())
                {
                    Excel.WorkbookConnection connection = connMngr.get_Connection(lname);
                    connMngr.Dispose();
                    return connection.ODBCConnection.CommandText;
                }
            }
            set
            {
                using (ConnectionManger connMngr = new ConnectionManger())
                {
                    Excel.WorkbookConnection connection = connMngr.get_Connection(lname);
                    if (connection.ODBCConnection.Refreshing) { connection.ODBCConnection.CancelRefresh(); }
                    connection.ODBCConnection.CommandText = value;
                    connMngr.Dispose();
                }
            }
        }

    }
}
