using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KanimExplorer.Settings
{
	public class WindowState
	{
		[JsonInclude]
		public int? ScreenIndex
		{ get; set; }

		[JsonInclude]
		public bool? Maximized
		{ get; set; }

		[JsonInclude]
		public int? Top
		{ get; set; }

		[JsonInclude]
		public int? Left
		{ get; set; }

		[JsonInclude]
		public int? Width
		{ get; set; }

		[JsonInclude]
		public int? Height
		{ get; set; }
		
		public void SetRectangle(int left, int top, int width, int height)
		{
			Left = left;
			Top = top;
			Width = width;
			Height = height;
		}
		
		public void SetRectangle(Rectangle rect) => SetRectangle(rect.Left, rect.Top, rect.Width, rect.Height);
	}
}
