using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;

namespace KanimalExplorer
{
	public static class AnimFactory
	{
		private const float UI_IMG_WIDTH = 80f;
		private const float UI_IMG_HEIGHT = 80f;

		private const int CELL_SIZE = 100;

		private static readonly Font fnt = new Font(FontFamily.GenericSansSerif, 16f);

		public static void MakePlaceholderBuilding(string name, int cellWidth, int cellHeight, out Bitmap atlas, out KBuild build, out KAnim anim)
		{
			int buildingWidth = cellWidth * CELL_SIZE;
			int buildingHeight = cellHeight * CELL_SIZE;

			build = new KBuild();
			build.Name = name;
			build.Version = 10;
			build.SymbolCount = 4;
			build.FrameCount = 4;

			KSymbol ui = AddSymbol(build, "ui", UI_IMG_WIDTH, UI_IMG_HEIGHT);
			Bitmap uiBmp = MakePlaceholderUIBitmap((int)UI_IMG_WIDTH, Color.Blue);
			Sprite uiSpr = new Sprite(ui.Frames[0], uiBmp);

			KSymbol place = AddSymbol(build, "place", buildingWidth, buildingHeight);
			Bitmap placeBmp = MakePlaceholderPlaceBitmap(name, buildingWidth, buildingHeight);
			Sprite placeSpr = new Sprite(place.Frames[0], placeBmp);

			KSymbol off = AddSymbol(build, "off", buildingWidth, buildingHeight);
			Bitmap offBmp = MakePlaceholderBitmap(name, buildingWidth, buildingHeight, Color.Red);
			Sprite offSpr = new Sprite(off.Frames[0], offBmp);

			KSymbol on = AddSymbol(build, "on", buildingWidth, buildingHeight);
			Bitmap onBmp = MakePlaceholderBitmap(name, buildingWidth, buildingHeight, Color.Green);
			Sprite onSpr = new Sprite(on.Frames[0], onBmp);

			build.SymbolCount = build.Symbols.Count;

			atlas = SpriteUtils.RebuildAtlas(new Sprite[] { uiSpr, placeSpr, offSpr, onSpr });

			anim = new KAnim();
			anim.Version = 5;
			anim.ElementCount = 0;
			anim.FrameCount = 0;
			anim.BankCount = 4;
			anim.MaxVisSymbols = 1;

			KAnimBank uiAnim = AddBank(anim, "ui", Math.Max(buildingWidth, buildingHeight));
			KAnimBank placeAnim = AddBank(anim, "place", Math.Max(buildingWidth, buildingHeight));
			KAnimBank onAnim = AddBank(anim, "on", Math.Max(buildingWidth, buildingHeight));
			KAnimBank offAnim = AddBank(anim, "off", Math.Max(buildingWidth, buildingHeight));
		}

		private static KSymbol AddSymbol(KBuild parent, string name, float width, float height)
		{
			int hash = name.KHash();
			parent.SymbolNames[hash] = name;

			KSymbol symbol = new KSymbol(parent);
			symbol.Hash = hash;
			symbol.Path = hash;
			symbol.Color = Color.FromArgb(0);
			symbol.Flags = 0;

			AddFrame(symbol, width, height);
			symbol.FrameCount = symbol.Frames.Count;

			parent.Symbols.Add(symbol);
			return symbol;
		}

		private static KFrame AddFrame(KSymbol parent, float width, float height)
		{
			KFrame frame = new KFrame(parent);
			frame.Index = 0;
			frame.Duration = 1;
			frame.ImageIndex = 0;
			frame.PivotX = 0;
			frame.PivotY = -height;
			frame.PivotWidth = 2 * width;
			frame.PivotHeight = 2 * height;
			frame.UV_X1 = 0f;
			frame.UV_Y1 = 0f;
			frame.UV_X2 = 0f;
			frame.UV_Y2 = 0f;
			frame.Time = 0;

			parent.Frames.Add(frame);
			return frame;
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
				g.Clear(Color.FromArgb(0,0,0,0));
				g.DrawRectangle(Pens.White, 0, 0, width, height);
				g.DrawRectangle(Pens.White, 1, 1, width-2, height-2);
				g.DrawRectangle(Pens.White, 2, 2, width-4, height-4);
				g.DrawRectangle(Pens.White, 3, 3, width-6, height-6);
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
			parent.BankNames[hash] = name;

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
			element.ImageHash = hash;
			element.Index = 0;
			element.Layer = hash;
			element.Flags = 0;
			element.Alpha = 1.0f;
			element.Red = 1.0f;
			element.Green = 1.0f;
			element.Blue = 1.0f;
			element.M1 = 1f;
			element.M2 = 0f;
			element.M3 = 0f;
			element.M4 = 1f;
			element.M5 = 0f;
			element.M6 = 0f;
			element.Order = 0;

			parent.Elements.Add(element);
			return element;
		}
	}
}
