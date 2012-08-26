using System.Web.Mvc;
using System.Web.Routing;

namespace CacheTag.Mvc
{
	public static class Routing
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			using (routes.GetWriteLock())
			{
				routes.Insert(
					0,
					new Route(
						CacheTagMvcSettings.RouteUrl,
						new RouteValueDictionary(new {controller = "CacheTag", action = "Resource"}),
						new MvcRouteHandler()
						)
					);

				routes.Insert(
					0,
					new Route(
						CacheTagMvcSettings.RouteUrl + ".{ext}",
						new RouteValueDictionary(new { controller = "CacheTag", action = "Resource", ext = UrlParameter.Optional }),
						new MvcRouteHandler()
						)
					);
			}
		}
	}
}
