using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Novelist.Web
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			if (routes == null)
			{
				throw new ArgumentNullException("routes");
			}

			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			// register declarative routing
			routes.MapMvcAttributeRoutes();

			//routes.MapRoute(
			//	name: "Default",
			//	url: "{controller}/{action}/{id}",
			//	defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
			//);
		}
	}
}
