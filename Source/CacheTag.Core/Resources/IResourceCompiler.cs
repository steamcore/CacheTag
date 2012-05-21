using System.Collections.Generic;

namespace CacheTag.Core.Resources
{
	public interface IResourceCompiler<T> where T : IResource
	{
		IEnumerable<T> Compile(IEnumerable<T> resources);
	}
}
