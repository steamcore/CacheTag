using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using CacheTag.Core.Resources.Scripts;

namespace CacheTag.Mvc.HtmlHelpers
{
	public static class RenderScriptHelpers
	{
		public static IHtmlString RenderScript(this HtmlHelper helper, string item)
		{
			return helper.RenderScripts(new[] { item });
		}

		public static IHtmlString RenderScripts(this HtmlHelper helper, IEnumerable<string> items)
		{
			return helper.Render(new ScriptList(items));
		}

		public static IHtmlString RenderScriptInline(this HtmlHelper helper, string item)
		{
			return helper.RenderScriptsInline(new[] { item });
		}

		public static IHtmlString RenderScriptsInline(this HtmlHelper helper, IEnumerable<string> items)
		{
			return helper.RenderInline(new ScriptList(items));
		}
	}
}
