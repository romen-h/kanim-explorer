using System;
using System.ComponentModel;
using System.Drawing;

namespace KanimLib
{
	public class KFrame
	{
		internal KFrame()
		{ }

		public KFrame(KSymbol parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		public static KFrame Copy(KFrame original)
		{
			KFrame frame = new KFrame()
			{
				Index = original.Index,
				Duration = original.Duration,
				ImageIndex = original.ImageIndex,
				PivotX = original.PivotX,
				PivotY = original.PivotY,
				PivotWidth = original.PivotWidth,
				PivotHeight = original.PivotHeight,
				UV_X1 = original.UV_X1,
				UV_X2 = original.UV_X2,
				UV_Y1 = original.UV_Y1,
				UV_Y2 = original.UV_Y2,
				Time = original.Time
			};

			return frame;
		}

		public bool NeedsRepack = false;

		/// <summary>
		/// Gets the KSymbol that this KFrame belongs to.
		/// </summary>
		[Browsable(false)]
		public KSymbol Parent
		{ get; internal set; }

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

		[RefreshProperties(RefreshProperties.All)]
		public float PivotX
		{ get; set; }

		[RefreshProperties(RefreshProperties.All)]
		public float PivotY
		{ get; set; }

		[RefreshProperties(RefreshProperties.All)]
		public float SpriterPivotX
		{
			get => 1f - ((PivotX / PivotWidth) + 0.5f);
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value));
				PivotX = (0.5f - value) * PivotWidth;
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		public float SpriterPivotY
		{
			get => 1f - ((PivotY / PivotHeight) + 0.5f);
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value));
				PivotY = (0.5f - value) * PivotHeight;
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		public float PivotWidth
		{ get; set; }

		[RefreshProperties(RefreshProperties.All)]
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

		public RectangleF GetUVRectangle(int width, int height)
		{
			// If the UV rect is 0x0 we need to grow to 1x1, some vanilla kanims have this problem

			float uv_x2 = UV_X2;
			if (UV_X2 == UV_X1)
			{
				uv_x2 = UV_X1 + 1.0f / width;
			}
			float uv_y2 = UV_Y2;
			if (UV_Y2 == UV_Y1)
			{
				uv_y2 = UV_Y1 + 1.0f / height;
			}

			return RectangleF.FromLTRB(UV_X1 * width, UV_Y1 * height, uv_x2 * width, uv_y2 * height);
		}

		public RectangleF GetUVRectangle() => RectangleF.FromLTRB(UV_X1, UV_Y1, UV_X2, UV_Y2);

		public void SetNewSize(Rectangle box, int atlasWidth, int atlasHeight)
		{
			float halfPixelW = 0.5f / atlasWidth;
			float halfPixelH = 0.5f / atlasHeight;

			PivotWidth = box.Width * 2;
			PivotHeight = box.Height * 2;

			UV_X1 = (float)box.Left / (float)atlasWidth + halfPixelW;
			UV_Y1 = (float)box.Top / (float)atlasHeight + halfPixelH;
			UV_X2 = (float)box.Right / (float)atlasWidth - halfPixelW;
			UV_Y2 = (float)box.Bottom / (float)atlasHeight - halfPixelH;
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
