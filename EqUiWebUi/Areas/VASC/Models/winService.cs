using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public partial class winService
    {
        public string ServiceName { get; set; }
        public string DisplayName { get; set; }
        public string Status { get; set; }
        public string StartName { get; set; }
        public string Description { get; set; }
    }
}