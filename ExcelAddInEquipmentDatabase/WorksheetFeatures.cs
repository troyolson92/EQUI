using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using EQUICommunictionLib;
using System.Threading;

namespace ExcelAddInEquipmentDatabase
{
    public class WorksheetFeatures
    {
        //debugger
        myDebugger Debugger = new myDebugger();

        Excel._Worksheet lClickedSheet;
        string wonum;
        string errornum;
        string location;
        string locationtree;
        string assetnum;
        string Logtype;
        string LogText;
        int downtime;
        int refid;

        Office.CommandBarButton btn;

        public void Application_SheetBeforeRightClick(object Sh, Excel.Range Target, ref bool Cancel)
        {
         wonum = "";
         errornum = "";
         location = "";
         locationtree = "";
         assetnum = "";
         Logtype = "";
         LogText = "";
         downtime = 0;
         refid = 0;

            //
            lClickedSheet = Sh as Excel._Worksheet; //pass the sheet so we can work with it 

            //adding code to support old weldguncockpit. can be removed once file is killed.
            //17w47d2
            if (lClickedSheet.Name == "Alerts")
            {
                Debugger.Log("Dit not append VSTO context menu (weldguncockpit exeption");
                return;
            }
            //
            ResetTableMenu();  // reset the cell context menu back to the default (can mess up other peoples code)
            //foreach collum in collums with a switch statement that add controlls 
            foreach (Excel.ListObject oListobject in lClickedSheet.ListObjects)
            {
                if (Target.Row == 1) { return; } //because can not convert row header (this wil not work if table is somewhere else 
                //
                foreach (Excel.ListColumn oListColum in oListobject.ListColumns)
                {
                 //Debug.WriteLine("ListColum: {0} Column: {1}", oListColum.Name, oListColum.Range.Column);
                    switch (oListColum.Name.ToLower())
                      {
                        case "wonum":
                        case "werkorder":
                            wonum = (string)Convert.ToString(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value);
                              break;
                        case "logcode":
                              errornum = (string)Convert.ToString(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value);
                              break;
                        case "location":
                              location = (string)Convert.ToString(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value);
                              break;
                        case "locationtree":
                            locationtree = (string)Convert.ToString(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value) ?? location;
                            break;
                        case "assetnum":
                              assetnum = (string)Convert.ToString(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value);
                              break;
                        case "logtype":
                              Logtype = (string)Convert.ToString(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value);
                              break;
                        case "refid":
                              refid = (int)Convert.ToInt32(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value);
                              break;
                        case "logtext":
                              LogText = (string)Convert.ToString(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value);
                              break;
                        case "downtime":
                              downtime = (int)Convert.ToInt32(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value);
                              break;
                          default:
                              //nothing to do 
                              break;
                      }
                }

            }

            // add button controls where as needed
            if (wonum != "")
            {
                btn = AddButtonToTableMenuItem("WorkorderDetails", 1, 487); //if we have a wonum enable wo details
                btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(WorkorderDetailsMenuItemClick);
            }
            if (errornum != "" && (Logtype == "BREAKDOWN" || Logtype == "ERROR" || Logtype == "WARNING" || Logtype == "ALERT"
                || Logtype == "ControllerEvent" || Logtype == "ErrDispLog" || Logtype == "SHIFTBOOK" || Logtype == "STOerror"))
            {
                btn = AddButtonToTableMenuItem("LogDetails", 1, 463); //if we have a logcode enabel errordetails
                btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(ErrorDetailsMenuItemClick);
                //
                btn = AddButtonToTableMenuItem("ErrorStats", 2, 430); //if we have a logcode enable errostats (graph)
                btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(ErrorStatsMenuItemClick);
            }
            if (location != "")
            {
                addMaximoSubmenu(3); // if we have a reference location enable maximo tools
            }
            if (Logtype == "TIMELINE" )
            {
                btn = AddButtonToTableMenuItem("SetNoProduction", 3, 0);
                btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(SetNoProductionItemClick);
                if (!ExcelAddInEquipmentDatabase.Properties.Settings.Default.IsPowerUser){ btn.Enabled = false;}
            }
            addFormattingSubmenu(5);
        }

        // reset the Table context menu back to the default
        private void ResetTableMenu()
        {

            GetTableContextMenu().Reset();
            Office.CommandBar test = GetTableContextMenu();
            //must only reset my menus
        }
        //get command bar for Table property
        private Office.CommandBar GetTableContextMenu()
        {
            return Globals.ThisAddIn.Application.CommandBars["List Range Popup"];
        }
        //for adding a control button
        public  Office.CommandBarButton AddButtonToTableMenuItem(string btnName, int position, int faceid) // how to pass event handelere here ?
        {
            Office.MsoControlType menuItem = Office.MsoControlType.msoControlButton;
            Office.CommandBarButton btn = (Office.CommandBarButton)GetTableContextMenu().Controls.Add(menuItem, Type.Missing, Type.Missing,position , true);
            btn.FaceId = faceid;
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = btnName;
            return btn;
        }


        //**********************************Table formatting*********************************************
        //create formatting on table 
        private static void AddFormatToTable(Excel._Worksheet Sheet, string TriggerCollum, string TriggerValue, Int32 BackgroundColor) //this works
        {
            foreach (Excel.ListObject oListobject in Sheet.ListObjects)
            {
                foreach (Excel.ListColumn oListColum in oListobject.ListColumns)
                {
                    if (oListColum.Name.ToUpper() == TriggerCollum.ToUpper())
                    {
                         String collumLetter = GetExcelColumnLetter(oListColum.Range.Column);
                        Excel.FormatCondition format = (Excel.FormatCondition)(Sheet.get_Range(oListobject.Name,
                                Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlExpression, Excel.XlFormatConditionOperator.xlEqual,
                                "=$" + collumLetter + "2  = \"" + TriggerValue + "\"", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing));

                        format.StopIfTrue = false;
                        //format.Font.Bold = true;
                        format.Interior.Color = BackgroundColor;
                    }
                }
            }
        }
        //converts collum number to leter 
        private static string GetExcelColumnLetter(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }
        //remove sheets format condtions
        private static void ClearFormatConditions(Excel._Worksheet Sheet)
        {
            foreach (Excel.ListObject oListobject in Sheet.ListObjects)
            {
                Sheet.get_Range(oListobject.Name).FormatConditions.Delete();   
            }

        }

        //create Robotdb default format rules 
        private static void AddRobotFormatting(Excel._Worksheet Sheet)
        { 
        // find existing rules for table and remove
            ClearFormatConditions(Sheet);
            //new type 
            AddFormatToTable(Sheet, "LogType", "SHIFTBOOK", 8708322);
            AddFormatToTable(Sheet, "LogType", "WARNING", 16760832);
            AddFormatToTable(Sheet, "LogType", "LIVE", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red));
            AddFormatToTable(Sheet, "LogType", "WARN", 16305069);
            AddFormatToTable(Sheet, "LogType", "BREAKDOWN", 45296);
            AddFormatToTable(Sheet, "LogType", "BREAKDOWN_start", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGoldenrodYellow));
            AddFormatToTable(Sheet, "LogType", "TIMELINE", 11128974); 
            AddFormatToTable(Sheet, "LogType", "STW040", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightCoral));
        }
        //create Workorder default format rules 
        private static void AddWorkorderFormatting(Excel._Worksheet Sheet)
        {
            // find existing rules for table and remove
            ClearFormatConditions(Sheet);
            //
            AddFormatToTable(Sheet, "ACTSTATUS", "INPRG", 16305069);
            AddFormatToTable(Sheet, "ACTSTATUS", "DRAFT", 16711680);
            AddFormatToTable(Sheet, "ACTSTATUS", "WAPPR", 16711680);
            AddFormatToTable(Sheet, "ACTSTATUS", "COMP", 45136);

        }
        //submenu for formatting tables
        public void addFormattingSubmenu(int position)
        {
            Office.MsoControlType menuItem = Office.MsoControlType.msoControlButton;

            Office.MsoControlType ControlPopup = Office.MsoControlType.msoControlPopup;
            Office.CommandBarPopup subMenu = (Office.CommandBarPopup)GetTableContextMenu().Controls.Add(ControlPopup, Type.Missing, Type.Missing, position, true);
            subMenu.Caption = "Formatting";
            
            //
            Office.CommandBarButton btnFormatSheetData = (Office.CommandBarButton)subMenu.Controls.Add(menuItem, Type.Missing, Type.Missing, 1, true);
            btnFormatSheetData.Style = Office.MsoButtonStyle.msoButtonCaption;
            btnFormatSheetData.Caption = "GADATA default";
            btnFormatSheetData.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(ApplyRobotFromattingClick);
            //
            Office.CommandBarButton btnFormatSheetWorkorder = (Office.CommandBarButton)subMenu.Controls.Add(menuItem, Type.Missing, Type.Missing, 2, true);
            btnFormatSheetWorkorder.Style = Office.MsoButtonStyle.msoButtonCaption;
            btnFormatSheetWorkorder.Caption = "Maximo default";
            btnFormatSheetWorkorder.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(ApplyWorkorderFromattingClick);
            //
        }

        //event handelere for format button
        void ApplyRobotFromattingClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            AddRobotFormatting(lClickedSheet);
        }
        void ApplyWorkorderFromattingClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            AddWorkorderFormatting(lClickedSheet);
        }

        //**********************************workorder details*********************************************
        void WorkorderDetailsMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var newThread = new System.Threading.Thread(frmNewWorkoderThread);
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();
        }
        public void frmNewWorkoderThread()
        {
            try
            {
                string urlSkelation = @"http:\\gensvw1178.gen.volvocars.net\Maximo_ui\workorderdetails\WoDetails?wonum={0}";
                string url = string.Format(urlSkelation, Uri.EscapeDataString(wonum));
                System.Diagnostics.Process.Start(url);
                //System.Windows.Forms.Application.Run(new ExcelAddInEquipmentDatabase.Forms.EquiBrowser(url));
            }
            catch (Exception ex)
            {
                Debugger.Exeption(ex);
                Debugger.Message("Dam..." + ex.Message);
            }
        }
        //**********************************Error details*********************************************
        void ErrorDetailsMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var newThread = new System.Threading.Thread(frmNewErrordetailsThread);
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();
        }
        public void frmNewErrordetailsThread()
        {
            try
            {
                string urlSkelation = @"http:\\gensvw1178.gen.volvocars.net\supervision\table\Moreinfo?location={0}&errornum={1}&refid={2}&logtype={3}&logtext={4}";
                string url = string.Format(urlSkelation
                    , Uri.EscapeDataString(location)
                    , Uri.EscapeDataString(errornum)
                    , Uri.EscapeDataString(refid.ToString())
                    , Uri.EscapeDataString(Logtype)
                    , Uri.EscapeDataString(LogText)
                    );
                System.Diagnostics.Process.Start(url);
                //System.Windows.Forms.Application.Run(new ExcelAddInEquipmentDatabase.Forms.EquiBrowser(url));
            }
            catch (Exception ex)
            {
                Debugger.Exeption(ex);
                Debugger.Message("Dam..." + ex.Message);
            }
        }
        //**********************************Error Stats*********************************************
        void ErrorStatsMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var newThread = new System.Threading.Thread(frmNewErrorStatsThread);
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();
        }
        public void frmNewErrorStatsThread()
        { 
            try
            {
                string urlSkelation = @"http:\\gensvw1178.gen.volvocars.net\chart\GetErrorTrend?location={0}&errornum={1}&refid={2}&logtype={3}&logtext={4}";
                string url = string.Format(urlSkelation
                    ,Uri.EscapeDataString(location)
                    ,Uri.EscapeDataString(errornum)
                    ,Uri.EscapeDataString(refid.ToString())
                    ,Uri.EscapeDataString(Logtype)
                    ,Uri.EscapeDataString(LogText) 
                    );
                System.Diagnostics.Process.Start(url);
                //System.Windows.Forms.Application.Run(new ExcelAddInEquipmentDatabase.Forms.EquiBrowser(url));
            }
            catch (Exception ex)
            {
                Debugger.Exeption(ex);
                Debugger.Message("Dam..." + ex.Message);
            }
        }
        //**********************************No Production*********************************************
        void SetNoProductionItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            //removed make it in the web 
            throw new NotSupportedException();
        }

        //**********************************MAXIMO*********************************************
        //submenu for mawimo tools
        public void addMaximoSubmenu(int position)
        {
            Office.MsoControlType menuItem = Office.MsoControlType.msoControlButton;

            Office.MsoControlType ControlPopup = Office.MsoControlType.msoControlPopup;
            Office.CommandBarPopup subMenu = (Office.CommandBarPopup)GetTableContextMenu().Controls.Add(ControlPopup, Type.Missing, Type.Missing, position, true);
            
            subMenu.Caption = "Maximo";
            //
            Office.CommandBarButton btnShowWorkorderHistory = (Office.CommandBarButton)subMenu.Controls.Add(menuItem, Type.Missing, Type.Missing, 1, true);
            btnShowWorkorderHistory.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btnShowWorkorderHistory.Caption = "Wo history";
            btnShowWorkorderHistory.FaceId = 805;
            btnShowWorkorderHistory.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(btnShowWorkorderHistoryClick);
            //
            Office.CommandBarButton btnShowPartsWorkorder = (Office.CommandBarButton)subMenu.Controls.Add(menuItem, Type.Missing, Type.Missing, 2, true);
            btnShowPartsWorkorder.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btnShowPartsWorkorder.Caption = "Part history";
            btnShowPartsWorkorder.FaceId = 806;
            btnShowPartsWorkorder.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(btnShowPartsWorkorderClick);
            //
            //
        }
        void btnShowWorkorderHistoryClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var newThread = new System.Threading.Thread(frmNewWorkordersThread);
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();
        }
        public void frmNewWorkordersThread()
        {
            try
            {
                string urlSkelation = @"http:\\gensvw1178.gen.volvocars.net\Maximo_ui\Workorder\Workorders?location={0}&loadOnInit=true";
                string url = string.Format(urlSkelation, Uri.EscapeDataString(location));
                System.Diagnostics.Process.Start(url);
                //System.Windows.Forms.Application.Run(new ExcelAddInEquipmentDatabase.Forms.EquiBrowser(url));
            }
            catch (Exception ex)
            {
                Debugger.Exeption(ex);
                Debugger.Message("Dam..." + ex.Message);
            }

        }

        void btnShowPartsWorkorderClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var newThread = new System.Threading.Thread(frmNewPartsonlocationThread);
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();
        }

        public void frmNewPartsonlocationThread()
        {
            try
            {
                string urlSkelation = @"http:\\gensvw1178.gen.volvocars.net\Maximo_ui\Workorder\PartsOnLocation?location={0}&loadOnInit=true";
                string url = string.Format(urlSkelation, Uri.EscapeDataString(location));
                System.Diagnostics.Process.Start(url);
                //System.Windows.Forms.Application.Run(new ExcelAddInEquipmentDatabase.Forms.EquiBrowser(url));
            }
            catch (Exception ex)
            {
                Debugger.Exeption(ex);
                Debugger.Message("Dam..." + ex.Message);
            }
        }
    }
}
