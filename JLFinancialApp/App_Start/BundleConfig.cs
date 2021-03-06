﻿using System.Web;
using System.Web.Optimization;

namespace JLFinancialApp
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/lib").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap-4.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/subscriptionForm").Include(
                        "~/Scripts/Custom/subscriptionFormValidation.js",
                        "~/Scripts/Custom/alert.js"));

            bundles.Add(new ScriptBundle("~/bundles/recurringAmountList").Include(
                        "~/Scripts/Custom/recurringAmountList.js",
                        "~/Scripts/Custom/alert.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap-materia.css",
                      "~/Content/site.css"));
        }
    }
}
