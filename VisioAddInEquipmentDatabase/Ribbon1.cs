using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using System.Data;
using System.Data.SqlClient;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Diagnostics;
using System.Windows.Forms;

namespace VisioAddInEquipmentDatabase
{
    public partial class Ribbon1
    {
        //mx7 connection
        OracleConnection Maximo7conn = new OracleConnection("data source = dpmxarct;user id = ARCTVCG;password=vcg$tokfeb2017");
        //connection to GADATA
        SqlConnection Gadataconn = new SqlConnection("user id=EqUi; password=EqUi; server=SQLA001.gen.volvocars.net;" +
                                                      "Trusted_Connection=no; database=gadata; connection timeout=30");

        //conntion to GADATA admin
        SqlConnection GadataconnAdmin = new SqlConnection("user id=GADATA; password=GADATA987; server=SQLA001.gen.volvocars.net;" +
                                                      "Trusted_Connection=no; database=gadata; connection timeout=60");

        public DataTable oracle_runQuery(string Query)
        {
            try
            {
                using (OracleDataAdapter dadapter = new OracleDataAdapter(Query, Maximo7conn))
                {
                    //get location and asset data from maximo
                    DataTable table = new DataTable();
                    dadapter.Fill(table);
                    return table;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                DataTable table = new DataTable();
                return table;
            }

        }

        public void BulkCopyToGadata(string as_schema, DataTable adt_table, string as_destination)
        {
            {         
                using (Gadataconn)
                {
                    Gadataconn.Open();
                    // there is no need to map columns.  
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(Gadataconn))
                    {
                        bulkCopy.DestinationTableName = "[" + as_schema + "].[" + as_destination + "]";
                        try
                        {
                            // Write from the source to the destination.
                            bulkCopy.WriteToServer(adt_table);
                        }
                        catch (Exception ex)
                        {                     
                            Debug.WriteLine(ex.Message);
                        }
                    }
                    Gadataconn.Close();
                    Gadataconn.Dispose();
                }
            }
        }

        public void RunCommandGadata(string as_command, bool ab_admin = false)
        {
            SqlConnection lconn;
            if (ab_admin)
            { lconn = GadataconnAdmin; }
            else
            { lconn = Gadataconn; }

            try
            {
                lconn.Open();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            try
            {
                using (SqlCommand myCommand = new SqlCommand(as_command, lconn))
                {
                    myCommand.ExecuteNonQuery();
                    Object returnValue = myCommand.ExecuteScalar();
                    try
                    {
                        lconn.Close();
                    }
                    catch (Exception e)
                   {
                       Debug.WriteLine(e.Message);
                   }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }


        }

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
            DataTable mxDT = oracle_runQuery(MxQuery);

            if (mxDT.Rows.Count != 0)
            {
                //clear all data from table on GADATA side
                RunCommandGadata("DELETE GADATA.VISIO.QiconRawMX7 FROM GADATA.VISIO.QiconRawMX7",false);
                //copy new data from maximo to gadaya.
                BulkCopyToGadata("VISIO",mxDT,"QiconRawMX7");
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
