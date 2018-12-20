using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Areas.Welding.Models
{
    public class UltralogData
    {
    }

    public class UltralogControlPicture
    {
        public int Id { get; set; }
        public string Picture { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<PicturePoint> picturePoints { get; set; }
    }

    public class PicturePoint
    {
        public int Id { get; set; }
        public int PlanPointID { get; set; }
        public int Xpos { get; set; }
        public int Ypos { get; set; }
    }
}