﻿using Hangfire.Console;
using Hangfire.Server;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;


namespace EqUiWebUi.Areas.Alert
{

    /* Used to send SMS message via IT.
     * Now I hyjac the sattline system and post to p1x_Supervis1GA (administrator system)
     * path is special \\p1x_supervis1GA\D$
     * 
     * Messages must go in Send folder in right format
     * 
     * Filename = XPR001-<unieke naam>.txt
     * Contenct = <CPT600Name>;<message>
     * 
     * Config of system names and clientnumber list to be configured in CPT600
     * 
     * Sample message => EQUI_TEST => 3069
     * !!BE aware that phone must be activated for send / recive SMS messages
     * 
     */
    public class SmsComm
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        string publishHost = @"\\p1x_supervis1GA";
        string publishLocation = @"\\p1x_supervis1GA\D$\Send";
        string publishUser = "sattline";
        string publishPass = "sattline";

        bool debugMode = false; //if debug mode nu sms is send 

        //EnqueueSMS in hangfire (returns jobid)
        public string EnqueueSMS(string Messagetype, string Message)
        {
           return Hangfire.BackgroundJob.Enqueue(() => SendSMS(Messagetype, Message,null));
        }

        //SEND sms 
        public void SendSMS(string Messagetype, string Message, PerformContext context)
        {
            context.WriteLine("SendingSMS");
            string filenamePrefix = "XPR001-EQUI_";
            string filenameId = System.DateTime.Now.ToString("yyyyMMddHHmmssFFF");
            var path = System.Web.Hosting.HostingEnvironment.MapPath(string.Format("~/App_Data/{0}.txt", filenamePrefix + Messagetype + filenameId));
            //check message max length

            //make file
            try
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(path))
                {
                    file.WriteLine(Messagetype + ";" + Message);
                }
                context.WriteLine("File build: " + path);
            }
            catch (Exception ex)
            {
                context.WriteLine("Failed to build SMS: " + ex.Message);
                log.Error("Failed to build SMS", ex);
                throw;
            }

            //debug mode
            if(debugMode)
            {
                context.WriteLine("DEBUG MODE ACTIVE DID NOT SEND SMS");
                log.Info("Sms debugmode active SMS NOT SEND");
                return;
            }

            //map drive 
            try
                {
                    var credentials = new NetworkCredential(publishUser, publishPass);
                    NetworkConnection networkConnection = new NetworkConnection(publishHost, credentials);
                    context.WriteLine("logged in on network using incode credentials");
                    log.Debug("logged in on network using incode credentials");
                }
                catch(Exception ex)
                {
                   context.WriteLine("Error connecting to server " + ex.Message);
                   log.Error("Error connecting to server", ex);
                    //continue might get lucky
                }

            //publish file
            try
            {
                string publishFullname = Path.Combine(publishLocation, filenamePrefix + filenameId + ".txt");
                File.Copy(path, publishFullname,true);
                context.WriteLine("file copied: " + filenamePrefix);
                //if succes delete file out of data
                try
                {
                    File.Delete(path);
                    context.WriteLine("Deleted from appdata");
                }
                catch(Exception ex)
                {
                    context.WriteLine("Failed to delete file " + ex.Message);
                    log.Error("Failed to delete file", ex);
                }
            }
            catch (Exception ex)
            {
                context.WriteLine("Failed to send SMS to server " + ex.Message);
                log.Error("Failed to send SMS to server", ex);
                throw;
            }
            context.WriteLine("Done (succes)");
            log.Info("SMS send Type: " + Messagetype + " => " + Message);
        }
    }
}
