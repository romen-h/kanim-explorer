using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

using KanimLib.KanimModel;
using KanimLib.Sprites;

using Microsoft.Extensions.Logging;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KanimLib
{
	public static class KAnimUtils
	{
		private static readonly ILogger s_log = Logging.Factory.CreateLogger("KanimUtils");

		public static KAnim CreateEmptyAnim()
		{
			KAnim anim = new KAnim
			{
				Version = KAnim.CURRENT_ANIM_VERSION,
				FrameCount = 0,
				ElementCount = 0,
				BankCount = 0,
				MaxVisSymbols = 0
			};
			return anim;
		}

		public static void AutoFlagSymbols(KanimPackage pkg)
		{
			if (pkg == null) throw new ArgumentNullException(nameof(pkg));
			if (pkg.Build == null) throw new InvalidOperationException("No build data is loaded.");
			
			using (s_log.BeginFunction());
			
			foreach (var symbol in pkg.Build.Symbols)
			{
				string lowerName = symbol.Name.ToLowerInvariant();
				
				if (lowerName.Contains("_bloom"))
				{
					s_log.LogTrace($"Enabling bloom flag on {symbol.Name}.");
					symbol.Flags = symbol.Flags.SetFlag(SymbolFlags.Bloom, true);
				}

				if (lowerName.Contains("_fg"))
				{
					s_log.LogTrace($"Enabling foreground flag on {symbol.Name}.");
					symbol.Flags = symbol.Flags.SetFlag(SymbolFlags.Foreground, true);
				}
			}
		}
		
		public static void RenameSymbol(KanimPackage pkg, string oldSymbolName, string newSymbolName)
		{
			if (pkg == null) throw new ArgumentNullException(nameof(pkg));
			if (pkg.Build == null && pkg.Anim == null) throw new InvalidOperationException("No build or animation data is loaded.");
			if (oldSymbolName == null) throw new ArgumentNullException(nameof(oldSymbolName));
			if (newSymbolName == null) throw new ArgumentNullException(nameof(newSymbolName));
			
			using (s_log.BeginFunction());
			
			int oldHash = KleiUtil.HashString(oldSymbolName);
			int newHash = KleiUtil.HashString(newSymbolName);
			
			if (pkg.HasBuild)
			{
				if (pkg.Build.SymbolExists(newSymbolName)) throw new InvalidOperationException($"A symbol with the name {newSymbolName} already exists.");

				s_log.LogTrace($"Renaming {oldSymbolName} to {newSymbolName} in build.bytes...");
				KSymbol symbolToRename = pkg.Build.GetSymbol(oldSymbolName);
				
				symbolToRename.Hash = newHash;
				
				s_log.LogTrace("Replacing build hash dictionary entry...");
				pkg.Build.SymbolNames.Remove(oldHash);
				pkg.Build.SymbolNames.Add(newHash, newSymbolName);
			}
			else
			{
				s_log.LogWarning("Current kanim data does not have a build. No changes happening to build.bytes.");
			}
			
			if (pkg.HasAnim)
			{
				s_log.LogTrace($"Renaming {oldSymbolName} to {newSymbolName} in anim.bytes...");
				if (!pkg.HasBuild) s_log.LogWarning("There was no build data to validate symbol name. If the new symbol name already exists then animations could look incorrect.");
				
				foreach (var bank in pkg.Anim.Banks)
				{
					for (int i=0; i<bank.Frames.Count; i++)
					{
						var frame = bank.Frames[i];
						foreach (var element in frame.Elements)
						{
							if (element.SymbolHash != oldHash) continue;
							
							s_log.LogTrace($"Replaced {oldSymbolName} with {newSymbolName} in frame {i} of {bank.Name}");
							element.SymbolHash = newHash;
						}
					}
				}
				
				s_log.LogTrace("Replacing animations hash dictionary entry...");
				pkg.Anim.SymbolNames.Remove(oldHash);
				pkg.Anim.SymbolNames.Add(newHash, newSymbolName);
			}
			else
			{
				s_log.LogWarning("Current kanim data does not have animations. No changes happening to anim.bytes.");
			}
		}
		
		public static void DuplicateSymbols(KanimPackage pkg, IReadOnlyList<string> symbolsToDuplicate, IReadOnlyList<string> targetBanks, string prefix, string suffix, int zOffset, bool invisible)
		{
			if (pkg == null) throw new ArgumentNullException(nameof(pkg));
			if (!pkg.HasBuild) throw new InvalidOperationException("No build data is loaded.");
			if (!pkg.HasAnim) throw new InvalidOperationException("No animation data is loaded.");

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
					if (pkg.Build.SymbolExists(duplicatedName)) throw new Exception($"A symbol with the name {duplicatedName} already exists.");
						
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
					else
					{
						for (int i=0; i<duplicated.Frames.Count; i++)
						{
							var originalFrame = original.Frames[i];
							var duplicatedFrame = duplicated.Frames[i];
							duplicatedFrame.Sprite = new Sprite(duplicatedFrame, (Bitmap)originalFrame.Sprite.Image.Clone());
							pkg.AddSprite(duplicatedFrame.Sprite);
						}
					}
					pkg.Build.InsertSymbolAfter(duplicated, original);
					originalHashToDupeHash[original.Hash] = duplicated.Hash;
					pkg.Anim.SymbolNames[duplicated.Hash] = duplicated.Name;
				}
			}
			
			pkg.RebuildAtlas();

			foreach (string targetBank in targetBanks)
			{
				KAnimBank bank = pkg.Anim.GetBank(targetBank);

				foreach (var animFrame in bank.Frames)
				{
					if (animFrame.ElementCount == 0) continue;
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
		
		public static void DeleteSymbol(KanimPackage pkg, string symbolName, bool inBuild, bool inAnims)
		{
			ArgumentNullException.ThrowIfNull(pkg);
			ArgumentNullException.ThrowIfNull(symbolName);
			if (!inBuild && !inAnims) throw new ArgumentException("inBuild and inAnims arguments are both false.");
			if (inBuild && pkg.Build == null) throw new InvalidOperationException("No build data is loaded.");
			if (inAnims && pkg.Anim == null) throw new InvalidOperationException("No anim data is loaded.");
			
			if (inBuild)
			{
				KSymbol symbolToRemove = pkg.Build.GetSymbol(symbolName);
				if (symbolToRemove == null) return; // Symbol doesn't exist anyway
				
				foreach (var frame in symbolToRemove.Frames)
				{
					if (frame.Sprite == null) continue;
					pkg.RemoveSprite(frame.Sprite);
				}
				
				pkg.Build.Symbols.Remove(symbolToRemove);
				pkg.Build.SymbolCount = pkg.Build.Symbols.Count;
				pkg.Build.SymbolNames.Remove(symbolToRemove.Hash);
				
				pkg.RebuildAtlas();
			}
			
			if (inAnims)
			{
				// TODO
			}
		}
		
		public static void ReplaceSprite(KanimPackage pkg, KFrame frame, Bitmap newSprite, bool adjustForPadding)
		{
			ArgumentNullException.ThrowIfNull(pkg);
			ArgumentNullException.ThrowIfNull(frame);
			ArgumentNullException.ThrowIfNull(newSprite);
			if (pkg.Texture == null) throw new InvalidOperationException("No texture is loaded.");
			if (pkg.Build == null) throw new InvalidOperationException("No build data is loaded.");

			Debug.Assert(frame.Sprite != null);
			
			if (adjustForPadding)
			{
				int originalWidth = frame.Sprite.Width;
				int originalHeight = frame.Sprite.Height;
				
				int dWidth = newSprite.Width - originalWidth;
				int dHeight = newSprite.Height - originalHeight;
				int xPadding = dWidth/2;
				int yPadding = dHeight/2;
				float originalPivotX = frame.SpriterPivotX * originalWidth;
				float originalPivotY = frame.SpriterPivotY * originalHeight;

				frame.Sprite.Image = newSprite;
				frame.PivotWidth = newSprite.Width * 2;
				frame.PivotHeight = newSprite.Height * 2;
				frame.SpriterPivotX = (originalPivotX + xPadding) / newSprite.Width;
				frame.SpriterPivotY = (originalPivotY + yPadding) / newSprite.Height;
			}
			else
			{
				float originalPivotX = frame.SpriterPivotX;
				float originalPivotY = frame.SpriterPivotY;
				frame.Sprite.Image = newSprite;
				frame.PivotWidth = newSprite.Width * 2;
				frame.PivotHeight = newSprite.Height * 2;
				frame.SpriterPivotX = originalPivotX;
				frame.SpriterPivotY = originalPivotY;
			}
			
			pkg.RebuildAtlas();
		}
		
		public static void SplitTextureAtlas(Bitmap texture, KBuild build, string outputDir)
		{
			if (texture == null) throw new ArgumentNullException(nameof(texture));
			if (build == null) throw new ArgumentNullException(nameof(build));
			if (string.IsNullOrWhiteSpace(outputDir)) throw new ArgumentNullException(nameof(outputDir));
			if (!Directory.Exists(outputDir)) throw new Exception("Output directory does not exist.");

			JsonObject json = new JsonObject();

			List<Sprite> sprites = SpriteUtils.BuildSprites(texture, build);
			foreach (Sprite sprite in sprites)
			{
				string frameFileName = $"{sprite.SymbolData.Name}_{sprite.FrameData.Index}.png";
				string framePath = Path.Combine(outputDir, frameFileName);
				sprite.Image.Save(framePath, ImageFormat.Png);
				
				JsonObject spriteJson = new JsonObject();
				spriteJson["width"] = sprite.FrameData.SpriteWidth;
				spriteJson["height"] = sprite.FrameData.SpriteHeight;
				spriteJson["pivotX"] = sprite.FrameData.SpriterPivotX * sprite.FrameData.SpriteWidth;
				spriteJson["pivotY"] = sprite.FrameData.SpriterPivotY * sprite.FrameData.SpriteHeight;
				
				json[frameFileName] = spriteJson;
			}
			
			string jsonStr = json.ToJsonString(new JsonSerializerOptions()
			{
				WriteIndented = true
			});
			
			string jsonFile = Path.Combine(outputDir, "pivots.json");
			
			File.WriteAllText(jsonFile, jsonStr, Encoding.UTF8);
		}
	}
}
