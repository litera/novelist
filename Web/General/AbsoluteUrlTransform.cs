using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Optimization;

namespace Web.General
{
	/// <summary>
	/// Provides functionality to transform relative URLs in CSS to abosulte for minification services.
	/// </summary>
	public class AbsoluteUrlTransform : IItemTransform
	{
		private static readonly Regex urlExtractor = new Regex("url\\(['\"]?(?<url>[^)]+?)['\"]?\\)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

		/// <summary>
		/// Processes specified input and changes relative to absolute URLs.
		/// </summary>
		/// <param name="includedVirtualPath">The included virtual path.</param>
		/// <param name="input">The actual CSS text as input.</param>
		/// <returns>Retruns converted string with absolute URLs.</returns>
		/// <exception cref="System.ArgumentNullException">includedVirtualPath</exception>
		public string Process(string includedVirtualPath, string input)
		{
			if (includedVirtualPath == null)
			{
				throw new ArgumentNullException("includedVirtualPath");
			}

			// no need to transform empty or missing content
			if (string.IsNullOrWhiteSpace(input))
			{
				return input;
			}

			string folder = VirtualPathUtility.GetDirectory(includedVirtualPath);

			if (!folder.EndsWith("/", StringComparison.OrdinalIgnoreCase))
			{
				folder += "/";
			}

			return urlExtractor.Replace(input, (Match m) => {
				string url = m.Groups["url"].Value;

				if (!string.IsNullOrWhiteSpace(url) &&
					!string.IsNullOrWhiteSpace(folder) &&
					!url.StartsWith("/", StringComparison.OrdinalIgnoreCase) &&
					!url.StartsWith("http", StringComparison.OrdinalIgnoreCase))
				{
					url = VirtualPathUtility.ToAbsolute(folder + url);
				}

				return string.Format(CultureInfo.InvariantCulture, @"url({0})", url);
			});
		}
	}
}