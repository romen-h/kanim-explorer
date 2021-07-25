using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

using MaxRectsBinPack;

namespace KanimLib.Sprites
{
	public class SpriteUtils
	{
		//public static Sprite GenerateBuildingSprite(int width, int height)
		//{

		//}

		public static Sprite[] BuildSprites(Bitmap atlas, KBuild buildData)
		{
			List<Sprite> sprites = new List<Sprite>();
			foreach (KSymbol symbol in buildData.Symbols)
			{
				foreach (KFrame frame in symbol.Frames)
				{
					Bitmap croppedImg = atlas.Clone(frame.GetUVRectangle(atlas.Width, atlas.Height), atlas.PixelFormat);
					Sprite spr = new Sprite(frame, croppedImg);
					sprites.Add(spr);
				}
			}
			return sprites.ToArray();
		}

		public static void ResizeSprites(Sprite[] sprites)
		{
			if (sprites == null || sprites.Length == 0) return;

			foreach (Sprite spr in sprites)
			{
				if (spr.Image == null) continue;
				if (spr.FrameData.NeedsRepack)
				{
					spr.Resize(spr.FrameData.SpriteWidth, spr.FrameData.SpriteHeight);
				}
			}
		}

		public static Bitmap RebuildAtlas(Sprite[] sprites)
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
			using (Graphics g = Graphics.FromImage(newAtlas))
			{
				foreach (PackedSprite packed in packedSprites)
				{
					packed.Sprite.FrameData.SetNewSize(packed.BoundingBox, atlasWidth, atlasHeight);
					g.DrawImage(packed.Sprite.Image, packed.BoundingBox);
				}
			}

			return newAtlas;
		}

		public static PackedSprite[] Pack(Sprite[] sprites, out int sheetW, out int sheetH)
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

		private static PackedSprite[] TryPack(Sprite[] sprites, int sheet_w, int sheet_h)
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
