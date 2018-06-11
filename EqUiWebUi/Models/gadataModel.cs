using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Models
{
    public class gadataModel
    {
    }

    public partial class LogInfo
    {
        public string location { get; set; }
        public string errornum { get; set; }
        public int refid { get; set; }
        public string logtype { get; set; }
        public string logtext { get; set; }
        public string logDetails { get; set; }
    }
}
