using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace KanimLib.Converters
{
	public static class SCMLExporter
	{
		public static void Convert(KAnimPackage package, string outputPath)
		{
			if (package == null) throw new ArgumentNullException(nameof(package));
			if (!package.HasBuild) throw new ArgumentException("KAnimPackge has not build data.");
			if (!package.HasAnim) throw new ArgumentException("KAnimPackage has no anim data.");
			if (!package.HasTexture) throw new ArgumentException("KAnimPackage has no texture data.");
			if (string.IsNullOrWhiteSpace(outputPath)) throw new ArgumentNullException(nameof(outputPath));

			Directory.CreateDirectory(outputPath);

			using (MemoryStream buildStream = new MemoryStream())
			using (MemoryStream animStream = new MemoryStream())
			using (MemoryStream textureStream = new MemoryStream())
			{
				KAnimUtils.WriteBuild(buildStream, package.Build);
				buildStream.Seek(0, SeekOrigin.Begin);

				KAnimUtils.WriteAnim(animStream, package.Anim);
				animStream.Seek(0, SeekOrigin.Begin);

				package.Texture.Save(textureStream, ImageFormat.Png);
				textureStream.Seek(0, SeekOrigin.Begin);

				var reader = new kanimal.KanimReader(buildStream, animStream, textureStream);
				reader.Read();

				var writer = new kanimal.ScmlWriter(reader);
				writer.SaveToDir(outputPath);
			}
		}
	}
}
