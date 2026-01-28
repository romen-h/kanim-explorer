using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

using KanimLib.KanimModel;
using KanimLib.Serialization;

using Microsoft.Extensions.Logging;

namespace KanimLib.Converters
{
	public static class SCMLExporter
	{
		private static readonly ILogger s_log = Logging.Factory.CreateLogger("SCMLExporter");

		public static void Convert(KanimPackage kanim, string outputPath)
		{
			ArgumentNullException.ThrowIfNull(kanim);
			ArgumentNullException.ThrowIfNull(outputPath);
			if (kanim.Texture == null) throw new ArgumentException("No texture is loaded.");
			if (kanim.Build == null) throw new ArgumentException("No build data is loaded.");

			if (!kanim.HasAnim)
			{
				kanim.SetAnim(KAnimUtils.CreateEmptyAnim());
			}

			Directory.CreateDirectory(outputPath);

			using MemoryStream buildStream = new MemoryStream();
			using MemoryStream animStream = new MemoryStream();
			using MemoryStream textureStream = new MemoryStream();
			
			KanimWriter.WriteBuild(buildStream, kanim.Build);
			buildStream.Seek(0, SeekOrigin.Begin);

			KanimWriter.WriteAnim(animStream, kanim.Anim);
			animStream.Seek(0, SeekOrigin.Begin);

			kanim.Texture.Save(textureStream, ImageFormat.Png);
			textureStream.Seek(0, SeekOrigin.Begin);

			var reader = new kanimal.KanimReader(buildStream, animStream, textureStream);
			reader.Read();

			var writer = new kanimal.ScmlWriter(reader);
			writer.SaveToDir(outputPath);
		}
	}
}
