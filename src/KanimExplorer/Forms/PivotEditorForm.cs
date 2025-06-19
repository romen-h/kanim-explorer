using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KanimExplorer.OpenGL.Objects;

using KanimLib;

namespace KanimExplorer.Forms
{
	public partial class PivotEditorForm : Form
	{
		private bool _mousePressed = false;

		private readonly KFrame _originalFrame;
		private readonly Bitmap _originalTexture;

		public float PivotX
		{ get; private set; }

		public float PivotY
		{ get; private set; }

		public PivotEditorForm([NotNull] KFrame frame, [NotNull] Bitmap texture)
		{
			InitializeComponent();

			_originalFrame = frame;
			PivotX = frame.SpriterPivotX;
			textBox1.Text = PivotX.ToString("F5");

			PivotY = frame.SpriterPivotY;
			textBox2.Text  = PivotY.ToString("F6");

			_originalTexture = texture;

			UpdatePictureBox();
		}

		private void okButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}

		private void cancelButton_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			_mousePressed = true;
			UpdatePivotFromControlCoordinates(e.X, e.Y);
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			_mousePressed = false;
		}

		private void pictureBox_MouseLeave(object sender, EventArgs e)
		{
			_mousePressed = false;
		}

		private void pictureBox_MouseMove(object sender, MouseEventArgs e)
		{
			if (_mousePressed)
			{
				UpdatePivotFromControlCoordinates(e.X, e.Y);
			}
		}

		private void UpdatePivotFromControlCoordinates(int x, int y)
		{
			float boxWidth = pixelPictureBox.ClientSize.Width;
			float boxHeight = pixelPictureBox.ClientSize.Height;
			float imgWidth = pixelPictureBox.Image.Width;
			float imgHeight = pixelPictureBox.Image.Height;

			float boxAspect = boxWidth / boxHeight;
			float imgAspect = imgWidth / imgHeight;

			float scale;
			if (boxAspect >= imgAspect)
			{
				scale = boxHeight / imgHeight;
			}
			else
			{
				scale = boxWidth / imgWidth;
			}

			float imageLeft = (boxWidth - (imgWidth * scale)) / 2f;
			float imageTop = (boxHeight - (imgHeight * scale)) / 2f;

			float pivotXPixel = (x - imageLeft) / scale;
			PivotX = pivotXPixel / imgWidth;
			textBox1.Text = PivotX.ToString("F5");

			float pivotYPixel = (y - imageTop) / scale;
			PivotY = pivotYPixel / imgHeight;
			textBox2.Text = PivotY.ToString("F5");

			UpdatePictureBox();
		}

		private void UpdatePictureBox()
		{
			Rectangle cropRect = _originalFrame.GetTextureRectangle(_originalTexture.Width, _originalTexture.Height);
			Bitmap cropped = new Bitmap(cropRect.Width, cropRect.Height, PixelFormat.Format32bppArgb);

			using (Graphics g = Graphics.FromImage(cropped))
			{
				g.DrawImage(_originalTexture, 0, 0, cropRect, GraphicsUnit.Pixel);

				if (!float.IsNaN(PivotX) && !float.IsNaN(PivotY))
				{
					int pivotXPixel = (int)(PivotX * cropRect.Width);
					int pivotYPixel = (int)(PivotY * cropRect.Height);
					if (pivotXPixel >= 0 && pivotXPixel < cropped.Width && pivotYPixel >= 0 && pivotYPixel < cropped.Height)
					{
						Color color = cropped.GetPixel(pivotXPixel, pivotYPixel);
						Color invertedColor = Color.FromArgb(255, 255 - color.R, 255 - color.G, 255 - color.B);
						using (Pen pen = new Pen(invertedColor))
						{
							g.DrawLine(pen, pivotXPixel - 1, pivotYPixel, pivotXPixel + 1, pivotYPixel);
							g.DrawLine(pen, pivotXPixel, pivotYPixel - 1, pivotXPixel, pivotYPixel + 1);
						}
					}
				}
			}

			pixelPictureBox.Image = cropped;
		}
	}
}
