using System;
using System.Web.Caching;

namespace CacheTag.Core.Cache
{
	public interface ICacheProvider
	{
		T Get<T>(string key) where T : class;

		void Put<T>(string key, T value, CacheDependency cacheDependency, DateTime? absoluteExpiration) where T : class;
	}
}