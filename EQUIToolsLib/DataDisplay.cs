using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EQUIToolsLib
{
    public partial class DataDisplay : MetroFramework.Forms.MetroForm
    {
        DataSet lds;
        public DataDisplay(DataSet ds)
        {
            lds = ds;
            InitializeComponent();
            cb_datasource.Items.Clear();
            foreach (DataTable dt in lds.Tables)
            {
                cb_datasource.Items.Add(dt.TableName);
            }
            cb_datasource.SelectedValueChanged += cb_datasource_SelectedValueChanged;
            Show();
        }

        void cb_datasource_SelectedValueChanged(object sender, EventArgs e)
        {
            foreach (DataTable dt in lds.Tables)
            {
                if (dt.TableName.ToString() == cb_datasource.SelectedItem.ToString())
                {
                    dataGridView1.DataSource = dt;
                    return;
                }
            }
           
        }
    }
}
