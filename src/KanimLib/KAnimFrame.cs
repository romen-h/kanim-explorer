using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace KanimLib
{
	public class KAnimFrame
	{
		internal KAnimFrame()
		{ }

		public KAnimFrame(KAnimBank parent)
		{
			if (parent == null) throw new ArgumentNullException();
			Parent = parent;
		}

		[Browsable(false)]
		public KAnimBank Parent
		{ get; internal set; }

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

		internal void AddElement(KAnimElement element)
		{
			element.Parent = this;
			Elements.Add(element);
			ElementCount = Elements.Count;
		}
	}
}
