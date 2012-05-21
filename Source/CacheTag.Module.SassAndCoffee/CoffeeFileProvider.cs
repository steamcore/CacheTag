using System.Text;
using CacheTag.Core.Filesystem;
using SassAndCoffee.Core;
using SassAndCoffee.JavaScript;
using SassAndCoffee.JavaScript.CoffeeScript;

namespace CacheTag.Module.SassAndCoffee
{
	public class CoffeeFileProvider : PhysicalFileProvider
	{
		public override byte[] ReadContent()
		{
			var fileContent = base.ReadContent();
			var compiledResult = GetCompiler().Compile(Encoding.UTF8.GetString(fileContent));
			return Encoding.UTF8.GetBytes(compiledResult);
		}

		public CoffeeFileProvider(string path)
			: base(path)
		{
		}

		private static IJavaScriptCompiler GetCompiler()
		{
			var jsRuntimeProvider = new InstanceProvider<IJavaScriptRuntime>(() => new IEJavaScriptRuntime());
			var compilerProvider = new InstanceProvider<IJavaScriptCompiler>(() => new CoffeeScriptCompiler(jsRuntimeProvider));
			return compilerProvider.GetInstance();
		}
	}
}
