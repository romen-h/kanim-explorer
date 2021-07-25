using System.Drawing;

namespace KanimLib
{
	/// <summary>
	/// Contains the texture, build, and anim data for the Klei animation format.
	/// </summary>
	public class KAnimPackage
	{
		/// <summary>
		/// The texture data.
		/// </summary>
		public Bitmap Texture;
		/// <summary>
		/// The symbol data.
		/// </summary>
		public KBuild Build;
		/// <summary>
		/// The animation data.
		/// </summary>
		public KAnim Anim;

		/// <summary>
		/// Gets whether this package has texture data.
		/// </summary>
		public bool HasTexture => Texture != null;
		/// <summary>
		/// Gets whether this package has symbol data.
		/// </summary>
		public bool HasBuild => Build != null;
		/// <summary>
		/// Gets whether this package has animation data.
		/// </summary>
		public bool HasAnim => Anim != null;
		/// <summary>
		/// Gets whether this package has any data.
		/// </summary>
		public bool HasAnyData => (HasTexture || HasBuild || HasAnim);
		/// <summary>
		/// Gets whether this package has enough data to fully represent a texture atlas.
		/// </summary>
		public bool IsValidAtlas => (HasTexture && HasBuild);
		/// <summary>
		/// Gets whether this package is a complete set of animation data.
		/// </summary>
		public bool IsComplete => (HasTexture && HasBuild && HasAnim);
	}
}
