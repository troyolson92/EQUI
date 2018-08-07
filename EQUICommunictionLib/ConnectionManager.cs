using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace EQUICommunictionLib
{
    //helper enum for differnet supported db types
    public enum db_type
    {
        msSqlServer = 1,
        Orcacle = 2
    }

    //helper object to store connection properties.
    public class Database
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public db_type Type { get; set; }
        public string ConnectionString { get; set; }
        public string Description { get; set; }
        //method to update connectingstring from DB
        public void UpdateConnectionString(string newConnString)
        {
            ConnectionManager connectionManager = new ConnectionManager();
            string updateCommand = "UPDATE GADATA.EqUi.c_datasource set ConnectionString = '{1}' from GADATA.EqUi.c_datasource where [id] = {0}";
            connectionManager.RunCommand(string.Format(updateCommand,Id,newConnString),enblExeptions:true);
        }
    }

    //this class manages all database connection in the equi system
    public class ConnectionManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //get main EQUI connection string. This is set in app.config and returns the EQUI main database. (All other connection strings are stored in there)
        public string EQUIConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString; }
        }

        //returns the default databaseobject
        public Database DefaultDatabase()
        {
            Database DB = new Database();
            DB.Id = 0;
            DB.Name = "Default";
            DB.Type = db_type.msSqlServer;
            DB.ConnectionString = EQUIConnectionString;
            DB.Description = "Default database for EQUI (connection string taken from app.config)";
            return DB;
        }

        //get connection for database X from EQUI
        //if dbName empty all will be returend
        public List<Database> GetDB(string dbName = "", int dbID = 0)
        {
            if (dbName != "" && dbID != 0)
            {
                throw new NotSupportedException();
            }

            SqlComm sqlComm = new SqlComm(EQUIConnectionString);
            DataTable dt = new DataTable();
            List<Database> list = new List<Database>();

            if (dbName != "") // get by name
            {
                string getCommand = "select * from GADATA.EqUi.c_datasource where [Name] = '{0}'";
                dt = sqlComm.RunQuery(string.Format(getCommand, dbName), enblExeptions: true);
                if (dt.Rows.Count != 1)
                {
                    if (dt.Rows.Count == 0)
                    {
                        log.Error("No valid result for: " + dbName);
                    }
                    else
                    {
                        log.Error("Config error found more than once: " + dbName);
                    }
                    throw new NotSupportedException();
                }
            }
            else if (dbID != 0) //get by ID
            {
                string getCommand = "select * from GADATA.EqUi.c_datasource where [ID] = {0}";
                dt = sqlComm.RunQuery(string.Format(getCommand, dbID), enblExeptions: true);
                if (dt.Rows.Count != 1)
                {
                    if (dt.Rows.Count == 0)
                    {
                        log.Error("No valid result for: " + dbName);
                    }
                    else
                    {
                        log.Error("Config error found more than once: " + dbName);
                    }
                    throw new NotSupportedException();
                }
            }
            else //get all
            {
                string getCommand = "select * from GADATA.EqUi.c_datasource";
                dt = sqlComm.RunQuery(string.Format(getCommand, dbName), enblExeptions: true);
                if (dt.Rows.Count == 0)
                {
                    log.Error("No valid result for: " + dbName);
                    throw new NotSupportedException();
                }
                //add the default database to the list if full list
                list.Add(DefaultDatabase());
            }

            foreach (DataRow row in dt.Rows)
            {
                Database DB = new Database();
                DB.Id = dt.Rows[0].Field<int>("Id");
                DB.Name = dt.Rows[0].Field<string>("Name");
                DB.Type = (db_type)Enum.ToObject(typeof(db_type), dt.Rows[0].Field<int>("Type"));
                DB.ConnectionString = dt.Rows[0].Field<string>("ConnectionString");
                DB.Description = dt.Rows[0].Field<string>("Description");
                list.Add(DB);
            }
            return list;
        }

        //run Query for a db
        //option to run get database by name or by ID
        //if dbName and ID is left blank run against main datbase
        public DataTable RunQuery(string sqlQuery, string dbName = "", int dbID = 0 , bool enblExeptions = false, int maxEXECtime = 300)
        {
            Database db = new Database();
            if (dbName != "" || dbID != 0)
            {
                db = GetDB(dbName,dbID).First();
            }
            else
            {
                db = DefaultDatabase();
            }


            //temp debug
            if (dbID == 1)
            {
                log.Error("BUG BUG BUG");
                db = DefaultDatabase();
            }


            switch (db.Type)
            {
                case db_type.msSqlServer:
                    SqlComm sqlComm = new SqlComm(db.ConnectionString);
                    return sqlComm.RunQuery(sqlQuery, enblExeptions: enblExeptions, maxEXECtime: maxEXECtime);

                case db_type.Orcacle:
                    OracleComm oracleComm = new OracleComm(db.ConnectionString);
                    return oracleComm.RunQuery(sqlQuery, enblExeptions: enblExeptions, maxEXECtime: maxEXECtime);

                default:
                    log.Error("db type not supported");
                    throw new NotSupportedException();
            }
        }

        //run Command form a db
        //option to run get database by name or by ID
        //if dbName and ID is left blank run against main datbase
        public void RunCommand(string sqlCommand, string dbName = "", int dbID = 0, bool enblExeptions = false, int maxEXECtime = 300)
        {
            Database db = new Database();
            if (dbName != "" || dbID != 0)
            {
                db = GetDB(dbName, dbID).First();
            }
            else
            {
                db = DefaultDatabase();
            }
        

            switch (db.Type)
            {
                case db_type.msSqlServer:
                    SqlComm sqlComm = new SqlComm(db.ConnectionString);
                    sqlComm.RunCommand(sqlCommand, enblExeptions: enblExeptions, maxEXECtime: maxEXECtime);
                    break;

                case db_type.Orcacle:
                    OracleComm oracleComm = new OracleComm(db.ConnectionString);
                    oracleComm.RunCommand(sqlCommand, enblExeptions: enblExeptions, maxEXECtime: maxEXECtime);
                    break;

                default:
                    log.Error("db type not supported");
                    throw new NotSupportedException();
            }

        }

        //run bulkCopy command (only for msSQL)
        //option to run get database by name or by ID
        //if dbName and ID is left blank run against main datbase
        public void BulkCopy(DataTable data,string destination, string dbName = "", int dbID = 0, bool enblExeptions = false, int maxEXECtime = 300)
        {
            Database db = new Database();
            if (dbName != "" || dbID != 0)
            {
                db = GetDB(dbName, dbID).First();
            }
            else
            {
                db = DefaultDatabase();
            }


            switch (db.Type)
            {
                case db_type.msSqlServer:
                    SqlComm sqlComm = new SqlComm(db.ConnectionString);
                    sqlComm.BulkCopy(data, destination, enblExeptions: enblExeptions, maxEXECtime: maxEXECtime);
                    break;

                case db_type.Orcacle:
                    throw new NotImplementedException();

                default:
                    log.Error("db type not supported");
                    throw new NotSupportedException();
            }

        }

        //run CLOB command (only for oracle)
        //option to run get database by name or by ID
        //if dbName and ID is left blank run against main datbase
        public string GetCLOB(string qry, string dbName = "", int dbID = 0, bool enblExeptions = false)
        {
            Database db = new Database();
            if (dbName != "" || dbID != 0)
            {
                db = GetDB(dbName, dbID).First();
            }
            else
            {
                db = DefaultDatabase();
            }


            switch (db.Type)
            {
                case db_type.msSqlServer:
                    throw new NotImplementedException();

                case db_type.Orcacle:
                    OracleComm oracleComm = new OracleComm(db.ConnectionString);
                    return oracleComm.GetCLOB(qry,enblExeptions:enblExeptions);

                default:
                    log.Error("db type not supported");
                    throw new NotSupportedException();
            }

        }

        //get storced proc parameters (only for msSQL)
        //option to run get database by name or by ID
        //if dbName and ID is left blank run against main datbase
        public SqlCommand GetSpParms(string sp_name, string dbName = "", int dbID = 0, bool enblExeptions = false)
        {
            Database db = new Database();
            if (dbName != "" || dbID != 0)
            {
                db = GetDB(dbName, dbID).First();
            }
            else
            {
                db = DefaultDatabase();
            }


            switch (db.Type)
            {
                case db_type.msSqlServer:
                    SqlComm sqlComm = new SqlComm(db.ConnectionString);
                    return sqlComm.GetSpParms(sp_name, enblExeptions: enblExeptions);

                case db_type.Orcacle:
                    throw new NotImplementedException();

                default:
                    log.Error("db type not supported");
                    throw new NotSupportedException();
            }

        }

        //test to auto change passwords for DB
        public void PWCheck(string dbName = "", bool ChangeIfExpired = false, string newPW = "")
        {
            Database db = GetDB(dbName).FirstOrDefault();
            log.Debug("Starting db PWcheck for: " + db.Name);
            switch (db.Type)
            {
                case db_type.msSqlServer:
                    throw new NotImplementedException();

                case db_type.Orcacle:
                    OracleComm oracleComm = new OracleComm(db.ConnectionString);
                    if (oracleComm.CheckPassWorkExpired())
                    {
                        log.Info("password Expired");
                        if (ChangeIfExpired)
                        {
                            //change the pasword on the server
                            oracleComm.CheckPassWorkExpired(ChangeIfExpired: true, newPW: newPW);
                            //update the connectionstring in the connfiguration
                            SqlConnectionStringBuilder Connbuilder = new SqlConnectionStringBuilder(db.ConnectionString);
                            Connbuilder.Password = newPW;
                            db.UpdateConnectionString(Connbuilder.ConnectionString);
                        }
                    }
                    else
                    {
                        log.Info("password OKE");
                    }
                    break;

                default:
                    log.Error("db type not supported");
                    throw new NotSupportedException();
            }
            log.Debug("Ended db PWcheck for: " + db.Name);
        }



        //test command to test al DB's
        //I just do a getdate() sysdata on all systems. (if logon error like that will crap out)
        public void TestAllDb()
        {
            List<Database> list = GetDB();
            foreach (Database db in list)
            {
                log.Debug("Starting db test for: " + db.Name);
                switch (db.Type)
                {
                    case db_type.msSqlServer:
                        SqlComm sqlComm = new SqlComm(db.ConnectionString);
                        sqlComm.RunQuery("SELECT GETDATE()", enblExeptions: true);
                        break;

                    case db_type.Orcacle:
                        OracleComm oracleComm = new OracleComm(db.ConnectionString);
                        oracleComm.RunQuery("SELECT SYSDATE FROM DUAL", enblExeptions: true);
                        break;

                    default:
                        log.Error("db type not supported");
                        throw new NotSupportedException();
                }
                log.Debug("Ended db test for: " + db.Name);
            }
            log.Info("database test succes");
        }

    }
}
