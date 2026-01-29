using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection.Metadata.Ecma335;
using KanimLib.KanimModel;
using KanimLib.Sprites;

namespace KanimLib
{
	/// <summary>
	/// Contains the texture, build, and anim data for the Klei animation format.
	/// </summary>
	public class KanimPackage
	{
		private readonly List<Bitmap> _textures = new List<Bitmap>();
		private readonly List<KBuild> _builds = new List<KBuild>();
		private readonly List<KAnim> _anims = new List<KAnim>();
		private readonly List<Sprite> _sprites = new List<Sprite>();
		private readonly Dictionary<string, List<Sprite>> _spriteAtlases = new Dictionary<string, List<Sprite>>();
		
		/// <summary>
		/// The texture data.
		/// </summary>
		public Bitmap Texture
		{ get; private set; }
		/// <summary>
		/// The symbol data.
		/// </summary>
		public KBuild Build
		{ get; private set; }
		/// <summary>
		/// The animation data.
		/// </summary>
		public KAnim Anim
		{ get; private set; }
		
		public IReadOnlyList<Bitmap> Textures => _textures;
		
		public IReadOnlyList<KBuild> Builds => _builds;
		
		public IReadOnlyList<KAnim> Anims => _anims;
		
		public IReadOnlyDictionary<string,IReadOnlyList<Sprite>> SpriteAtlases => (IReadOnlyDictionary<string, IReadOnlyList<Sprite>>)_spriteAtlases;
		
		/// <summary>
		/// The sprite instances that exist in the current build + texture data.
		/// </summary>
		public IReadOnlyList<Sprite> Sprites => _sprites;
		
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
		public bool HasAnyData => HasTexture || HasBuild || HasAnim;
		/// <summary>
		/// Gets whether this package has enough data to fully represent a texture atlas.
		/// </summary>
		public bool IsValidAtlas => HasTexture && HasBuild;
		/// <summary>
		/// Gets whether this package is a complete set of animation data.
		/// </summary>
		public bool IsComplete => HasTexture && HasBuild && HasAnim;
		
		public event EventHandler TextureChanged;
		public event EventHandler BuildChanged;
		public event EventHandler AnimChanged;
		
		public KanimPackage(Bitmap texture = null, KBuild build = null, KAnim anim = null)
		{
			Texture = texture;
			Build = build;
			Anim = anim;
			_sprites = IsValidAtlas ? SpriteUtils.BuildSprites(Texture, Build) : [];

			if (Build != null)
			{
				Build.Parent = this;
			}
			
			if (Anim != null)
			{
				Anim.Parent = this;
			}
		}
		
		internal KanimPackage(Bitmap texture, KBuild build, KAnim anim, List<Sprite> sprites)
		{
			if (texture == null) throw new ArgumentNullException(nameof(texture));
			if (build == null) throw new ArgumentNullException(nameof(build));
			if (anim == null) throw new ArgumentNullException(nameof(anim));
			if (sprites == null) throw new ArgumentNullException(nameof(sprites));
			
			Texture = texture;
			Build = build;
			Build.Parent = this;
			Anim = anim;
			Anim.Parent = this;
			_sprites = sprites;
		}
		
		public void SetTexture(Bitmap texture, bool invoke = true)
		{
			Texture = texture;

			_sprites.Clear();
			
			if (IsValidAtlas)
			{
				_sprites.AddRange(SpriteUtils.BuildSprites(Texture, Build));
			}
			else
			{
				if (Build != null)
				{
					foreach (var symbol in Build.Symbols)
					{
						foreach (var frame in symbol.Frames)
						{
							frame.Sprite = null;
						}
					}
				}
			}
			
			if (invoke)
			{
				TextureChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		
		public void SetBuild(KBuild build, bool invoke = true)
		{
			if (Build != null)
			{
				Build.Parent = null;
			}
			
			Build = build;
			
			if (Build != null)
			{
				Build.Parent = this;
			}

			_sprites.Clear();

			if (IsValidAtlas)
			{
				_sprites.AddRange(SpriteUtils.BuildSprites(Texture, Build));
			}
			else
			{
				if (Build != null)
				{
					foreach (var symbol in Build.Symbols)
					{
						foreach (var frame in symbol.Frames)
						{
							frame.Sprite = null;
						}
					}
				}
			}
			
			if (invoke)
			{
				BuildChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		
		public void SetAnim(KAnim anim, bool invoke = true)
		{
			if (Anim != null)
			{
				Anim.Parent = null;
			}
			
			Anim = anim;
			
			if (Anim != null)
			{
				Anim.Parent = this;
			}
			
			if (invoke)
			{
				AnimChanged?.Invoke(this, EventArgs.Empty);
			}
		}
		
		internal void RebuildAtlas()
		{
			SetTexture(SpriteUtils.RebuildAtlas(Sprites));
			BuildChanged?.Invoke(this, EventArgs.Empty);
		}
		
		internal void AddSprite(Sprite sprite)
		{
			_sprites.Add(sprite);
		}
		
		internal void RemoveSprite(Sprite sprite)
		{
			_sprites.Remove(sprite);
		}
	}
}
