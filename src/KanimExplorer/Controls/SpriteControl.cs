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

		private bool _resettingUI = false;

		private SelectionContext _ctx = SelectionContext.None;
		private float _temporaryPivotX = float.NaN;
		private float _temporaryPivotY = float.NaN;

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
					_temporaryPivotX = float.NaN;
					_temporaryPivotY = float.NaN;
					ResetPivotEditing();
					OnFrameUpdated();
				}
			}
		}

		public event EventHandler FramePivotUpdated;

		public SpriteControl()
		{
			InitializeComponent();
		}

		public void ResetPivotEditing()
		{
			_resettingUI = true;
			_ctx = SelectionContext.None;
			checkBoxEditPivot.Checked = false;
			buttonApplyPivot.Enabled = false;
			buttonResetPivot.Enabled = false;
			_resettingUI = false;
		}

		public void OnFrameUpdated()
		{
			if (Frame == null || Frame.Sprite == null)
			{
				imageBoxSprite.Text = "No sprite selected.";
				imageBoxSprite.Image = null;
				return;
			}

			imageBoxSprite.Text = Frame.FrameName;

			Bitmap spriteBmp = Frame.Sprite.Image;

			float tempPivotX = Frame.SpriterPivotX;
			float tempPivotY = Frame.SpriterPivotY;
			if (!float.IsNaN(_temporaryPivotX) && !float.IsNaN(_temporaryPivotY))
			{
				tempPivotX = _temporaryPivotX;
				tempPivotY = _temporaryPivotY;
			}

			int pivotX = (int)(tempPivotX * Frame.SpriteWidth);
			int pivotY = (int)(tempPivotY * Frame.SpriteHeight);

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
			if (_resettingUI) return;

			if (checkBoxEditPivot.Checked)
			{
				_ctx = SelectionContext.SetPivot;
				OnFrameUpdated();
			}
			else
			{
				_ctx = SelectionContext.None;
				_temporaryPivotX = float.NaN;
				_temporaryPivotY = float.NaN;
				buttonApplyPivot.Enabled = false;
				buttonResetPivot.Enabled = false;
				OnFrameUpdated();
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
				_temporaryPivotX = imagePos.X / Frame.SpriteWidth;
				_temporaryPivotY = imagePos.Y / Frame.SpriteHeight;
				buttonApplyPivot.Enabled = true;
				buttonResetPivot.Enabled = true;
				OnFrameUpdated();
			}
		}

		private void buttonApplyPivot_Click(object sender, EventArgs e)
		{
			if (Frame == null) return;
			if (float.IsNaN(_temporaryPivotX)) return;
			if (float.IsNaN(_temporaryPivotY)) return;

			Frame.SpriterPivotX = _temporaryPivotX;
			Frame.SpriterPivotY = _temporaryPivotY;
			FramePivotUpdated?.Invoke(this, EventArgs.Empty);
			OnFrameUpdated();

			_temporaryPivotX = float.NaN;
			_temporaryPivotY = float.NaN;
			buttonApplyPivot.Enabled = false;
			buttonResetPivot.Enabled = false;
		}

		private void buttonResetPivot_Click(object sender, EventArgs e)
		{
			if (Frame == null) return;
			_temporaryPivotX = float.NaN;
			_temporaryPivotY = float.NaN;
			OnFrameUpdated();

			buttonApplyPivot.Enabled = false;
			buttonResetPivot.Enabled = false;
		}

		private void buttonChangePivotColor_Click(object sender, EventArgs e)
		{
			ColorDialog dlg = new ColorDialog()
			{
				Color = _pivotColor
			};
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				PivotColor = dlg.Color;
			}
		}
	}
}
