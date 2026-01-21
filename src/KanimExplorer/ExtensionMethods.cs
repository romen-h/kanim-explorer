using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanimExplorer
{
	internal static class ExtensionMethods
	{
		internal static Point Center(this Rectangle rect)
		{
			return new Point(rect.X + rect.Width/2, rect.Y + rect.Height/2);
		}
		
		internal static PointF Center(this RectangleF rect)
		{
			return new PointF(rect.X + rect.Width/2f, rect.Y + rect.Height/2f);
		}
	}
}
