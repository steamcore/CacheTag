using System.Text.RegularExpressions;
using CacheTag.Core;
using CacheTag.Core.Configuration;
using CacheTag.Core.Resources;
using CacheTag.Core.Resources.Scripts;

namespace CacheTag.Module.SassAndCoffee
{
	public class ModuleConfiguration : ICacheTagModule
	{
		public void PreApplicationStart()
		{
			Container.Resolve<IResourceFactory<IScriptResource>>().RegisterType(new Regex(@"\.coffee$"), x => new CoffeeScript(x));
		}

		public void PostApplicationStart()
		{
		}
	}
}
