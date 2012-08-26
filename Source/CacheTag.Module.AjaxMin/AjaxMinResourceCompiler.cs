using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Caching;
using CacheTag.Core.Cache;
using CacheTag.Core.Configuration;
using CacheTag.Core.Extensions;
using CacheTag.Core.Resources;
using CacheTag.Core.Resources.Scripts;
using CacheTag.Core.Resources.Styles;
using Microsoft.Ajax.Utilities;

namespace CacheTag.Module.AjaxMin
{
	public class AjaxMinResourceCompiler : IResourceCompiler<IScriptResource>, IResourceCompiler<IStyleResource>
	{
		private readonly CodeSettings codeSettings;
		private readonly CssSettings cssSettings;
		private readonly Minifier minifier = new Minifier();
		private readonly StringBuilderCompiler<IScriptResource> internalScriptCompiler = new StringBuilderCompiler<IScriptResource>(snippet => new ScriptSnippet(snippet));
		private readonly StringBuilderCompiler<IStyleResource> internalStyleCompiler = new StringBuilderCompiler<IStyleResource>(snippet => new StyleSnippet(snippet));

		public AjaxMinResourceCompiler()
		{
			codeSettings = new CodeSettings();
			cssSettings = new CssSettings();
		}

		public AjaxMinResourceCompiler(CodeSettings codeSettings, CssSettings cssSettings)
		{
			// https://github.com/andrewdavey/cassette/issues/157
			this.codeSettings = codeSettings ?? new CodeSettings { KillSwitch = 0x40000000000 };
			this.cssSettings = cssSettings ?? new CssSettings();
		}

		public IEnumerable<IScriptResource> Compile(IEnumerable<IScriptResource> resources)
		{
			if (!resources.Any())
				return resources;

			return Compile(
				resources,
				items =>
					{
						var combinedContent = internalScriptCompiler.Compile(items).First().Content;
						var minifiedContent = minifier.MinifyJavaScript(combinedContent, codeSettings);
						return new ScriptSnippet(minifiedContent);
					});
		}

		public IEnumerable<IStyleResource> Compile(IEnumerable<IStyleResource> resources)
		{
			if (!resources.Any())
				return resources;

			return Compile(
				resources,
				items =>
					{
						var combinedContent = internalStyleCompiler.Compile(items).First().Content;
						var minifiedContent = minifier.MinifyStyleSheet(combinedContent, cssSettings);
						return new StyleSnippet(minifiedContent);
					});
		}

		private static IEnumerable<T> Compile<T>(IEnumerable<T> resources, Func<IEnumerable<T>,T> compile)
			where T : class, IResource
		{
			var cacheProvider = Container.Resolve<ICacheProvider>();

			var items = resources.ToArray();
			var cacheKey = BuildCacheKey(items);
			var cachedItem = cacheProvider.Get<T>(cacheKey);

			if (cachedItem != null)
				return new [] { cachedItem };

			var compiledResource = compile(items);

			cacheProvider.Put(cacheKey, compiledResource, BuildCacheDependency(items), null);

			return new [] { compiledResource };
		}

		private static string BuildCacheKey<T>(IEnumerable<T> resources)
			where T : IResource
		{
			var sb = new StringBuilder();
			foreach (var res in resources)
			{
				sb.AppendLine(res.Name);
			}
			using (var hashAlgorithm = HashAlgorithm.Create(CacheTagSettings.HashAlgorithm))
			{
				return "cachetag_" + hashAlgorithm.ComputeStringHash(sb.ToString());
			}
		}

		private static CacheDependency BuildCacheDependency<T>(IEnumerable<T> resources)
			where T : IResource
		{
			var aggregateDependency = new AggregateCacheDependency();

			foreach (var resource in resources)
			{
				var dependency = resource.CacheDependency;
				if (dependency != null)
				{
					aggregateDependency.Add(resource.CacheDependency);
				}
			}

			return aggregateDependency;
		}
	}
}
