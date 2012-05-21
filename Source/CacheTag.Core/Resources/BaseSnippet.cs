using System.Text;
using System.Web.Caching;
using CacheTag.Core.Configuration;
using CacheTag.Core.Extensions;

namespace CacheTag.Core.Resources
{
	public abstract class BaseSnippet : IPlainTextResource
	{
		public byte[] BinaryContent { get { return Encoding.UTF8.GetBytes(Content); } }
		public CacheDependency CacheDependency { get { return null; } }
		public string Content { get; private set; }
		public string Hash { get; private set; }
		public abstract string MimeType { get; }
		public string Name { get { return string.Empty; } }
		public abstract string Url { get; }

		protected BaseSnippet(string snippet)
		{
			Content = snippet;
			Hash = Settings.HashAlgorithm.ComputeStringHash(snippet);
		}
	}
}
