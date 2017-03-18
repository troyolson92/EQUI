using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ExcelAddInEquipmentDatabase.Forms
{
    public partial class MXxWOoverview : MetroFramework.Forms.MetroForm
    {
        MaximoComm lMaximocomm = new MaximoComm();
        bool lPartmode;
        BackgroundWorker bwLongDescription = new BackgroundWorker();

        public MXxWOoverview(string location, bool partmode)
        {
            InitializeComponent();
            bwLongDescription.DoWork += bwLongDescription_DoWork;
         
            tb_location.Text = location;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            lPartmode = partmode;
            
            this.Show();

            getMaximoWorkorder(tb_location.Text, lPartmode);

            dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
        }

        void bwLongDescription_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                getMaximoDetails(row.Cells[0].Value.ToString());
            }
        }


        private void getMaximoWorkorder(string location, bool partmode)
        {
            //get asset list from maximo M7 daily copy
            DataTable tableFromMx7 = new DataTable();
            string strSqlGetFromMaximo = string.Format(@"
SELECT 
 TO_NUMBER(WORKORDER.WONUM) WONUM
,WORKORDER.STATUS
,WORKORDER.STATUSDATE
,WORKORDER.WORKTYPE
,WORKORDER.DESCRIPTION
,WORKORDER.LOCATION
,WORKORDER.REPORTEDBY
,WORKORDER.REPORTDATE
FROM MAXIMO.WORKORDER WORKORDER  
WHERE WORKORDER.LOCATION LIKE '{0}'
ORDER BY WORKORDER.STATUSDATE DESC
            ",location);

            string strSqlGetPartsMaximo = string.Format(@"

SELECT 
 WORKORDER.WONUM
,WORKORDER.REPORTDATE
,WORKORDER.WORKTYPE
,WORKORDER.DESCRIPTION
,WPITEM.ITEMNUM
,WPITEM.DESCRIPTION PARTDESCRIPTION
,WORKORDER.LOCATION
,WPITEM.REQUESTBY
FROM MAXIMO.WORKORDER WORKORDER  
JOIN MAXIMO.WPITEM WPITEM ON WPITEM.WONUM = WORKORDER.WONUM
WHERE WORKORDER.LOCATION LIKE '{0}'
ORDER BY WORKORDER.STATUSDATE
            ", location);

            if (partmode)
            {
                this.Text = string.Format("Maximo Wo browser: 'Parts on location' <{0}>", location);
                tableFromMx7 = lMaximocomm.oracle_runQuery(strSqlGetPartsMaximo);
                dataGridView1.DataSource = tableFromMx7;
            }
            else
            {
                this.Text = string.Format("Maximo Wo browser: 'Workorder on location' <{0}>", location);
                tableFromMx7 = lMaximocomm.oracle_runQuery(strSqlGetFromMaximo);
                dataGridView1.DataSource = tableFromMx7;
            }
        }

        private void getMaximoDetails(string wonum)
        {
            string cmdFAILUREREMARK = (@"
                select  NVL2(LD.LDTEXT, LD.LDTEXT, '') LDTEXT
                from MAXIMO.FAILUREREMARK FM 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = FM.FAILUREREMARKID AND LD.LDOWNERTABLE = 'FAILUREREMARK'
                where fm.wonum = '{0}'
            ");
            string cmdLONGDESCRIPTION = (@"
                select NVL2(LD.LDTEXT, LD.LDTEXT, '') LDTEXT
                from MAXIMO.WORKORDER WO 
                left join MAXIMO.LONGDESCRIPTION LD on LD.LDKEY = WO.WORKORDERID AND LD.LDOWNERTABLE = 'WORKORDER'
                where WO.wonum = '{0}'
            ");
            cmdFAILUREREMARK = string.Format(cmdFAILUREREMARK, wonum);
            cmdLONGDESCRIPTION = string.Format(cmdLONGDESCRIPTION, wonum);

            string sEb = "<div>***************************</div>";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(sEb).AppendLine("<div>LONGDESCRIPTION</div>").AppendLine(sEb);
            sb.AppendLine(lMaximocomm.GetClobMaximo7(cmdLONGDESCRIPTION));

            sb.AppendLine(sEb).AppendLine("<div>FAILUREREMARK</div>").AppendLine(sEb);
            sb.AppendLine(lMaximocomm.GetClobMaximo7(cmdFAILUREREMARK));

            webBrowser1.DocumentText = sb.ToString();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (bwLongDescription.IsBusy) { return; }
            bwLongDescription.RunWorkerAsync();
        }

        private void btn_refresh_Click(object sender, EventArgs e)
        {
            getMaximoWorkorder(tb_location.Text, lPartmode);
        }
    }
}
