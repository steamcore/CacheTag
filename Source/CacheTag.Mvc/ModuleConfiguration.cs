using CacheTag.Core;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;

namespace CacheTag.Mvc
{
	public class ModuleConfiguration : ICacheTagModule
	{
		public void PreApplicationStart()
		{
			DynamicModuleUtility.RegisterModule(typeof(HttpModule));
		}

		public void PostApplicationStart()
		{
		}
	}
}
