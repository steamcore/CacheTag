using System;
using System.Text.RegularExpressions;

namespace CacheTag.Core.Resources
{
	public interface IResourceFactory<T>
		where T : IResource
	{
		T Create(string path);
		IResourceFactory<T> RegisterType(Regex matcher, Func<string, T> factory);
	}
}
