using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
        applData.c4gL_errorDataTable lc4gError = new applData.c4gL_errorDataTable();
        applData.c_SubgroupDataTable lc_subgroup = new applData.c_SubgroupDataTable();

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

        private void fill_classification()
        {
            //fill the classification list
            //make new table with all classifcations that start with U 
            //don't forget a system to maintain that list.
            //don't allow user to make new ones
        }

        private void fill_rules()
        {

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

                    break;
                case "C4G":
                    using (applDataTableAdapters.c4gL_errorTableAdapter c4gAdapter = new applDataTableAdapters.c4gL_errorTableAdapter())
                    {
                        c4gAdapter.Fill(lc4gError, tb_logTextFilter.Text);
                    }
                    dg_Result.DataSource = lc4gError;
                    break;
                case "ABB-NGAC":

                    break;
                default:
                    MessageBox.Show("system unkown", "OEPS", MessageBoxButtons.OK);
                    return;
            }
            
            dg_Result.AllowUserToOrderColumns = true;
            dg_Result.AllowUserToResizeColumns = true;
            dg_Result.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_Result.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
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

                        break;

                    case "C4G":
                  

                        break;
                    case "ABB-NGAC":

                        break;

                    default:
                        MessageBox.Show("system unkown", "OEPS", MessageBoxButtons.OK);
                        return;
                }

            }
        }
 
        private void cb_Subgroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            // if selected item = create new 
            //popup input and insert. 
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
            //if other get rules for that subgroup and classification
        }
    }
}
