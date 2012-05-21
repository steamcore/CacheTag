using CacheTag.Core.Filesystem;

namespace CacheTag.Core.Resources.Scripts
{
	public class ScriptFile : PlainTextFileResource<IScriptResource>, IScriptResource
	{
		public ScriptFile(string path)
			: base(path, "application/x-javascript")
		{
		}

		public ScriptFile(string path, IFileProvider fileProviderProvider)
			: base(path, "application/x-javascript", fileProviderProvider)
		{
		}
	}
}
