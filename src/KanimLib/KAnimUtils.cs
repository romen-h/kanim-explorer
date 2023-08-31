using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KanimLib
{
	public class KAnimUtils
	{

		const string ANIM_HEADER = "ANIM";

		public static KAnimPackage OpenPackage(string textureFile, string buildFile, string animFile)
		{
			KAnimPackage pkg = new KAnimPackage();

			if (File.Exists(textureFile))
			{
				pkg.Texture = new System.Drawing.Bitmap(textureFile);
			}

			if (File.Exists(buildFile))
			{
				pkg.Build = ReadBuild(buildFile);
			}

			if (File.Exists(animFile))
			{
				pkg.Anim = ReadAnim(animFile);
			}

			return pkg;
		}

		public static KBuild ReadBuild(string buildFile)
		{
			if (!File.Exists(buildFile)) throw new ArgumentException("The given file does not exist.");

			using (FileStream file = new FileStream(buildFile, FileMode.Open))
			using (BinaryReader reader = new BinaryReader(file))
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
					symbol.Path = (build.Version > 9 ? reader.ReadInt32() : 0);
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

		public static bool WriteBuild(string buildFile, KBuild build)
		{
			try
			{
				using (FileStream file = new FileStream(buildFile, FileMode.Create))
				using (BinaryWriter writer = new BinaryWriter(file))
				{
					writer.Write(Encoding.ASCII.GetBytes(KBuild.BUILD_HEADER));

					writer.Write(build.Version);
					writer.Write(build.SymbolCount);
					writer.Write(build.FrameCount);
					writer.WriteKString(build.Name);

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
						writer.WriteKString(kvp.Value);
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

				return anim;
			}
		}

		public static KAnim CreateEmptyAnim()
		{
			KAnim anim = new KAnim();
			anim.Version = 5;
			anim.FrameCount = 0;
			anim.ElementCount = 0;
			anim.BankCount = 0;
			anim.MaxVisSymbols = 0;
			return anim;
		}

		public static bool WriteAnim(string animFile, KAnim anim)
		{
			try
			{
				using (FileStream file = new FileStream(animFile, FileMode.Create))
				using (BinaryWriter writer = new BinaryWriter(file))
				{
					writer.Write(Encoding.ASCII.GetBytes(ANIM_HEADER));

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
				}

				return true;
			}
			catch (Exception ex)
			{
				return false;
			}
		}
		
		public static void DuplicateSymbols(KAnimPackage pkg, IReadOnlyList<string> symbolsToDuplicate, IReadOnlyList<string> targetBanks, string prefix, string suffix, int zOffset, bool invisible)
		{
			if (!pkg.HasBuild) throw new ArgumentException("No build data.", nameof(pkg));
			if (!pkg.HasAnim) throw new ArgumentException("No animation data.", nameof(pkg));

			if (symbolsToDuplicate == null || symbolsToDuplicate.Count == 0) throw new ArgumentException("No symbols to duplicate.", nameof(symbolsToDuplicate));
			if (targetBanks == null || targetBanks.Count == 0) throw new ArgumentException("No target animations.", nameof(targetBanks));

			if (string.IsNullOrWhiteSpace(prefix) && string.IsNullOrWhiteSpace(suffix)) throw new ArgumentException("Prefix and Suffix are both empty.");

			if (zOffset == 0) throw new ArgumentException("zOffset must not be 0", nameof(zOffset));

			Dictionary<int, int> originalHashToDupeHash = new Dictionary<int, int>();

			foreach (string symbolName in symbolsToDuplicate)
			{
				KSymbol original = pkg.Build.GetSymbol(symbolName);
				if (original != null)
				{
					string duplicatedName = prefix + symbolName + suffix;
					KSymbol duplicated = KSymbol.Copy(original, duplicatedName);
					if (invisible)
					{
						foreach (var frame in duplicated.Frames)
						{
							frame.UV_X1 = 0;
							frame.UV_Y1 = 0;
							frame.UV_X2 = 0;
							frame.UV_Y2 = 0;
						}
					}
					pkg.Build.AddSymbol(duplicated);
					originalHashToDupeHash[original.Hash] = duplicated.Hash;
					pkg.Anim.SymbolNames[duplicated.Hash] = duplicated.Name;
				}
			}

			foreach (string targetBank in targetBanks)
			{
				KAnimBank bank = pkg.Anim.GetBank(targetBank);

				foreach (var animFrame in bank.Frames)
				{
					foreach (string symbolName in symbolsToDuplicate)
					{
						KSymbol originalSymbol = pkg.Build.GetSymbol(symbolName);
						LinkedList<KAnimElement> tempElements = new LinkedList<KAnimElement>(animFrame.Elements);

						LinkedListNode<KAnimElement> currentElement = tempElements.First;
						do
						{
							if (currentElement.Value.SymbolHash == originalSymbol.Hash)
							{
								int newHash = originalHashToDupeHash[currentElement.Value.SymbolHash];
								KAnimElement copy = KAnimElement.Copy(currentElement.Value);
								copy.SymbolHash = newHash;
								copy.FolderHash = newHash;

								int steps = Math.Abs(zOffset) - 1;
								if (zOffset > 0)
								{
									// Step forward
									LinkedListNode<KAnimElement> pos = currentElement;
									while (steps > 0)
									{
										var next = pos.Next;
										if (next == null) break;
										pos = next;
										steps--;
									}

									tempElements.AddAfter(pos, copy);
								}
								else if (zOffset < 0)
								{
									// Step backward
									LinkedListNode<KAnimElement> pos = currentElement;
									while (steps > 0)
									{
										var prev = pos.Previous;
										if (prev == null) break;
										pos = prev;
										steps--;
									}

									tempElements.AddBefore(pos, copy);
								}
							}
							currentElement = currentElement.Next;
						} while (currentElement != null);

						animFrame.Elements.Clear();
						animFrame.Elements.AddRange(tempElements);
						animFrame.ElementCount = animFrame.Elements.Count;
					}
				}
			}
		}
	}
}
