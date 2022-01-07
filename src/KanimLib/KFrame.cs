using System;
using System.ComponentModel;
using System.Drawing;

namespace KanimLib
{
	public class KFrame
	{
		public KFrame(KSymbol parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		public bool NeedsRepack = false;

		/// <summary>
		/// Gets the KSymbol that this KFrame belongs to.
		/// </summary>
		[Browsable(false)]
		public KSymbol Parent
		{ get; private set; }

		[RefreshProperties(RefreshProperties.All)]
		public int SpriteWidth
		{
			get => (int)(PivotWidth / 2);
			set
			{
				PivotWidth = value * 2;
				NeedsRepack = true;
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		public int SpriteHeight
		{
			get => (int)(PivotHeight / 2);
			set
			{
				PivotHeight = value * 2;
				NeedsRepack = true;
			}
		}

		/// <summary>
		/// Gets or sets the index of the KFrame.
		/// </summary>
		[ReadOnly(true)]
		public int Index
		{ get; internal set; }

		/// <summary>
		/// Unknown
		/// </summary>
		[ReadOnly(true)]
		public int Duration
		{ get; set; }

		/// <summary>
		/// Unknown
		/// </summary>
		[ReadOnly(true)]
		public int ImageIndex
		{ get; set; }

		public float PivotX
		{ get; set; }

		public float PivotY
		{ get; set; }

		public float SpriterPivotX => 1f - ((PivotX / PivotWidth) + 0.5f);

		public float SpriterPivotY => 1f - ((PivotY / PivotHeight) + 0.5f);

		public float PivotWidth
		{ get; set; }

		public float PivotHeight
		{ get; set; }

		public float UV_X1
		{ get; set; }

		public float UV_Y1
		{ get; set; }

		public float UV_X2
		{ get; set; }

		public float UV_Y2
		{ get; set; }

		[ReadOnly(true)]
		public int Time
		{ get; set; }

		public RectangleF GetUVRectangle(int width, int height) => RectangleF.FromLTRB(UV_X1 * width, UV_Y1 * height, UV_X2 * width, UV_Y2 * height);

		public RectangleF GetUVRectangle() => RectangleF.FromLTRB(UV_X1, UV_Y1, UV_X2, UV_Y2);

		public void SetNewSize(Rectangle box, int atlasWidth, int atlasHeight)
		{
			PivotWidth = box.Width * 2;
			PivotHeight = box.Height * 2;

			UV_X1 = (float)box.Left / (float)atlasWidth;
			UV_Y1 = (float)box.Top / (float)atlasHeight;
			UV_X2 = (float)box.Right / (float)atlasWidth;
			UV_Y2 = (float)box.Bottom / (float)atlasHeight;
		}

		public PointF GetPivotPoint(float width, float height)
		{
			float pvtX = (PivotX / PivotWidth) + 0.5f;
			float pvtY = (PivotY / PivotHeight) + 0.5f;

			float uvWidth = (UV_X2 - UV_X1);
			float uvHeight = (UV_Y2 - UV_Y1);

			float imgX = (UV_X2 - pvtX * uvWidth) * width;
			float imgY = (UV_Y2 - pvtY * uvHeight) * height;

			return new PointF(imgX, imgY);
		}
	}
}
