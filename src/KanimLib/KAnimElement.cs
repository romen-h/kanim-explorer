using System;
using System.ComponentModel;

namespace KanimLib
{
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
