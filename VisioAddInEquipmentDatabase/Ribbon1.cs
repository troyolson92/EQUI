using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;
using EQUICommunictionLib;

namespace VisioAddInEquipmentDatabase
{
    public partial class Ribbon1
    {
        MaximoComm lMaximoComm = new MaximoComm();
        GadataComm lGadataComm = new GadataComm();

        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            #region Query
            string MxQuery = @"
select 
 WORKORDER.WONUM
,WORKORDER.WORKTYPE
,WORKORDER.CHANGEDATE
,WORKORDER.JPNUM
,locancestor.ANCESTOR
from MAXIMO.WORKORDER WORKORDER
left join MAXIMO.locancestor locancestor on 
locancestor.LOCATION = WORKORDER.LOCATION
and 
locancestor.ORGID = 'VCCBE'

WHERE 
WORKORDER.JPNUM = 'PQAGEO'
OR 
WORKORDER.WORKTYPE = 'CI'
AND
WORKORDER.CHANGEDATE > TO_TIMESTAMP('2017-06-12 00:00:00', 'yyyy/mm/dd hh24:mi:ss')
AND
WORKORDER.CHANGEDATE < TO_TIMESTAMP('2018-06-10 00:00:00', 'yyyy/mm/dd hh24:mi:ss')

";
            #endregion
            DataTable mxDT =   lMaximoComm.oracle_runQuery(MxQuery);

            if (mxDT.Rows.Count != 0)
            {
                //clear all data from table on GADATA side
               lGadataComm.RunCommandGadata("DELETE GADATA.VISIO.QiconRawMX7 FROM GADATA.VISIO.QiconRawMX7",false);
                //copy new data from maximo to gadaya.
               lGadataComm.BulkCopyToGadata("VISIO",mxDT,"QiconRawMX7");
                //report succes
                MessageBox.Show(string.Format(
@"Operation complete with succes. 
{0} rows where send to GADATA from MX7", mxDT.Rows.Count));              
            }
            else
            {
                MessageBox.Show("Failed to get data from MX7");
            }
        }
    }
}
