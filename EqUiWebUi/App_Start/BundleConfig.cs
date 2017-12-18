using System.Web;
using System.Web.Optimization;

namespace EqUiWebUi
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/spin.js" //https://github.com/fgnass/spin.js/
                            ));

            bundles.Add(new ScriptBundle("~/bundles/MvcGrid").Include(
                         "~/Scripts/MvcGrid/mvc-grid.js", //https://github.com/NonFactors/MVC5.Grid
                         "~/Scripts/jquery-3.2.1.min.js", //why do we load this here again ? 
                         "~/Scripts/jquery.bootstrap-autohidingnavbar.js", //https://github.com/istvan-ujjmeszaros/bootstrap-autohidingnavbar 
                         "~/Scripts/printThis.js" //https://github.com/jasonday/printThis
                        ));

            bundles.Add(new ScriptBundle("~/bundles/MyScripts").Include(
             "~/Scripts/MyScripts/tablehelper.js"
            ));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/mvc-grid.css" //style for mvc grids
                      ));
        }
    }
}
