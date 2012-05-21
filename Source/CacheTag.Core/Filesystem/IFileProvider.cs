using System;
using System.Web.Caching;

namespace CacheTag.Core.Filesystem
{
	public interface IFileProvider
	{
		DateTime? AbsoluteExpiration { get; }
		string AppRelativePath { get; }
		CacheDependency CacheDependency { get; }

		byte[] ReadContent();
	}
}
