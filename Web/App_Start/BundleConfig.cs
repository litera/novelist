using System;
using System.Web.Optimization;
using Novelist.Web.General;

namespace Novelist.Web
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			// clear file replacements to circumvent bug related to
			// https://aspnetoptimization.codeplex.com/workitem/132
			// which will prevent usage of existing minified files
			bundles.FileExtensionReplacementList.Clear();

			RegisterStyleBundles(bundles);
			RegisterScriptBundles(bundles);
		}

		private static void RegisterStyleBundles(BundleCollection bundles)
		{
			if (bundles == null)
			{
				throw new ArgumentNullException("bundles");
			}

			var safeUrls = new AbsoluteUrlTransform();

			bundles.Add(new StyleBundle("~/bundles/css/general")
				.Include("~/Content/Styles/Novelist.css", safeUrls));
		}

		private static void	RegisterScriptBundles(BundleCollection bundles)
		{
			if (bundles == null)
			{
				throw new ArgumentNullException("bundles");
			}

			bundles.Add(new ScriptBundle("~/bundles/js/general")
				.Include("~/Scripts/angular.js")
				.Include("~/Scripts/angular-route.js")
				.Include("~/Scripts/angular-resource.js")
				.Include("~/Scripts/angular-sanitize.js"));
		}
	}
}
