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
 * 
 * man_set make it an integer with the rule ID if manset make it -1
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
        BindingSource bindingSourceRules = new BindingSource();
        //
        applData.c_SubgroupDataTable lc_subgroup = new applData.c_SubgroupDataTable();
        applData.c_ClassificationDataTable lc_classification = new applData.c_ClassificationDataTable();
        //
        static string[] systems = new string[] { "ABB-NGAC" ,"C3G", "C4G"};
        string CreateNew = "Create new...";
        string UpdateFromMaximo = "Update from Maximo...";

        //main
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

        //get all available classifcations
        private void fill_classification()
        {
            using (applDataTableAdapters.c_ClassificationTableAdapter c_classification_Adapter = new applDataTableAdapters.c_ClassificationTableAdapter())
            {
                c_classification_Adapter.Fill(lc_classification);
            }

            var data = from a in lc_classification
                       orderby a.Classification ascending
                       select string.Format("{1,-10}  <{2}> <{0}>",a.id, a.Classification.TrimEnd(),a.Discription);
            List<string> data2 = data.Distinct().ToList();
            data2.Add(UpdateFromMaximo);
            cb_classification.DataSource = data2;
        }

        //get all available subgroups
        private void fill_subgroup()
        {
            using (applDataTableAdapters.c_SubgroupTableAdapter c_subgroup_Adapter = new applDataTableAdapters.c_SubgroupTableAdapter())
            {
                c_subgroup_Adapter.Fill(lc_subgroup);
            }

            var data = from a in lc_subgroup
                       select string.Format("{1,-10}  <{2}> <{0}>", a.id, a.Subgroup.TrimEnd(), a.Discription);
            List<string> data2 = data.Distinct().ToList();
            data2.Add(CreateNew);
            cb_Subgroup.DataSource = data2;
        }

        //
        private Int32 GetID(string sIn) 
        { 
        try
        {
            return Convert.ToInt32(sIn.Split('<')[2].TrimEnd('>'));
        }
        catch (Exception ex )
        {
            Debug.WriteLine("convertFailed: " + ex.Message);
            return 0;
        }
        }

        //fill rules based on system and filters
        private void fill_rules()
        {
            Cursor.Current = Cursors.AppStarting;
            switch (cb_system.Text)
            {
                case "C3G":
                    using (applDataTableAdapters.c3gC_LogClassRulesTableAdapter c3gAdapter = new applDataTableAdapters.c3gC_LogClassRulesTableAdapter())
                    {
                        c3gAdapter.Fill(lv3gRules, GetID(cb_classification.Text), GetID(cb_Subgroup.Text));
                    }
                    //dg_Rules.DataSource = lv3gRules;
                    bindingSourceRules.DataSource = lv3gRules;
                    break;
                case "C4G":
                    using (applDataTableAdapters.c4gC_LogClassRulesTableAdapter c4gAdapter = new applDataTableAdapters.c4gC_LogClassRulesTableAdapter())
                    {
                        c4gAdapter.Fill(lv4gRules, GetID(cb_classification.Text), GetID(cb_Subgroup.Text));
                    }
                    //dg_Rules.DataSource = lv4gRules;
                    bindingSourceRules.DataSource = lv4gRules;
                    break;
                case "ABB-NGAC":
                    MessageBox.Show("system not implemented", "OEPS", MessageBoxButtons.OK);
                    break;
                default:
                    MessageBox.Show("system unkown", "OEPS", MessageBoxButtons.OK);
                    return;
            }
            dg_Rules.DataSource = bindingSourceRules;
            //
            dg_Rules.AllowUserToOrderColumns = true;
            dg_Rules.AllowUserToResizeColumns = true;
            dg_Rules.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dg_Rules.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            Cursor.Current = Cursors.Default;
        }

        //fill logs based on filters
        private void fill_logs()
        {
            //check if the logcode filters are numbers
            try
            {
                Convert.ToInt32(tb_LogCodeFilterFrom.Text);
                Convert.ToInt32(tb_LogCodeFilterUntil.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"The logcodeFilter contains an invalid char" , "OEPS", MessageBoxButtons.OK);
                Debug.WriteLine(ex.Message);
                return;
            }
            //
            Cursor.Current = Cursors.AppStarting;
            switch (cb_system.Text)
            {
                case "C3G":
                    using (applDataTableAdapters.c3gL_errorTableAdapter c3gAdapter = new applDataTableAdapters.c3gL_errorTableAdapter())
                    {
                        c3gAdapter.Fill(lc3gError, tb_logTextFilter.Text, Convert.ToInt32(tb_LogCodeFilterFrom.Text), Convert.ToInt32(tb_LogCodeFilterUntil.Text));
                    }
                    dg_Result.DataSource = lc3gError;
                    break;
                case "C4G":
                    using (applDataTableAdapters.c4gL_errorTableAdapter c4gAdapter = new applDataTableAdapters.c4gL_errorTableAdapter())
                    {
                        c4gAdapter.Fill(lc4gError, tb_logTextFilter.Text, Convert.ToInt32(tb_LogCodeFilterFrom.Text), Convert.ToInt32(tb_LogCodeFilterUntil.Text));
                    }
                    dg_Result.DataSource = lc4gError;
                    break;
                case "ABB-NGAC":
                    MessageBox.Show("system not implemented", "OEPS", MessageBoxButtons.OK);
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

        //refresh log result based on filters
        private void btn_GetLogs_Click(object sender, EventArgs e)
        {
            fill_logs();
        }

        //update selected rows with the set subgroup and classification
        private void btn_Set_Click(object sender, EventArgs e)
        {
            //confirm with user before handeling more than 10 rows
            if (dg_Result.SelectedRows.Count > 10)
            {
                DialogResult result = MessageBox.Show(string.Format("You have selected {0} to change OK?",dg_Result.SelectedRows.Count), "CONFIRMATION", MessageBoxButtons.OKCancel);
                if (result != DialogResult.OK) { return; }
            }
            //loop selecte rows and update 
            foreach(DataGridViewRow row in dg_Result.SelectedRows)
            {
            try {
                    switch (cb_system.Text)
                    {
                        case "C3G":
                            using (applDataTableAdapters.c3gL_errorTableAdapter Adapter = new applDataTableAdapters.c3gL_errorTableAdapter())
                            {
                                Adapter.UpdateQuery(Convert.ToInt32(row.Cells[0].Value), GetID(cb_classification.Text), GetID(cb_Subgroup.Text));
                            }
                            break;
                        case "C4G":
                            using (applDataTableAdapters.c4gL_errorTableAdapter Adapter = new applDataTableAdapters.c4gL_errorTableAdapter())
                            {
                                Adapter.UpdateQuery(Convert.ToInt32(row.Cells[0].Value), GetID(cb_classification.Text), GetID(cb_Subgroup.Text));
                            }
                            break;
                        case "ABB-NGAC":
                            MessageBox.Show("system not implemented", "OEPS", MessageBoxButtons.OK);
                            break;

                        default:
                            MessageBox.Show("system unkown", "OEPS", MessageBoxButtons.OK);
                            return;
                    }
                  }
                  catch (Exception ex)
                            {
                                DialogResult result = MessageBox.Show("Something is wrong: " + ex.Message, "Oeps", MessageBoxButtons.AbortRetryIgnore);
                                if (result == DialogResult.Abort) {return;}
                                else if (result == DialogResult.Ignore){break;}
                                else if (result == DialogResult.Retry){} //not done
                            }
            }
            //update the result view 
            fill_logs();
        }
 
        //handle creation of a new subgroup
        private void cb_Subgroup_SelectedIndexChanged(object sender, EventArgs e)
        { 
            if (cb_Subgroup.SelectedItem.ToString() == CreateNew)
            {
                string input = Microsoft.VisualBasic.Interaction.InputBox("Enter the name of the new subgroup \n\r (between 4 And 20 char)", "Create new subgroup", "", -1, -1);
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
                else if (lc_subgroup.AsEnumerable().Any(row => input == row.Field<String>("Subgroup")))
                {
                    MessageBox.Show("the subgroup name already exists", "OEPS", MessageBoxButtons.OK);
                    return;
                }
                else
                {
                    using (applDataTableAdapters.c_SubgroupTableAdapter c_subgroup_Adapter = new applDataTableAdapters.c_SubgroupTableAdapter())
                    {
                        c_subgroup_Adapter.Insert(input,Microsoft.VisualBasic.Interaction.InputBox("Enter a discription for the subgroup", "Create new subgroup", "N/A", -1, -1));
                    }
                    fill_subgroup();
                }
            }
            else
            {
                fill_rules();
            }
        }

        //handle sync of classification CHECK WITH MX7
        private void cb_classification_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cb_classification.SelectedItem == null) { return; }
            if (cb_classification.SelectedItem.ToString() == UpdateFromMaximo)
            {
                //ask confirm
                DialogResult result = MessageBox.Show("Update NEW classifications from MX7 => GADATA", "CONFIRMATION", MessageBoxButtons.OKCancel);
                if (result != DialogResult.OK) { return; }
                //get current list from maximo
                Cursor.Current = Cursors.AppStarting;
                string QueryMaximoClassifications =
@"
SELECT  
 TRIM(STRUC.CLASSIFICATIONID) CLASSIFICATIONID
,STRUC.DESCRIPTION
FROM MAXIMO.CLASSSTRUCTURE STRUC
WHERE 
STRUC.SITEID = 'VCG'
AND 
STRUC.CLASSIFICATIONID like 'U%'
";
            
             MaximoComm lMaximoComm = new MaximoComm();
             DataTable MaximoClassifications = lMaximoComm.oracle_runQuery(QueryMaximoClassifications);
            //update gadata side with new items.
             using (applDataTableAdapters.c_ClassificationTableAdapter adapter = new applDataTableAdapters.c_ClassificationTableAdapter())
             {
                 foreach (DataRow lrow in MaximoClassifications.Rows)
                 {
                         adapter.InsertQuery(lrow.Field<String>("CLASSIFICATIONID"), lrow.Field<String>("DESCRIPTION"));
                 }
             }
                fill_classification();
                Cursor.Current = Cursors.Default;
            }
            else
            {
                fill_rules();
            }
        }

        //Apply the new rules on the dataset UPDATE ! 
        private void btn_ApplyRules_Click(object sender, EventArgs e)
        {
            GadataComm lGadataComm = new GadataComm();
            if (cb_OverRideManualSet.Checked)
            {
                DialogResult result = MessageBox.Show(
@"WARNING
You are about to let the rules OVERRIDE manually set classications.
Are you sure? 
This can not be undone.
"               , "CONFIRMATION", MessageBoxButtons.OKCancel);
                if (result != DialogResult.OK) { cb_OverRideManualSet.Checked = false; return; }
            }
            switch (cb_system.Text)
            {
                case "C3G":
                    lGadataComm.RunCommandGadata(string.Format(
                        @"exec gadata.[C3G].[sp_update_Lerror_classifcation] 
                                  @update = 1
                                , @OverRideManualSet = {0}
                                , @c_ClassificationId = {1}
                                , @c_SubgroupId = {2}"
                        , cb_OverRideManualSet.Checked, GetID(cb_classification.Text), GetID(cb_Subgroup.Text))
                        ,true);
                    break;
                case "C4G":
                    lGadataComm.RunCommandGadata(string.Format(
                        @"exec gadata.[C4G].[sp_update_Lerror_classifcation] 
                                  @update = 1
                                , @OverRideManualSet = {0}
                                , @c_ClassificationId = {1}
                                , @c_SubgroupId = {2}"
                        , cb_OverRideManualSet.Checked, GetID(cb_classification.Text), GetID(cb_Subgroup.Text))
                        ,true);
                    break;
                case "ABB-NGAC":
                    MessageBox.Show("system not implemented", "OEPS", MessageBoxButtons.OK);
                    break;

                default:
                    MessageBox.Show("system unkown", "OEPS", MessageBoxButtons.OK);
                    return;
            }
            cb_OverRideManualSet.Checked = false;
            fill_logs();
        }

        //clear data on change of system. 
        private void cb_system_SelectedIndexChanged(object sender, EventArgs e)
        {
            dg_Result.DataSource = null;
            dg_Rules.DataSource = null;
            fill_rules();
        }

        //handel row added 
        private void dg_Rules_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            try
            {
                switch (cb_system.Text)
                {
                    case "C3G":
                        using (applDataTableAdapters.c3gC_LogClassRulesTableAdapter Adapter = new applDataTableAdapters.c3gC_LogClassRulesTableAdapter())
                        {
                            Adapter.Insert(null, null, null, null, null, null, GetID(cb_classification.Text), GetID(cb_Subgroup.Text));
                        }
                        break;
                    case "C4G":
                        using (applDataTableAdapters.c4gC_LogClassRulesTableAdapter Adapter = new applDataTableAdapters.c4gC_LogClassRulesTableAdapter())
                        {
                            Adapter.Insert(null, null, null, null, null, null, GetID(cb_classification.Text), GetID(cb_Subgroup.Text));
                        }
                        break;
                    case "ABB-NGAC":
                        MessageBox.Show("system not implemented", "OEPS", MessageBoxButtons.OK);
                        break;

                    default:
                        MessageBox.Show("system unkown", "OEPS", MessageBoxButtons.OK);
                        return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to add row: " + ex.Message);
            }
            fill_rules();
        }

        //cell validation of rules 
        private void dg_Rules_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            switch (dg_Rules.Columns[e.ColumnIndex].HeaderText)
            {
                    //these 2 collums belong to old system so they can not be touched 
                case "Appl_id":
                    if (!string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        MessageBox.Show("CellChange not allowed Leave empty!", "Validation", MessageBoxButtons.OK);
                    }
                    break;
                case "Subgroup_id":
                    if (!string.IsNullOrEmpty(e.FormattedValue.ToString()))
                    {
                        MessageBox.Show("CellChange not allowed Leave empty!", "Validation", MessageBoxButtons.OK);
                    }
                    break;
                case "c_ClassificationId":
                        MessageBox.Show("CellChange not allowed", "Validation", MessageBoxButtons.OK);
                        bindingSourceRules.CancelEdit();
                        dg_Rules.RefreshEdit();
                    break;
                case "c_SubgroupId":
                      MessageBox.Show("CellChange not allowed", "Validation", MessageBoxButtons.OK);
                      bindingSourceRules.CancelEdit();
                      dg_Rules.RefreshEdit();
                      break;
                default:
                    // do nothing
                    break;
            }
        }

        //when row is valided calls this to update db
        private void dg_Rules_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            bindingSourceRules.EndEdit();
            int rowsUpdated = 0;
            //
            try
            {
                switch (cb_system.Text)
                {
                    case "C3G":
                        using (applDataTableAdapters.c3gC_LogClassRulesTableAdapter Adapter = new applDataTableAdapters.c3gC_LogClassRulesTableAdapter())
                        {
                            applData.c3gC_LogClassRulesDataTable dt = (applData.c3gC_LogClassRulesDataTable)bindingSourceRules.DataSource;
                            rowsUpdated = Adapter.Update(dt);
                        }
                        break;
                    case "C4G":
                        using (applDataTableAdapters.c4gC_LogClassRulesTableAdapter Adapter = new applDataTableAdapters.c4gC_LogClassRulesTableAdapter())
                        {
                            applData.c4gC_LogClassRulesDataTable dt = (applData.c4gC_LogClassRulesDataTable)bindingSourceRules.DataSource;
                            rowsUpdated = Adapter.Update(dt);
                        }
                        break;
                    case "ABB-NGAC":
                        MessageBox.Show("system not implemented", "OEPS", MessageBoxButtons.OK);
                        break;

                    default:
                        MessageBox.Show("system unkown", "OEPS", MessageBoxButtons.OK);
                        return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to Update row: " + ex.Message);
            }
            //
            fill_rules();
        }

        //confirm delete.
        private void dg_Rules_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure? Delete this row?", "CONFIRMATION", MessageBoxButtons.OKCancel);
            if (result != DialogResult.OK) { e.Cancel = true; }
        }

    }
}
