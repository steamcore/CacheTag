using System;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Threading;
using System.Web;
using dotless.Core;
using dotless.Core.Input;

namespace CacheTag.Module.DotLess
{
	public class DotlessParser : IPathResolver
	{
		private readonly FileReader fileReader;

		public DotlessParser()
		{
			fileReader = new FileReader(this);
		}

		public ParseResult ParseFile(string appRelativePath)
		{
			var physicalPath = GetFullPath(appRelativePath);

			using (new CurrentDirectoryWrapper(Path.GetDirectoryName(physicalPath)))
			{
				var engine = new LessEngine();

				return
					new ParseResult
						{
							// Note, the order here is important
							Content = engine.TransformToCss(fileReader.GetFileContents(appRelativePath), appRelativePath),
							FileDependencies = new[] { physicalPath }.Concat(engine.GetImports().Select(x => GetImportPath(appRelativePath, x)))
						};
			}
		}

		public string GetFullPath(string path)
		{
			return HttpContext.Current.Server.MapPath(path);
		}

		private string GetImportPath(string originalPath, string file)
		{
			return GetFullPath(Path.Combine(Path.GetDirectoryName(originalPath), file));
		}

		private class CurrentDirectoryWrapper : IDisposable
		{
			private readonly string originalPath;
			private readonly InterprocessMutex mutex;

			public CurrentDirectoryWrapper(string path)
			{
				mutex = new InterprocessMutex();
				originalPath = Environment.CurrentDirectory;
				Environment.CurrentDirectory = path;
			}

			public void Dispose()
			{
				Environment.CurrentDirectory = originalPath;
				mutex.Dispose();
			}
		}

		private class InterprocessMutex : IDisposable
		{
			private const string MutexName = @"Global\6C7EF913-E3ED-4B82-B643-008F93F0435B";
			private static readonly Mutex MutexInstance = CreateOrOpenMutex();

			public InterprocessMutex()
			{
				MutexInstance.WaitOne();
			}

			public void Dispose()
			{
				MutexInstance.ReleaseMutex();
			}

			private static Mutex CreateOrOpenMutex()
			{
				try
				{
					return Mutex.OpenExisting(MutexName, MutexRights.FullControl);
				}
				catch (WaitHandleCannotBeOpenedException)
				{
					bool createdNew; // Uninteresting in this case, but required by the constructor
					var mutexSecurity = new MutexSecurity();
					mutexSecurity.AddAccessRule(new MutexAccessRule("Everyone", MutexRights.FullControl, AccessControlType.Allow));
					return new Mutex(false, MutexName, out createdNew, mutexSecurity);
				}
			}
		}
	}
}
