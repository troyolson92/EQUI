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
                                new SelectListItem { Selected = true, Text = "English (en-GB)", Value = "en-GB"}, //default
                                new SelectListItem { Selected = false, Text = "Dutch (nl-be)", Value = "nl-be"},
                                new SelectListItem { Selected = false, Text = "Swedish (??)", Value = "??"},
            }, "Value", "Text", 1);
        }
    }
}