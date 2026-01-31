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

		public static bool Convert(TextureAtlas atlas, KAnim anim, string outputPath)
		{
			ArgumentNullException.ThrowIfNull(atlas);
			ArgumentNullException.ThrowIfNull(anim);
			ArgumentNullException.ThrowIfNullOrWhiteSpace(outputPath);
			if (atlas.Texture == null) throw new ArgumentException("No texture is loaded.");

			Directory.CreateDirectory(outputPath);

			if (anim == null)
			{
				anim = AnimFactory.CreateEmptyAnim();
			}

			using MemoryStream buildStream = new MemoryStream();
			using MemoryStream textureStream = new MemoryStream();
			using MemoryStream animStream = new MemoryStream();
			
			atlas.WriteToKleiBuildBytes(buildStream);
			buildStream.Seek(0, SeekOrigin.Begin);

			atlas.Texture.Save(textureStream, ImageFormat.Png);
			textureStream.Seek(0, SeekOrigin.Begin);

			KanimWriter.WriteAnim(animStream, anim);
			animStream.Seek(0, SeekOrigin.Begin);

			var reader = new kanimal.KanimReader(buildStream, animStream, textureStream);
			reader.Read();

			var writer = new kanimal.ScmlWriter(reader);
			writer.SaveToDir(outputPath);
			return true;
		}
	}
}
