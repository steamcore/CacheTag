using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace CacheTag.Core.Resources
{
	public class ResourceFactory<T> : IResourceFactory<T>
		where T : IResource
	{
		private class InternalFactory<TFunc>
			where TFunc : IResource
		{
			public Regex Matcher { get; set; }
			public Func<string, TFunc> Create { get; set; }
		}

		private readonly List<InternalFactory<T>> factories = new List<InternalFactory<T>>();
		private readonly Dictionary<string, InternalFactory<T>> matchCache = new Dictionary<string, InternalFactory<T>>();

		public T Create(string path)
		{
			if (matchCache.ContainsKey(path))
				return matchCache[path].Create(path);

			var result = factories.FirstOrDefault(x => x.Matcher.IsMatch(path) || x.Matcher.IsMatch(path.ToLowerInvariant()));

			if (result == null)
				throw new ArgumentException("No matching resource type found for file " + path);

			matchCache[path] = result;

			return result.Create(path);
		}

		public IResourceFactory<T> RegisterType(Regex matcher, Func<string, T> factory)
		{
			factories.Insert(0, new InternalFactory<T> { Matcher = matcher, Create = factory });
			return this;
		}
	}
}
