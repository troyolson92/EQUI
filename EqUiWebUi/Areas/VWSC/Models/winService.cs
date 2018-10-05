using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Web;
using static EqUiWebUi.Areas.VWSC.Models.VWSCenums;

namespace EqUiWebUi.Areas.VWSC.Models
{
    public partial class winService
    {
        //for c_service setup 
        public int id { get; set; }
        public Nullable<long> bit_id { get; set; }
        public string SessionName { get; set; }
        public string description { get; set; }
        public Enable_bit _Enable_bit
        {
            get
            {
                return (Enable_bit)Enum.ToObject(typeof(Enable_bit), this.bit_id.GetValueOrDefault());
            }
            set
            {
                this.bit_id = (int)value;
            }
        }
        //for win services
        public string ServiceName { get; set; }
        public string ServiceDisplayName { get; set; }
        public string ServiceStatus { get; set; }
        public string ServiceStartName { get; set; }
        public string ServiceDescription { get; set; }
    }
}