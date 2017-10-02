using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Dynamic;
using System.Web.Helpers;
using HtmlAgilityPack;

namespace EqUiWebUi.WebGridHelpers
{
    public class WebGridHelper
    {

        public List<dynamic> datatableToDynamic(DataTable dt)
        {
            List<dynamic> data = new List<dynamic>();
            foreach (DataRow row in dt.Rows)
            {
                var element = new ExpandoObject() as IDictionary<string, Object>;
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

        public List<WebGridColumn> getHtmltabelCollumns(string htmlTable)
        {
            List<WebGridColumn> columns = new List<WebGridColumn>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlTable);
            //must handle multible tables ? 
            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    foreach (HtmlNode col in row.SelectNodes("td"))
                    {
                       columns.Add(new WebGridColumn() { ColumnName = col.InnerHtml, Header = col.InnerHtml });
                    }
                    //only the first one ofcoarse
                    break;
                }
            }
            return columns;
        }

        public List<dynamic> htmltableToDynamic(string htmlTable)
        {
            List<dynamic> data = new List<dynamic>();
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlTable);
            //must handle multible tables ? 
            foreach (HtmlNode table in doc.DocumentNode.SelectNodes("//table"))
            {
                //get columns
                List<WebGridColumn> columns = getHtmltabelCollumns(htmlTable);
                foreach (HtmlNode row in table.SelectNodes("tr"))
                {
                    var element = new ExpandoObject() as IDictionary<string, Object>;
                    int colIdx = 0;
                    foreach (HtmlNode col in row.SelectNodes("td"))
                    {
                        element.Add(columns[colIdx].ColumnName, col.InnerHtml);
                        colIdx += 1;
                    }
                    data.Add(element);
                }
            }
            return data;
        }
    }

    public class DefaultModel
    {
        //pagination
        public int TotalCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int PagerCount { get; set; }
        public string DataTimestamp { get; set; }

        public List<dynamic> Data { get; set; }
    }
}