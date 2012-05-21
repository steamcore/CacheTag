using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CacheTag.Core.Resources.Scripts;

namespace CacheTag.Core.Resources
{
	public class StringBuilderCompiler<T> : IResourceCompiler<T>
		where T : IPlainTextResource
	{
		private readonly Func<string, T> createSnippet;

		public StringBuilderCompiler(Func<string, T> createSnippet)
		{
			this.createSnippet = createSnippet;
		}

		public IEnumerable<T> Compile(IEnumerable<T> resources)
		{
			var list = resources.ToArray();

			if (!list.Any())
				return Enumerable.Empty<T>();

			if (list.Count() == 1)
				return list;

			var builder = new StringBuilder();

			foreach (var resource in list)
			{
				builder.Append(resource.Content);
				builder.AppendLine(resource is IScriptResource ? ";" : string.Empty);
			}

			return new [] { createSnippet(builder.ToString()) };
		}
	}
}
