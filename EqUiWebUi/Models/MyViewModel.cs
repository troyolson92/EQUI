using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Models
{
    public class MyViewModel
    {
        public int? Year { get; set; }
        public int? Month { get; set; }
        public string feedback { get; set; }

        public IEnumerable<SelectListItem> Years
        {
            get
            {
                return Enumerable.Range(2000, 12).Select(x => new SelectListItem
                {
                    Value = x.ToString(),
                    Text = x.ToString()
                });
            }
        }
    }
}