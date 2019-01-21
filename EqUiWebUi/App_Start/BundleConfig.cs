using System.Web;
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
                         "~/node_modules/jquery/dist/jquery.js",
                         "~/Scripts/umd/popper.min.js", //popper BEFORE bootstrap 
                         "~/Scripts/bootstrap.min.js", //bootstrap
                         "~/Scripts/respond.js",
                         "~/Scripts/jquery.bootstrap-autohidingnavbar.js", //https://github.com/istvan-ujjmeszaros/bootstrap-autohidingnavbar 
                         "~/node_modules/moment/moment.js", //https://github.com/moment/moment/
                         "~/node_modules/daterangepicker/daterangepicker.js", //https://github.com/dangrossman/bootstrap-daterangepicker
                         "~/node_modules/bootstrap-select/dist/js/bootstrap-select.js" //https://developer.snapappointments.com/bootstrap-select/
                            ));

            //loads after content
            bundles.Add(new ScriptBundle("~/bundles/MyScripts").Include(
                         "~/Scripts/MyScripts/Interface.js",

                         "~/Scripts/MvcGrid/mvc-grid.js", //https://github.com/NonFactors/MVC5.Grid

                         "~/node_modules/tablesaw/dist/tablesaw.jquery.js", //https://github.com/filamentgroup/tablesaw
                         "~/node_modules/tablesaw/dist/tablesaw-init.js",

                         "~/node_modules/print-this/printThis.js", //https://github.com/jasonday/printThis
                         "~/Scripts/jquery.toaster.js" //https://github.com/scottoffen/jquery.toaster/wiki
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.

            //loads before content
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

             
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Bootstrap.css",
                      "~/Content/MvcGrid/mvc-grid.css", //style for mvc grids
                      "~/node_modules/tablesaw/dist/tablesaw.css", //for tablesaw

                      "~/node_modules/daterangepicker/daterangepicker.css", //style for date range pickers
                      "~/Content/fontawesome-all.css",  //for or icons https://fontawesome.com

                      "~/Content/TableRowStyles.css", //custom table row styles and animations
                      "~/Content/CustomHyperlinkBoxes.css", //custom CustomHyperlinkBoxes
                      "~/Content/CustomStyleTweeks.css", //custom style tweaks
                      
                      "~/node_modules/bootstrap-select/dist/css/bootstrap-select.css"
                      ));
        }
    }
}
