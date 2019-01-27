using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VASCClientService.ServiceReference1;
using EqUiWebUi.Areas.VASC.Models;

namespace EqUiWebUi.Areas.VASC.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClientController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        GADATAEntitiesVASC db = new GADATAEntitiesVASC();
        VASCDll.VASCDll connectionToVASC = null;

        public void connect(string ServiceName)
        {
            c_service_setup session = null;
            try
            {
                session = db.c_service_setup.Where(c => c.name == "SESSION_NAME").First();
            }
            catch(Exception ex)
            {
                log.Error($"session {ServiceName} not found", ex);
                throw new Exception("session not found");
            }

            string host = db.c_service_setup.Where(c => c.name == "COMPUTERNAME" && c.bit_id == session.bit_id).First().value;
            string port = db.c_service_setup.Where(c => c.name == "SERVICE_PORT" && c.bit_id == session.bit_id).First().value;

            log.Info($"Connecting to {session.value} on {host}:{port}");

            connectionToVASC = new VASCDll.VASCDll(host, Convert.ToInt16(port), session.value);
            connectionToVASC.Connect();
        }

        // GET: VASC/Client
        public ActionResult Index(string ServiceName)
        {
            if (connectionToVASC == null)
            {
                connect(ServiceName);
            }

            Dictionary<string, string> _about = connectionToVASC.About();
            foreach (KeyValuePair<string, string> info in _about)
            {
                Console.WriteLine("{0} = {1}", info.Key, info.Value);
            }
            return View();
        }

        public ActionResult GetControlerStats(string ServiceName)
        {
            if(connectionToVASC == null)
            {
                connect(ServiceName);
            }
            List<VASCControllerGeneralStats> data = connectionToVASC.ControllerGeneralStats().ToList();
            return View(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(connectionToVASC != null)
                {
                    connectionToVASC.Disconnect();
                }
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}