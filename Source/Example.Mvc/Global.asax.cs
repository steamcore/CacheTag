using System.Web.Mvc;
using System.Web.Routing;
using CacheTag.Core.Configuration;
using CacheTag.Mvc;

namespace Example.Mvc
{
	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterGlobalFilters(GlobalFilterCollection filters)
		{
			filters.Add(new HandleErrorAttribute());
		}

		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				"Default", // Route name
				"{controller}/{action}/{id}", // URL with parameters
				new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
			);
		}

		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();

			RegisterGlobalFilters(GlobalFilters.Filters);
			RegisterRoutes(RouteTable.Routes);

			// Example configuration
			CacheTagMvcSettings.RouteUrl = "_res/{id}";
			CacheTagSettings.RuntimeMode = RuntimeMode.ForceRelease;
			//CacheTagSettings.HashFilesInDebugMode = true;
			//CacheTagSettings.HideFileExtensions = true;
		}
	}
}
