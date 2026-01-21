using System;
using System.ComponentModel;
using System.Drawing;
using KanimLib.Sprites;

namespace KanimLib.KanimModel
{
	public class KFrame
	{
		private bool _rebuilding = false;
		
		internal KFrame()
		{ }

		internal KFrame(KSymbol parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		internal static KFrame Copy(KFrame original)
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

		/// <summary>
		/// Gets the KSymbol that this KFrame belongs to.
		/// </summary>
		[Browsable(false)]
		public KSymbol Parent
		{ get; internal set; }

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

		[Browsable(false)]
		public float PivotX
		{ get; set; }

		[Browsable(false)]
		public float PivotY
		{ get; set; }

		[Browsable(false)]
		public float PivotWidth
		{ get; set; }

		[Browsable(false)]
		public float PivotHeight
		{ get; set; }

		[DisplayName("Pivot X (%)")]
		[RefreshProperties(RefreshProperties.All)]
		public float SpriterPivotX
		{
			get => 1f - (PivotX / PivotWidth + 0.5f);
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value));
				PivotX = (0.5f - value) * PivotWidth;
			}
		}

		[DisplayName("Pivot X (px)")]
		[RefreshProperties(RefreshProperties.All)]
		public float PixelPivotX
		{
			get => SpriterPivotX * SpriteWidth;
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value));
				SpriterPivotX = value / SpriteWidth;
			}
		}

		[DisplayName("Pivot Y (%)")]
		[RefreshProperties(RefreshProperties.All)]
		public float SpriterPivotY
		{
			get => 1f - (PivotY / PivotHeight + 0.5f);
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value));
				PivotY = (0.5f - value) * PivotHeight;
			}
		}

		[DisplayName("Pivot Y (px)")]
		[RefreshProperties(RefreshProperties.All)]
		public float PixelPivotY
		{
			get => SpriterPivotY * SpriteHeight;
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value));
				SpriterPivotY = value / SpriteHeight;
			}
		}

		[DisplayName("Sprite Width (px)")]
		[RefreshProperties(RefreshProperties.All)]
		public int SpriteWidth
		{
			get => (int)(PivotWidth / 2);
			set
			{
				if (_rebuilding) return;

				PivotWidth = value * 2;
				if (Sprite != null)
				{
					Sprite.Resize(value, SpriteHeight);
				}
				_rebuilding = true;
				Parent?.TriggerAtlasRebuild();
				_rebuilding = false;
			}
		}

		[DisplayName("Sprite Height (px)")]
		[RefreshProperties(RefreshProperties.All)]
		public int SpriteHeight
		{
			get => (int)(PivotHeight / 2);
			set
			{
				if (_rebuilding) return;

				PivotHeight = value * 2;
				if (Sprite != null)
				{
					Sprite.Resize(SpriteWidth, value);
				}
				_rebuilding = true;
				Parent?.TriggerAtlasRebuild();
				_rebuilding = false;
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		public float UV_X1
		{ get; set; }

		[RefreshProperties(RefreshProperties.All)]
		public float UV_Y1
		{ get; set; }

		[RefreshProperties(RefreshProperties.All)]
		public float UV_X2
		{ get; set; }

		[RefreshProperties(RefreshProperties.All)]
		public float UV_Y2
		{ get; set; }

		[ReadOnly(true)]
		public int Time
		{ get; set; }

		[Browsable(false)]
		public Sprite Sprite
		{ get; internal set; }
		
		public string FrameName
		{
			get
			{
				string parent = Parent?.Name ?? "[Unknown Symbol]";
				string frameNo = Index.ToString();
				return $"{parent}_{frameNo}";
			}
		}

		public Rectangle GetTextureRectangle(int texWidth, int texHeight)
		{
			// If the UV rect is 0 length on one side it has to be expanded to at least 1 pixel.

			float uv_x2 = UV_X2;
			if (UV_X2 == UV_X1)
			{
				uv_x2 = UV_X1 + 1.0f / texWidth;
			}
			float uv_y2 = UV_Y2;
			if (UV_Y2 == UV_Y1)
			{
				uv_y2 = UV_Y1 + 1.0f / texHeight;
			}

			int left = Math.Max(0, (int)(UV_X1 * texWidth));
			int top = Math.Max(0, (int)(UV_Y1 * texHeight));
			int right = Math.Min(texWidth, (int)(uv_x2 * texWidth));
			int bottom = Math.Min(texHeight, (int)(uv_y2 * texHeight));

			return Rectangle.FromLTRB(left, top, right, bottom);
		}

		public RectangleF GetTextureRectangleF(int texWidth, int texHeight)
		{
			// If the UV rect is 0 length on one side it has to be expanded to at least 1 pixel.

			float uv_x2 = UV_X2;
			if (UV_X2 == UV_X1)
			{
				uv_x2 = UV_X1 + 1.0f / texWidth;
			}
			float uv_y2 = UV_Y2;
			if (UV_Y2 == UV_Y1)
			{
				uv_y2 = UV_Y1 + 1.0f / texHeight;
			}

			float left = Math.Max(0, UV_X1 * texWidth);
			float top = Math.Max(0, UV_Y1 * texHeight);
			float right = Math.Min(texWidth, uv_x2 * texWidth);
			float bottom = Math.Min(texHeight, uv_y2 * texHeight);

			return RectangleF.FromLTRB(left, top, right, bottom);
		}

		public RectangleF GetUVRectangle() => RectangleF.FromLTRB(UV_X1, UV_Y1, UV_X2, UV_Y2);

		internal void OnAtlasRebuilt(Rectangle box, int atlasWidth, int atlasHeight)
		{
			PivotWidth = box.Width * 2;
			PivotHeight = box.Height * 2;

			UV_X1 = (box.Left + 0.5f) / atlasWidth;
			UV_Y1 = (box.Top + 0.5f) / atlasHeight;
			UV_X2 = (box.Right - 0.5f) / atlasWidth;
			UV_Y2 = (box.Bottom - 0.5f) / atlasHeight;
		}

		public PointF GetPivotPoint(float width, float height)
		{
			float pvtX = PivotX / PivotWidth + 0.5f;
			float pvtY = PivotY / PivotHeight + 0.5f;

			float uvWidth = UV_X2 - UV_X1;
			float uvHeight = UV_Y2 - UV_Y1;

			float imgX = (UV_X2 - pvtX * uvWidth) * width;
			float imgY = (UV_Y2 - pvtY * uvHeight) * height;

			return new PointF(imgX, imgY);
		}
	}
}
