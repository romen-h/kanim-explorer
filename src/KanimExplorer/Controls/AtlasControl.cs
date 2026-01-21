using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KanimLib;
using KanimLib.KanimModel;
using Microsoft.Extensions.Logging;

namespace KanimExplorer.Controls
{
	public partial class AtlasControl : UserControl
	{
		private readonly ILogger _log = KanimLib.Logging.Factory.CreateLogger("AtlasControl");

		private Color _boundsColor = Color.Red;
		public Color BoundsColor
		{
			get => _boundsColor;
			set
			{
				if (_boundsColor != value)
				{
					_boundsColor = value;
					OnBuildUpdated();
				}
			}
		}

		private Color _pivotColor = Color.Yellow;
		public Color PivotColor
		{
			get => _pivotColor;
			set
			{
				if (_pivotColor != value)
				{
					_pivotColor = value;
					OnBuildUpdated();
				}
			}
		}

		private Bitmap _texture;
		public Bitmap Texture
		{
			get => _texture;
			set
			{
				if (_texture != value)
				{
					_texture = value;
					OnTextureUpdated();
				}
			}
		}

		private KBuild _build;
		public KBuild Build
		{
			get => _build;
			set
			{
				if (_build != value)
				{
					_build = value;
					OnBuildUpdated();
				}
			}
		}
		
		private IEnumerable<KFrame> _selectedFrames;
		public IEnumerable<KFrame> SelectedFrames
		{
			get => _selectedFrames;
			set
			{
				_selectedFrames = value;
				OnBuildUpdated();
			}
		}

		public AtlasControl()
		{
			InitializeComponent();
		}

		public void OnTextureUpdated()
		{
			if (_texture == null)
			{
				imageBoxAtlas.VirtualSize = Size.Empty;
				imageBoxAtlas.Text = "No texture loaded.";
				return;
			}
			
			imageBoxAtlas.VirtualSize = _texture.Size;
			imageBoxAtlas.ZoomToFit();
			imageBoxAtlas.Invalidate();
		}

		public void OnBuildUpdated()
		{
			if (_selectedFrames != null && _texture != null)
			{
				KFrame first = _selectedFrames.FirstOrDefault();
				if (first != null)
				{
					var bounds = first.GetTextureRectangleF(_texture.Width, _texture.Height);
					var center = bounds.Center();
					imageBoxAtlas.ScrollTo(center.X, center.Y, Width/2, Height/2);
				}
			}
			imageBoxAtlas.Invalidate();
		}

		private void imageBoxAtlas_VirtualDraw(object sender, PaintEventArgs e)
		{
			if (_texture == null) return;
			
			var viewRect = imageBoxAtlas.GetOffsetRectangle(0, 0, _texture.Width, _texture.Height);
			e.Graphics.TranslateTransform(viewRect.X, viewRect.Y);
			e.Graphics.ScaleTransform((float)imageBoxAtlas.ZoomFactor, (float)imageBoxAtlas.ZoomFactor);

			e.Graphics.DrawImage(_texture, 0, 0);
		
			if (_selectedFrames != null)
			{
				using Pen boundsPen = new Pen(_boundsColor);
				using Pen pivotPen = new Pen(_pivotColor);
				foreach (var frame in _selectedFrames)
				{
					if (frame == null) continue;
					
					Rectangle bounds = frame.GetTextureRectangle(_texture.Width, _texture.Height);
					e.Graphics.DrawRectangle(boundsPen, bounds);

					PointF pivot = frame.GetPivotPoint(_texture.Width, _texture.Height);
					int pivotX = (int)Math.Round(pivot.X);
					int pivotY = (int)Math.Round(pivot.Y);
					e.Graphics.DrawLine(pivotPen, pivotX - 1, pivotY, pivotX + 1, pivotY);
					e.Graphics.DrawLine(pivotPen, pivotX, pivotY - 1, pivotX, pivotY + 1);
				}
			}
		}
	}
}
