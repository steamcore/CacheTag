using CacheTag.Core;
using CacheTag.Core.Configuration;
using CacheTag.Core.Resources;
using CacheTag.Core.Resources.Scripts;
using CacheTag.Core.Resources.Styles;

namespace CacheTag.Module.AjaxMin
{
	public class ModuleConfiguration : ICacheTagModule
	{
		public void PreApplicationStart()
		{
			var ajaxMinResourceBundler = new AjaxMinResourceCompiler();

			Container.Register<IResourceCompiler<IScriptResource>>(ajaxMinResourceBundler);
			Container.Register<IResourceCompiler<IStyleResource>>(ajaxMinResourceBundler);
		}

		public void PostApplicationStart()
		{
		}
	}
}
