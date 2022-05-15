using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KanimLib
{
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

		[RefreshProperties(RefreshProperties.All)]
		public float Rate
		{ get; set; }

		[ReadOnly(true)]
		public int FrameCount
		{ get; set; }

		public readonly List<KAnimFrame> Frames = new List<KAnimFrame>();

		public override string ToString() => Name;
	}
}
