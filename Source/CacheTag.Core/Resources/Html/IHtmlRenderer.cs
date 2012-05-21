namespace CacheTag.Core.Resources.Html
{
	public interface IHtmlRenderer<T>
		where T : IResource
	{
		string RenderHtml(T resource);
		string RenderInlineHtml(T resource);
	}
}
