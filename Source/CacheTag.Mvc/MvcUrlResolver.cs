using CacheTag.Core.Configuration;
using CacheTag.Core.Resources;

namespace CacheTag.Mvc
{
	public class MvcUrlResolver : IUrlResolver
	{
		public string GetResourceUrl(IResource resource)
		{
			var id = resource.Hash;

			if (CacheTagSettings.HideFileExtensions == false)
			{
				id += MimeTypes.GetFileExtension(resource.MimeType);
			}

			return "/" + CacheTagMvcSettings.RouteUrl.Replace("{id}", id);
		}
	}
}
