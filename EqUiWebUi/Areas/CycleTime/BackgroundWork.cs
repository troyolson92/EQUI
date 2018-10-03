using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography.Xml;
using System.Web;

namespace EqUiWebUi.Areas.CycleTime
{
    public class BackgroundWork
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void configHangfireJobs()
        {
            if (EqUiWebUi.MyBooleanExtensions.IsAreaEnabled("CycleTime"))
            {
                BackgroundWork backgroundwork = new BackgroundWork();
                //set job to refresh every minute
                RecurringJob.AddOrUpdate("CycleTime", () => backgroundwork.GetP4CycleTime(null), Cron.Hourly);
            }
            else
            {
                log.Warn("Gadata area is disabled removing hangfire jobs");
                RecurringJob.RemoveIfExists("CycleTime");
            }
        }

        /*
        Api documentation Cycle Time VCG Brownfield
        The endpoint api for HTTP rest (Get) call http://gensvw2107.gen.volvocars.net/CMACTAPP/api/GetCyclesP4/GetOverView?fromDate={fromDate}&toDate={toDate}&model={model}&ignoreBelow={ignoreBelow}
        Please do not call this if not needed more often than once per hour for performance reason.

        Parameters:
        fromDate: yyyymmddhhMMss0000
        toDate: yyyymmddhhMMss0000
        model:
        * P4: for All P4 models together (V40/CMA)
        * P3: for All P3 models (not applicable anymore)
        * 23: XC40 only
        * 15: V40/V40CC only
        * 32: V60 è not sure yet if new V60 would have the same as old (to be verified)
        ignoreBelow: values below this number (seconds) are not taken into account for T calculation

        Example api call for P4 http://gensvw2107.gen.volvocars.net/CMACTAPP/api/GetCyclesP4/GetOverView?fromDate=201809170001010000&toDate=201809232359010000&model=P4&ignoreBelow=20
         * */
        public static class carmodels
        {
            public static String P4 { get { return "P4"; } }
            public static String XC40 { get { return "23"; } }
            public static String V40_V40CC { get { return "15"; } }
            public static String V60 { get { return "32"; } }
        }

        [Queue("debug")]
        [AutomaticRetry(Attempts = 0)]
        public void GetP4CycleTime(PerformContext context)
        {
            System.DateTime fromDate = System.DateTime.Now.AddDays(-10);
            System.DateTime toDate = System.DateTime.Now;
            string model = carmodels.XC40;
            int ignoreBelow = 20;
            context.WriteLine($"Get GetP4CycleTime from:{fromDate} to:{toDate} model:{model} ignoreBelow:{ignoreBelow}");
            //build api string
            string basepath = "http://gensvw2107.gen.volvocars.net/CMACTAPP/api/GetCyclesP4/GetOverView";
            NameValueCollection queryString = System.Web.HttpUtility.ParseQueryString(string.Empty);
            queryString["fromDate"] = fromDate.ToString("yyyymmddhhMMss0000");
            queryString["fromDate"] = fromDate.ToString("yyyymmddhhMMss0000");
            queryString["toDate"] = toDate.ToString();
            queryString["model"] = model;
            queryString["ignoreBelow"] = ignoreBelow.ToString();
            context.WriteLine(basepath + queryString.ToString());
            //make call
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(basepath);
            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
            // List data response.
            HttpResponseMessage response = client.GetAsync(queryString.ToString()).Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<DataObject>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects)
                {
                 //   Console.WriteLine("{0}", d.Name);
                }
            }
            else
            {
                log.Error("CycleTime API call failed");
                Console.WriteLine($"error in api call {(int)response.StatusCode} ({response.ReasonPhrase})");
            }
            client.Dispose();


            //handle data

        }
    }
}