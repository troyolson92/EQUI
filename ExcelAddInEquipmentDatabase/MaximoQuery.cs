﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExcelAddInEquipmentDatabase
{
    class OracleQuery
    {
        EquiEntities db = new EquiEntities();

        //maximo comm TO GADATA
        /*
                public void oracle_update_Query_to_GADATA(string System, string Queryname, string QueryDiscription, string Query)
                {
                    using (EquiEntities db = new EquiEntities())
                    {
                        db.QUERYS.Where(c => c.SYSTEM == System && c.NAME == Queryname).ToList();
                            var ds = from a in db.QUERYS
                                     where a.SYSTEM == System && a.NAME == Queryname
                                     select a;

                    }
                }

                public void oracle_delete_Query_GADATA(string System, string Queryname)
                {
                    using (EquiEntities db = new EquiEntities())
                    {
                        db.QUERYS.Remove(db.QUERYS.Where(c => c.SYSTEM == System && c.NAME == Queryname).First());
                        db.SaveChanges();
                    }
                }

                public void oracle_send_new_Query_to_GADATA(string System, string Queryname, string QueryDiscription, string Query)
                {

                    using (EquiEntities db = new EquiEntities())
                    {
                        adapter.Insert(System, Queryname, QueryDiscription, Query);
                    }
                }

                public string oracle_get_QueryTemplate_from_GADATA(string QueryName, string System)
                {
                    string Query;
                    using (EquiEntities db = new EquiEntities())
                    {
                        using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                        {
                            adapter.Fill(lQUERYS);
                        }
                        Query = (from a in lQUERYS
                                 where a.SYSTEM == System && a.NAME == QueryName
                                 select a.QUERY).First().ToString();
                    }
                    return Query.Trim().TrimEnd(';').ToUpper();
                }

                public string oracle_get_QueryDiscription_from_GADATA(string QueryName, string System)
                {
                    string Disciption;
                    using (EquiEntities db = new EquiEntities())
                    {
                        using (applDataTableAdapters.QUERYSTableAdapter adapter = new applDataTableAdapters.QUERYSTableAdapter())
                        {
                            adapter.Fill(lQUERYS);
                        }
                        Disciption = (from a in lQUERYS
                                      where a.SYSTEM == System && a.NAME == QueryName
                                      select a.DISCRIPTION).First().ToString();
                    }
                    return Disciption.Trim();
                }
                */

            /*
        public List<OracleQueryParm> oracle_get_QueryParms_from_GADATA(string QueryName, string System)
                {
                    string Query = db.QUERYS.Where(c => c.NAME == QueryName && c.SYSTEM == System).First().QUERY;
                    //gets part of the query containing the params
                    List<string> ParmLines = Query.ToUpper().Split(new string[] { "SELECT" }, StringSplitOptions.None)[0].Trim()
                                                            .Split(new string[] { "DEFINE" }, StringSplitOptions.None).ToList();
                    List<OracleQueryParm> _parmList = new List<OracleQueryParm>();
                    foreach (string parm in ParmLines)
                    {
                        if (parm.Contains("="))
                        {
                            string ParmName = parm.Split('=')[0].Trim();
                            string ParmValue = parm.Split('=')[1].Trim().Split('\'')[1];
                            _parmList.Add(new OracleQueryParm { ParameterName = ParmName, Defaultvalue = ParmValue });
                        }
                    }
                    return _parmList;
                }
*/
        /*

            }


            */
    }
        public class OracleQueryParm
        {
            public string ParameterName { get; set; }
            public string Defaultvalue { get; set; }
        }
    }
