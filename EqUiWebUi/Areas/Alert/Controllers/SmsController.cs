using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.Alert.Controllers
{
    public class SmsController : Controller
    {
        // GET: Alert/Sms
        public ActionResult Index()
        {
            return View();
        }

        // GET: SendTest SMS
        public ActionResult SendSMS(string system = "EQUI_TEST", string message = "this is a test message")
        {
            var path =  Server.MapPath("~/App_Data/tempSmsFile.txt");
            SmsComm smsComm = new SmsComm();
            //USE HANGFIRE !
            smsComm.SendSMS(system, message, path);
            return View();
        }
    }
}