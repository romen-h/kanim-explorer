using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

using MaxRectsBinPack;

using Sprite = KanimLib.KanimModel.Sprite;

namespace KanimLib.Sprites
{
	internal class SpriteUtils
	{
		public class PackedSprite
		{
			public readonly Sprite Sprite;
			public readonly Point Position;

			public Rectangle BoundingBox => new Rectangle(Position.X, Position.Y, Sprite.Width, Sprite.Height);

			public PackedSprite(Sprite spr, Point pos)
			{
				Sprite = spr;
				Position = pos;
			}
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
