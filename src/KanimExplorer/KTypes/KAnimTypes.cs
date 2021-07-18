using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace KanimExplorer
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

		internal bool NeedsRepack
		{
			get
			{
				foreach (var symbol in Symbols)
				{
					if (symbol.NeedsRepack) return true;
				}
				return false;
			}
		}

		public readonly List<KSymbol> Symbols = new List<KSymbol>();

		public readonly Dictionary<int, string> SymbolNames = new Dictionary<int, string>();

		public string GetSymbolName(int hash)
		{
			if (SymbolNames.ContainsKey(hash))
			{
				return SymbolNames[hash];
			}

			return null;
		}

		public KFrame GetFrame(string name, int index)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("name");
			if (index < 0) throw new ArgumentOutOfRangeException("index");

			foreach (var symbol in Symbols)
			{
				if (symbol.Name == name)
				{
					foreach (var frame in symbol.Frames)
					{
						if (frame.Index == index) return frame;
					}
				}
			}

			return null;
		}
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

		internal bool NeedsRepack
		{
			get
			{
				foreach (var frame in Frames)
				{
					if (frame.NeedsRepack) return true;
				}
				return false;
			}
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

		internal bool NeedsRepack = false;

		/// <summary>
		/// Gets the KSymbol that this KFrame belongs to.
		/// </summary>
		[Browsable(false)]
		public KSymbol Parent
		{ get; private set; }

		[RefreshProperties(RefreshProperties.All)]
		public int SpriteWidth
		{
			get => (int)(PivotWidth / 2);
			set
			{
				PivotWidth = value * 2;
				NeedsRepack = true;
			}
		}

		[RefreshProperties(RefreshProperties.All)]
		public int SpriteHeight
		{
			get => (int)(PivotHeight / 2);
			set
			{
				PivotHeight = value * 2;
				NeedsRepack = true;
			}
		}

		/// <summary>
		/// Gets or sets the index of the KFrame.
		/// </summary>
		[ReadOnly(true)]
		public int Index
		{ get; internal set; }

		/// <summary>
		/// Unknown
		/// </summary>
		[ReadOnly(true)]
		public int Duration
		{ get; set; }

		/// <summary>
		/// Unknown
		/// </summary>
		[ReadOnly(true)]
		public int ImageIndex
		{ get; set; }

		public float PivotX
		{ get; set; }

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

		public RectangleF GetUVRectangle(int width, int height) => RectangleF.FromLTRB(UV_X1 * width, UV_Y1 * height, UV_X2 * width, UV_Y2 * height);

		public RectangleF GetUVRectangle() => RectangleF.FromLTRB(UV_X1, UV_Y1, UV_X2, UV_Y2);

		public void SetNewSize(Rectangle box, int atlasWidth, int atlasHeight)
		{
			PivotWidth = box.Width * 2;
			PivotHeight = box.Height * 2;

			UV_X1 = (float)box.Left / (float)atlasWidth;
			UV_Y1 = (float)box.Top / (float)atlasHeight;
			UV_X2 = (float)box.Right / (float)atlasWidth;
			UV_Y2 = (float)box.Bottom / (float)atlasHeight;
		}

		public PointF GetPivotPoint(float width, float height)
		{
			float pvtX = (PivotX / PivotWidth) + 0.5f;
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

		[ReadOnly(true)]
		public int MaxVisSymbols
		{ get; set; }

		public readonly List<KAnimBank> Banks = new List<KAnimBank>();

		public readonly Dictionary<int, string> BankNames = new Dictionary<int, string>();

		public string GetBankName(int hash)
		{
			if (BankNames.ContainsKey(hash))
			{
				return BankNames[hash];
			}

			return null;
		}
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

		public override string ToString() => Name;
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
		public int SymbolHash
		{ get; set; }

		[ReadOnly(true)]
		public int FrameNumber
		{ get; set; }

		[ReadOnly(true)]
		public int FolderHash
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
		public float M00
		{ get; set; }

		[ReadOnly(true)]
		public float M10
		{ get; set; }

		[ReadOnly(true)]
		public float M01
		{ get; set; }

		[ReadOnly(true)]
		public float M11
		{ get; set; }

		[ReadOnly(true)]
		public float M02
		{ get; set; }

		[ReadOnly(true)]
		public float M12
		{ get; set; }

		[ReadOnly(true)]
		public float Unused
		{ get; set; }
	}
}
