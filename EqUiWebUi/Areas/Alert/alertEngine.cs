﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EqUiWebUi.Areas.Alert.Models;
using EQUICommunictionLib;
using Hangfire;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.IO;

namespace EqUiWebUi.Areas.Alert
{
    public class AlertEngine
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //write alert trigger configureation to hangfire
        public void ConfigureHangfireAlertWork(int? id = null)
        {
            //get alert pol config from database and configure hangfire.
            Models.GADATA_AlertModel gADATA_AlertModel = new GADATA_AlertModel();
            List<c_triggers> c_Triggers = (from triggers in gADATA_AlertModel.c_triggers.Where(c => c.id == id || !id.HasValue)
                                           select triggers).ToList();

            AlertEngine alertEngine = new AlertEngine();
            foreach (c_triggers trigger in c_Triggers)
            {
                if (trigger.enabled)
                {
                    log.Info("Adding HF alertTriggerJob for: " + trigger.id);
                    Hangfire.RecurringJob.AddOrUpdate("AT" + trigger.id + "_" + trigger.alertType, () => alertEngine.CheckForalerts(trigger.id, trigger.discription),trigger.Cron);
                }
            }

        }

        //Clear al the 'inactive' triggers from hangfire
        //if ClearAll remove everything active or not
        public void ClearHanfireAlertwork(bool ClearALL = false)
        {
            //get alert pol config from database and configure hangfire.
            Models.GADATA_AlertModel gADATA_AlertModel = new GADATA_AlertModel();
            List<c_triggers> c_Triggers = (from triggers in gADATA_AlertModel.c_triggers
                                           select triggers).ToList();

            AlertEngine alertEngine = new AlertEngine();
            foreach (c_triggers trigger in c_Triggers)
            {
                if (trigger.enabled == false || ClearALL == true)
                {
                    log.Info("Removing HF alertTriggerJob for: " + trigger.id);
                    RecurringJob.RemoveIfExists("AT" + trigger.id + "_" + trigger.alertType);
                }
            }
        }

        //Gets called by Hanfire to processAlertwork.
        [Queue("alertengine")]
        [AutomaticRetry(Attempts = 0)] //no hangfire retrys 
        public void CheckForalerts(int c_triggerID,string AlertDiscription)
        {
            //get trigger 
            GADATA_AlertModel gADATA_AlertModel = new GADATA_AlertModel();
            c_triggers trigger = (from trig in gADATA_AlertModel.c_triggers
                                  where trig.id == c_triggerID
                                  select trig).FirstOrDefault();

            //if trigger not found stop processing
            if (trigger == null)
            {
                log.Error("Did not find Alerttrigger: " + c_triggerID);
                throw new System.ArgumentException("Did not find Alerttrigger: " + c_triggerID, "Alertengine");
            }

            //if trigger not active stop processing
            if (trigger.enabled == false)
            {
                log.Debug("trigger not enabled:" + c_triggerID);
                return;
            }
            
            //check database for active alerts
            DataTable ActiveAlerts = new DataTable();
            StoComm stoComm = new StoComm();
            MaximoComm maximoComm = new MaximoComm();
            GadataComm gadataComm = new GadataComm();
            STW040Comm sTW040Comm = new STW040Comm();
            //run command against selected database.

            switch (trigger.RunAgainst)
            {
                case (int)SmsDatabases.GADATA:
                    ActiveAlerts = gadataComm.RunQueryGadata(trigger.sqlStatement,enblExeptions:true);
                    break;

                case (int)SmsDatabases.STO:
                    ActiveAlerts = stoComm.oracle_runQuery(trigger.sqlStatement); //exeptions enabled by default
                    break;

                case (int)SmsDatabases.MAXIMOrt:
                    ActiveAlerts = maximoComm.Oracle_runQuery(trigger.sqlStatement, RealtimeConn: true, enblExeptions: true);
                    break;

                case (int)SmsDatabases.MAXIMOrep:
                    ActiveAlerts = maximoComm.Oracle_runQuery(trigger.sqlStatement, RealtimeConn: false, enblExeptions: true);
                    break;

                case (int)SmsDatabases.DBI:
                    ActiveAlerts = sTW040Comm.Oracle_runQuery(trigger.sqlStatement, enblExeptions: true);
                    break;

                default:
                    log.Error("Database not defined");
                    throw new System.ArgumentException("Database not defined", "Alertengine");
            }

            //check for errors in query (no result)
            if (ActiveAlerts == null)
            {
                log.Error("NO RESULT FORM QUERY!! for: " + trigger.alertType + " ABORTING");
                throw new System.ArgumentException("NO RESULT FORM QUERY","Alertengine");
            }
            //check for no row
            log.Debug("trigger: " + trigger.alertType + " count:" + ActiveAlerts.Rows.Count);

           //handle active alert results
           foreach (DataRow ActiveAlert in ActiveAlerts.Rows)
           {

                //can not use .Field in Linq query 
                string Alertalarmobject = ActiveAlert.Field<string>("alarmobject");

                List<h_alert> h_alert = (from alerts in gADATA_AlertModel.h_alert
                                         where alerts.c_tirgger_id == c_triggerID //must be from same trigger
                                         && alerts.alarmobject == Alertalarmobject //must be same object
                                         && alerts.state != (int)alertState.COMP //alert must be active so not in these states
                                         && alerts.state != (int)alertState.VOID 
                                         && alerts.state != (int)alertState.TECHCOMP
                                         orderby alerts.id descending
                                         select alerts).ToList();

                //if more than one active we have a config issue
                if (h_alert.Count > 1)
                {
                    log.Error("More than one alert active for location");
                  //allow continue ? 
                }

               //if alert not active make  one
                if (h_alert.Count == 0)
                {
                    log.Info("New alert for: " + ActiveAlert.Field<string>("Location") + " => "+ ActiveAlert.Field<string>("info"));

                    h_alert newAlert = new h_alert
                    {
                        c_tirgger_id = c_triggerID
                    };
                    if (trigger.RunAgainstDatabase == SmsDatabases.GADATA) //for gata the locationtree an location MUSt be in the query result
                    {
                        //we already have the location tree and location
                        newAlert.locationTree = ActiveAlert.Field<string>("LocationTree");
                        newAlert.location = ActiveAlert.Field<string>("Location");
                    }
                    else if (trigger.RunAgainstDatabase == SmsDatabases.DBI) //for DBI the locationtree and location MUST be in the query result 
                    {
                        //we already have the location tree and location
                        newAlert.locationTree = ActiveAlert.Field<string>("LocationTree");
                        newAlert.location = ActiveAlert.Field<string>("Location");
                    }
                    else if (trigger.RunAgainstDatabase == SmsDatabases.STO) //for STO qet the location tree from GADATA (manipulate object from ZM to ZS (Zone mode does not exist in asset list)
                    {
                        //ask gadata for location tree and location
                        string qry =
                            @"select top 1 LocationTree, Location from GADATA.EqUi.ASSETS as a 
                            where REPLACE('{0}','ZM','ZS') LIKE a.[LOCATION] + '%'";
                        DataTable result = gadataComm.RunQueryGadata(string.Format(qry, ActiveAlert.Field<string>("alarmobject")));
                        if (result.Rows.Count == 1)
                        {
                            newAlert.locationTree = result.Rows[0].Field<string>("LocationTree");
                            newAlert.location = result.Rows[0].Field<string>("Location");
                        }
                        else //handle if we don't get a response from gadata
                        {
                            log.Debug("did not get a valid location tree from gadata");
                            newAlert.locationTree = ActiveAlert.Field<string>("alarmobject");
                            newAlert.location = ActiveAlert.Field<string>("alarmobject");
                        }
                    }
                    else if(trigger.RunAgainstDatabase == SmsDatabases.MAXIMOrep && trigger.RunAgainstDatabase == SmsDatabases.MAXIMOrt) //for macimo get the location tree from GADATA (direct match on location)
                    {
                        //ask gadata for location tree and location
                        string qry =
                            @"select top 1 LocationTree, Location from GADATA.EqUi.ASSETS as a 
                            where '{0}' LIKE a.[LOCATION] + '%'";
                        DataTable result = gadataComm.RunQueryGadata(string.Format(qry, ActiveAlert.Field<string>("alarmobject")));
                        if (result.Rows.Count == 1)
                        {
                            newAlert.locationTree = result.Rows[0].Field<string>("LocationTree");
                            newAlert.location = result.Rows[0].Field<string>("Location");
                        }
                        else //handle if we don't get a response from gadata
                        {
                            log.Debug("did not get a valid location tree from gadata");
                            newAlert.locationTree = ActiveAlert.Field<string>("alarmobject");
                            newAlert.location = ActiveAlert.Field<string>("alarmobject");
                        }
                    }
                    //
                    newAlert.alarmobject = ActiveAlert.Field<string>("alarmobject");
                    newAlert.Classification = ActiveAlert.Field<string>("ClassTree");
                    newAlert.C_timestamp = ActiveAlert.Field<DateTime>("timestamp");
                    newAlert.state = trigger.initial_state;
                    newAlert.info = ActiveAlert.Field<string>("info");
                    newAlert.triggerCount = 1;
                    newAlert.lastTriggerd = ActiveAlert.Field<DateTime>("timestamp");
                    //adde badge to comment 
                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(newAlert.comments);
                    sb.AppendLine("<hr />");
                    sb.AppendLine("<div class='alert alert-danger'>");
                    sb.AppendLine("<strong>Triggerd: " + ActiveAlert.Field<DateTime>("timestamp").ToString("yyyy-MM-dd HH:mm:ss") + " Detected by server: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</strong>");
                    sb.AppendLine("<div>" + ActiveAlert.Field<string>("info") + "</div>");
                    if (trigger.isDebugmode)
                    {
                        sb.AppendLine("<div>" + ConvertDataTableToHTML(ActiveAlerts, h_alert[0].alarmobject) + "</div>");
                    }
                    sb.AppendLine("</div>");
                    newAlert.comments = sb.ToString();

                    //check if we need to send SMS
                    if (trigger.enableSMS)
                    {
                       newAlert =  HandleSms(trigger, newAlert);
                    }

                        //dont forget to ADD and SAVE! 
                        gADATA_AlertModel.h_alert.Add(newAlert);
                        gADATA_AlertModel.SaveChanges();

                }
                //Alert is already active RETRIGGER
                else
                {
                    log.Debug("Alert already active: " + ActiveAlert.Field<string>("Location"));
                    //if the active alert has a new timestamp tis should mean a new datapoint (retrigger event)

                    if (h_alert[0].lastTriggerd.ToString("yyyy-MM-dd HH:mm:ss") != ActiveAlert.Field<DateTime>("timestamp").ToString("yyyy-MM-dd HH:mm:ss"))
                    {
                        log.Debug("RETrigger: " + h_alert[0].lastTriggerd.ToString("yyyy-MM-dd HH:mm:ss") + " => " + ActiveAlert.Field<DateTime>("timestamp").ToString("yyyy-MM-dd HH:mm:ss"));
                        //added badge to comment with retrigger event
                        StringBuilder sb = new StringBuilder(); 
                        sb.AppendLine(h_alert[0].comments);
                        sb.AppendLine("<hr />");
                        sb.AppendLine("<div class='alert alert-warning'>");
                        sb.AppendLine("<strong>Retriggerd: "+ ActiveAlert.Field<DateTime>("timestamp").ToString("yyyy-MM-dd HH:mm:ss") + " Detected by server: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</strong>");
                        sb.AppendLine("<div>" + h_alert[0].lastTriggerd.ToString("yyyy-MM-dd HH:mm:ss") + " => " + ActiveAlert.Field<DateTime>("timestamp").ToString("yyyy-MM-dd HH:mm:ss") + "</div>");
                        sb.AppendLine("<div>" + ActiveAlert.Field<string>("info") + "</div>");
                        if (trigger.isDebugmode)
                        {
                            sb.AppendLine("<div>" + ConvertDataTableToHTML(ActiveAlerts, h_alert[0].alarmobject) + "</div>");
                        }
                        sb.AppendLine("</div>");
                        h_alert[0].comments = sb.ToString();

                        //set alert info to latest message
                        h_alert[0].info = ActiveAlert.Field<string>("info");
                        //update active alert with timestamp and increment trigger counter
                        h_alert[0].triggerCount += 1; //increment count
                        h_alert[0].lastTriggerd = ActiveAlert.Field<DateTime>("timestamp");

                        //check if we need to REsend SMS
                        if (trigger.enableSMS && trigger.smsOnRetrigger)
                        {        
                            h_alert[0] = HandleSms(trigger, h_alert[0]);
                        }
                        //dont forget SAVE!!
                        gADATA_AlertModel.SaveChanges();
                    }
                }
           }

           //handle auto clossing of alerts 
           if (trigger.AutoSetStateTechComp)
            {
                //get the alerts that are still open.
                List<h_alert> OpenAlerts = (from alerts in gADATA_AlertModel.h_alert
                                            where alerts.c_tirgger_id == trigger.id //from same trigger
                                            && alerts.state == (int)alertState.WGK //only auto close alerts that are in wgk
                                            //this means that if you set autoSetSateTechComp you should have an inital state of WGK else it will not work
                                            select alerts).ToList();

                //handle the alerts that are still open
                foreach (h_alert OpenAlert in OpenAlerts)
                {
                    //check aganst the active alerts and if not active anymore close it.
                    if (ActiveAlerts.AsEnumerable().Any(row => OpenAlert.alarmobject == row.Field<String>("alarmobject")))
                    {
                        log.Debug("Alert is still active must not close it <" + OpenAlert.location + ">id:" + OpenAlert.id.ToString());
                        if (trigger.isDebugmode)
                        {
                            //adde badge to comment when closses
                            StringBuilder sb = new StringBuilder();
                            //add existing 
                            sb.AppendLine(OpenAlert.comments);
                            //add break 
                            sb.AppendLine("<hr />");
                            //add new pannel
                            sb.AppendLine("<div class='alert alert-info'>");
                            sb.AppendLine("<strong>Leave open " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</strong>");
                            sb.AppendLine("</div>");
                            OpenAlert.comments = sb.ToString();
                        }
                        
                    }
                    else
                    {
                        log.Debug("Alert no longer active closing it <" + OpenAlert.location + ">id:" + OpenAlert.id.ToString());
                        //set state
                        OpenAlert.state = (int)alertState.TECHCOMP; //techcomp
                        //set last triggerd timestamp when close so we can calc downtime
                        OpenAlert.lastTriggerd = System.DateTime.Now;
                        //adde badge to comment when closses
                        StringBuilder sb = new StringBuilder();
                        //add existing 
                        sb.AppendLine(OpenAlert.comments);
                        //add break 
                        sb.AppendLine("<hr />");
                        //add new pannel
                        sb.AppendLine("<div class='alert alert-success'>");
                        sb.AppendLine("<strong>Closed by server: " + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</strong>");
                        sb.AppendLine("<div>(AutoSetStateTechComp mode)</div>");
                        sb.AppendLine("</div>");
                        OpenAlert.comments = sb.ToString();;
                    }

                }
                //dont forget SAVE!
                gADATA_AlertModel.SaveChanges();
            }
        }

        //handle the sms message 
        //add comments alert (the ref makes alert in out parameter
        public h_alert HandleSms(c_triggers trigger,  h_alert alert)
        {
            SmsComm smsComm = new SmsComm();
            //init comments
            StringBuilder sbComments = new StringBuilder();
            sbComments.AppendLine(alert.comments);
            sbComments.AppendLine("<hr />");
            //get all CPT600 systems linked to this trigger
            List<c_SMSconfig> SMSconfigs = trigger.c_smsSystem.c_SMSconfig.ToList();
            foreach  (c_SMSconfig SMSconfig in SMSconfigs)
            {
                //check if location is allow 
                if (alert.locationTree != null)
                {
                    if (alert.locationTree.Contains(SMSconfig.c_CPT600.LocationTree))
                    {
                        log.Debug("locationTree tree oke Can send");
                    }
                    else
                    {
                        log.Debug("locationTree NOK next");
                        continue;
                    }
                }
                
                //check if asset root is allow 
                if (alert.Classification != null)
                {
                    if (alert.Classification.Contains(SMSconfig.c_CPT600.AssetRoot))
                    {
                        log.Debug("Asset root oke Can send");
                    }
                    else
                    {
                        log.Debug("Asser root NOK next");
                        continue;
                    }
                }
                
                //check if were are not at limmit
                if(SMSconfig.c_CPT600.SMSlimit.GetValueOrDefault(10000) >= SMSconfig.c_CPT600.SMSsend.GetValueOrDefault(0))
                {
                    log.Debug("Sms limit oke Can Send");
                    SMSconfig.c_CPT600.SMSsend += 1;
                }
                else
                {
                    log.Debug("Sms limit NOK can not Send");
                    continue;
                }

                //check if were are in right ploeg 
                if(SMSconfig.c_CPT600.ActivePloeg != null)
                {
                    //not implemented
                }
                
                //check if ware are in right time slot
                if (SMSconfig.c_CPT600.StartTime.HasValue && SMSconfig.c_CPT600.EndTime.HasValue)
                {
                    //not implemented
                }

                //build SMS message 
                StringBuilder sbSMS = new StringBuilder();
                sbSMS.Append("Alert: ").AppendLine(alert.location + " ");
                sbSMS.AppendLine(trigger.alertType + " " );
                sbSMS.AppendLine(alert.info + " " );
                //USE HANGFIRE to send it!
                
                Hangfire.BackgroundJob.Enqueue(() => smsComm.SendSMS(SMSconfig.c_CPT600.System, sbSMS.ToString()));
                //Add in the comment section that the sms was send. (a badge for each SMS)
                sbComments.AppendLine("<div class='alert alert-info'>");
                sbComments.AppendLine("<strong>SMS send!: " + SMSconfig.c_CPT600.System + "</strong>");
                sbComments.AppendLine(sbSMS.ToString());
                sbComments.AppendLine("</div>");
                alert.comments = sbComments.ToString();
            }
            return alert;
        }


        //to convert DT to html table for debugging)
        public static string ConvertDataTableToHTML(DataTable dt, string alarmobject)
        {
            //first filter the active alters based on the alarm object 
            DataTable tblFiltered = dt.AsEnumerable()
           .Where(row => row.Field<String>("alarmobject") == alarmobject)
           .CopyToDataTable();
          return  string.Join(Environment.NewLine, tblFiltered.Rows.OfType<DataRow>().Select(x => string.Join(" ; ", x.ItemArray)));
        }
    }
}

