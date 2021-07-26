using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace KanimLib
{
	public class KSymbol
	{
		public KSymbol()
		{ }

		public KSymbol(KBuild parent)
		{
			if (parent == null) throw new ArgumentNullException("parent");
			Parent = parent;
		}

		[Browsable(false)]
		public KBuild Parent
		{ get; private set; }

		public string Name
		{
			get
			{
				if (Parent != null && Parent.SymbolNames.ContainsKey(Hash))
				{
					return Parent.SymbolNames[Hash];
				}

				return $"Hash: {Hash}";
			}
		}

		public int Hash
		{ get; internal set; }

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

		/// <summary>
		/// Gets whether the symbol data has changed in a way that requires the texture to be repacked.
		/// </summary>
		public bool NeedsRepack
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

#if false
		public void SetName(string name)
		{
			int hash = name.KHash();

			if (Parent != null && Parent.SymbolNames.ContainsKey(hash))
			{
				Parent.SymbolNames.
			}
		}
#endif
	}
}
