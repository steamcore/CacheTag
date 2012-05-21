using System;
using System.Collections.Generic;
using System.Linq;
using CacheTag.Core;
using CacheTag.Core.Configuration;

[assembly: WebActivator.PreApplicationStartMethod(typeof(Startup), "PreApplicationStart")]
[assembly: WebActivator.PostApplicationStartMethod(typeof(Startup), "PostApplicationStart")]

namespace CacheTag.Core
{
	public static class Startup
	{
		private static readonly Type ModuleInterfaceType = typeof(ICacheTagModule);

		private static List<ICacheTagModule> modules;

		public static void PreApplicationStart()
		{
			Container.Initialize();

			modules =
				AppDomain.CurrentDomain.GetAssemblies()
					.SelectMany(x => x.GetTypes())
					.Where(x => ModuleInterfaceType.IsAssignableFrom(x) && x.IsClass)
					.Select(moduleType => (ICacheTagModule) Activator.CreateInstance(moduleType))
					.ToList();

			modules.ForEach(module => module.PreApplicationStart());
		}

		public static void PostApplicationStart()
		{
			modules.ForEach(module => module.PostApplicationStart());
			modules = null;
		}
	}
}
