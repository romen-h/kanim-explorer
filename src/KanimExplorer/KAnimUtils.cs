using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace KanimalExplorer
{
	class KAnimUtils
	{
		const string BUILD_HEADER = "BILD";
		const string ANIM_HEADER = "ANIM";

		public static KBuild ReadBuild(string buildFile)
		{
			if (!File.Exists(buildFile)) throw new ArgumentException("The given file does not exist.");

			using (FileStream file = new FileStream(buildFile, FileMode.Open))
			using (BinaryReader reader = new BinaryReader(file))
			{
				// Verify header
				string header = Encoding.ASCII.GetString(reader.ReadBytes(BUILD_HEADER.Length));
				if (header != BUILD_HEADER) throw new Exception("Header is not valid.");

				// Parse Build, Symbols, Frames
				KBuild build = new KBuild();

				build.Version = reader.ReadInt32();
				build.SymbolCount = reader.ReadInt32();
				build.FrameCount = reader.ReadInt32();
				build.Name = reader.ReadPString();

				for (int s=0; s<build.SymbolCount; s++)
				{
					KSymbol symbol = new KSymbol(build);
					symbol.Hash = reader.ReadInt32();
					symbol.Path = (build.Version > 9 ? reader.ReadInt32() : 0);
					symbol.Color = reader.ReadColor32();
					symbol.Flags = reader.ReadKSymbolFlags();
					symbol.FrameCount = reader.ReadInt32();

					int time = 0;
					for (int f=0; f<symbol.FrameCount; f++)
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
				for (int h=0; h<numHashes; h++)
				{
					int hash = reader.ReadInt32();
					string str = reader.ReadPString();
					build.SymbolNames[hash] = str;
				}

				return build;
			}
		}

		public static bool WriteBuild(string buildFile, KBuild build)
		{
			try
			{
				using (FileStream file = new FileStream(buildFile, FileMode.Create))
				using (BinaryWriter writer = new BinaryWriter(file))
				{
					writer.Write(Encoding.ASCII.GetBytes(BUILD_HEADER));

					writer.Write(build.Version);
					writer.Write(build.SymbolCount);
					writer.Write(build.FrameCount);
					writer.WritePString(build.Name);

					for (int s = 0; s < build.SymbolCount; s++)
					{
						KSymbol symbol = build.Symbols[s];

						writer.Write(symbol.Hash);
						if (build.Version > 9) writer.Write(symbol.Path);
						writer.Write(symbol.Color);
						writer.Write(symbol.Flags);
						writer.Write(symbol.FrameCount);

						for (int f = 0; f < symbol.FrameCount; f++)
						{
							KFrame frame = symbol.Frames[f];

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
						writer.WritePString(kvp.Value);
					}
				}

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}

		public static KAnim ReadAnim(string animFile)
		{
			if (!File.Exists(animFile)) throw new ArgumentException("The given file does not exist.");

			using (FileStream file = new FileStream(animFile, FileMode.Open))
			using (BinaryReader reader = new BinaryReader(file))
			{
				// Verify header
				string header = Encoding.ASCII.GetString(reader.ReadBytes(ANIM_HEADER.Length));
				if (header != ANIM_HEADER) throw new Exception("Header is not valid.");

				// Parse Anim
				KAnim anim = new KAnim();

				anim.Version = reader.ReadInt32();
				anim.ElementCount = reader.ReadInt32();
				anim.FrameCount = reader.ReadInt32();
				anim.BankCount = reader.ReadInt32();

				for (int a=0; a<anim.BankCount; a++)
				{
					KAnimBank bank = new KAnimBank(anim);

					bank.Name = reader.ReadPString();
					bank.Hash = reader.ReadInt32();
					bank.Rate = reader.ReadSingle();
					bank.FrameCount = reader.ReadInt32();

					for (int f=0; f<bank.FrameCount; f++)
					{
						KAnimFrame frame = new KAnimFrame(bank);

						frame.X = reader.ReadSingle();
						frame.Y = reader.ReadSingle();
						frame.Width = reader.ReadSingle();
						frame.Height = reader.ReadSingle();
						frame.ElementCount = reader.ReadInt32();

						for (int e=0; e<frame.ElementCount; e++)
						{
							KAnimElement element = new KAnimElement(frame);

							element.ImageHash = reader.ReadInt32();
							element.Index = reader.ReadInt32();
							element.Layer = reader.ReadInt32();
							element.Flags = reader.ReadInt32();
							element.Alpha = reader.ReadSingle();
							element.Blue = reader.ReadSingle();
							element.Green = reader.ReadSingle();
							element.Red = reader.ReadSingle();
							element.M1 = reader.ReadSingle();
							element.M2 = reader.ReadSingle();
							element.M3 = reader.ReadSingle();
							element.M4 = reader.ReadSingle();
							element.M5 = reader.ReadSingle();
							element.M6 = reader.ReadSingle();
							element.Order = reader.ReadSingle();

							frame.Elements.Add(element);
						}

						bank.Frames.Add(frame);
					}

					anim.Banks.Add(bank);
				}

				return anim;
			}
		}

		public static void WriteAnim(string animFile, KAnim anim)
		{

		}
	}
}
