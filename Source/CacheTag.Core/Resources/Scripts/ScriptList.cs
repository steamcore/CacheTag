using System.Collections.Generic;
using System.Linq;
using CacheTag.Core.Configuration;

namespace CacheTag.Core.Resources.Scripts
{
	public class ScriptList : List<IScriptResource>
	{
		public ScriptList()
		{
		}

		public ScriptList(string script)
		{
			Add(Container.Resolve<IResourceFactory<IScriptResource>>().Create(script));
		}

		public ScriptList(IEnumerable<string> scripts)
		{
			var factory = Container.Resolve<IResourceFactory<IScriptResource>>();
			AddRange(scripts.Select(factory.Create));
		}
	}
}
