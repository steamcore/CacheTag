using System.Text.RegularExpressions;
using CacheTag.Core;
using CacheTag.Core.Configuration;
using CacheTag.Core.Resources;
using CacheTag.Core.Resources.Styles;

namespace CacheTag.Module.DotLess
{
	public class ModuleConfiguration : ICacheTagModule
	{
		public void PreApplicationStart()
		{
			Container.Resolve<IResourceFactory<IStyleResource>>().RegisterType(new Regex(@"\.less$"), x => new DotlessStyle(x));
		}

		public void PostApplicationStart()
		{
		}
	}
}
