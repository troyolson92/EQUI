using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;

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
    }


    public class ConnectionManager
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //get main EQUI connection string. This is set in app.config and returns the EQUI main database. (All other connection strings are stored in there)
        public string EQUIConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["EQUIConnectionString"].ConnectionString; }
        }

        //get connection for dabase X from EQUI
        //if dbName empty all will be returend
        public List<Database> GetDB(string dbName = "")
        {
            SqlComm sqlComm = new SqlComm(EQUIConnectionString);
            DataTable dt = new DataTable();
            //specific db
            if (dbName != "")
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
            else
            {
                string getCommand = "select * from GADATA.EqUi.c_datasource";
                dt = sqlComm.RunQuery(string.Format(getCommand, dbName), enblExeptions: true);
                if (dt.Rows.Count == 0)
                {
                    log.Error("No valid result for: " + dbName);
                    throw new NotSupportedException();
                }
            }

            List<Database> list = new List<Database>();

            foreach (DataRow row in dt.Rows)
            {
                Database DB = new Database();
                DB.Id = dt.Rows[1].Field<int>("Id");
                DB.Name = dt.Rows[1].Field<string>("Id");
                DB.Type = (db_type)Enum.ToObject(typeof(db_type), dt.Rows[1].Field<int>("Type"));
                DB.ConnectionString = dt.Rows[1].Field<string>("ConnectionString");
                DB.Description = dt.Rows[1].Field<string>("Description");
                list.Add(DB);
            }
            return list;
        }


        //run Query for a db
        public DataTable RunQuery(string dbName, string sqlQuery, bool enblExeptions = false, int maxEXECtime = 300)
        {
            Database db = GetDB(dbName).First();
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
        public void RunCommand(string dbName, string sqlCommand, bool enblExeptions = false, int maxEXECtime = 300)
        {
            Database db = GetDB(dbName).First();
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

    }
}
