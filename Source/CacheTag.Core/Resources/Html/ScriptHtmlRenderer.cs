using CacheTag.Core.Configuration;
using CacheTag.Core.Resources.Scripts;

namespace CacheTag.Core.Resources.Html
{
	public class ScriptHtmlRenderer : IHtmlRenderer<IScriptResource>
	{
		public string RenderHtml(IScriptResource resource)
		{
			return string.Format(CacheTagSettings.ScriptTagFormat, resource.Url);
		}

		public string RenderInlineHtml(IScriptResource resource)
		{
			return string.Format(CacheTagSettings.InlineScriptTagFormat, resource.Content);
		}
	}
}