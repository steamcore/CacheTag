using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CacheTag.Core.Extensions;
using CacheTag.Core.Resources;

namespace CacheTag.Mvc.Extensions
{
	public static class ResourceExtensions
	{
		public static IHtmlString RenderMvcHtml<T>(this IEnumerable<T> items)
			where T : IResource
		{
			return new MvcHtmlString(items.RenderHtml());
		}

		public static IHtmlString RenderMvcHtmlInline<T>(this IEnumerable<T> items)
			where T : IPlainTextResource
		{
			return new MvcHtmlString(items.RenderHtmlInline());
		}
	}
}
