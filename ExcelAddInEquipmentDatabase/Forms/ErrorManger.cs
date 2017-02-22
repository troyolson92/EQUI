using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

/*
select * from MAXIMO.CLASSIFICATION CLASS 
where CLASS.SITEID = 'VCG' AND CLASS.CLASSIFICATIONID like '%UR%';
select * from MAXIMO.CLASSSTRUCTURE STRUC
WHERE STRUC.SITEID = 'VCG' AND STRUC.CLASSIFICATIONID like 'U%';
 * 
 * => need to change the asset list to include the full asset classification (assetmanger)
 * */

//Lerror def
/*
 id, logcode , logtekst, class_id, subgroup_id,man_set
 * */


namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class ErrorManger : Form
    {
        applData.c3gL_errorDataTable lc3gError = new applData.c3gL_errorDataTable();
        applData.c4gL_errorDataTable lc4gError = new applData.c4gL_errorDataTable();
        //
        applData.c3gC_LogClassRulesDataTable lv3gRules = new applData.c3gC_LogClassRulesDataTable();
        applData.c4gC_LogClassRulesDataTable lv4gRules = new applData.c4gC_LogClassRulesDataTable();
        //
        applData.c_SubgroupDataTable lc_subgroup = new applData.c_SubgroupDataTable();
        applData.c_ClassificationDataTable lc_classification = new applData.c_ClassificationDataTable();
        //

        static string[] systems = new string[] { "C3G", "C4G", "ABB-NGAC" };
        string CreateNew = "Create new...";

        public ErrorManger()
        {
            InitializeComponent();
//
            cb_system.Items.AddRange(systems);
            cb_system.Text = systems.Last();
//
            fill_classification();
            fill_subgroup();
        }

        private void fill_rules()
        {
            //fill rules based on system and filters
            Cursor.Current = Cursors.AppStarting;
            switch (cb_system.Text)
            {
                case "C3G":
                    using (applDataTableAdapters.c3gC_LogClassRulesTableAdapter c3gAdapter = new applDataTableAdapters.c3gC_LogClassRulesTableAdapter())
                    {
                        c3gAdapter.Fill(lv3gRules);
                    }
                    dg_Rules.DataSource = lv3gRules;
                    break;
                case "C4G":
                    using (applDataTableAdapters.c4gC_LogClassRulesTableAdapter c4gAdapter = new applDataTableAdapters.c4gC_LogClassRulesTableAdapter())
                    {
                        c4gAdapter.Fill(lv4gRules);
                    }
                    dg_Rules.DataSource = lv4gRules;
                    break;
                case "ABB-NGAC":
                    //tbt
                    break;
                default:
                    MessageBox.Show("system unkown", "OEPS", MessageBoxButtons.OK);
                    return;
            }

            dg_Rules.AllowUserToOrderColumns = true;
            dg_Rules.AllowUserToResizeColumns = true;
            dg_Rules.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_Rules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Cursor.Current = Cursors.Default;
        }

        private void fill_classification()
        {
            using (applDataTableAdapters.c_ClassificationTableAdapter c_classification_Adapter = new applDataTableAdapters.c_ClassificationTableAdapter())
            {
                c_classification_Adapter.Fill(lc_classification);
            }

            var data = from a in lc_classification
                       select a.Classification;
            List<string> data2 = data.Distinct().ToList();
            data2.Add(CreateNew);
            cb_classification.DataSource = data2;
        }

        private void fill_subgroup()
        {
            using (applDataTableAdapters.c_SubgroupTableAdapter c_subgroup_Adapter = new applDataTableAdapters.c_SubgroupTableAdapter())
            {
                c_subgroup_Adapter.Fill(lc_subgroup);
            }

            var data = from a in lc_subgroup
                       select a.Subgroup;
            List<string> data2 = data.Distinct().ToList();
            data2.Add(CreateNew);
            cb_Subgroup.DataSource = data2;
        }

        private void btn_GetLogs_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.AppStarting;
            switch (cb_system.Text)
            {
                case "C3G":
                    using (applDataTableAdapters.c3gL_errorTableAdapter c3gAdapter = new applDataTableAdapters.c3gL_errorTableAdapter())
                    {
                        c3gAdapter.Fill(lc3gError, tb_logTextFilter.Text);
                    }
                    dg_Result.DataSource = lc3gError;
                    break;
                case "C4G":
                    using (applDataTableAdapters.c4gL_errorTableAdapter c4gAdapter = new applDataTableAdapters.c4gL_errorTableAdapter())
                    {
                        c4gAdapter.Fill(lc4gError, tb_logTextFilter.Text);
                    }
                    dg_Result.DataSource = lc4gError;
                    break;
                case "ABB-NGAC":
                    //tbt
                    break;
                default:
                    MessageBox.Show("system unkown", "OEPS", MessageBoxButtons.OK);
                    return;
            }
            
            dg_Result.AllowUserToOrderColumns = true;
            dg_Result.AllowUserToResizeColumns = true;
            dg_Result.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_Result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            lbl_Results.Text = "LogText Records that mach the Query";
            Cursor.Current = Cursors.Default;

        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
//set selected errors to selected subgroup and classification
            foreach(DataGridViewRow row in dg_Result.SelectedRows)
            {
                switch (cb_system.Text)
                {
                    case "C3G":
                        Debug.WriteLine(row.ToString());
                        break;

                    case "C4G":
                        Debug.WriteLine(row.ToString());
                        break;
                    case "ABB-NGAC":
                        Debug.WriteLine(row.ToString());
                        break;

                    default:
                        MessageBox.Show("system unkown", "OEPS", MessageBoxButtons.OK);
                        return;
                }

            }
        }
 
        private void cb_Subgroup_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (cb_Subgroup.SelectedItem == CreateNew)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the new subgroup /n (between 4 And 20char)", "Create new subgroup", "", -1, -1);
                if (input.Length < 3)
                {
                    MessageBox.Show("the new subgroup name is to short", "OEPS", MessageBoxButtons.OK);
                    return;
                }
                else if (input.Length > 21)
                {
                    MessageBox.Show("the new subgroup name is to long", "OEPS", MessageBoxButtons.OK);
                    return;
                }
/*
               else if (lc_subgroup )
                {
                    MessageBox.Show("the subgroup name already exists", "OEPS", MessageBoxButtons.OK);
                    return;
                }
*/
                else
                {
                    using (applDataTableAdapters.c_SubgroupTableAdapter c_subgroup_Adapter = new applDataTableAdapters.c_SubgroupTableAdapter())
                    {
                        c_subgroup_Adapter.Insert(input);
                    }
                    fill_subgroup();
                }
            }
            else
            {
                fill_rules();
            }
        }

        private void cb_classification_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_classification.SelectedItem == CreateNew)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the new classification /n (between 4 And 40char)", "Create new classification", "", -1, -1);
                if (input.Length < 3)
                {
                    MessageBox.Show("the new classification name is to short", "OEPS", MessageBoxButtons.OK);
                    return;
                }
                else if (input.Length > 41)
                {
                    MessageBox.Show("the new classification name is to long", "OEPS", MessageBoxButtons.OK);
                    return;
                }
                /*
                               else if (lc_subgroup )
                                {
                                    MessageBox.Show("the subgroup name already exists", "OEPS", MessageBoxButtons.OK);
                                    return;
                                }
                                else if (check with maximo)
                */
                else
                {
                    using (applDataTableAdapters.c_ClassificationTableAdapter c_classification_Adapter = new applDataTableAdapters.c_ClassificationTableAdapter())
                    {
                        c_classification_Adapter.Insert(input);
                    }
                    fill_classification();
                }
            }
            else
            {
                fill_rules();
            }
        }

        private void btn_TestRules_Click(object sender, EventArgs e)
        {
            lbl_Results.Text = "LogText Records that mach the Query  THIS IS A TESTRESULT!";
        }

        private void btn_ApplyRules_Click(object sender, EventArgs e)
        {

            if (cb_OverRideManualSet.Checked)
            {
                //if check first count the number you are about to override and prompt user.

            }

        }

        private void dg_Rules_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
         //   MessageBox.Show("row added", "OEPS", MessageBoxButtons.OK);
        }
    }
}
