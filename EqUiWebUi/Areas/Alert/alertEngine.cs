
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using EqUiWebUi.Areas.Alert.Models;
using EQUICommunictionLib;
using Hangfire;

namespace EqUiWebUi.Areas.Alert
{
    public class AlertEngine
    {
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
                                             && alerts.location == ActiveAlert.Field<string>("Location")
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
                        newAlert.location = "";
                        newAlert.C_timestamp = System.DateTime.Now;
                        newAlert.state = trigger.initial_state;
                        newAlert.info = "";
                        newAlert.triggerCount = 1;
                        newAlert.lastTriggerd = System.DateTime.Now;

                        //dont forget SAVE! 

                        //check if we need to send SMS
                        HandleSms(trigger, newAlert);

                    }
                    else
                    {
                        Log.Debug("Alert trigger but already active");
                        //update active alert with timestamp and increment trigger counter
                        h_alert[0].triggerCount += 1; //increment count
                        h_alert[0].lastTriggerd = System.DateTime.Now;
                        //dont forget SAVE!!
                    }
                }
        }


        public void HandleSms(c_triggers trigger, h_alert alert)
        {

        }

    }
}

