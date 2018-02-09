
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EqUiWebUi.Areas.Alert.Models;
using EQUICommunictionLib;
using Hangfire;
using System.Text;

namespace EqUiWebUi.Areas.Alert
{
    public class AlertEngine
    {
        [AutomaticRetry(Attempts = 0)]
        [DisableConcurrentExecution(50)] //locks the job from starting multible times if other one stil running.
        public void ConfigureHangfireAlertWork()
        {
            //get alert pol config from database and configure hangfire.
            Models.GADATA_AlertModel gADATA_AlertModel = new GADATA_AlertModel();
            List<c_triggers> c_Triggers = (from triggers in gADATA_AlertModel.c_triggers
                                           select triggers).ToList();

            AlertEngine alertEngine = new AlertEngine();
            foreach (c_triggers trigger in c_Triggers)
            {
                RecurringJob.AddOrUpdate(() => alertEngine.CheckForalerts(trigger.id,trigger.discription), Cron.MinuteInterval(trigger.Pollrate));
            }

        }


        public void CheckForalerts(int c_triggerID,string AlertDiscription)
        {
            //get trigger 
            Models.GADATA_AlertModel gADATA_AlertModel = new GADATA_AlertModel();
            c_triggers trigger = (from trig in gADATA_AlertModel.c_triggers
                                  where trig.id == c_triggerID
                                  select trig).FirstOrDefault();

            if (trigger == null)
            {
                Log.Error("Did not find Alerttrigger: " + c_triggerID);
                return;
            }

            if (trigger.enabled == false)
            {
                Log.Debug("trigger not enabled:" + c_triggerID);
                return;
            }
            
            //check database for active alerts
            DataTable ActiveAlerts = new DataTable();
            GadataComm gadataComm = new GadataComm();
            ActiveAlerts = gadataComm.RunQueryGadata(trigger.sqlStqStatement);

            //check if there is work
            if (ActiveAlerts.Rows.Count == 0)
            {
                Log.Info("no work");
                return;
            }

           //handle results
           foreach (DataRow ActiveAlert in ActiveAlerts.Rows)
                {
                
                    List<h_alert> h_alert = (from alerts in gADATA_AlertModel.h_alert
                                             where alerts.c_tirgger_id == c_triggerID
                                             //&& alerts.location == ActiveAlert.Field<string>("LocationTree")
                                             && alerts.closeTimestamp != null
                                             select alerts).ToList();

                    //if more than one active we have a config issue
                    if (h_alert.Count > 1)
                    {
                        Log.Error("More than one alert active for location");
                    //allow continue ? 
                    }

                    //if alert not active make new one
                    if (h_alert.Count == 0)
                    {
                        h_alert newAlert = new h_alert();
                        newAlert.c_tirgger_id = c_triggerID;
                        newAlert.location = ActiveAlert.Field<string>("LocationTree");
                        newAlert.C_timestamp = System.DateTime.Now;
                        newAlert.state = trigger.initial_state;
                        newAlert.info = ActiveAlert.Field<string>("info");
                        newAlert.triggerCount = 1;
                        newAlert.lastTriggerd = System.DateTime.Now;

                        //dont forget to ADD and SAVE! 
                        gADATA_AlertModel.h_alert.Add(newAlert);
                        gADATA_AlertModel.SaveChangesAsync();

                    //check if we need to send SMS
                    if (trigger.smsSystem.HasValue)
                    {
                        HandleSms(trigger, newAlert);
                    }

                    }
                    else
                    {
                        Log.Debug("Alert trigger but already active");
                        //update active alert with timestamp and increment trigger counter
                        h_alert[0].triggerCount += 1; //increment count
                        h_alert[0].lastTriggerd = System.DateTime.Now;

                    //check if we need to REsend SMS
                        if (trigger.smsOnRetrigger && trigger.smsSystem.HasValue)
                        {
                            
                            HandleSms(trigger, h_alert[0]);
                        }
                    //dont forget SAVE!!
                    gADATA_AlertModel.SaveChangesAsync();
                }
                }
        }


        public void HandleSms(c_triggers trigger, h_alert alert)
        {
            //check if we NEED to send an SMS! 


            SmsComm smsComm = new SmsComm();
            var path = ("~/App_Data/tempSmsFile.txt");
            //build message 
            StringBuilder sb = new StringBuilder();
            sb.Append("Alert from: ").AppendLine(alert.location);
            sb.AppendLine(trigger.alertType);
            sb.AppendLine(alert.info);

            //USE HANGFIRE !
            smsComm.SendSMS(trigger.c_smsSystem.system, sb.ToString(), path);
        }

    }
}

