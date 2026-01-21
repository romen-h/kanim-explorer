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
using SpriterDotNet;

namespace KanimExplorer.Controls
{
	public partial class SpriteControl : UserControl
	{
		private enum SelectionContext
		{
			None,
			SetPivot
		}

		private readonly ILogger _log = KanimLib.Logging.Factory.CreateLogger("SpriteControl");

		private SelectionContext _ctx = SelectionContext.None;

		private Color _pivotColor = Color.Yellow;
		public Color PivotColor
		{
			get => _pivotColor;
			set
			{
				if (_pivotColor != value)
				{
					_pivotColor = value;
					OnFrameUpdated();
				}
			}
		}

		private KFrame _frame;
		public KFrame Frame
		{
			get => _frame;
			set
			{
				if (_frame != value)
				{
					_frame = value;
					OnFrameUpdated();
				}
			}
		}

		public event EventHandler FramePivotUpdated;

		public SpriteControl()
		{
			InitializeComponent();
		}

		public void OnFrameUpdated()
		{ if (Frame == null || Frame.Sprite == null)
			{
				imageBoxSprite.Text = "No sprite selected.";
				imageBoxSprite.Image = null;
				return;
			}

			imageBoxSprite.Text = Frame.FrameName;

			Bitmap spriteBmp = Frame.Sprite.Image;

			int pivotX = (int)Math.Round(Frame.SpriterPivotX * spriteBmp.Width);
			int pivotY = (int)Math.Round(Frame.SpriterPivotY * spriteBmp.Height);

			Bitmap bmp = (Bitmap)spriteBmp.Clone();
			using (Graphics g = Graphics.FromImage(bmp))
			using (Pen pen = new Pen(_pivotColor))
			{
				g.DrawLine(pen, pivotX - 1, pivotY, pivotX + 1, pivotY);
				g.DrawLine(pen, pivotX, pivotY - 1, pivotX, pivotY + 1);
			}

			imageBoxSprite.Image = bmp;
		}

		private void buttonZoomOutSprite_Click(object sender, EventArgs e)
		{
			imageBoxSprite.ZoomOut(true);
		}

		private void buttonZoomInSprite_Click(object sender, EventArgs e)
		{
			imageBoxSprite.ZoomIn(true);
		}

		private void buttonResetZoomSprite_Click(object sender, EventArgs e)
		{
			imageBoxSprite.Zoom = 100;
		}

		private void checkBoxEditPivot_CheckedChanged(object sender, EventArgs e)
		{
			if (checkBoxEditPivot.Checked)
			{
				_ctx = SelectionContext.SetPivot;
			}
			else
			{
				_ctx = SelectionContext.None;
			}
		}

		private void buttonExportSprite_Click(object sender, EventArgs e)
		{
			KanimPackage kanim = DocumentManager.Instance.Data;
			UIUtils.TryWithErrorMessage(() => ModalTasks.ExportSprite(kanim, Frame, _log), "Exporting Sprite");
		}

		private void imageBoxSprite_MouseDown(object sender, MouseEventArgs e)
		{
			if (Frame == null) return;

			if (_ctx == SelectionContext.SetPivot)
			{
				PointF imagePos = imageBoxSprite.PointToImage(e.Location);
				Frame.SpriterPivotX = imagePos.X / Frame.SpriteWidth;
				Frame.SpriterPivotY = imagePos.Y / Frame.SpriteHeight;
				FramePivotUpdated?.Invoke(this, EventArgs.Empty);
				OnFrameUpdated();
			}
		}
	}
}
