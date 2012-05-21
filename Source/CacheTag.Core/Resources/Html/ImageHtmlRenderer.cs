using System;
using CacheTag.Core.Configuration;
using CacheTag.Core.Resources.Images;

namespace CacheTag.Core.Resources.Html
{
	public class ImageHtmlRenderer : IHtmlRenderer<IImageResource>
	{
		public string RenderHtml(IImageResource resource)
		{
			return string.Format(Settings.ImageTagFormat, resource.Url, resource.Width, resource.Height, resource.AlternateText);
		}

		public string RenderInlineHtml(IImageResource resource)
		{
			throw new InvalidOperationException("Images cannot be rendered inline");
		}
	}
}