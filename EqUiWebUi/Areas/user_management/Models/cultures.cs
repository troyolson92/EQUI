using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EqUiWebUi.Areas.user_management.Models
{
    public static class cultures
    {

       public static SelectList CultureList()
        {
           return new SelectList(
            new List<SelectListItem>
            {
                                new SelectListItem { Selected = true, Text = "English (EN-GB)", Value = "EN-GB"}, //default
                                new SelectListItem { Selected = false, Text = "Dutch (NL-BE)", Value = "NL-BE"},
                                new SelectListItem { Selected = false, Text = "Swedish (SV)", Value = "SV"},
            }, "Value", "Text", 1);
        }
    }
}