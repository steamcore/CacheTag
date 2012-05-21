using CacheTag.Core.Resources.Scripts;

namespace CacheTag.Module.SassAndCoffee
{
	public class CoffeeScript : ScriptFile
	{
		public CoffeeScript(string path)
			: base(path, new CoffeeFileProvider(path))
		{
		}
	}
}
