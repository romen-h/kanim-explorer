using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace KanimLib
{
	public static class TileFactory
	{
		public const int CELL_SIZE = 208;
		public const int CELL_MARGIN = 40;
		public const int SEAMLESS_SIZE = 128;
		public const int BORDER_IMG_SIZE = 336;
		public const int BORDER_SIZE = 104;

		enum HorizontalSide
		{
			Left,
			Middle,
			Right
		}

		enum VerticalSide
		{
			Top,
			Middle,
			Bottom
		}

		public static Bitmap RenderTileAtlas(Bitmap border, Bitmap fill)
		{
			Bitmap atlas = new Bitmap(1024, 1024, PixelFormat.Format32bppArgb);
			using (Graphics g = Graphics.FromImage(atlas))
			{
				g.Clear(Color.FromArgb(0));
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;

				for (int cy = 0; cy < 4; cy++)
				{
					for (int cx = 0; cx < 4; cx++)
					{
						int x0 = cx * CELL_SIZE;
						int y0 = cy * CELL_SIZE;

						// Blit fill texture
						g.DrawImage(fill, x0 + CELL_MARGIN, y0 + CELL_MARGIN);

						int i = cy * 4 + cx;

						bool up = ((i & 0x08) > 0);
						bool left = ((i & 0x04) > 0);
						bool right = ((i & 0x02) > 0);
						bool down = ((i & 0x01) > 0);

						// Draw fills

						if (up)
						{
							// Draw bottom of seamless in top
							g.DrawImage(fill, x0 + CELL_MARGIN, y0, GetSeamlessPatch(HorizontalSide.Middle, VerticalSide.Bottom), GraphicsUnit.Pixel);
						}
						if (left)
						{
							// Draw right of seamless in left
							g.DrawImage(fill, x0, y0 + CELL_MARGIN, GetSeamlessPatch(HorizontalSide.Right, VerticalSide.Middle), GraphicsUnit.Pixel);
						}
						if (right)
						{
							// Draw left of seamless in right
							g.DrawImage(fill, x0 + CELL_SIZE - CELL_MARGIN, y0 + CELL_MARGIN, GetSeamlessPatch(HorizontalSide.Left, VerticalSide.Middle), GraphicsUnit.Pixel);
						}
						if (down)
						{
							// Draw top of seamless in bottom
							g.DrawImage(fill, x0 + CELL_MARGIN, y0 + CELL_SIZE - CELL_MARGIN, GetSeamlessPatch(HorizontalSide.Middle, VerticalSide.Top), GraphicsUnit.Pixel);
						}

						if (up&&left)
						{
							// Draw bottom-right of seamless in top-left
							g.DrawImage(fill, x0, y0, GetSeamlessPatch(HorizontalSide.Right, VerticalSide.Bottom), GraphicsUnit.Pixel);
						}
						if (up&&right)
						{
							// Draw bottom-left of seamless in top-right
							g.DrawImage(fill, x0 + CELL_SIZE - CELL_MARGIN, y0, GetSeamlessPatch(HorizontalSide.Left, VerticalSide.Bottom), GraphicsUnit.Pixel);
						}
						if (down&&left)
						{
							// Draw top-right of seamless in bottom-left
							g.DrawImage(fill, x0, y0 + CELL_SIZE - CELL_MARGIN, GetSeamlessPatch(HorizontalSide.Right, VerticalSide.Top), GraphicsUnit.Pixel);
						}
						if (down&&right)
						{
							// Draw top-left of seamless in bottom-right
							g.DrawImage(fill, x0 + CELL_SIZE - CELL_MARGIN, y0 + CELL_SIZE - CELL_MARGIN, GetSeamlessPatch(HorizontalSide.Left, VerticalSide.Top), GraphicsUnit.Pixel);
						}

						// Draw borders

						if (!up)
						{
							// Draw top of border in top
							g.DrawImage(border, x0 + CELL_MARGIN, y0, GetBorderPatch(HorizontalSide.Middle, VerticalSide.Top), GraphicsUnit.Pixel);
						}
						if (!left)
						{
							// Draw left of border in left
							g.DrawImage(border, x0, y0 + CELL_MARGIN, GetBorderPatch(HorizontalSide.Left, VerticalSide.Middle), GraphicsUnit.Pixel);
						}
						if (!right)
						{
							// Draw right of border in right
							g.DrawImage(border, x0 + CELL_SIZE - BORDER_SIZE, y0 + CELL_MARGIN, GetBorderPatch(HorizontalSide.Right, VerticalSide.Middle), GraphicsUnit.Pixel);
						}
						if (!down)
						{
							// Draw bottom of border in bottom
							g.DrawImage(border, x0 + CELL_MARGIN, y0 + CELL_SIZE - BORDER_SIZE, GetBorderPatch(HorizontalSide.Middle, VerticalSide.Bottom), GraphicsUnit.Pixel);
						}

						if (!up&&!left)
						{
							// Draw top-left of border in top-left
							g.DrawImage(border, x0, y0, GetBorderPatch(HorizontalSide.Left, VerticalSide.Top), GraphicsUnit.Pixel);
						}
						if (!up&&!right)
						{
							// Draw top-right of border in top-right
							g.DrawImage(border, x0 + CELL_SIZE - BORDER_SIZE, y0, GetBorderPatch(HorizontalSide.Right, VerticalSide.Top), GraphicsUnit.Pixel);
						}
						if (!down&&!left)
						{
							// Draw bottom-left of border in bottom-left
							g.DrawImage(border, x0, y0 + CELL_SIZE - BORDER_SIZE, GetBorderPatch(HorizontalSide.Left, VerticalSide.Bottom), GraphicsUnit.Pixel);
						}
						if (!down&&!right)
						{
							// Draw bottom-right of border in bottom-right
							g.DrawImage(border, x0 + CELL_SIZE - BORDER_SIZE, y0 + CELL_SIZE - BORDER_SIZE, GetBorderPatch(HorizontalSide.Right, VerticalSide.Bottom), GraphicsUnit.Pixel);
						}
					}
				}
			}
			return atlas;
		}

		private static Rectangle GetSeamlessPatch(HorizontalSide h, VerticalSide v)
		{
			int left = 0;
			int top = 0;
			int width = 0;
			int height = 0;

			if (h == HorizontalSide.Left)
			{
				left = 0;
				width = CELL_MARGIN;
			}
			else if (h == HorizontalSide.Middle)
			{
				left = 0;
				width = SEAMLESS_SIZE;
			}
			else if (h == HorizontalSide.Right)
			{
				left = SEAMLESS_SIZE - CELL_MARGIN;
				width = CELL_MARGIN;
			}

			if (v == VerticalSide.Top)
			{
				top = 0;
				height = CELL_MARGIN;
			}
			else if (v == VerticalSide.Middle)
			{
				top = 0;
				height = SEAMLESS_SIZE;
			}
			else if (v == VerticalSide.Bottom)
			{
				top = SEAMLESS_SIZE - CELL_MARGIN;
				height = CELL_MARGIN;
			}

			return new Rectangle(left, top, width, height);
		}

		private static Rectangle GetBorderPatch(HorizontalSide h, VerticalSide v)
		{
			int left = 0;
			int top = 0;
			int width = 0;
			int height = 0;

			if (h == HorizontalSide.Left)
			{
				left = 0;
				width = BORDER_SIZE;
			}
			else if (h == HorizontalSide.Middle)
			{
				left = BORDER_SIZE;
				width = SEAMLESS_SIZE;
			}
			else if (h == HorizontalSide.Right)
			{
				left = BORDER_IMG_SIZE - BORDER_SIZE;
				width = BORDER_SIZE;
			}

			if (v == VerticalSide.Top)
			{
				top = 0;
				height = BORDER_SIZE;
			}
			else if (v == VerticalSide.Middle)
			{
				top = BORDER_SIZE;
				height = SEAMLESS_SIZE;
			}
			else if (v == VerticalSide.Bottom)
			{
				top = BORDER_IMG_SIZE - BORDER_SIZE;
				height = BORDER_SIZE;
			}

			return new Rectangle(left, top, width, height);
		}
	}
}
