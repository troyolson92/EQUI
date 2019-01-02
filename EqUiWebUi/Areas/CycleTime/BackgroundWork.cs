using Hangfire;
using Hangfire.Console;
using Hangfire.Server;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

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
                //RecurringJob.AddOrUpdate("CycleTime", () => backgroundwork.GetP4CycleTime(null), Cron.Hourly);
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
            System.DateTime fromDate = System.DateTime.Now.AddDays(-11);
            System.DateTime toDate = System.DateTime.Now.AddDays(-10);
            const string Format = "yyyyMMddHHmmss0000";
            int bodyType = 151110; //random body type
            int ignoreBelow = 20;
            context.WriteLine($"Get GetP4CycleTime from:{fromDate} to:{toDate} bodyType:{bodyType} ignoreBelow:{ignoreBelow}");
            //
            var client = new RestClient("http://gensvw2107.gen.volvocars.net/CMACTAPP/api/");
            var request = new RestRequest("GetCyclesP4/GetOverView?fromDate={fromDate}&toDate={toDate}&bodyType={bodyType}&ignoreBelow={ignoreBelow}", Method.GET);
            request.AddParameter("fromDate", fromDate.ToString(Format), ParameterType.UrlSegment);
            request.AddParameter("toDate", toDate.ToString(Format), ParameterType.UrlSegment);
            request.AddParameter("bodyType", bodyType.ToString(), ParameterType.UrlSegment);
            request.AddParameter("ignoreBelow", ignoreBelow.ToString(), ParameterType.UrlSegment);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            var queryResult = client.Execute(request);
            //
            context.WriteLine(queryResult.Content);
        }
    }

    public class OverviewKPIP4
    {
        public string Linenumber { get; set; }
        public string Linename { get; set; }
        public List<OverViewStationP4> Stations { get; set; }
        public OverviewKPIP4()
        {
            Stations = new List<OverViewStationP4>();
        }
    }
    public class OverViewStationP4
    {
        public string IMZONE { get; set; } //Mode zone
        public string STATION { get; set; } //Station
        public string Area { get; set; } // Area name given
        public string Line { get; set; }
        public double LCT { get; set; }

        public double PCTL_05 { get; set; } //not used external
        public double PCTL_05_sec { get; set; } //T05 seconds
        public double PCTL_50 { get; set; } //not used external
        public double PCTL_50_sec { get; set; } //T50 seconds
        public double PCTL_90 { get; set; } //not used external
        public double PCTL_90_sec { get; set; } //T90 seconds

        public double PCTL_95 { get; set; }//not used external
        public double PCTL_95_sec { get; set; } //T95 seconds

        public double Weeklyaverage { get; set; } //Not used external
        public double Weeklyaverage_sec { get; set; } //Weekly avg seconds
        public int WeeklyCTCount { get; set; } // counts bodies, initially was for weekly intended, but should work for given period as well.

    }


}