using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.Maximo_ui.Models
{
    public class Maximo
    {
    }

    public class WorkorderSelectOptions
    {
        public string location { get; set; } //can be ; list !IF only one you can use wildcard
        public string locancestor { get; set; } //can be ; list !IF only one you can use wildcard
        public bool b_ciblings { get; set; } 
        public bool b_preventive { get; set; } // if set not worktype ('PP','PCI','WSCH')
        public string jpnum { get; set; } //taakplan (blank = all)
        public string worktype { get; set; } //worktypes (blank = all)
        public string wonum { get; set; } //wonum (blank = all)
        public string status { get; set; }
        public string ownergroup { get; set; } //OWNERGROUP (blank = all)
        public DateTime startdate { get; set; } //point to start looking 
        public DateTime enddate { get; set; } //point to stop looking 
        public bool realtimeConn { get; set; } //run on realtime maximo
    }


    //testing with direct maximo to gird.mvc
    public partial class Workorder
    {
        public string WONUM { get; set; }
        public string STATUS { get; set; }
        public Nullable<System.DateTime> STATUSDATE { get; set; }
        public string WORKTYPE { get; set; }
        public string DESCRIPTION { get; set; }
        public string LOCATION { get; set; }
        public string REPORTEDBY { get; set; }
        public Nullable<System.DateTime> REPORTDATE { get; set; }
        public string ANCESTOR { get; set; }
        public string OWNERGROUP { get; set; }
    }

    //partial class for labor 
    public partial class Labor
    {
        public string DisplayName { get; set; }
        public string Craft { get; set; }
        public string Supervisor{ get; set; }
        public DateTime EnterDate { get; set; }  
        public decimal REGULARHRS { get; set; }
    }
}