using CacheTag.Core.Configuration;

namespace CacheTag.Core.Resources.Scripts
{
	public class ScriptSnippet : BaseSnippet, IScriptResource
	{
		public ScriptSnippet(string snippet) : base(snippet)
		{
		}

		public override string MimeType
		{
			get { return "application/x-javascript"; }
		}

		public override string Url
		{
			get { return Container.Resolve<IUrlResolver>().GetResourceUrl(this); }
		}
	}
}
