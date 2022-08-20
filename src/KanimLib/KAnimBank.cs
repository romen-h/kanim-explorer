using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KanimLib
{
	public class KAnimBank
	{
		internal KAnimBank(string name)
		{
			this.Name = name;
			this.Hash = name.KHash();
		}

		public KAnimBank(KAnim parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		[Browsable(false)]
		public KAnim Parent
		{ get; internal set; }

		[ReadOnly(true)]
		public string Name
		{ get; set; }

		[ReadOnly(true)]
		public int Hash
		{ get; set; }

		[RefreshProperties(RefreshProperties.All)]
		public float Rate
		{ get; set; } = 30;

		[ReadOnly(true)]
		public int FrameCount
		{ get; set; } = 0;

		public readonly List<KAnimFrame> Frames = new List<KAnimFrame>();

		public override string ToString() => Name;

		internal void AddFrame(KAnimFrame frame)
		{
			frame.Parent = this;
			Frames.Add(frame);
			FrameCount = Frames.Count;
		}
	}
}
