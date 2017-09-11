using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using EQUICommunictionLib;

namespace EQUIToolsLib
{
    public partial class MXxBomTree : MetroFramework.Forms.MetroForm
    {
        MaximoComm lMaximocomm = new MaximoComm();

        public MXxBomTree()
        {
            InitializeComponent();
            tb_item.Text = "6600020641";
        }

        protected void GetNodes(string Parent, TreeNode Tn)
        {
            string Sql = string.Format("SELECT ITEMNUM, QUANTITY, CXSEQUENCE FROM MAXIMO.ITEMSTRUCT WHERE PARENT = '{0}' AND CXSEQUENCE is not null ORDER BY CXSEQUENCE", Parent);
          DataTable dt = lMaximocomm.oracle_runQuery(Sql);

          Debug.WriteLine(string.Format("Parent: {0} Items: {1}",Parent, dt.Rows.Count));

          if (dt.Rows.Count == 0) return;

          foreach (DataRow row in dt.Rows)
          {
              TreeNode tn = new TreeNode(row.ItemArray[0].ToString());
              tn.Name = row.ItemArray[0].ToString();
              tn.Text = string.Format("item: {0} Q={1} CX= {2}", row.ItemArray[0].ToString(), row.ItemArray[1].ToString(), row.ItemArray[2].ToString());
              Tn.Nodes.Add(tn);

              Parent = row.ItemArray[0].ToString();
              GetNodes(Parent, tn);
              
          }
        }

        private void btn_get_Click(object sender, EventArgs e)
        {
            metroProgressSpinner1.Show();
            treeView1.Nodes.Clear();
            TreeNode tn = new TreeNode(tb_item.Text);
            treeView1.Nodes.Add(tn);
            GetNodes(tb_item.Text, tn); 
            metroProgressSpinner1.Hide();
        }

        private void Btn_copy_Click(object sender, EventArgs e)
        {
            string csvData = "";
            BuildCSV(treeView1.Nodes, ref csvData, 0);
            Clipboard.SetText(csvData);
        }

        private void BuildCSV(TreeNodeCollection nodes, ref string csvData, int depth)
        {
            foreach (TreeNode node in nodes)
            {
                csvData = csvData + new String(',', depth) + node.Name + "\n";
                if (node.Nodes.Count > 0)
                    BuildCSV(node.Nodes, ref csvData, depth + 1);
            }
        }

    }
}
