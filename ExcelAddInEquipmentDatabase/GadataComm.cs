using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Data.Odbc;
using System.Text.RegularExpressions;

namespace ExcelAddInEquipmentDatabase
{
    public class GadataComm
    {
        //connection to GADATA
        SqlConnection Gadataconn = new SqlConnection("user id=GADATA; password=GADATA987; server=SQLA001.gen.volvocars.net;" +
                                                      "Trusted_Connection=no; database=gadata; connection timeout=30");

        public string GADATAconnectionString
        {
            get { return @"ODBC;DSN=GADATA;Description= GADATA;UID=GADATA;PWD=GADATA987;APP=SQLFront;WSID=GNL1004ZCBQC2\\SDEBEUL;DATABASE=GADATA"; }
        }

        public void BulkCopyToGadata(string as_schema, DataTable adt_table, string as_destination)
        {
            {
                string connectionString = "user id=GADATA; password=GADATA987; server=SQLA001.gen.volvocars.net; Trusted_Connection=no; database=gadata; connection timeout=30";
                using (SqlConnection connection =
                           new SqlConnection(connectionString))
                {
                    connection.Open();
                    // there is no need to map columns.  
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "[" + as_schema + "].[" + as_destination + "]";
                        try
                        {
                            // Write from the source to the destination.
                            bulkCopy.WriteToServer(adt_table);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("BulkCopyToGadata exeption: {0} ", ex.Message);
                        }
                    }
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        public void RunCommandGadata(string as_command)
        {
            try
            {
                Gadataconn.Open();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            try
            {
                using (SqlCommand myCommand = new SqlCommand(as_command, Gadataconn))
                {
                    myCommand.ExecuteNonQuery();
                    Object returnValue = myCommand.ExecuteScalar();
                    try
                    {
                        Gadataconn.Close();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }


        }

        public DataTable RunQueryGadata(string as_query)
        {
            try
            {
                Gadataconn.Open();
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            try
            {
                using (SqlCommand myCommand = new SqlCommand(as_query, Gadataconn))
                {
                    DataTable dt = new DataTable();
                    dt.Load(myCommand.ExecuteReader());
                    try
                    {
                        Gadataconn.Close();
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.ToString());
                    }
                    return dt;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                DataTable dt = new DataTable();
                return dt;
            }


        }

        public SqlCommand get_GADATA_sp_parameters(string sp_name)
        {
            SqlCommand cmd = new SqlCommand(sp_name, Gadataconn);
            cmd.CommandType = CommandType.StoredProcedure;
                Gadataconn.Open();
                SqlCommandBuilder.DeriveParameters(cmd);
                Gadataconn.Close();
                return cmd;
        }

        //connection To MX7
        public DataTable GetAssetListFromMX7()
        {
            string strCon = @"DRIVER={Microsoft ODBC for Oracle};UID=BGASTHUY;PWD=BGASTHUY$123;SERVER=dpmxarct";
            string strSql = @"
     SELECT DISTINCT
                 l1.SYSTEMID
                ,l1.LOCATION LOCATION
                ,LOCATIONS.DESCRIPTION LocationDescription
                ,ASSET.ASSETNUM 
                ,ASSET.DESCRIPTION AssetDescription
                ,(
                 NVL2(l10.PARENT , l10.PARENT ||' -> ' ,'')||
                 NVL2(l9.PARENT , l9.PARENT ||' -> ','') ||
                 NVL2(l8.PARENT , l8.PARENT ||' -> ','') ||
                 NVL2(l7.PARENT , l7.PARENT ||' -> ','') ||
                 NVL2(l6.PARENT , l6.PARENT ||' -> ','') ||
                 NVL2(l5.PARENT , l5.PARENT ||' -> ','') ||
                 NVL2(l4.PARENT , l4.PARENT ||' -> ','') ||
                 NVL2(l3.PARENT , l3.PARENT ||' -> ','') ||
                 NVL2(l2.PARENT , l2.PARENT ||' -> ','') ||
                 NVL2(l1.PARENT , l1.PARENT ||' -> ','') ||
                 l1.LOCATION
                 ) LocationTree

                FROM MAXIMO.LOCHIERARCHY l1
                JOIN MAXIMO.LOCATIONS LOCATIONS on LOCATIONS.LOCATION = l1.LOCATION -- to get equipment status and details 
                JOIN MAXIMO.ASSET ASSET on ASSET.LOCATION = l1.LOCATION --to get asset number

                --join next 10 levels to build up structure
                LEFT JOIN MAXIMO.LOCHIERARCHY l2 on l2.LOCATION = l1.PARENT AND l2.SYSTEMID = L1.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l3 on l3.LOCATION = l2.PARENT AND l3.SYSTEMID = L2.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l4 on l4.LOCATION = l3.PARENT AND l4.SYSTEMID = L3.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l5 on l5.LOCATION = l4.PARENT AND l5.SYSTEMID = L4.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l6 on l6.LOCATION = l5.PARENT AND l6.SYSTEMID = L5.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l7 on l7.LOCATION = l6.PARENT AND l7.SYSTEMID = L6.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l8 on l8.LOCATION = l7.PARENT AND l8.SYSTEMID = L7.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l9 on l9.LOCATION = l8.PARENT AND l9.SYSTEMID = L8.SYSTEMID
                LEFT JOIN MAXIMO.LOCHIERARCHY l10 on l10.LOCATION = l9.PARENT AND l10.SYSTEMID = L9.SYSTEMID
                WHERE 
                l1.SITEID = 'VCG'
                AND
                l1.SYSTEMID = 'PRODMID'
            ";
            try
            {
                using (OdbcConnection con = new OdbcConnection(strCon))
                using (OdbcDataAdapter dadapter = new OdbcDataAdapter(strSql, con))
                {
                    //get location and asset data from maximo
                    DataTable table = new DataTable();
                    dadapter.Fill(table);
                    return table;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("GetAssetListFromMX7: Dataget: {0}", e.Message);
                DataTable table = new DataTable();
                return table;
            }

        }
    }
    
    public static class MyStringExtensions
    {
        public static bool Like(this string toSearch, string toFind)
        {
            return new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(toSearch);
        }
    }
}
