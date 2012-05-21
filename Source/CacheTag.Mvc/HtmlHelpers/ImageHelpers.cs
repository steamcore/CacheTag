using System.Web;
using System.Web.Mvc;
using CacheTag.Core.Extensions;
using CacheTag.Core.Resources.Images;
using CacheTag.Mvc.Extensions;

namespace CacheTag.Mvc.HtmlHelpers
{
	public static class ImageHelpers
	{
		public static IHtmlString RenderImage(this HtmlHelper helper, string image)
		{
			return new ImageList(image).Compile().RenderMvcHtml();
		}

		public static IHtmlString RenderImage(this HtmlHelper helper, string image, string alternateText)
		{
			return new ImageList(new [] { new ImageFile(image, alternateText) }).Compile().RenderMvcHtml();
		}
	}
}
