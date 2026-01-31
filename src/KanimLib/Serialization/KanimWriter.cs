using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KanimLib.KanimModel;

using Microsoft.Extensions.Logging;

namespace KanimLib.Serialization
{
	public static class KanimWriter
	{
		private static readonly ILogger s_log = Logging.Factory.CreateLogger("KanimWriter");

#if false
		public static bool WriteBuild(string buildFile, TextureAtlas build)
		{
			try
			{
				using (FileStream file = new FileStream(buildFile, FileMode.Create))
				{
					return WriteBuild(file, build);
				}
			}
			catch
			{
				return false;
			}
		}

		public static bool WriteBuild(Stream stream, TextureAtlas build)
		{
			try
			{
				BinaryWriter writer = new BinaryWriter(stream);
				{
					writer.Write(Encoding.ASCII.GetBytes(TextureAtlas.BUILD_HEADER));

					writer.Write(build.Version);
					writer.Write(build.SymbolCount);
					writer.Write(build.FrameCount);
					writer.WriteKString(build.Name);

					for (int s = 0; s < build.SymbolCount; s++)
					{
						Symbol symbol = build.Symbols[s];

						writer.Write(symbol.Hash);
						if (build.Version > 9) writer.Write(symbol.Path);
						writer.Write(symbol.Color);
						writer.Write(symbol.Flags);
						writer.Write(symbol.FrameCount);

						for (int f = 0; f < symbol.FrameCount; f++)
						{
							Sprite frame = symbol.Sprites[f];

							writer.Write(frame.Index);
							writer.Write(frame.Duration);
							writer.Write(frame.ImageIndex);
							writer.Write(frame.PivotX);
							writer.Write(frame.PivotY);
							writer.Write(frame.PivotWidth);
							writer.Write(frame.PivotHeight);
							writer.Write(frame.UV_X1);
							writer.Write(frame.UV_Y1);
							writer.Write(frame.UV_X2);
							writer.Write(frame.UV_Y2);
						}
					}

					int numHashes = build.SymbolNames.Count;
					writer.Write(numHashes);
					foreach (KeyValuePair<int, string> kvp in build.SymbolNames)
					{
						writer.Write(kvp.Key);
						writer.WriteKString(kvp.Value);
					}

					writer.Flush();
				}

				return true;
			}
			catch
			{
				return false;
			}
		}
#endif

		public static void WriteAnim(string animFile, KAnim anim)
		{
			using FileStream file = new FileStream(animFile, FileMode.Create);
			WriteAnim(file, anim);
		}

		public static void WriteAnim(Stream stream, KAnim anim)
		{
			using BinaryWriter writer = new BinaryWriter(stream);
			
			writer.Write(Encoding.ASCII.GetBytes(KAnim.ANIM_HEADER));

			writer.Write(anim.Version);
			writer.Write(anim.FrameCount);
			writer.Write(anim.ElementCount);
			writer.Write(anim.BankCount);

			for (int b = 0; b < anim.BankCount; b++)
			{
				KAnimBank bank = anim.Banks[b];

				writer.WriteKString(bank.Name);
				writer.Write(bank.Hash);
				writer.Write(bank.Rate);
				writer.Write(bank.FrameCount);

				for (int f = 0; f < bank.FrameCount; f++)
				{
					KAnimFrame frame = bank.Frames[f];

					writer.Write(frame.X);
					writer.Write(frame.Y);
					writer.Write(frame.Width);
					writer.Write(frame.Height);
					writer.Write(frame.ElementCount);

					for (int e = 0; e < frame.ElementCount; e++)
					{
						KAnimElement element = frame.Elements[e];

						writer.Write(element.SymbolHash);
						writer.Write(element.FrameNumber);
						writer.Write(element.FolderHash);
						writer.Write(element.Flags);
						writer.Write(element.Alpha);
						writer.Write(element.Blue);
						writer.Write(element.Green);
						writer.Write(element.Red);
						writer.Write(element.M00);
						writer.Write(element.M10);
						writer.Write(element.M01);
						writer.Write(element.M11);
						writer.Write(element.M02);
						writer.Write(element.M12);
						writer.Write(element.Unused);
					}
				}
			}

			writer.Write(anim.MaxVisSymbols);

			int numHashes = anim.SymbolNames.Count;
			writer.Write(numHashes);
			foreach (KeyValuePair<int, string> kvp in anim.SymbolNames)
			{
				writer.Write(kvp.Key);
				writer.WriteKString(kvp.Value);
			}

			writer.Flush();
		}
	}
}
