using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EQUICommunictionLib;
using EqUiWebUi.Areas.Alert.Models;
using System.Text;
using System.Web.Helpers;

namespace EqUiWebUi.Controllers
{
    public class DynamicGridModel
    {
        //pagination
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagerCount { get; set; }
        public string DataTimestamp { get; set; }

        public List<dynamic> Data { get; set; }
    }

    public class DynamicController : Controller
    {
        // GET: Dynamic
        public ActionResult Index()
        {
            return new HttpNotFoundResult("Woeps there seems to bo nothing here");
        }

        public List<dynamic> datatableToDynamic(DataTable dt)
        {
            List<dynamic> data = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                var element = new System.Dynamic.ExpandoObject() as IDictionary<string, Object>;
                foreach (DataColumn col in dt.Columns)
                {
                    element.Add(col.ColumnName, row.ItemArray[col.Ordinal]);
                }
                data.Add(element);
            }
            return data;
        }

        public List<WebGridColumn> getDatatabelCollumns(DataTable dt)
        {
            List<WebGridColumn> columns = new List<WebGridColumn>();
            foreach (DataColumn col in dt.Columns)
            {
                columns.Add(new WebGridColumn() { ColumnName = col.ColumnName, Header = col.ColumnName });
            }
            return columns;
        }

        //return partial based on a query run against a database.
        public ActionResult _dynamicWebgridRunQueryAgainstDB(string qry, int db = 0)
        {
            DataTable dt = new DataTable();
            //run against database.
            ConnectionManager connectionManager = new ConnectionManager();
            //run command against selected database.
            dt = connectionManager.RunQuery(qry, dbID: db, enblExeptions: true);
            ViewBag.Columns = getDatatabelCollumns(dt);
            List<dynamic> data = datatableToDynamic(dt);
            DynamicGridModel model = new DynamicGridModel();
            model.PageSize = 100;
            model.TotalCount = data.Count();
            model.Data = data;
            return PartialView(model);
        }

      
    }
}