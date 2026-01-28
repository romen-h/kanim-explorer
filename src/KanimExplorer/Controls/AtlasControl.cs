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

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KanimExplorer.Controls
{
	public partial class AtlasControl : UserControl
	{
		private readonly ILogger _log = KanimLib.Logging.Factory.CreateLogger("AtlasControl");

		private Bitmap _texture = null;
		private KBuild _build = null;
		private IEnumerable<KFrame> _selectedFrames = null;

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

		public AtlasControl()
		{
			InitializeComponent();

			DocumentManager.Instance.LoadedTextureChanged += DocumentManager_LoadedTextureChanged;
			DocumentManager.Instance.LoadedBuildChanged += DocumentManager_LoadedBuildChanged;
			DocumentManager.Instance.SelectedObjectChanged += DocumentManager_SelectedObjectChanged;
		}

		private void DocumentManager_LoadedTextureChanged(object sender, EventArgs e)
		{
			var texture = DocumentManager.Instance.Data?.Texture;
			if (_texture != texture)
			{
				_texture = texture;
				OnTextureUpdated();
			}
		}

		private void DocumentManager_LoadedBuildChanged(object sender, EventArgs e)
		{
			var build = DocumentManager.Instance.Data?.Build;
			if (_build != build)
			{
				_build = build;
				OnBuildUpdated();
			}
		}

		private void DocumentManager_SelectedObjectChanged(object sender, SelectedObjectChangedEventArgs e)
		{
			if (_texture == null || _build == null) return;
			
			switch (e.Object)
			{
				case KSymbol symbol:
					_selectedFrames = symbol.Frames;
					break;
				
				case KFrame frame:
					_selectedFrames = [frame];
					break;

				case KAnimFrame animFrame:
					List<KFrame> framesInAnim = new List<KFrame>();
					foreach (KAnimElement element in animFrame.Elements)
					{
						KSymbol symbol2 = _build.GetSymbol(element.SymbolHash);
						if (symbol2 != null)
						{
							if (symbol2.FrameCount > element.FrameNumber)
							{
								KFrame frame = symbol2.Frames[element.FrameNumber];
								framesInAnim.Add(frame);
							}
						}
					}
					_selectedFrames = framesInAnim;
					break;

				case KAnimElement element:
					KFrame frameInElement = null;
					KSymbol symbol3 = _build.GetSymbol(element.SymbolHash);
					if (symbol3 != null)
					{
						if (symbol3.FrameCount > element.FrameNumber)
						{
							frameInElement = symbol3.Frames[element.FrameNumber];
						}
					}
					_selectedFrames = [frameInElement];
					break;

				default:
					return;
			}
			
			OnBuildUpdated();
		}

		public void OnTextureUpdated()
		{
			if (_texture == null)
			{
				imageBoxAtlas.VirtualSize = Size.Empty;
				imageBoxAtlas.Text = "No texture loaded.";
				imageBoxAtlas.Invalidate();
				return;
			}
			else
			{
				imageBoxAtlas.Text = null;
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
