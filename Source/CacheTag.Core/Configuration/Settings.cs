using System.Security.Cryptography;
using System.Web;

namespace CacheTag.Core.Configuration
{
	public static class Settings
	{
		public const string AssemblyVersion = "1.0.*";

		// ReSharper disable FieldCanBeMadeReadOnly.Global
		// ReSharper disable ConvertToConstant.Global

		public static HashAlgorithm HashAlgorithm = MD5.Create();
		public static RuntimeMode RuntimeMode = RuntimeMode.Dynamic;

		public static string ImageTagFormat = @"<img src=""{0}"" width=""{1}"" height=""{2}"" alt=""{3}"" />";
		public static string ScriptTagFormat = @"<script src=""{0}"" type=""text/javascript""></script>";
		public static string StyleTagFormat = @"<link rel=""stylesheet"" type=""text/css"" href=""{0}"" />";
		public static string InlineScriptTagFormat = @"<script type=""text/javascript"">{0}</script>";
		public static string InlineStyleTagFormat = @"<style type=""text/css"">{0}</style>";

		// ReSharper restore ConvertToConstant.Global
		// ReSharper restore FieldCanBeMadeReadOnly.Global

		public static bool IsReleaseMode
		{
			get
			{
				return RuntimeMode == RuntimeMode.ForceRelease || HttpContext.Current.IsDebuggingEnabled == false;
			}
		}
	}
}
