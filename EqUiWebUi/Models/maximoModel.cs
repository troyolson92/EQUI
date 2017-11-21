using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Models
{
    public class maximoModel
    {

    }
    public class MaximoWorkorder
    {
        public string sWoNum { get; set; }
        public string sWoDetails { get; set; }
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
