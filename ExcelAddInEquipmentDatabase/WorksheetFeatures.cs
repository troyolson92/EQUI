using EQUICommunictionLib;
using System;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace ExcelAddInEquipmentDatabase
{
    public class WorksheetFeatures
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private Excel._Worksheet lClickedSheet;
        private string wonum;
        private string errornum;
        private string location;
        private string locationtree;
        private string assetnum;
        private string Logtype;
        private string LogText;
        private int downtime;
        private int refid;

        private Office.CommandBarButton btn;

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

            lClickedSheet = Sh as Excel._Worksheet; //pass the sheet so we can work with it
            // reset the cell context menu back to the default (can mess up other peoples code)
            GetTableContextMenu().Reset();
            Office.CommandBar test = GetTableContextMenu();

            foreach (Excel.ListObject oListobject in lClickedSheet.ListObjects)
            {
                if (Target.Row == 1) { return; } //because can not convert row header (this will not work if table is somewhere else)
                //
                foreach (Excel.ListColumn oListColum in oListobject.ListColumns)
                {
                    //Debug.WriteLine("ListColum: {0} Column: {1}", oListColum.Name, oListColum.Range.Column);
                    switch (oListColum.Name.ToLower())
                    {
                        case "wonum":
                        case "werkorder":
                        case "workorder":
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
            int pos = 1;
            if (wonum != "")
            {
                btn = AddButtonToTableMenuItem("Maximo WorkorderDetails", pos, 487); //if we have a wonum enable wo details
                btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(WorkorderDetailsMenuItemClick);
                pos += 1;
            }

            if (location != "")
            {
                btn = AddButtonToTableMenuItem("Maximo location history", pos, 0025); //if we have a log code enable error details
                btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(btnShowWorkorderHistoryClick);
                pos += 1;
            }

            if (errornum != "" && (Logtype == "BREAKDOWN" || Logtype == "ERROR" || Logtype == "WARNING" || Logtype == "ALERT"
                || Logtype == "ControllerEvent" || Logtype == "ErrDispLog" || Logtype == "SHIFTBOOK" || Logtype == "STOerror"))
            {
                btn = AddButtonToTableMenuItem("Equi LogDetails", pos, 0017); //if we have a log code enable error details
                btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(ErrorDetailsMenuItemClick);
                pos += 1;
            }

            btn = AddButtonToTableMenuItem("Apply default formatting", pos, 463); //if we have a log code enable error details
            btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(ApplyRobotFromattingClick);
            pos += 1;
        }

        //get command bar for Table property
        private Office.CommandBar GetTableContextMenu()
        {
            return Globals.ThisAddIn.Application.CommandBars["List Range Popup"];
        }

        //for adding a control button
        public Office.CommandBarButton AddButtonToTableMenuItem(string btnName, int position, int faceid)
        {
            Office.MsoControlType menuItem = Office.MsoControlType.msoControlButton;
            Office.CommandBarButton btn = (Office.CommandBarButton)GetTableContextMenu().Controls.Add(menuItem, Type.Missing, Type.Missing, position, true);
            btn.FaceId = faceid;
            btn.Style = Office.MsoButtonStyle.msoButtonIconAndCaption;
            btn.Caption = btnName;
            return btn;
        }

        //**********************************Table formatting*********************************************
        //create formatting on table
        private static void AddFormatToTable(Excel._Worksheet Sheet, string TriggerCollum, string TriggerValue, Int32 BackgroundColor)
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

        //converts column number to letter
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

        //remove sheets format conditions
        private static void ClearFormatConditions(Excel._Worksheet Sheet)
        {
            foreach (Excel.ListObject oListobject in Sheet.ListObjects)
            {
                Sheet.get_Range(oListobject.Name).FormatConditions.Delete();
            }
        }

        //create Equi default format rules
        private static void AddRobotFormatting(Excel._Worksheet Sheet)
        {
            // find existing rules for table and remove
            ClearFormatConditions(Sheet);
            //new type
            AddFormatToTable(Sheet, "LogType", "LIVE", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red));
            AddFormatToTable(Sheet, "LogType", "BREAKDOWN", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Orange));
            AddFormatToTable(Sheet, "LogType", "BREAKDOWN_start", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGoldenrodYellow));
            AddFormatToTable(Sheet, "LogType", "TIMELINE", System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LimeGreen));
        }

        //event handler for format button
        private void ApplyRobotFromattingClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            AddRobotFormatting(lClickedSheet);
        }

        //**********************************Error details*********************************************
        private void ErrorDetailsMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var newThread = new System.Threading.Thread(frmNewErrordetailsThread);
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();
        }

        public void frmNewErrordetailsThread()
        {
            try
            {
                string urlSkelation = Properties.Settings.Default.EquiBasepath + @"\supervision\table\Moreinfo?location={0}&errornum={1}&refid={2}&logtype={3}&logtext={4}";
                string url = string.Format(urlSkelation
                    , Uri.EscapeDataString(location)
                    , Uri.EscapeDataString(errornum)
                    , Uri.EscapeDataString(refid.ToString())
                    , Uri.EscapeDataString(Logtype)
                    , Uri.EscapeDataString(LogText)
                    );
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        //**********************************work order details*********************************************
        private void WorkorderDetailsMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var newThread = new System.Threading.Thread(frmNewWorkoderThread);
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();
        }

        public void frmNewWorkoderThread()
        {
            try
            {
                string urlSkelation = Properties.Settings.Default.EquiBasepath + @"\Maximo_ui\workorderdetails\WoDetails?wonum={0}";
                string url = string.Format(urlSkelation, Uri.EscapeDataString(wonum));
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        private void btnShowWorkorderHistoryClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            var newThread = new System.Threading.Thread(frmNewWorkordersThread);
            newThread.SetApartmentState(System.Threading.ApartmentState.STA);
            newThread.Start();
        }

        public void frmNewWorkordersThread()
        {
            try
            {
                string urlSkelation = Properties.Settings.Default.EquiBasepath + @"\Maximo_ui\Workorder\Workorders?location={0}&loadOnInit=true";
                string url = string.Format(urlSkelation, Uri.EscapeDataString(location));
                System.Diagnostics.Process.Start(url);
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }       
        }
    }
}