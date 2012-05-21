using System.Web.Mvc;
using System.Web.UI;
using CacheTag.Core.Configuration;
using CacheTag.Core.Resources;

namespace CacheTag.Mvc
{
	public class CacheTagController : Controller
	{
		[OutputCache(Location = OutputCacheLocation.Any, VaryByParam = "*", Duration = 90 * 24 * 60 * 60)]
		public FileContentResult Resource(string id)
		{
			var resource = Container.Resolve<IResourceStorage>().Retrieve(id);

			return File(resource.BinaryContent, resource.MimeType);
		}
	}
}
