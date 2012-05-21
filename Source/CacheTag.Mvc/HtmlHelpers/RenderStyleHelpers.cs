using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CacheTag.Core.Resources.Styles;

namespace CacheTag.Mvc.HtmlHelpers
{
	public static class RenderStyleHelpers
	{
		public static IHtmlString RenderStyle(this HtmlHelper helper, string item)
		{
			return helper.RenderStyles(new[] { item });
		}

		public static IHtmlString RenderStyles(this HtmlHelper helper, IEnumerable<string> items)
		{
			return helper.Render(new StyleList(items));
		}

		public static IHtmlString RenderStyleInline(this HtmlHelper helper, string item)
		{
			return helper.RenderStylesInline(new[] { item });
		}

		public static IHtmlString RenderStylesInline(this HtmlHelper helper, IEnumerable<string> items)
		{
			return helper.RenderInline(new StyleList(items));
		}
	}
}
