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
	public static class KanimReader
	{
		private static readonly ILogger s_log = Logging.Factory.CreateLogger("KanimReader");

		public static KBuild ReadBuild(string buildFile)
		{
			if (!File.Exists(buildFile)) throw new ArgumentException("The given file does not exist.");

			using (FileStream file = new FileStream(buildFile, FileMode.Open))
			{
				return ReadBuild(file);
			}
		}

		public static KBuild ReadBuild(Stream stream)
		{
			using (BinaryReader reader = new BinaryReader(stream))
			{
				// Verify header
				string header = Encoding.ASCII.GetString(reader.ReadBytes(KBuild.BUILD_HEADER.Length));
				if (header != KBuild.BUILD_HEADER) throw new Exception("Header is not valid.");

				// Parse Build, Symbols, Frames
				KBuild build = new KBuild();

				build.Version = reader.ReadInt32();
				build.SymbolCount = reader.ReadInt32();
				build.FrameCount = reader.ReadInt32();
				build.Name = reader.ReadKString();

				for (int s = 0; s < build.SymbolCount; s++)
				{
					KSymbol symbol = new KSymbol(build);
					symbol.Hash = reader.ReadInt32();
					symbol.Path = build.Version > 9 ? reader.ReadInt32() : 0;
					symbol.Color = reader.ReadColor32();
					symbol.Flags = reader.ReadKSymbolFlags();
					symbol.FrameCount = reader.ReadInt32();

					int time = 0;
					for (int f = 0; f < symbol.FrameCount; f++)
					{
						KFrame frame = new KFrame(symbol);
						frame.Index = reader.ReadInt32();
						frame.Duration = reader.ReadInt32();
						frame.ImageIndex = reader.ReadInt32();
						frame.PivotX = reader.ReadSingle();
						frame.PivotY = reader.ReadSingle();
						frame.PivotWidth = reader.ReadSingle();
						frame.PivotHeight = reader.ReadSingle();
						frame.UV_X1 = reader.ReadSingle();
						frame.UV_Y1 = reader.ReadSingle();
						frame.UV_X2 = reader.ReadSingle();
						frame.UV_Y2 = reader.ReadSingle();
						frame.Time = time;

						time += frame.Duration;
						symbol.Frames.Add(frame);
					}

					build.Symbols.Add(symbol);
				}

				// Read Symbol Hashes
				int numHashes = reader.ReadInt32();
				for (int h = 0; h < numHashes; h++)
				{
					int hash = reader.ReadInt32();
					string str = reader.ReadKString();
					build.SymbolNames[hash] = str;
				}

				return build;
			}
		}

		public static KAnim ReadAnim(string animFile)
		{
			if (!File.Exists(animFile)) throw new ArgumentException("The given file does not exist.");

			using (FileStream file = new FileStream(animFile, FileMode.Open))
			{
				return ReadAnim(file);
			}
		}

		public static KAnim ReadAnim(Stream stream)
		{
			using (BinaryReader reader = new BinaryReader(stream))
			{
				// Verify header
				string header = Encoding.ASCII.GetString(reader.ReadBytes(KAnim.ANIM_HEADER.Length));
				if (header != KAnim.ANIM_HEADER) throw new Exception("Header is not valid.");

				// Parse Anim
				KAnim anim = new KAnim();

				anim.Version = reader.ReadInt32();
				anim.FrameCount = reader.ReadInt32();
				anim.ElementCount = reader.ReadInt32();
				anim.BankCount = reader.ReadInt32();

				for (int a = 0; a < anim.BankCount; a++)
				{
					KAnimBank bank = new KAnimBank(anim);

					bank.Name = reader.ReadKString();
					bank.Hash = reader.ReadInt32();
					bank.Rate = reader.ReadSingle();
					bank.FrameCount = reader.ReadInt32();

					for (int f = 0; f < bank.FrameCount; f++)
					{
						KAnimFrame frame = new KAnimFrame(bank);

						frame.X = reader.ReadSingle();
						frame.Y = reader.ReadSingle();
						frame.Width = reader.ReadSingle();
						frame.Height = reader.ReadSingle();
						frame.ElementCount = reader.ReadInt32();

						for (int e = 0; e < frame.ElementCount; e++)
						{
							KAnimElement element = new KAnimElement(frame);

							element.SymbolHash = reader.ReadInt32();
							element.FrameNumber = reader.ReadInt32();
							element.FolderHash = reader.ReadInt32();
							element.Flags = reader.ReadInt32();
							element.Alpha = reader.ReadSingle();
							element.Blue = reader.ReadSingle();
							element.Green = reader.ReadSingle();
							element.Red = reader.ReadSingle();
							element.M00 = reader.ReadSingle();
							element.M10 = reader.ReadSingle();
							element.M01 = reader.ReadSingle();
							element.M11 = reader.ReadSingle();
							element.M02 = reader.ReadSingle();
							element.M12 = reader.ReadSingle();
							element.Unused = reader.ReadSingle();

							frame.Elements.Add(element);
						}

						bank.Frames.Add(frame);
					}

					anim.Banks.Add(bank);
				}

				anim.MaxVisSymbols = reader.ReadInt32();

				// Read Anim Hashes
				int numHashes = reader.ReadInt32();
				for (int h = 0; h < numHashes; h++)
				{
					int hash = reader.ReadInt32();
					string str = reader.ReadKString();
					anim.SymbolNames[hash] = str;
				}
				
				anim.EnsureUILast();

				return anim;
			}
		}
	}
}
