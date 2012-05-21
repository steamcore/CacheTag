using System.Web;
using System.Web.Routing;
using CacheTag.Core.Configuration;

namespace CacheTag.Mvc
{
	public class HttpModule : IHttpModule
	{
		public void Init(HttpApplication context)
		{
			Routing.RegisterRoutes(RouteTable.Routes);
			Container.Register<IUrlResolver>(new MvcUrlResolver());
		}

		public void Dispose()
		{
		}
	}
}
