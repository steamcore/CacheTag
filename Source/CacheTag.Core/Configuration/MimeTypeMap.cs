using System;
using System.Collections.Generic;
using System.Linq;

namespace CacheTag.Core.Configuration
{
	public static class FileExtensions
	{
		public static string GetMimeType(string extension)
		{
			switch (extension.ToLowerInvariant())
			{
				case ".jpg":
				case ".jpeg":
					return "image/jpeg";

				case ".gif":
					return "image/gif";

				case ".png":
					return "image/png";

				default:
					throw new ArgumentException("Unknown file extension");
			}
		}
	}

	public static class MimeTypes
	{
		public static string GetFileExtension(string mimeType)
		{
			switch (mimeType.ToLowerInvariant())
			{
				case "application/x-javascript":
				case "application/javascript":
				case "application/ecmascript":
				case "text/javascript":
				case "text/ecmascript":
					return ".js";

				case "text/css":
					return ".css";

				case "image/jpeg":
					return ".jpg";

				case "image/gif":
					return ".gif";

				case "image/png":
					return ".png";

				default:
					throw new ArgumentException("Unknown mime type");
			}
		}
	}
}
