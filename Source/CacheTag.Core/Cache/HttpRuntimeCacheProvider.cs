using System;
using System.Web;
using System.Web.Caching;

namespace CacheTag.Core.Cache
{
	public class HttpRuntimeCacheProvider : ICacheProvider
	{
		public T Get<T>(string key)
			where T : class
		{
			return HttpRuntime.Cache.Get(key) as T;
		}

		public void Put<T>(string key, T value, CacheDependency cacheDependency, DateTime? absoluteExpiration)
			where T : class
		{
			HttpRuntime.Cache.Add(
				key,
				value,
				cacheDependency,
				absoluteExpiration ?? System.Web.Caching.Cache.NoAbsoluteExpiration,
				System.Web.Caching.Cache.NoSlidingExpiration,
				CacheItemPriority.Normal,
				null);
		}
	}
}