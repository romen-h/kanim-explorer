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

		public static void AutoFlagSymbols(TextureAtlas atlas)
		{
			ArgumentNullException.ThrowIfNull(atlas);
			
			using (s_log.BeginFunction());
			
			atlas.AutoFlagSymbols(true);
		}
		
		public static void RenameSymbolInAnim(TextureAtlas atlas, string oldSymbolName, string newSymbolName)
		{
			ArgumentNullException.ThrowIfNull(atlas);
			ArgumentNullException.ThrowIfNull(oldSymbolName);
			ArgumentNullException.ThrowIfNull(newSymbolName);

			using (s_log.BeginFunction());
#if false
			if (atlas.HasAnim)
			{
				s_log.LogTrace($"Renaming {oldSymbolName} to {newSymbolName} in anim.bytes...");
				if (!atlas.HasBuild) s_log.LogWarning("There was no build data to validate symbol name. If the new symbol name already exists then animations could look incorrect.");
				
				foreach (var bank in atlas.Anim.Banks)
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
				atlas.Anim.SymbolNames.Remove(oldHash);
				atlas.Anim.SymbolNames.Add(newHash, newSymbolName);
			}
			else
			{
				s_log.LogWarning("Current kanim data does not have animations. No changes happening to anim.bytes.");
			}
#endif
		}
		
		public static void DuplicateSymbols(TextureAtlas atlas, IReadOnlyList<string> symbolsToDuplicate, IReadOnlyList<string> targetBanks, string prefix, string suffix, int zOffset, bool invisible)
		{
			ArgumentNullException.ThrowIfNull(atlas);

			using (s_log.BeginFunction());
			
#if false
			if (symbolsToDuplicate == null || symbolsToDuplicate.Count == 0) throw new ArgumentException("No symbols to duplicate.", nameof(symbolsToDuplicate));
			if (targetBanks == null || targetBanks.Count == 0) throw new ArgumentException("No target animations.", nameof(targetBanks));

			if (string.IsNullOrWhiteSpace(prefix) && string.IsNullOrWhiteSpace(suffix)) throw new ArgumentException("Prefix and Suffix are both empty.");

			if (zOffset == 0) throw new ArgumentException("zOffset must not be 0", nameof(zOffset));

			Dictionary<int, int> originalHashToDupeHash = new Dictionary<int, int>();

			foreach (string symbolName in symbolsToDuplicate)
			{
				Symbol original = atlas.GetSymbol(symbolName);
				if (original != null)
				{
					string duplicatedName = prefix + symbolName + suffix;
					if (atlas.SymbolExists(duplicatedName)) throw new Exception($"A symbol with the name {duplicatedName} already exists.");
						
					Symbol duplicated = Symbol.Copy(original, duplicatedName);
					if (invisible)
					{
						foreach (var frame in duplicated.Sprites)
						{
							frame.UV_X1 = 0;
							frame.UV_Y1 = 0;
							frame.UV_X2 = 0;
							frame.UV_Y2 = 0;
						}
					}
					else
					{
						for (int i=0; i<duplicated.Sprites.Count; i++)
						{
							var originalFrame = original.Sprites[i];
							var duplicatedFrame = duplicated.Sprites[i];
							duplicatedFrame.Sprite = new Sprites.Sprite(duplicatedFrame, (Bitmap)originalFrame.Sprite.Image.Clone());
							atlas.AddSprite(duplicatedFrame.Sprite);
						}
					}
					atlas.Build.InsertSymbolAfter(duplicated, original);
					originalHashToDupeHash[original.Hash] = duplicated.Hash;
					atlas.Anim.SymbolNames[duplicated.Hash] = duplicated.Name;
				}
			}
			
			atlas.RebuildAtlas();

			foreach (string targetBank in targetBanks)
			{
				KAnimBank bank = atlas.Anim.GetBank(targetBank);

				foreach (var animFrame in bank.Frames)
				{
					if (animFrame.ElementCount == 0) continue;
					foreach (string symbolName in symbolsToDuplicate)
					{
						Symbol originalSymbol = atlas.Build.GetSymbol(symbolName);
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
#endif
		}
	}
}
