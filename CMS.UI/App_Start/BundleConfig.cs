using System.Web;
using System.Web.Optimization;

namespace CMS.UI
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundle for angular
            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                          "~/Scripts/angular.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/site.css"));



            //main style 
            var cssBundle = new StyleBundle("~/Content/bundle/css").Include(
                "~/Content/assets/plugins/simplebar/css/simplebar.css",
                "~/Content/assets/css/bootstrap.min.css",
                "~/Content/assets/css/animate.css",
                "~/Content/assets/css/icons.css",
                "~/Content/assets/css/sidebar-menu.css",
                 "~/Content/assets/css/toastr.css",
                "~/Content/assets/css/app-style.css",
                "~/Content/assets/plugins/select2/css/select2.min.css",
                "~/Content/assets/plugins/jquery-multi-select/multi-select.css",
                "~/Content/assets/plugins/bootstrap-datepicker/css/bootstrap-datepicker.min.css"
                );
            cssBundle.Transforms.Clear();

            var jsBundle = new ScriptBundle("~/Content/bundle/js").Include(
                "~/Content/assets/js/jquery.min.js",
                "~/Content/assets/js/popper.min.js",
                "~/Content/assets/js/bootstrap.min.js",
                "~/Content/assets/plugins/simplebar/js/simplebar.js",
                "~/Content/assets/js/sidebar-menu.js",
                "~/Content/assets/js/app-script.js",
                "~/Content/assets/js/toastr.min.js",
                "~/Content/assets/js/AppUtil.js",
                "~/Content/assets/plugins/select2/js/select2.min.js",
                "~/Content/assets/plugins/jquery-multi-select/jquery.multi-select.js",
                "~/Content/assets/plugins/bootstrap-datepicker/js/bootstrap-datepicker.min.js"
                );
            jsBundle.Transforms.Clear();

            bundles.Add(cssBundle);
            bundles.Add(jsBundle);
        }
    }
}
