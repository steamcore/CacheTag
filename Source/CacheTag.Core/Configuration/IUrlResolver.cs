using CacheTag.Core.Resources;

namespace CacheTag.Core.Configuration
{
	public interface IUrlResolver
	{
		string GetResourceUrl(IResource resource);
	}
}
