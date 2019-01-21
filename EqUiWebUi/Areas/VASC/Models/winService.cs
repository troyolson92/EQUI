using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Web;

namespace EqUiWebUi.Areas.VASC.Models
{
    public partial class winService
    {
        //for c_service setup 
        public int id { get; set; }
        public Nullable<int> bit_id { get; set; }
        public string SessionName { get; set; }
        public string description { get; set; }
        public int[] _Enable_mask
        {
            get
            {
                return VASCenums.IntMaskToIntArray(this.bit_id, Enum.GetNames(typeof(SQL_Action)).Length);
            }
            set
            {
                this.bit_id = VASCenums.IntArrayToIntMask(value); 
            }
        }
        //for win services
        public string ServiceName { get; set; }
        public string ServiceDisplayName { get; set; }
        public string ServiceStatus { get; set; }
        public string ServiceStartName { get; set; }
        public string ServiceDescription { get; set; }
        //for controller count
        public int controllerCount { get; set; }
        public int OKcontrollerCount { get; set; }
    }
}