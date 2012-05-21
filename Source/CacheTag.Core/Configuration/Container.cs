using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CacheTag.Core.Cache;
using CacheTag.Core.Resources;
using CacheTag.Core.Resources.Html;
using CacheTag.Core.Resources.Images;
using CacheTag.Core.Resources.Scripts;
using CacheTag.Core.Resources.Styles;

namespace CacheTag.Core.Configuration
{
	public static class Container
	{
		private static readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

		public static void Register<T>(T instance)
		{
			Services[typeof(T)] = instance;
		}

		public static T Resolve<T>()
		{
			if (!Services.ContainsKey(typeof(T)))
				throw new ArgumentException("Unregistered service " + typeof(T).FullName);

			return (T)Services[typeof(T)];
		}

		public static void Initialize()
		{
			RegisterBaseComponents();
			RegisterImageComponents();
			RegisterScriptComponents();
			RegisterStyleComponents();
		}

		private static void RegisterBaseComponents()
		{
			Register<ICacheProvider>(new HttpRuntimeCacheProvider());
			Register<IResourceStorage>(new StaticResourceStorage());
		}

		private static void RegisterImageComponents()
		{
			Register<IResourceCompiler<IImageResource>>(new PassThroughCompiler<IImageResource>());
			Register<IHtmlRenderer<IImageResource>>(new ImageHtmlRenderer());
		}

		private static void RegisterScriptComponents()
		{
			var factory = new ResourceFactory<IScriptResource>();
			factory.RegisterType(new Regex(@"\.js$"), x => new ScriptFile(x));

			Register<IResourceCompiler<IScriptResource>>(new StringBuilderCompiler<IScriptResource>(snippet => new ScriptSnippet(snippet)));
			Register<IResourceFactory<IScriptResource>>(factory);
			Register<IHtmlRenderer<IScriptResource>>(new ScriptHtmlRenderer());
		}

		private static void RegisterStyleComponents()
		{
			var factory = new ResourceFactory<IStyleResource>();
			factory.RegisterType(new Regex(@"\.css$"), x => new StyleFile(x));

			Register<IResourceCompiler<IStyleResource>>(new StringBuilderCompiler<IStyleResource>(snippet => new StyleSnippet(snippet)));
			Register<IResourceFactory<IStyleResource>>(factory);
			Register<IHtmlRenderer<IStyleResource>>(new StyleHtmlRenderer());
		}
	}
}
