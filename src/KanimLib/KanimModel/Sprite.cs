using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;

namespace KanimLib.KanimModel
{
	public class Sprite : INotifyPropertyChanged
	{
		private int _index;
		private int _duration;
		private int _imageIndex;
		private float _pivotX;
		private float _pivotY;
		private int _width;
		private int _height;
		private float _uvX1;
		private float _uvX2;
		private float _uvY1;
		private float _uvY2;
		
		private Bitmap _texture;
		
		internal string symbolName = null;

		[Description("The index ID of the sprite.")]
		public int Index => _index;

		[Description("The number of frames the sprite is visible for. (Unused)")]
		public int Duration => _duration;

		[Description("The texture index that the sprite references. (Unused)")]
		public int ImageIndex => _imageIndex;

		[Description("A user-readable name for the KFrame.")]
		public string DisplayName
		{
			get
			{
				string parent = symbolName ?? "[Unknown Symbol]";
				string frameNo = Index.ToString();
				return $"{parent}_{frameNo}";
			}
		}

		[DisplayName("Pivot X (%)")]
		[Description("The pivot X position, as a percentage between 0 and 1.")]
		[RefreshProperties(RefreshProperties.All)]
		public float SpriterPivotX
		{
			get => _pivotX;
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value));
				if (_pivotX != value)
				{
					_pivotX = value;
					InvokePropertyChanged(nameof(SpriterPivotX));
					InvokePropertyChanged(nameof(PixelPivotX));
				}
			}
		}

		[DisplayName("Pivot X (px)")]
		[Description("The pivot X position, as pixels.")]
		[RefreshProperties(RefreshProperties.All)]
		public float PixelPivotX
		{
			get => _pivotX * _width;
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value));
				SpriterPivotX = value / Width;
			}
		}

		[DisplayName("Pivot Y (%)")]
		[Description("The pivot Y position, as a percentage between 0 and 1.")]
		[RefreshProperties(RefreshProperties.All)]
		public float SpriterPivotY
		{
			get => _pivotY;
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value));
				if (_pivotY != value)
				{
					_pivotY = value;
					InvokePropertyChanged(nameof(SpriterPivotY));
					InvokePropertyChanged(nameof(PixelPivotY));
				}
			}
		}

		[DisplayName("Pivot Y (px)")]
		[Description("The pivot Y position, as pixels.")]
		[RefreshProperties(RefreshProperties.All)]
		public float PixelPivotY
		{
			get => _pivotY * _height;
			set
			{
				if (float.IsNaN(value) || float.IsInfinity(value)) throw new ArgumentOutOfRangeException(nameof(value));
				SpriterPivotY = value / Height;
			}
		}

		[DisplayName("Sprite Width (px)")]
		[Description("The width of the sprite in pixels.")]
		[RefreshProperties(RefreshProperties.All)]
		public int Width
		{
			get => _width;
			set => Resize(value, _height, true);
		}

		[DisplayName("Sprite Height (px)")]
		[Description("The height of the sprite in pixels.")]
		[RefreshProperties(RefreshProperties.All)]
		public int Height
		{
			get => _height;
			set => Resize(_width, value, true);
		}

		[Browsable(false)]
		public int Area => _width * _height;

		[DisplayName("UV Left")]
		[Description("The left UV coordinate for the sprite inside the texture.")]
		[RefreshProperties(RefreshProperties.All)]
		public float UV_X1
		{
			get => _uvX1;
			set
			{
				if (_uvX1 != value)
				{
					_uvX1 = value;
					InvokePropertyChanged(nameof(UV_X1));
				}
			}
		}

		[DisplayName("UV Top")]
		[Description("The top UV coordinate for the sprite inside the texture.")]
		[RefreshProperties(RefreshProperties.All)]
		public float UV_Y1
		{
			get => _uvY1;
			set
			{
				if (_uvY1 != value)
				{
					_uvY1 = value;
					InvokePropertyChanged(nameof(UV_Y1));
				}
			}
		}

		[DisplayName("UV Right")]
		[Description("The right UV coordinate for the sprite inside the texture.")]
		[RefreshProperties(RefreshProperties.All)]
		public float UV_X2
		{
			get => _uvX2;
			set
			{
				if (_uvX2 != value)
				{
					_uvX2 = value;
					InvokePropertyChanged(nameof(UV_X2));
				}
			}
		}

		[DisplayName("UV Bottom")]
		[Description("The bottom UV coordinate for the sprite inside the texture.")]
		[RefreshProperties(RefreshProperties.All)]
		public float UV_Y2
		{
			get => _uvY2;
			set
			{
				if (_uvY2 != value)
				{
					_uvY2 = value;
					InvokePropertyChanged(nameof(UV_Y2));
				}
			}
		}

		[Browsable(false)]
		public Bitmap Texture => _texture;

		public event PropertyChangedEventHandler PropertyChanged;
		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		internal Sprite(Stream stream)
		{
			using BinaryReader reader = new BinaryReader(stream, Encoding.ASCII, true);
			
			_index = reader.ReadInt32();
			_duration = reader.ReadInt32();
			_imageIndex = reader.ReadInt32();
			float kPivotX = reader.ReadSingle();
			float kPivotY = reader.ReadSingle();
			float kPivotWidth = reader.ReadSingle();
			float kPivotHeight = reader.ReadSingle();
			_uvX1 = reader.ReadSingle();
			_uvY1 = reader.ReadSingle();
			_uvX2 = reader.ReadSingle();
			_uvY2 = reader.ReadSingle();
			
			_pivotX = 0.5f - (kPivotX / kPivotWidth);
			_pivotY = 0.5f - (kPivotY / kPivotHeight);
			_width = (int)(kPivotWidth/2f);
			_height = (int)(kPivotHeight/2f);
		}
		
		internal Sprite(Sprite other)
		{
			ArgumentNullException.ThrowIfNull(other);
			
			_index = other._index;
			_duration = other._duration;
			_imageIndex = other._imageIndex;
			_pivotX = other._pivotX;
			_pivotY = other._pivotY;
			_width = other._width;
			_height = other._height;
			_uvX1 = other._uvX1;
			_uvY1 = other._uvY1;
			_uvX2 = other._uvX2;
			_uvY2 = other._uvY2;
		}
		
		private Sprite(int index, float pivotX, float pivotY, int width, int height, RectangleF uvs, Bitmap atlasTexture)
		{
			_index = index;
			_duration = 1;
			_imageIndex = 0;
			_pivotX = pivotX;
			_pivotY = pivotY;
			_width = width;
			_height = height;
			_uvX1 = uvs.Left;
			_uvY1 = uvs.Top;
			_uvX2 = uvs.Right;
			_uvY2 = uvs.Bottom;
			
			if (atlasTexture != null)
			{
				SetFromAtlas(atlasTexture, GetTextureRectangle(atlasTexture.Width, atlasTexture.Height), false);
			}
		}
		
		internal static Sprite MakeStandalone(Bitmap image, float pivotX, float pivotY)
		{
			var sprite = new Sprite(
				index: -1,
				pivotX: pivotX,
				pivotY: pivotY,
				width: image?.Width ?? 0,
				height: image?.Height ?? 0,
				uvs: RectangleF.FromLTRB(0f, 0f, 1f, 1f),
				null);
			
			sprite._texture = image;
			return sprite;
		}
		
		public void Save(string filePath)
		{
			using FileStream fs = File.Create(filePath);
			WritePng(fs);
		}
		
		internal void WritePng(Stream stream)
		{
			if (_texture == null) throw new InvalidOperationException("Texture is null.");
			_texture.Save(stream, ImageFormat.Png);
		}
		
		internal void WriteToKleiBuildBytes(Stream stream)
		{
			using BinaryWriter writer = new BinaryWriter(stream, Encoding.ASCII, true);

			float kWidth = _width * 2f;
			float kHeight = _height * 2f;
			float kPivotX = (0.5f - _pivotX) * kWidth;
			float kPivotY = (0.5f - _pivotY) * kHeight;

			writer.Write(_index);
			writer.Write(_duration);
			writer.Write(_imageIndex);
			writer.Write(kPivotX);
			writer.Write(kPivotY);
			writer.Write(kWidth);
			writer.Write(kHeight);
			writer.Write(_uvX1);
			writer.Write(_uvY1);
			writer.Write(_uvX2);
			writer.Write(_uvY2);
		}
		
		internal void SetIndex(int index, bool invoke)
		{
			_index = index;
			if (invoke)
			{
				InvokePropertyChanged(nameof(Index));
			}
		}

		internal void SetFromAtlas(Bitmap atlasTexture, Rectangle rect, bool invoke, bool extractImage = true)
		{
			_width = rect.Width;
			_height = rect.Height;
			if (atlasTexture == null)
			{
				_uvX1 = 0f;
				_uvY1 = 0f;
				_uvX2 = 1f;
				_uvY2 = 1f;
				if (extractImage)
				{
					_texture = null;
				}
			}
			else
			{
				_uvX1 = (rect.Left + 0.5f) / atlasTexture.Width;
				_uvY1 = (rect.Top + 0.5f) / atlasTexture.Height;
				_uvX2 = (rect.Right + 0.5f) / atlasTexture.Width;
				_uvY2 = (rect.Bottom + 0.5f) / atlasTexture.Height;
				if (extractImage)
				{
					_texture = atlasTexture?.Clone(rect, atlasTexture.PixelFormat);
				}
			}

			if (!invoke) return;
			InvokePropertyChanged(null);
		}
		
		internal void SetFromImage(Bitmap newImage, bool adjustPivotForPadding, bool invoke)
		{
			int originalWidth = _width;
			int originalHeight = _height;

			if (newImage == null)
			{
				_width = 0;
				_height = 0;
				_texture = null;
			}
			else
			{
				_width = newImage.Width;
				_height = newImage.Height;
				_texture = newImage;

				if (adjustPivotForPadding)
				{
					int dWidth = newImage.Width - originalWidth;
					int dHeight = newImage.Height - originalHeight;
					int xPadding = dWidth / 2;
					int yPadding = dHeight / 2;
					float originalPivotXPixels = _pivotX * originalWidth;
					float originalPivotYPixels = _pivotY * originalHeight;

					_pivotX = (originalPivotXPixels + xPadding) / newImage.Width;
					_pivotY = (originalPivotYPixels + yPadding) / newImage.Height;
				}
			}

			if (!invoke) return;
			InvokePropertyChanged(null);
		}

		internal void Resize(int width, int height, bool invoke)
		{
			if (_width != width || _height != height)
			{
				if (_texture != null)
				{
					int dx = (width - _width)/2;
					int dy = (height - _height)/2;
					
					Bitmap newImage = new Bitmap(width, height, PixelFormat.Format32bppArgb);
					using Graphics g = Graphics.FromImage(newImage);

					g.Clear(Color.FromArgb(0, 0, 0, 0));
					g.DrawImage(_texture, dx, dy);
					_texture.Dispose();
					_texture = newImage;
				}
				
				_width = width;
				_height = height;
				
				if (invoke)
				{
					InvokePropertyChanged(null);
				}
			}
		}

		public Rectangle GetTextureRectangle(int atlasWidth, int atlasHeight)
		{
			// If the UV rect is 0 length on one side it has to be expanded to at least 1 pixel.

			float uv_x2 = UV_X2;
			if (UV_X2 == UV_X1)
			{
				uv_x2 = UV_X1 + 1.0f / atlasWidth;
			}
			float uv_y2 = UV_Y2;
			if (UV_Y2 == UV_Y1)
			{
				uv_y2 = UV_Y1 + 1.0f / atlasHeight;
			}

			int left = Math.Max(0, (int)Math.Floor(UV_X1 * atlasWidth));
			int top = Math.Max(0, (int)Math.Floor(UV_Y1 * atlasHeight));
			int right = Math.Min(atlasWidth, (int)Math.Ceiling(uv_x2 * atlasWidth));
			int bottom = Math.Min(atlasHeight, (int)Math.Ceiling(uv_y2 * atlasHeight));

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

		public PointF GetTexturePivotPoint(float textureWidth, float textureHeight)
		{
			float uvWidth = UV_X2 - UV_X1;
			float uvHeight = UV_Y2 - UV_Y1;

			float imgX = (UV_X2 - (_pivotX * uvWidth)) * textureWidth;
			float imgY = (UV_Y2 - (_pivotY * uvHeight)) * textureHeight;

			return new PointF(imgX, imgY);
		}

		
	}
}
