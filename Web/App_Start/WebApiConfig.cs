using System.Web.Http;
using Newtonsoft.Json.Serialization;

namespace Novelist.Web
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config)
		{
			// Web API configuration and services

			// register declarative Web API routes
			config.MapHttpAttributeRoutes();

			//config.Routes.MapHttpRoute(
			//	name: "DefaultApi",
			//	routeTemplate: "api/{controller}/{id}",
			//	defaults: new { id = RouteParameter.Optional }
			//);

			// make sure that C# property names using PascalCasing get converted to Javascript camelCasing
			GlobalConfiguration
				.Configuration
				.Formatters
				.JsonFormatter
				.SerializerSettings
				.ContractResolver = new CamelCasePropertyNamesContractResolver();
		}
	}
}
