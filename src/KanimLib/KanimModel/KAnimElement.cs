using System;
using System.ComponentModel;

using KanimLib.KanimModel;

namespace KanimLib
{
	public class KAnimElement
	{
		internal KAnimElement()
		{ }

		public KAnimElement(KAnimFrame parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		public static KAnimElement Copy(KAnimElement original)
		{
			KAnimElement element = new KAnimElement()
			{
				SymbolHash = original.SymbolHash,
				FrameNumber = original.FrameNumber,
				FolderHash = original.FolderHash,
				Flags = original.Flags,
				Alpha = original.Alpha,
				Red = original.Red,
				Green = original.Green,
				Blue = original.Blue,
				M00 = original.M00,
				M01 = original.M01,
				M02 = original.M02,
				M10 = original.M10,
				M11 = original.M11,
				M12 = original.M12,
				Unused = original.Unused
			};

			return element;
		}

		[Browsable(false)]
		public KAnimFrame Parent
		{ get; internal set; }

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
		{ get; set; } = 0;

		[ReadOnly(true)]
		public float Alpha
		{ get; set; } = 1.0f;

		[ReadOnly(true)]
		public float Red
		{ get; set; } = 1.0f;

		[ReadOnly(true)]
		public float Green
		{ get; set; } = 1.0f;

		[ReadOnly(true)]
		public float Blue
		{ get; set; } = 1.0f;

		[ReadOnly(true)]
		public float M00
		{ get; set; } = 1;

		[ReadOnly(true)]
		public float M10
		{ get; set; } = 0;

		[ReadOnly(true)]
		public float M01
		{ get; set; } = 0;

		[ReadOnly(true)]
		public float M11
		{ get; set; } = 1;

		[ReadOnly(true)]
		public float M02
		{ get; set; } = 0;

		[ReadOnly(true)]
		public float M12
		{ get; set; } = 0;

		[ReadOnly(true)]
		public float Unused
		{ get; set; }
	}
}
