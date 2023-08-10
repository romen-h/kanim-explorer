using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using KanimLib.Sprites;

using SpriterDotNet;
using SpriterDotNet.Parsers;
using SpriterDotNet.Providers;

namespace KanimLib.Converters
{
	public static class SCMLImporter
	{
		public static KAnimPackage Convert(string scmlPath)
		{
			if (!File.Exists(scmlPath)) throw new Exception("Could not find scml file.");

			string directory = Path.GetDirectoryName(scmlPath);

			string fileContent = File.ReadAllText(scmlPath);

			Spriter spriterData = SpriterReader.Default.Read(fileContent);
			return Convert(directory, spriterData);
		}

		private static KAnimPackage Convert(string directory, Spriter spriterData)
		{
			Debug.Assert(Directory.Exists(directory), "Path to scml file does not exist.");
			Debug.Assert(spriterData != null, "Spriter data is null");
			Debug.Assert(spriterData.Folders.Length == 1, "Spriter project does not have exactly one folder element.");
			Debug.Assert(spriterData.Entities.Length == 1, "Spriter project does not have exactly one entity.");

			SpriterEntity entity = spriterData.Entities[0];

			string kanimName = entity.Name;

			KAnimPackage pkg = new KAnimPackage();

			// Sprite Atlas + Build File

			KBuild build = new KBuild();
			build.Name = kanimName;
			build.Version = KBuild.CURRENT_BUILD_VERSION;
			pkg.Build = build;

			SpriterFolder folder = spriterData.Folders[0];
			Dictionary<int, Sprite> sprites = new Dictionary<int, Sprite>();

			Dictionary<string, KSymbol> symbols = new Dictionary<string, KSymbol>();
			Dictionary<int, KSymbol> symbolsById = new Dictionary<int, KSymbol>();
			Dictionary<int, KFrame> framesById = new Dictionary<int, KFrame>();

			foreach (var file in folder.Files)
			{
				string spriteFile = Path.Combine(directory, file.Name);
				if (File.Exists(spriteFile))
				{
					string frameName = Path.GetFileNameWithoutExtension(file.Name);
					string[] frameNameCmps = frameName.Split('_');
					string[] symbolNameCmps = frameNameCmps.Take(frameNameCmps.Length - 1).ToArray();
					string frameIndexStr = frameNameCmps.Last();
					int frameIndex = int.Parse(frameIndexStr);
					string symbolName = string.Join("_", symbolNameCmps);

					KSymbol symbol;
					if (!symbols.TryGetValue(symbolName, out symbol))
					{
						symbol = new KSymbol(symbolName);
						symbols[symbolName] = symbol;
						build.AddSymbol(symbol);
					}
					symbolsById[file.Id] = symbol;

					KFrame frame = new KFrame();
					frame.Index = frameIndex;
					frame.Duration = 1; // TODO: What is duration for?
					frame.ImageIndex = 0;
					frame.Time = 0;
					frame.SpriteWidth = file.Width;
					frame.SpriteHeight = file.Height;
					frame.SpriterPivotX = file.PivotX;
					frame.SpriterPivotY = 1.0f - file.PivotY;
					frame.NeedsRepack = true;

					symbol.AddFrame(frame);
					framesById[file.Id] = frame;
					build.FrameCount++;

					Bitmap bmp = (Bitmap)Bitmap.FromFile(spriteFile);

					Sprite sprite = new Sprite(frame, bmp);
					sprites.Add(file.Id, sprite);
				}
			}

			pkg.Texture = SpriteUtils.RebuildAtlas(sprites.Values.ToArray());
			pkg.Build.NeedsRepack = false;

			// Anim File

			KAnim anim = KAnimUtils.CreateEmptyAnim();
			pkg.Anim = anim;

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
				anim.AddBank(bank);

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
						anim.ElementCount++;

						element.SymbolHash = symbolsById[spriteData.FileId].Hash;
						element.FolderHash = element.SymbolHash;
						KFrame symbolFrame = framesById[spriteData.FileId];
						element.FrameNumber = symbolFrame.Index;

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
					}
				}
			}

			anim.MaxVisSymbols = maxElements;

			return pkg;
		}
	}
}
