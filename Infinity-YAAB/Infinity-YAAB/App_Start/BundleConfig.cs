using System.Web;
using System.Web.Optimization;

namespace Infinity_YAAB
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*
             * Style bundles, mapped to a non-directory path so the routing doesn't mess up
             */
            bundles.Add(new StyleBundle("~/StyleBundles/bootstrap-core").Include(
                    "~/Content/bootstrap.css"));

            bundles.Add(new LessBundle("~/StyleBundles/site-core-less").Include(
                    "~/Content/site.less"));



            /*
             * Script bundles, mapped to a non-directory path so the routing doesn't mess up
             */ 
            bundles.Add(new ScriptBundle("~/ScriptBundles/jquery-core").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/ScriptBundles/jquery-core").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/ScriptBundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/ScriptBundles/bootstrap-core").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/ScriptBundles/InfinityFrontEnd").Include("~/Scripts/layout.js"));

            
        }
    }
}
