using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;

using kanimal;

using KanimLib.KanimModel;

namespace KanimLib.Sprites
{
	public class Sprite
	{
		public readonly KFrame FrameData;
		public KSymbol SymbolData => FrameData.Parent;
		public string Name => SymbolData.Name;
		public int FrameIndex => FrameData.Index;

		[NotNull]
		public Bitmap Image
		{ get; internal set; } = null;

		public int Width => Image.Width;
		public int Height => Image.Height;
		public int Area => Image.Width * Image.Height;
		
		public Sprite(KFrame frame, Bitmap img)
		{
			ArgumentNullException.ThrowIfNull(frame);
			ArgumentNullException.ThrowIfNull(img);

			FrameData = frame;
			Image = img;
		}

		internal void Resize(int newWidth, int newHeight)
		{
			int deltaWidth = newWidth - Image.Width;
			int deltaHeight = newHeight - Image.Height;
			Bitmap newImage = new Bitmap(newWidth, newHeight, Image.PixelFormat);
			using (Graphics g = Graphics.FromImage(newImage))
			{
				g.Clear(Color.FromArgb(0, 0, 0, 0));
			}
			Image.CopyTo(newImage, deltaWidth / 2, deltaHeight / 2);
			Image.Dispose();
			Image = newImage;
		}
	}

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
}
