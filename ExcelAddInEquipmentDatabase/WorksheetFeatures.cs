using System;
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

        public void Application_SheetBeforeRightClick(object Sh, Excel.Range Target, ref bool Cancel)
        {
            lClickedSheet = Sh as Excel._Worksheet; //pass the sheet so we can work with it 
            ResetTableMenu();  // reset the cell context menu back to the default
            //foreach collum in collums with a switch statement that add controlls 
            foreach (Excel.ListObject oListobject in lClickedSheet.ListObjects)
            {
                foreach (Excel.ListColumn oListColum in oListobject.ListColumns)
                {
                 //Debug.WriteLine("ListColum: {0} Column: {1}", oListColum.Name, oListColum.Range.Column);
                    switch (oListColum.Name)
                      {
                          case "WONUM":
                              wonum = (string)lClickedSheet.Cells[Target.Row, oListColum.Range.Column].Value;
        
                              AddButtonToTableMenuItem("WorkorderDetails");
                              break;

                          case "ERROR":

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
        public void AddButtonToTableMenuItem(string btnName) // how to pass event handelere here ?
        {
            Office.MsoControlType menuItem = Office.MsoControlType.msoControlButton;
            Office.CommandBarButton btn = (Office.CommandBarButton)GetTableContextMenu().Controls.Add(menuItem, Type.Missing, Type.Missing, 1, true);

            btn.Style = Office.MsoButtonStyle.msoButtonCaption;
            btn.Caption = btnName;
            btn.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(WorkorderDetailsMenuItemClick);
        }

        //**********************************Table formatting*********************************************
        //create formatting on table 
        private static void AddFormatToTable(Excel._Worksheet Sheet, string TriggerCollum, string TriggerValue, Int32 BackgroundColor) //this works
        {
            //find table name on sheet

            //find collum in that table 

            Excel.FormatCondition format = (Excel.FormatCondition)(Sheet.get_Range("table1",
                    Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlExpression, Excel.XlFormatConditionOperator.xlEqual,
                    "=$D2  = \"SHIFTBOOK\"", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing));

            format.StopIfTrue = false;
            format.Font.Bold = true;
            format.Font.Color = BackgroundColor;
        }
        //create Robotdb default format rules 
        private static void AddRobotFormatting(Excel._Worksheet Sheet)
        { 
        // find existing rules for table and remove;
            AddFormatToTable(Sheet, "ERRORTYPE", "SHIFTBOOK", 0x000000FF);
            AddFormatToTable(Sheet, "ERRORTYPE", "BREAKDOWN", 0x000000FF);
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




//*******************************************************************************************************************************************
        //testing stuff

        //testing with condition formatting rules
        /*
         * will store table on db. 'format sets'
         * ie a format set called robot std will containt a list of collums and if collum value = apply format style
         */
        public static void TestCondFormat() //this works
        {
            Excel.Worksheet activeWorksheet = Globals.ThisAddIn.Application.ActiveSheet as Excel.Worksheet;

            Excel.FormatCondition format = (Excel.FormatCondition)(activeWorksheet.get_Range("table1",
                    Type.Missing).FormatConditions.Add(Excel.XlFormatConditionType.xlExpression, Excel.XlFormatConditionOperator.xlEqual,
                    "=$D2  = \"SHIFTBOOK\"", Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing));

            format.StopIfTrue = false;
            format.Font.Bold = true;
            format.Font.Color = 0x000000FF;
          //  format.Font.Background = 0x000000FF;
        }

        //testing with context menu
            public  void AddExampleMenuItem()
            {
                Office.MsoControlType menuItem = Office.MsoControlType.msoControlButton;
                Office.CommandBarButton exampleMenuItem = (Office.CommandBarButton)GetTableContextMenu().Controls.Add(menuItem, Type.Missing, Type.Missing, 1, true);

                exampleMenuItem.Style = Office.MsoButtonStyle.msoButtonCaption;
                exampleMenuItem.Caption = "Example Menu Item";
                exampleMenuItem.Click += new Microsoft.Office.Core._CommandBarButtonEvents_ClickEventHandler(exampleMenuItemClick);
            }

            void exampleMenuItemClick(Microsoft.Office.Core.CommandBarButton Ctrl, ref bool CancelDefault)
            {
                System.Windows.Forms.MessageBox.Show("Example Menu Item clicked");
            }

            private void RemoveCutCopyPasteMenuItems()
            {
                Office.CommandBar contextMenu = GetTableContextMenu();

                for (int i = contextMenu.Controls.Count; i > 0; i--)
                {
                    Office.CommandBarControl control = contextMenu.Controls[i];

                    if (control.Caption == "Cu&t") control.Delete();  // Sample code: remove cut menu item
                    else if (control.Caption == "&Copy") control.Delete();  // Sample code: remove copy menu item
                    else if (control.accDescription.Contains("Paste")) control.Delete(); // Sample code: remove any paste menu items
                }
            }
        

    }
}
