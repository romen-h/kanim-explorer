using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using KanimLib.KanimModel;
using KanimLib.Sprites;

using SpriterDotNet;
using SpriterDotNet.Parsers;
using SpriterDotNet.Providers;

namespace KanimLib.Converters
{
	public static class SCMLImporter
	{
		public static bool Convert(string scmlPath, out TextureAtlas atlas, out KAnim animations)
		{
			if (!File.Exists(scmlPath)) throw new Exception("Could not find scml file.");

			string directory = Path.GetDirectoryName(scmlPath);

			string fileContent = File.ReadAllText(scmlPath);

			Spriter spriterData = SpriterReader.Default.Read(fileContent);
			return Convert(directory, spriterData, out atlas, out animations);
		}

		private static bool Convert(string directory, Spriter spriterData, out TextureAtlas atlas, out KAnim animations)
		{
			Debug.Assert(Directory.Exists(directory), "Path to scml file does not exist.");
			Debug.Assert(spriterData != null, "Spriter data is null");
			Debug.Assert(spriterData.Folders.Length == 1, "Spriter project does not have exactly one folder element.");
			Debug.Assert(spriterData.Entities.Length == 1, "Spriter project does not have exactly one entity.");

			SpriterEntity entity = spriterData.Entities[0];

			string kanimName = entity.Name;

			// Sprite Atlas + Build File

			SpriterFolder folder = spriterData.Folders[0];

			Dictionary<string, Symbol> symbols = new Dictionary<string, Symbol>();
			Dictionary<string, Dictionary<int,Sprite>> spritesForSymbol = new Dictionary<string, Dictionary<int, Sprite>>();
			Dictionary<int, Symbol> symbolForFileId = new Dictionary<int, Symbol>();
			Dictionary<int, Sprite> spritesForFileId = new Dictionary<int, Sprite>();

			foreach (var file in folder.Files)
			{
				string spriteFile = Path.Combine(directory, file.Name);
				if (!File.Exists(spriteFile)) continue;
				
				string frameName = Path.GetFileNameWithoutExtension(file.Name);
				string[] frameNameCmps = frameName.Split('_');
				string[] symbolNameCmps = frameNameCmps.Take(frameNameCmps.Length - 1).ToArray();
				string frameIndexStr = frameNameCmps.Last();
				int frameIndex = int.Parse(frameIndexStr);
				string symbolName = string.Join("_", symbolNameCmps);

				if (!symbols.TryGetValue(symbolName, out Symbol symbol))
				{
					symbol = Symbol.MakeStandalone(symbolName);
					symbols[symbolName] = symbol;
				}
				symbolForFileId[file.Id] = symbol;
				
				if (!spritesForSymbol.TryGetValue(symbolName, out var sprites))
				{
					sprites = new Dictionary<int, Sprite>();
					spritesForSymbol[symbolName] = sprites;
				}

				Bitmap bmp = (Bitmap)(new Bitmap(spriteFile)).Clone();
				Sprite sprite = Sprite.MakeStandalone(bmp, file.PivotX, 1.0f - file.PivotY);
				sprites[frameIndex] = sprite;
				spritesForFileId[file.Id] = sprite;
			}
			
			foreach (var symbol in symbols.Values)
			{
				var sprites = spritesForSymbol[symbol.Name];
				
				int count = sprites.Keys.Max() + 1;

				Sprite[] sortedSprites = new Sprite[count];
				foreach (var sprite in sprites.Values)
				{
					sortedSprites[sprite.Index] = sprite;
				}
				
				for (int i=0; i<count; i++)
				{
					Sprite sprite = sortedSprites[i];
					if (sprite == null)
					{
						sprite = Sprite.MakeStandalone(null, 0.5f, 0.5f);
					}
					
					symbol.AddSprite(sprite, false, true);
				}
			}
			
			atlas = TextureAtlas.MakeFromStandalone(kanimName, symbols.Values);

			// Anim File

			animations = AnimFactory.CreateEmptyAnim();

			int maxElements = 0;

			Config cfg = new Config()
			{
				EventsEnabled = false,
				MetadataEnabled = false,
				SoundsEnabled = false,
				TagsEnabled = false,
				VarsEnabled = false
			};

			var animData = SnapshotFrameDataProvider.Calculate(entity, 33, cfg);

			foreach (var kvp in animData)
			{
				string bankName = kvp.Key;
				KAnimBank bank = new KAnimBank(bankName);
				animations.AddBank(bank);

				foreach (var frameData in kvp.Value)
				{
					KAnimFrame frame = new KAnimFrame();
					bank.AddFrame(frame);
					//frame.X = -100f;
					//frame.Y = -100f;
					//frame.Width = 200f;
					//frame.Height = 200f;

					maxElements = Math.Max(maxElements, frameData.SpriteData.Count);

					foreach (var spriteData in frameData.SpriteData)
					{
						//Debug.Assert(spriteData.FolderId == 0);

						KAnimElement element = new KAnimElement();
						frame.AddElement(element);
						animations.ElementCount++;

						element.SymbolHash = symbolForFileId[spriteData.FileId].Hash;
						element.FolderHash = element.SymbolHash;
						Sprite sprite = spritesForFileId[spriteData.FileId];
						element.FrameNumber = sprite.Index;

						PointF pivot = new PointF(spriteData.PivotX, spriteData.PivotY);

						Matrix mat = new Matrix();

						float unwoundAngle = (spriteData.Angle + 360f) % 360f;

						mat.Translate(2*spriteData.X, -2*spriteData.Y);
						mat.RotateAt(360 - unwoundAngle, pivot);
						mat.Scale(spriteData.ScaleX, spriteData.ScaleY);


						float m00 = mat.Elements[0];
						float m01 = mat.Elements[1];
						float m10 = mat.Elements[2];
						float m11 = mat.Elements[3];
						float mdx = mat.Elements[4];
						float mdy = mat.Elements[5];

						element.M00 = m00;
						element.M01 = m10;
						element.M02 = mdx;
						element.M10 = m01;
						element.M11 = m11;
						element.M12 = mdy;

						element.Alpha = spriteData.Alpha;

						animations.SymbolNames[element.SymbolHash] = atlas.GetSymbolName(element.SymbolHash);
					}
				}
			}

			animations.MaxVisSymbols = maxElements;
			
			return true;
		}
	}
}
