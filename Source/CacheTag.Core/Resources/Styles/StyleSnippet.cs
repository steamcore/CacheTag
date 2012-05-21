using CacheTag.Core.Configuration;

namespace CacheTag.Core.Resources.Styles
{
	public class StyleSnippet : BaseSnippet, IStyleResource
	{
		public StyleSnippet(string snippet) : base(snippet)
		{
		}

		public override string MimeType
		{
			get { return "text/css"; }
		}

		public override string Url
		{
			get { return Container.Resolve<IUrlResolver>().GetResourceUrl(this); }
		}
	}
}
