using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

using kanimal;

using KanimLib.KanimModel;

using MaxRectsBinPack;

namespace KanimLib.Sprites
{
	public class SpriteUtils
	{
		public static List<Sprite> BuildSprites(Bitmap atlas, KBuild buildData)
		{
			List<Sprite> sprites = new List<Sprite>();
			foreach (KSymbol symbol in buildData.Symbols)
			{
				//Debug.WriteLine($"Symbol={symbol.Name}");
				foreach (KFrame frame in symbol.Frames)
				{
					//Debug.WriteLine($"    FrameIndex={frame.Index}");
					if (atlas.Width > 0 && atlas.Height > 0)
					{
						Bitmap croppedImg = atlas.Clone(frame.GetTextureRectangle(atlas.Width, atlas.Height),
							atlas.PixelFormat);
						Sprite spr = new Sprite(frame, croppedImg);
						frame.Sprite = spr;
						sprites.Add(spr);
					}
				}
			}
			return sprites;
		}

		public static Bitmap RebuildAtlas(IEnumerable<Sprite> sprites)
		{
			// Make a new atlas with packed sprites
			int maxWidth = 0;
			int maxHeight = 0;
			foreach (Sprite spr in sprites)
			{
				maxWidth = Math.Max(spr.Image.Width, maxWidth);
				maxHeight = Math.Max(spr.Image.Height, maxHeight);
			}

			PackedSprite[] packedSprites = Pack(sprites, out int atlasWidth, out int atlasHeight);

			Bitmap newAtlas = new Bitmap(atlasWidth, atlasHeight, PixelFormat.Format32bppArgb);
			foreach (PackedSprite packed in packedSprites)
			{
				packed.Sprite.Image.CopyTo(newAtlas, packed.Position.X, packed.Position.Y);
				packed.Sprite.FrameData.OnAtlasRebuilt(packed.BoundingBox, atlasWidth, atlasHeight);
			}
			
			return newAtlas;
		}

		public static Bitmap GetHelperImage(int width, int height, KBuild build, bool boundingBoxes, bool pivots)
		{
			Bitmap bmp = new Bitmap(width, height);
			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.Transparent);

				foreach (var symbol in build.Symbols)
				{
					foreach (var frame in symbol.Frames)
					{
						int left = (int)(frame.UV_X1 * width);
						int top = (int)(frame.UV_Y1 * height);
						int right = (int)(frame.UV_X2 * width) - 1;
						int bottom = (int)(frame.UV_Y2 * height) - 1;

						if (boundingBoxes)
						{
							g.DrawLine(Pens.Red, left, top, right, top);
							g.DrawLine(Pens.Magenta, right, top, right, bottom);
							g.DrawLine(Pens.Blue, left, bottom, right, bottom);
							g.DrawLine(Pens.Lime, left, top + 1, left, bottom);
						}

						if (pivots)
						{
							int spriteWidth = right - left;
							int spriteHeight = bottom - top;
							int x = left + (int)(spriteWidth * frame.SpriterPivotX);
							int y = top + (int)(spriteHeight * frame.SpriterPivotY);

							g.FillRectangle(Brushes.Red, x - 1, y - 1, 3, 3);
							g.FillRectangle(Brushes.Black, x, y, 1, 1);
						}
					}
				}
			}
			return bmp;
		}

		public static Bitmap GetPivots(int width, int height, KBuild build)
		{
			Bitmap bmp = new Bitmap(width, height);
			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.Transparent);

				foreach (var symbol in build.Symbols)
				{
					foreach (var frame in symbol.Frames)
					{
						int left = (int)(frame.UV_X1 * width);
						int top = (int)(frame.UV_Y1 * height);
						int x = left + (int)(frame.PivotX / 2f);
					}
				}
			}
			return bmp;
		}

		public static PackedSprite[] Pack(IEnumerable<Sprite> sprites, out int sheetW, out int sheetH)
		{
			// Brute force trial-and-error sprite packing.
			// Double the smaller axis of the sheet each time it fails.
			sheetW = 256;
			sheetH = 256;

			bool packed = false;
			PackedSprite[] packedSprites = null;
			while (!packed)
			{
				packedSprites = TryPack(sprites, sheetW, sheetH);
				if (packedSprites == null)
				{
					if (sheetW > sheetH)
					{
						sheetH *= 2;
					}
					else
					{
						sheetW *= 2;
					}
				}
				else
				{
					packed = true;
				}
			}

			return packedSprites;
		}

		private static PackedSprite[] TryPack(IEnumerable<Sprite> sprites, int sheet_w, int sheet_h)
		{
			List<PackedSprite> packedSprites = new List<PackedSprite>();

			// load all sprites into list and sort
			var spritesToPack = new List<Sprite>(sprites);
			spritesToPack.Sort((sprite1, sprite2) => sprite2.Area.CompareTo(sprite1.Area));

			var packer = new MaxRectsBinPack.MaxRectsBinPack(sheet_w, sheet_h, false);

			foreach (var sprite in spritesToPack)
			{
				var rect = packer.Insert(sprite.Width, sprite.Height, FreeRectChoiceHeuristic.RectBestShortSideFit);
				if (rect.Width == 0 || rect.Height == 0) return null;

				packedSprites.Add(new PackedSprite(sprite, rect.Location));
			}

			return packedSprites.ToArray();
		}
	}
}
