using System;
using System.Collections.Generic;
using System.Text;

namespace KanimLib
{
	public class Bone
	{
		public string ID
		{ get; set; }

		public Bone ParentBone
		{ get; set; }

		public float Length
		{ get; set; }

		public float Thickness
		{ get; set; }

		private readonly List<object> Objects = new List<object>();

		private readonly List<Bone> ChildBones = new List<Bone>();

		public void AttachSymbol(object symbol)
		{
			Objects.Add(symbol);
		}

		public void AttachBone(Bone bone)
		{
			ChildBones.Add(bone);
			bone.ParentBone = this;
		}
	}

	public class BoneTree
	{
		public Bone RootBone = new Bone();
	}
}
