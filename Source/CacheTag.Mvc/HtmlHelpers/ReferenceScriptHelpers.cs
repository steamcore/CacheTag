using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CacheTag.Core.Resources.Scripts;

namespace CacheTag.Mvc.HtmlHelpers
{
	public static class ReferenceScriptHelpers
	{
		public static IHtmlString ReferenceScript(this HtmlHelper helper, string item)
		{
			return helper.ReferenceScripts(new[] { item });
		}

		public static IHtmlString ReferenceScripts(this HtmlHelper helper, IEnumerable<string> items)
		{
			GetReferencedItemList(helper).AddRange(items);

			return new MvcHtmlString(String.Empty);
		}

		public static IHtmlString RenderReferencedScripts(this HtmlHelper helper)
		{
			var items = GetReferencedItemList(helper);
			var result = helper.Render(new ScriptList(items.Distinct()));
			items.Clear();
			return result;
		}

		public static IHtmlString RenderReferencedScriptsInline(this HtmlHelper helper)
		{
			var items = GetReferencedItemList(helper);
			var result = helper.RenderInline(new ScriptList(items.Distinct()));
			items.Clear();
			return result;
		}

		private static List<string> GetReferencedItemList(HtmlHelper helper)
		{
			const string key = "_cachetag_referenced_scripts";

			if (helper.ViewDataContainer.ViewData[key] == null)
				helper.ViewDataContainer.ViewData[key] = new List<string>();

			return (List<string>) helper.ViewDataContainer.ViewData[key];
		}
	}
}
