using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace KanimLib
{
	public class KSymbol
	{
		internal KSymbol(string name)
		{
			this.name = name;
			this.Hash = name.KHash();
			this.Path = this.Hash;
		}

		public KSymbol(KBuild parent)
		{
			if (parent == null) throw new ArgumentNullException("parent");
			Parent = parent;
		}

		public static KSymbol Copy(KSymbol original, string newName)
		{
			KSymbol sym = new KSymbol(newName)
			{
				Flags = original.Flags,
				Color = original.Color
			};

			foreach (KFrame frame in original.Frames)
			{
				KFrame frameCopy = KFrame.Copy(frame);
				sym.AddFrame(frameCopy);
			}

			sym.FrameCount = sym.Frames.Count;

			return sym;
		}

		[Browsable(false)]
		public KBuild Parent
		{ get; internal set; }

		private string name = null;

		[ReadOnly(true)]
		public string Name
		{
			get
			{
				if (name != null) return name;

				if (Parent != null && Parent.SymbolNames.ContainsKey(Hash))
				{
					return Parent.SymbolNames[Hash];
				}

				return $"Hash: {Hash}";
			}
			set
			{
				name = value;

				if (Parent != null && Parent.SymbolNames.ContainsKey(Hash))
				{
					Parent.SymbolNames.Remove(Hash);
				}

				Hash = value.KHash();
				Path = value.KHash();

				if (Parent != null)
				{
					Parent.SymbolNames[Hash] = value;
				}
			}
		}

		public int Hash
		{ get; internal set; }

		[ReadOnly(true)]
		public int Path
		{ get; set; }

		public Color Color
		{ get; set; } = Color.FromArgb(0);

		[ReadOnly(true)]
		public SymbolFlags Flags
		{ get; set; } = 0;

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
		{ get; set; } = 0;

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
			set
			{
				foreach (var frame in Frames)
				{
					frame.NeedsRepack = value;
				}
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

		internal void AddFrame(KFrame frame)
		{
			frame.Parent = this;
			Frames.Add(frame);
			FrameCount = Frames.Count;
		}
	}
}
