using System.Web.Caching;

namespace CacheTag.Core.Resources
{
	public interface IResource
	{
		byte[] BinaryContent { get; }
		CacheDependency CacheDependency { get; }
		string Hash { get; }
		string MimeType { get; }
		string Name { get; }
		string Url { get; }
	}
}
