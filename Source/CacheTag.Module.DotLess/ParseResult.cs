using System.Collections.Generic;

namespace CacheTag.Module.DotLess
{
	public class ParseResult
	{
		public string Content { get; set; }
		public IEnumerable<string> FileDependencies { get; set; }
	}
}
