using System.Web;
using System.Web.Optimization;

namespace Store.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {


            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            //admin js bundle
            bundles.Add(new ScriptBundle("~/bundles/admin_js").Include(
                "~/resources/admin/vendor/jquery/jquery.js",
                "~/resources/admin/vendor/bootstrap/js/bootstrap.js",
                "~/resources/admin/js/sb-admin-2.js",
                "~/resources/admin/vendor/metisMenu/metisMenu.js",
                "~/resources/admin/libs/ckfinder/ckfinder.js",
                "~/resources/admin/libs/ckeditor/ckeditor.js",
                "~/resources/admin/libs/bootbox.min.js"));

            //admin css bundle
            bundles.Add(new StyleBundle("~/bundle/admin_css").Include(
                    "~/resources/admin/vendor/bootstrap/css/bootstrap.css",
                    "~/resources/admin/vendor/metisMenu/metisMenu.css",
                    "~/resources/admin/css/sb-admin-2.css",
                    "~/resources/admin/css/custom-admin.css",
                    "~/resources/admin/vendor/font-awesome/css/font-awesome.css",
                    "~/resources/admin/vendor/datatables/css/dataTables.bootstrap.css"));
        }
    }
}
