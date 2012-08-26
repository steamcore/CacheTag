using CacheTag.Core.Configuration;
using CacheTag.Core.Resources.Styles;

namespace CacheTag.Core.Resources.Html
{
	public class StyleHtmlRenderer : IHtmlRenderer<IStyleResource>
	{
		public string RenderHtml(IStyleResource resource)
		{
			return string.Format(CacheTagSettings.StyleTagFormat, resource.Url);
		}

		public string RenderInlineHtml(IStyleResource resource)
		{
			return string.Format(CacheTagSettings.InlineStyleTagFormat, resource.Content);
		}
	}
}