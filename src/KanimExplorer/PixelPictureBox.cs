using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KanimExplorer
{
	public class PixelPictureBox : PictureBox
	{
		protected override void OnPaint(PaintEventArgs pe)
		{
			pe.Graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
			pe.Graphics.PixelOffsetMode = PixelOffsetMode.Half;
			base.OnPaint(pe);
		}
	}
}
