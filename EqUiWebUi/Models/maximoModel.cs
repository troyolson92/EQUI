using System;
using System.Collections.Generic;

namespace EqUiWebUi.Models
{
    public class maximoModel
    {

    }
    public class WorkordersOnLocation
    {
        public string location { get; set; }
        public string station { get; set; }
        public bool b_ciblings { get; set; }
        public bool b_preventive { get; set; }
        public List<Workorder> workorders { get; set; }
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
    }


}