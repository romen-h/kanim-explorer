using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

using KanimLib.KanimModel;
using KanimLib.Sprites;

namespace KanimLib
{
	public static class AnimFactory
	{
		private const float UI_IMG_WIDTH = 80f;
		private const float UI_IMG_HEIGHT = 80f;

		private const int CELL_SIZE = 100;

		private static readonly Font fnt = new Font(FontFamily.GenericSansSerif, 16f);

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

		public static void MakePlaceholderBuilding(string name, int cellWidth, int cellHeight, out TextureAtlas textureAtlas, out KAnim anim)
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
			
			int buildingWidth = cellWidth * CELL_SIZE;
			int buildingHeight = cellHeight * CELL_SIZE;

			Bitmap uiBmp = MakePlaceholderUIBitmap((int)UI_IMG_WIDTH, Color.Blue);
			Sprite uiSprite = Sprite.MakeStandalone(uiBmp, 0.5f, 1.0f);
			Symbol uiSymbol = Symbol.MakeStandalone("ui", [uiSprite]);

			Bitmap placeBmp = MakePlaceholderPlaceBitmap(name, buildingWidth, buildingHeight);
			Sprite placeSprite = Sprite.MakeStandalone(placeBmp, 0.5f, 1f);
			Symbol placeSymbol = Symbol.MakeStandalone("place", [placeSprite]);
			
			Bitmap offBmp = MakePlaceholderBitmap(name, buildingWidth, buildingHeight, Color.Red);
			Sprite offSprite = Sprite.MakeStandalone(offBmp, 0.5f, 1f);
			Symbol offSymbol = Symbol.MakeStandalone("off", [offSprite]);

			Bitmap onBmp = MakePlaceholderBitmap(name, buildingWidth, buildingHeight, Color.Green);
			Sprite onSprite = Sprite.MakeStandalone(onBmp, 0.5f, 1f);
			Symbol onSymbol = Symbol.MakeStandalone("on", [onSprite]);

			textureAtlas = TextureAtlas.MakeFromStandalone(name, [uiSymbol, placeSymbol, offSymbol, onSymbol]);

			anim = new KAnim();
			anim.Version = KAnim.CURRENT_ANIM_VERSION;
			anim.ElementCount = 0;
			anim.FrameCount = 0;
			anim.BankCount = 4;
			anim.MaxVisSymbols = 1;

			KAnimBank placeAnim = AddBank(anim, "place", Math.Max(buildingWidth, buildingHeight));
			KAnimBank onAnim = AddBank(anim, "on", Math.Max(buildingWidth, buildingHeight));
			KAnimBank offAnim = AddBank(anim, "off", Math.Max(buildingWidth, buildingHeight));
			KAnimBank uiAnim = AddBank(anim, "ui", Math.Max(buildingWidth, buildingHeight));
		}

		private static Bitmap MakePlaceholderBitmap(string name, int width, int height, Color fill)
		{
			Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			using (Graphics g = Graphics.FromImage(bmp))
			using (Brush brush = new SolidBrush(fill))
			{
				g.Clear(Color.Black);
				g.FillRectangle(brush, 4, 4, width - 8, height - 8);
				g.DrawString(name, fnt, Brushes.Black, 10, 10);
			}

			return bmp;
		}

		private static Bitmap MakePlaceholderPlaceBitmap(string name, int width, int height)
		{
			Bitmap bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.Clear(Color.FromArgb(0, 0, 0, 0));
				g.DrawRectangle(Pens.White, 0, 0, width, height);
				g.DrawRectangle(Pens.White, 1, 1, width - 2, height - 2);
				g.DrawRectangle(Pens.White, 2, 2, width - 4, height - 4);
				g.DrawRectangle(Pens.White, 3, 3, width - 6, height - 6);
				g.DrawString(name, fnt, Brushes.White, 10, 10);
			}

			return bmp;
		}

		private static Bitmap MakePlaceholderUIBitmap(int size, Color fill)
		{
			Bitmap bmp = new Bitmap(size, size, PixelFormat.Format32bppArgb);
			using (Graphics g = Graphics.FromImage(bmp))
			using (Brush brush = new SolidBrush(fill))
			{
				g.Clear(Color.Black);
				g.FillRectangle(brush, 4, 4, size - 8, size - 8);
			}

			return bmp;
		}

		private static KAnimBank AddBank(KAnim parent, string name, int size)
		{
			int hash = name.KHash();
			parent.SymbolNames[hash] = name;

			KAnimBank bank = new KAnimBank(parent);
			bank.Name = name;
			bank.Hash = hash;
			bank.Rate = 1;

			AddAnimFrame(bank, hash, size);

			bank.FrameCount = bank.Frames.Count;

			parent.Banks.Add(bank);
			return bank;
		}

		private static KAnimFrame AddAnimFrame(KAnimBank parent, int hash, int size)
		{
			KAnimFrame frame = new KAnimFrame(parent);
			frame.X = size;
			frame.Y = size;
			frame.Width = 2 * size;
			frame.Height = 2 * size;

			AddAnimElement(frame, hash);

			frame.ElementCount = frame.Elements.Count;

			parent.Frames.Add(frame);
			return frame;
		}

		private static KAnimElement AddAnimElement(KAnimFrame parent, int hash)
		{
			KAnimElement element = new KAnimElement(parent);
			element.SymbolHash = hash;
			element.FrameNumber = 0;
			element.FolderHash = hash;
			element.Flags = 0;
			element.Alpha = 1.0f;
			element.Red = 1.0f;
			element.Green = 1.0f;
			element.Blue = 1.0f;
			element.M00 = 1f;
			element.M10 = 0f;
			element.M01 = 0f;
			element.M11 = 1f;
			element.M02 = 0f;
			element.M12 = 0f;
			element.Unused = 0;

			parent.Elements.Add(element);
			return element;
		}
	}
}
