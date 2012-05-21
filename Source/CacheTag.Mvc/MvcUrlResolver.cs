using CacheTag.Core.Configuration;
using CacheTag.Core.Resources;

namespace CacheTag.Mvc
{
	public class MvcUrlResolver : IUrlResolver
	{
		public string GetResourceUrl(IResource resource)
		{
			return "/" + MvcSettings.RouteUrl.Replace("{id}", resource.Hash);
		}
	}
}
