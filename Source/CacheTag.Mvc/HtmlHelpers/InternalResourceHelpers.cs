using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CacheTag.Core.Extensions;
using CacheTag.Core.Resources;
using CacheTag.Core.Resources.Scripts;
using CacheTag.Core.Resources.Styles;
using CacheTag.Mvc.Extensions;

namespace CacheTag.Mvc.HtmlHelpers
{
	internal static class InternalResourceHelpers
	{
		public static IHtmlString Render<T>(this HtmlHelper helper, IStyleResource item)
			where T : IResource
		{
			return helper.Render(new[] { item });
		}

		public static IHtmlString Render<T>(this HtmlHelper helper, IEnumerable<T> items)
			where T : IResource
		{
			return items.Compile().RenderMvcHtml();
		}

		public static IHtmlString RenderInline<T>(this HtmlHelper helper, IStyleResource item)
			where T : IResource
		{
			return helper.RenderInline(new[] { item });
		}

		public static IHtmlString RenderInline<T>(this HtmlHelper helper, IEnumerable<T> items)
			where T : IPlainTextResource
		{
			return items.Compile().RenderMvcHtmlInline();
		}
	}
}
