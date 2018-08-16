﻿using System.Web;
using System.Web.Optimization;

namespace EqUiWebUi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //loads before content
            bundles.Add(new ScriptBundle("~/bundles/mainjavabundel").Include(
                         "~/Scripts/jquery-{version}.js",
                         "~/Scripts/umd/popper.min.js", //popper BEFORE bootstrap 
                         "~/Scripts/bootstrap.min.js", //bootstrap
                         "~/Scripts/respond.js",
                         "~/Scripts/jquery.bootstrap-autohidingnavbar.js", //https://github.com/istvan-ujjmeszaros/bootstrap-autohidingnavbar 
                         "~/Scripts/moment.js", //https://github.com/moment/moment/
                         "~/Scripts/spin.js", //https://github.com/fgnass/spin.js/
                         "~/Scripts/daterangepicker.js" //https://github.com/dangrossman/bootstrap-daterangepicker
                            ));

            //loads afther content
            bundles.Add(new ScriptBundle("~/bundles/MyScripts").Include(
                         "~/Scripts/MyScripts/tablehelper.js",
                         "~/Scripts/MyScripts/Interface.js",
                         "~/Scripts/MvcGrid/mvc-grid.js", //https://github.com/NonFactors/MVC5.Grid
                         "~/Scripts/printThis.js", //https://github.com/jasonday/printThis
                         "~/Scripts/jquery.toaster.js" //https://github.com/scottoffen/jquery.toaster/wiki
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            //loads before content
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

             
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Bootstrap.css",
                      "~/Content/mvc-grid.css", //style for mvc grids
                      "~/Content/daterangepicker.css", //style for datarange pickers
                      "~/Content/fontawesome-all.css",  //for or icons https://fontawesome.com
                      "~/Content/CustomStyleTweeks.css", //custom style tweeks
                      "~/Content/TableRowStyles.css", //custom table row sytles and animations
                      "~/Content/CustomHyperlinkBoxes.css" //custom CustomHyperlinkBoxes
                      ));
        }
    }
}
