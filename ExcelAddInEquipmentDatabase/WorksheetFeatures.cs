﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using Microsoft.Office.Tools.Excel;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;

namespace ExcelAddInEquipmentDatabase
{
    public class WorksheetFeatures
    {
        Excel._Worksheet lClickedSheet;
        //dbg 
        string wonum;
        string errornum;
        Office.CommandBarButton btn;

        public void Application_SheetBeforeRightClick(object Sh, Excel.Range Target, ref bool Cancel)
        {
            lClickedSheet = Sh as Excel._Worksheet; //pass the sheet so we can work with it 
            ResetTableMenu();  // reset the cell context menu back to the default
            //foreach collum in collums with a switch statement that add controlls 
            foreach (Excel.ListObject oListobject in lClickedSheet.ListObjects)
            {
                //standard button for sheet formating
                btn = AddButtonToTableMenuItem("FormatSheet");
                btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(FormatMenuItemClick);
                //

                foreach (Excel.ListColumn oListColum in oListobject.ListColumns)
                {
                 //Debug.WriteLine("ListColum: {0} Column: {1}", oListColum.Name, oListColum.Range.Column);
                    switch (oListColum.Name)
                      {
                          case "WONUM":
                              wonum = (string)Convert.ToString(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value);
                              btn = AddButtonToTableMenuItem("WorkorderDetails");
                              btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(WorkorderDetailsMenuItemClick);
                              break;

                          case "ERROR":
                              errornum = (string)Convert.ToString(lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value);
                              btn = AddButtonToTableMenuItem("ErrorDetails");
                              btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(ErrorDetailsMenuItemClick);
                              break;

                          default:
                              //nothing to do 
                              break;
                      }
                }
            }
            foreach (Excel.QueryTable oQueryTable in lClickedSheet.QueryTables)
            {
                //if it is not formatted as listobject (just afther create.)
                //add format as table button here.
                Debug.WriteLine(oQueryTable.Name);
            }
        }

        // reset the Table context menu back to the default
        private void ResetTableMenu()
        {
            GetTableContextMenu().Reset(); 
        }
        //get command bar for Table property
        private Office.CommandBar GetTableContextMenu()
        {
            return Globals.ThisAddIn.Application.CommandBars["List Range Popup"];
        }
        //for adding a control button
        public  Office.CommandBarButton AddButtonToTableMenuItem(string btnName) // how to pass event handelere here ?
        {
            Office.MsoControlType menuItem = Office.MsoControlType.msoControlButton;
            Office.CommandBarButton btn = (Office.CommandBarButton)GetTableContextMenu().Controls.Add(menuItem, Type.Missing, Type.Missing, 1, true);

            btn.Style = Office.MsoButtonStyle.msoButtonCaption;
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
        public static void AddRobotFormatting(Excel._Worksheet Sheet)
        { 
        // find existing rules for table and remove
            ClearFormatConditions(Sheet);
           //
            AddFormatToTable(Sheet, "Errortype", "SHIFTBOOK", 8708322);
            AddFormatToTable(Sheet, "Errortype", "WARNING",16760832);
            AddFormatToTable(Sheet, "Errortype", "ALERT", 16724940);
            AddFormatToTable(Sheet, "Errortype", "SLOWspeed", 16305069);
            AddFormatToTable(Sheet, "Errortype", "LIVE", 16711680);
            AddFormatToTable(Sheet, "Errortype", "BREAKDOWN", 45296);
            AddFormatToTable(Sheet, "Errortype", "BEGIN", 45136);
            AddFormatToTable(Sheet, "Errortype", "TI", 11128974);

        }
        //event handelere for format button
        void FormatMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            AddRobotFormatting(lClickedSheet);
        }

        //**********************************workorder details*********************************************
        void WorkorderDetailsMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            Forms.MXxWOdetails lMXxWOdetails = new Forms.MXxWOdetails(wonum); //allow multible instances of the form.
            lMXxWOdetails.Show();
        }
        //**********************************Error details*********************************************
        void ErrorDetailsMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
        {
            System.Windows.Forms.MessageBox.Show("not done");
        }


    }
}
