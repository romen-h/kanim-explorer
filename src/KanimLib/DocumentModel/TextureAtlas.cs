using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanimLib.Serialization;
using KanimLib.Sprites;

namespace KanimLib.DocumentModel
{
	/// <summary>
	/// Wraps a Bitmap and a KBuild instance that are meant to be loaded and saved together.
	/// </summary>
	public class TextureAtlas : IDocument
	{
		private readonly List<Sprite> _sprites = new List<Sprite>();
		
		public bool UnsavedChanges
		{ get; private set; }
		
		public string TextureFile
		{ get; private set; }

		public string BuildFile
		{ get; private set; }
		
		public IEnumerable<string> Paths
		{
			get
			{
				yield return TextureFile;
				yield return BuildFile;
			}
		}

		public Bitmap Texture
		{ get; private set; }

		public KBuild Build
		{ get; private set; }
		
		public IReadOnlyList<Sprite> Sprites => _sprites;
		
		public event PropertyChangedEventHandler PropertyChanged;
		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public TextureAtlas(string textureFile, string buildFile)
		{
			ArgumentNullException.ThrowIfNull(textureFile);
			ArgumentNullException.ThrowIfNull(buildFile);
			if (!File.Exists(textureFile)) throw new ArgumentException("Texture file does not exist.");
			if (!File.Exists(buildFile)) throw new ArgumentException("Build file does not exist.");

			TextureFile = textureFile;
			BuildFile = buildFile;
			Reload();
			
			BuildSpritesFromTexture();
		}
		
		public TextureAtlas(Bitmap texture, KBuild build)
		{
			ArgumentNullException.ThrowIfNull(texture);
			ArgumentNullException.ThrowIfNull(build);
			
			TextureFile = null;
			BuildFile = null;
			
			Texture = (Bitmap)texture.Clone();
			Build = build;
			
			BuildSpritesFromTexture();
		}
		
		public void SetChangesFlag()
		{
			UnsavedChanges = true;
			InvokePropertyChanged(nameof(UnsavedChanges));
		}
		
		public bool Reload()
		{
			// Cleanup old data
			if (Texture != null)
			{
				Texture.Dispose();
				Texture = null;
			}
			
			if (Build != null)
			{
				// Dispose build
				Build = null;
			}
			
			_sprites.Clear();
			
			// Load new data
			Texture = (Bitmap)(new Bitmap(TextureFile)).Clone();
			
			Build = KanimReader.ReadBuild(BuildFile);
			
			BuildSpritesFromTexture();
			
			// Inform
			InvokePropertyChanged(nameof(Texture));
			InvokePropertyChanged(nameof(Build));
			InvokePropertyChanged(nameof(Sprites));
			
			return true;
		}
		
		public bool Save() => SaveAs(TextureFile, BuildFile);
		
		public bool SaveAs(params string[] paths)
		{
			if (paths == null || paths.Length != 2) throw new ArgumentException("TextureAtlas.SaveAs requires 2 path arguments.");
			
			Texture.Save(TextureFile, ImageFormat.Png);
			
			KanimWriter.WriteBuild(BuildFile, Build);
			
			return true;
		}
		
		private void BuildSpritesFromTexture()
		{
			if (Texture == null) throw new InvalidOperationException("Texture is null.");
			if (Build == null) throw new InvalidOperationException("Build is null.");
			
			Debug.Assert(_sprites.Count == 0, "The caller did not clear the _sprites list before calling BuildSpritesFromTexture().");

			_sprites.AddRange(SpriteUtils.BuildSprites(Texture, Build));
		}
		
		private void BuildTextureFromSprites()
		{
			if (Build == null) throw new InvalidOperationException("Build is null.");
			if (_sprites.Count == 0) throw new InvalidOperationException("Sprites have not been built yet.");
			
			if (Texture != null)
			{
				Texture.Dispose();
				Texture = null;
			}
			
			Texture = SpriteUtils.RebuildAtlas(_sprites);
			InvokePropertyChanged(nameof(Texture));
			SetChangesFlag();
		}
	}
}
