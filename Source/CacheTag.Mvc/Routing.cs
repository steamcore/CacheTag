using System.Web.Mvc;
using System.Web.Routing;
using CacheTag.Core.Configuration;

namespace CacheTag.Mvc
{
	public static class Routing
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.Insert(
				0,
				new Route(
					MvcSettings.RouteUrl,
					new RouteValueDictionary(new { controller = "CacheTag", action = "Resource" }),
					new MvcRouteHandler()
				)
			);
		}
	}
}
