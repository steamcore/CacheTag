using System.Collections.Generic;

namespace CacheTag.Core.Resources
{
	public class PassThroughCompiler<T> : IResourceCompiler<T>
		where T : IResource
	{
		public IEnumerable<T> Compile(IEnumerable<T> resources)
		{
			return resources;
		}
	}
}
