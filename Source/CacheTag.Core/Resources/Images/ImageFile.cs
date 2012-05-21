using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using CacheTag.Core.Filesystem;

namespace CacheTag.Core.Resources.Images
{
	public class ImageFile : BaseFileResource<IImageResource>, IImageResource
	{
		public string AlternateText { get; private set; }
		public int Height { get { return (int)Properties["Height"]; } }
		public int Width { get { return (int)Properties["Width"]; } }

		public ImageFile(string path)
			: this(path, Path.GetFileName(path))
		{
		}

		public ImageFile(string path, string alternateText)
			: base(path, GetMimeType(path))
		{
			AlternateText = alternateText;
		}

		public ImageFile(string path, IFileProvider fileProvider)
			: base(path, GetMimeType(path), fileProvider)
		{
		}

		public ImageFile(string path, string alternateText, IFileProvider fileProvider)
			: base(path, GetMimeType(path), fileProvider)
		{
			AlternateText = alternateText;
		}

		protected override void LoadFileProperties(Dictionary<string, object> properties)
		{
			base.LoadFileProperties(properties);

			var binaryContent = (byte[])properties["BinaryContent"];

			using (var stream = new MemoryStream(binaryContent))
			{
				var bitmap = new Bitmap(stream);

				properties["Height"] = bitmap.Height;
				properties["Width"] = bitmap.Width;
			}
		}

		private static string GetMimeType(string path)
		{
			var extension = Path.GetExtension(path) ?? string.Empty;

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
					throw new ArgumentException("Unknown image type");
			}
		}
	}
}
