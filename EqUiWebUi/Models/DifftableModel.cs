using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EqUiWebUi.Models
{
    public class DiffQuery
    {
        public string name { get; set; }
        public int id { get; set; }
        public DateTime timestamp { get; set; }

    }

    public class ListDiffQuerysViewModel
    {
        public List<DiffQuery> DiffQuerys { get; set; }
        //control
        public int SelectedId { get; set; }
    }


}