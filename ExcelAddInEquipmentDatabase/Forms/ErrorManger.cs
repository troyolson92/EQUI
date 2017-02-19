using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class ErrorManger : Form
    {
        public ErrorManger()
        {
            InitializeComponent();
    	    string[] systems = new string[]{"C3G", "C4G", "ABB-NGAC"};
            cb_system.Items.AddRange(systems);
            //fill the classification list
                    //make new table with all classifcations that start with U 
                    //don't forget a system to maintain that list.
                    //don't allow user to make new ones

            //fill the subgroup list
                    //make new table that will contain all the subgroups for ALL systems
                    //alow user to make new subgroup by typeing one 
        }

        private void btn_GetLogs_Click(object sender, EventArgs e)
        {
//fill datagrid based on the active system and both filters
        }

        private void btn_Set_Click(object sender, EventArgs e)
        {
//set selected errors to selected subgroup and classification
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        /*
select * from MAXIMO.CLASSIFICATION CLASS 
where CLASS.SITEID = 'VCG' AND CLASS.CLASSIFICATIONID like '%UR%';
select * from MAXIMO.CLASSSTRUCTURE STRUC
WHERE STRUC.SITEID = 'VCG' AND STRUC.CLASSIFICATIONID like 'U%';
         * 
         * => need to change the asset list to include the full asset classification (assetmanger)
         * */
    }
}
