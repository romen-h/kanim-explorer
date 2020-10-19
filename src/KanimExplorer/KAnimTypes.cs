using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;

namespace KanimalExplorer
{
	public class KBuild
	{
		[ReadOnly(true)]
		public string Name
		{ get; set; }

		[ReadOnly(true)]
		public int Version
		{ get; set; }

		[ReadOnly(true)]
		public int SymbolCount
		{ get; set; }

		[ReadOnly(true)]
		public int FrameCount
		{ get; set; }

		public readonly List<KSymbol> Symbols = new List<KSymbol>();

		public readonly Dictionary<int, string> SymbolNames = new Dictionary<int, string>();
	}

	public class KSymbol
	{
		[Flags]
		public enum SymbolFlags : int
		{
			Bloom = 1,
			OnLight = 2,
			SnapTo = 4,
			Foreground = 8
		}

		public KSymbol(KBuild parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		[Browsable(false)]
		public KBuild Parent
		{ get; private set; }

		public string Name
		{
			get
			{
				if (Parent.SymbolNames.ContainsKey(Hash))
					return Parent.SymbolNames[Hash];
				else
					return $"Hash: {Hash}";
			}
		}

		[Browsable(false)]
		public int Hash
		{ get; set; }

		[ReadOnly(true)]
		public int Path
		{ get; set; }

		public Color Color
		{ get; set; }

		[ReadOnly(true)]
		public SymbolFlags Flags
		{ get; set; }

		[RefreshProperties(RefreshProperties.All)]
		public bool Bloom
		{
			get { return Flags.HasFlag(SymbolFlags.Bloom); }
			set { Flags = Flags.SetFlag(SymbolFlags.Bloom, value); }
		}

		[RefreshProperties(RefreshProperties.All)]
		public bool OnLight
		{
			get { return Flags.HasFlag(SymbolFlags.OnLight); }
			set { Flags = Flags.SetFlag(SymbolFlags.OnLight, value); }
		}

		[RefreshProperties(RefreshProperties.All)]
		public bool SnapTo
		{
			get { return Flags.HasFlag(SymbolFlags.SnapTo); }
			set { Flags = Flags.SetFlag(SymbolFlags.SnapTo, value); }
		}

		[RefreshProperties(RefreshProperties.All)]
		public bool Foreground
		{
			get { return Flags.HasFlag(SymbolFlags.Foreground); }
			set { Flags = Flags.SetFlag(SymbolFlags.Foreground, value); }
		}

		[ReadOnly(true)]
		public int FrameCount
		{ get; set; }

		public readonly List<KFrame> Frames = new List<KFrame>();
	}

	public class KFrame
	{
		public KFrame(KSymbol parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		[Browsable(false)]
		public KSymbol Parent
		{ get; private set; }

		[ReadOnly(true)]
		public int Index
		{ get; set; }

		[ReadOnly(true)]
		public int Duration
		{ get; set; }

		[ReadOnly(true)]
		public int ImageIndex
		{ get; set; }

		[ReadOnly(true)]
		public float PivotX
		{ get; set; }

		[ReadOnly(true)]
		public float PivotY
		{ get; set; }

		[ReadOnly(true)]
		public float PivotWidth
		{ get; set; }

		[ReadOnly(true)]
		public float PivotHeight
		{ get; set; }

		[ReadOnly(true)]
		public float UV_X1
		{ get; set; }

		[ReadOnly(true)]
		public float UV_Y1
		{ get; set; }

		[ReadOnly(true)]
		public float UV_X2
		{ get; set; }

		[ReadOnly(true)]
		public float UV_Y2
		{ get; set; }

		[ReadOnly(true)]
		public int Time
		{ get; set; }

		public RectangleF GetUVRectangle(int width, int height)
		{
			return RectangleF.FromLTRB(UV_X1 * width, UV_Y1 * height, UV_X2 * width, UV_Y2 * height);
		}

		public PointF GetPivotPoint(int width, int height)
		{
			float pvtX = -(PivotX / PivotWidth) + 0.5f;
			float pvtY = (PivotY / PivotHeight) + 0.5f;

			float uvWidth = (UV_X2 - UV_X1);
			float uvHeight = (UV_Y2 - UV_Y1);

			float imgX = (UV_X2 - pvtX * uvWidth) * width;
			float imgY = (UV_Y2 - pvtY * uvHeight) * height;

			return new PointF(imgX, imgY);
		}
	}

	public class KAnim
	{
		[ReadOnly(true)]
		public int Version
		{ get; set; }

		[ReadOnly(true)]
		public int ElementCount
		{ get; set; }

		[ReadOnly(true)]
		public int FrameCount
		{ get; set; }

		[ReadOnly(true)]
		public int BankCount
		{ get; set; }

		public readonly List<KAnimBank> Banks = new List<KAnimBank>();
	}

	public class KAnimBank
	{
		public KAnimBank(KAnim parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		[Browsable(false)]
		public KAnim Parent
		{ get; private set; }

		[ReadOnly(true)]
		public string Name
		{ get; set; }

		[ReadOnly(true)]
		public int Hash
		{ get; set; }

		[ReadOnly(true)]
		public float Rate
		{ get; set; }

		[ReadOnly(true)]
		public int FrameCount
		{ get; set; }

		public readonly List<KAnimFrame> Frames = new List<KAnimFrame>();
	}

	public class KAnimFrame
	{
		public KAnimFrame(KAnimBank parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		[Browsable(false)]
		public KAnimBank Parent
		{ get; private set; }

		[ReadOnly(true)]
		public float X
		{ get; set; }

		[ReadOnly(true)]
		public float Y
		{ get; set; }

		[ReadOnly(true)]
		public float Width
		{ get; set; }

		[ReadOnly(true)]
		public float Height
		{ get; set; }

		[ReadOnly(true)]
		public int ElementCount
		{ get; set; }

		public readonly List<KAnimElement> Elements = new List<KAnimElement>();
	}

	public class KAnimElement
	{
		public KAnimElement(KAnimFrame parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		[Browsable(false)]
		public KAnimFrame Parent
		{ get; private set; }

		[ReadOnly(true)]
		public int ImageHash
		{ get; set; }

		[ReadOnly(true)]
		public int Index
		{ get; set; }

		[ReadOnly(true)]
		public int Layer
		{ get; set; }

		[ReadOnly(true)]
		public int Flags
		{ get; set; }

		[ReadOnly(true)]
		public float Alpha
		{ get; set; }

		[ReadOnly(true)]
		public float Red
		{ get; set; }

		[ReadOnly(true)]
		public float Green
		{ get; set; }

		[ReadOnly(true)]
		public float Blue
		{ get; set; }

		[ReadOnly(true)]
		public float M1
		{ get; set; }

		[ReadOnly(true)]
		public float M2
		{ get; set; }

		[ReadOnly(true)]
		public float M3
		{ get; set; }

		[ReadOnly(true)]
		public float M4
		{ get; set; }

		[ReadOnly(true)]
		public float M5
		{ get; set; }

		[ReadOnly(true)]
		public float M6
		{ get; set; }

		[ReadOnly(true)]
		public float Order
		{ get; set; }
	}
}
