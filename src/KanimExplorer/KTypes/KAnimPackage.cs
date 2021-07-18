using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanimExplorer
{
	public class KAnimPackage
	{
		public Bitmap Texture;
		public KBuild Build;
		public KAnim Anim;

		public bool HasTexture => Texture != null;
		public bool HasBuild => Build != null;
		public bool HasAnim => Anim != null;

		public bool HasAnyData => (HasTexture || HasBuild || HasAnim);
		public bool IsValidAtlas => (HasTexture && HasBuild);
		public bool IsComplete => (HasTexture && HasBuild && HasAnim);
	}
}
