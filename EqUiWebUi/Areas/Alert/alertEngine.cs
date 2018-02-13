
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

namespace EqUiWebUi.Areas.Alert
{
    public class AlertEngine
    {

        //write alert trigger configureation to hangfire
        public void ConfigureHangfireAlertWork()
        {
            //get alert pol config from database and configure hangfire.
            Models.GADATA_AlertModel gADATA_AlertModel = new GADATA_AlertModel();
            List<c_triggers> c_Triggers = (from triggers in gADATA_AlertModel.c_triggers
                                           select triggers).ToList();

            AlertEngine alertEngine = new AlertEngine();
            foreach (c_triggers trigger in c_Triggers)
            {
                if (trigger.enabled)
                {
                    Log.Info("Adding HF alertTriggerJob for: " + trigger.id);
                    Hangfire.RecurringJob.AddOrUpdate("AlertTrigger"+trigger.id,() => alertEngine.CheckForalerts(trigger.id, trigger.discription), Cron.MinuteInterval(trigger.Pollrate));
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
                    Log.Info("Removing HF alertTriggerJob for: " + trigger.id);
                    RecurringJob.RemoveIfExists("AlertTrigger" + trigger.id);
                }
            }
        }

        //Gets called by Hanfire to processAlertwork.
        [AutomaticRetry(Attempts = 0)] //no hangfire retrys 
        public void CheckForalerts(int c_triggerID,string AlertDiscription)
        {
            //get trigger 
            Models.GADATA_AlertModel gADATA_AlertModel = new GADATA_AlertModel();
            c_triggers trigger = (from trig in gADATA_AlertModel.c_triggers
                                  where trig.id == c_triggerID
                                  select trig).FirstOrDefault();

            //if trigger not found stop processing
            if (trigger == null)
            {
                Log.Error("Did not find Alerttrigger: " + c_triggerID);
                return;
            }

            //if trigger not active stop processing
            if (trigger.enabled == false)
            {
                Log.Debug("trigger not enabled:" + c_triggerID);
                return;
            }
            
            //check database for active alerts
            DataTable ActiveAlerts = new DataTable();
            GadataComm gadataComm = new GadataComm();

            ActiveAlerts = gadataComm.RunQueryGadata(trigger.sqlStqStatement);

           //handle active alert results
           foreach (DataRow ActiveAlert in ActiveAlerts.Rows)
           {

                //can not use .Field in Linq query 
                string AlertLocation = ActiveAlert.Field<string>("LocationTree");

                List<h_alert> h_alert = (from alerts in gADATA_AlertModel.h_alert
                                         where alerts.c_tirgger_id == c_triggerID //must be from same trigger
                                         && alerts.location == AlertLocation //must be same location
                                         //need to have an ID to compare for retrigger...
                                         && alerts.state < 2 //1 = WGK 2 = INUITV ALERT MUST BE ACTIVE
                                         orderby alerts.id descending
                                         select alerts).ToList();

                //if more than one active we have a config issue
                if (h_alert.Count > 1)
                {
                  Log.Error("More than one alert active for location");
                  //allow continue ? 
                }

               //if alert not active make  one
                if (h_alert.Count == 0)
                {
                        Log.Info("New alert for: " + ActiveAlert.Field<string>("LocationTree") + " => "+ ActiveAlert.Field<string>("info"));

                        h_alert newAlert = new h_alert();
                        newAlert.c_tirgger_id = c_triggerID;
                        newAlert.location = ActiveAlert.Field<string>("LocationTree");
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
                        sb.AppendLine("<strong>Triggerd: " + ActiveAlert.Field<DateTime>("timestamp") + "</strong>");
                        sb.AppendLine(ActiveAlert.Field<string>("info"));
                        sb.AppendLine("</div>");
                        newAlert.comments = sb.ToString();

                    //check if we need to send SMS
                    if (trigger.smsSystem.HasValue)
                    {
                        Log.Info("Sms system active");
                        if (trigger.smsLimit >= trigger.smsSend.GetValueOrDefault(0))
                            {
                            Log.Info("Sending SMS");
                                //inc trigger sms counter
                                trigger.smsSend += 1;
                                newAlert =  HandleSms(trigger, newAlert);
                            }
                        else //sms limit hit
                            {
                                Log.Info("Stopped sending sms for trigger: " + trigger.id + " (limit was hit) ");
                            }
                    }

                        //dont forget to ADD and SAVE! 
                        gADATA_AlertModel.h_alert.Add(newAlert);
                        gADATA_AlertModel.SaveChanges();

                }
                //Alert is already active (update it)
                else
                {
                    Log.Debug("Alert already active");
                    //if the active alert has a new timestamp tis should mean a new datapoint (retrigger event)
                    if (h_alert[0].lastTriggerd != ActiveAlert.Field<DateTime>("timestamp"))
                    {
                        Log.Debug("Alert is retriggerd");
                        //adde badge to comment with retrigger event
                        StringBuilder sb = new StringBuilder(); 
                        sb.AppendLine(h_alert[0].comments);
                        sb.AppendLine("<hr />");
                        sb.AppendLine("<div class='alert alert-warning'>");
                        sb.AppendLine("<strong>Retriggerd: "+ ActiveAlert.Field<DateTime>("timestamp")  +"</strong>");
                        sb.AppendLine(ActiveAlert.Field<string>("info"));
                        sb.AppendLine("</div>");
                        h_alert[0].comments = sb.ToString();

                        //set alert info to latest message
                        h_alert[0].info = ActiveAlert.Field<string>("info");
                        //update active alert with timestamp and increment trigger counter
                        h_alert[0].triggerCount += 1; //increment count
                        h_alert[0].lastTriggerd = ActiveAlert.Field<DateTime>("timestamp");

                        //check if we need to REsend SMS
                        if (trigger.smsOnRetrigger && trigger.smsSystem.HasValue)
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
                                            && alerts.state == 1 //only auto close alerts that are in wgk
                                            //this means that if you set autoSetSateTechComp you should have an inital state of WGK else it will not work
                                            select alerts).ToList();

                //handle the alerts that are still open
                foreach (h_alert OpenAlert in OpenAlerts)
                {
                    //check aganst the active alerts and if not active anymore close it.
                    if (ActiveAlerts.AsEnumerable().Any(row => OpenAlert.location == row.Field<String>("LocationTree")))
                    {
                        Log.Debug("Alert is still active must not close it");
                    }
                    else
                    {
                        Log.Debug("Alert no longer active closing it");
                        //set state
                        OpenAlert.state = 5; //techcomp
                        //adde badge to comment when closses
                        StringBuilder sb = new StringBuilder();
                        //add existing 
                        sb.AppendLine(OpenAlert.comments);
                        //add break 
                        sb.AppendLine("<hr />");
                        //add new pannel
                        sb.AppendLine("<div class='alert alert-success'>");
                        sb.AppendLine("<strong>Clossed by server: " + System.DateTime.Now + "</strong>(AutoSetStateTechComp mode)");
                        sb.AppendLine("</div>");
                        OpenAlert.comments = sb.ToString();
                        //dont forget SAVE!
                        gADATA_AlertModel.SaveChanges();
                    }

                }

            }
        }

        //handle the sms message 
        //add comments alert (the ref makes alert in out parameter
        public h_alert HandleSms(c_triggers trigger,  h_alert alert)
        {
            //check if we NEED to send an SMS! 
            SmsComm smsComm = new SmsComm();
            //build message 
            StringBuilder sb = new StringBuilder();
            //alert.location is the full location tree. take only the act location
            string location;
            try
            {
                 location = alert.location.Split('>')[alert.location.Split('>').Count()-1];
            }
            catch
            {
                 location = alert.location;
            }
            sb.Append("Alert from: ").AppendLine(location);
            sb.AppendLine("***********");
            sb.AppendLine(trigger.alertType);
            sb.AppendLine(alert.info);
            //USE HANGFIRE to send it!
            Hangfire.BackgroundJob.Enqueue(() => smsComm.SendSMS(trigger.c_smsSystem.system, sb.ToString()));
            //Add in the comment section that the sms was send.
            
            //adde badge to comment 
            StringBuilder sb2 = new StringBuilder();
            sb2.AppendLine(alert.comments);
            sb2.AppendLine("<hr />");
            sb2.AppendLine("<div class='alert alert-info'>");
            sb2.AppendLine("<strong>SMS send: " + trigger.c_smsSystem.system + "</strong>");
            sb2.AppendLine(sb.ToString());
            sb2.AppendLine("</div>");
            alert.comments = sb2.ToString();

            return alert;
        }

    }
}

