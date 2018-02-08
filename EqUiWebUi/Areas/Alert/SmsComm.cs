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
        string publishHost = @"\\p1x_supervis1GA";
        string publishLocation = @"\\p1x_supervis1GA\D$\Send";
        string publishUser = "sattline";
        string publishPass = "sattline";

        public void SendSMS(string Messagetype, string Message,string temppath)
        {
            string filenamePrefix = "XPR001-EQUI_";
            string filenameId = System.DateTime.Now.ToString("yyyyMMddHHmmssFFF");


            //check message max length

            //make file
            try
            {
                using (System.IO.StreamWriter file =
                new System.IO.StreamWriter(temppath))
                {
                    file.WriteLine(Messagetype + ";" + Message);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Failed to build SMS", ex);
                return;
            }

            //map drive 
            try
                {
                    var credentials = new NetworkCredential(publishUser, publishPass);
                    NetworkConnection networkConnection = new NetworkConnection(publishHost, credentials);
                    Log.Info("logged in on network using incode credentials");
                }
                catch(Exception ex)
                {
                    Log.Error("Error connecting to server", ex);
                    //continue might get lucky
                }

            //publish file
            try
            {
                string publishFullname = Path.Combine(publishLocation, filenamePrefix + filenameId + ".txt");
                Log.Debug(publishFullname);
                Log.Debug(temppath);
                File.Copy(temppath, publishFullname,true);
            }
            catch (Exception ex)
            {
                Log.Error("Failed to send SMS to server", ex);
                return;
            }
            Log.Info("SMS send Type: " + Message);
        }
    }
}
