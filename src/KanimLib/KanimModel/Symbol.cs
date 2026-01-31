using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.Json.Nodes;

namespace KanimLib.KanimModel
{
	public class Symbol : INotifyPropertyChanged
	{
		private int _hash;
		private string _name;
		private int _path;
		private Color _color;
		private SymbolFlags _flags;
		private readonly List<Sprite> _sprites = new List<Sprite>();

		[Description("The hash of the symbol name.")]
		public int Hash => _hash;

		public string Name => _name;

		[Description("The hash of something unknown. Probably unused.")]
		public int Path => _path;

		[Description("Sets the tint color of the symbol.")]
		public Color Color
		{
			get => _color;
			set
			{
				if (_color != value)
				{
					_color = value;
					InvokePropertyChanged(nameof(Color));
				}
			}
		}

		public SymbolFlags Flags => _flags;

		[Description("Toggles whether the sprite will glow in game.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Bloom
		{
			get => _flags.HasFlag(SymbolFlags.Bloom);
			set
			{
				_flags = _flags.SetFlag(SymbolFlags.Bloom, value);
				InvokePropertyChanged(nameof(Flags));
				InvokePropertyChanged(nameof(Bloom));
			}
		}

		[Description("Toggles whether the symbol is used as an on light.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool OnLight
		{
			get => _flags.HasFlag(SymbolFlags.OnLight);
			set
			{
				_flags = _flags.SetFlag(SymbolFlags.OnLight, value);
				InvokePropertyChanged(nameof(Flags));
				InvokePropertyChanged(nameof(OnLight));
			}
		}

		[Description("Toggles whether the symbol is used as a snap target.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool SnapTo
		{
			get => _flags.HasFlag(SymbolFlags.SnapTo);
			set
			{
				_flags = _flags.SetFlag(SymbolFlags.SnapTo, value);
				InvokePropertyChanged(nameof(Flags));
				InvokePropertyChanged(nameof(SnapTo));
			}
		}

		[Description("Toggles whether the symbol is draw in a separate layer from the rest of the kanim.")]
		[RefreshProperties(RefreshProperties.All)]
		public bool Foreground
		{
			get => _flags.HasFlag(SymbolFlags.Foreground);
			set
			{
				_flags = _flags.SetFlag(SymbolFlags.Foreground, value);
				InvokePropertyChanged(nameof(Flags));
				InvokePropertyChanged(nameof(Foreground));
			}
		}

		public IReadOnlyList<Sprite> Sprites => _sprites;

		public event PropertyChangedEventHandler PropertyChanged;
		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		internal Symbol(Stream stream, int buildVersion)
		{
			using BinaryReader reader = new BinaryReader(stream, Encoding.ASCII, true);
			
			_hash = reader.ReadInt32();
			_path = buildVersion > 9 ? reader.ReadInt32() : 0;
			_color = reader.ReadColor32();
			_flags = reader.ReadKSymbolFlags();
			int frameCount = reader.ReadInt32();

			for (int i = 0; i < frameCount; i++)
			{
				Sprite frame = new Sprite(stream);
				frame.PropertyChanged += OnPropertyChanged;
				_sprites.Add(frame);
			}
			Debug.Assert(_sprites.Count == frameCount);
		}
		
		internal Symbol(Symbol other)
		{
			_hash = other._hash;
			_name = other._name;
			_color = other._color;
			_flags = other._flags;
			
			foreach (var otherFrame in other._sprites)
			{
				Sprite frame = new Sprite(otherFrame);
				frame.PropertyChanged += OnPropertyChanged;
				_sprites.Add(frame);
			}
		}
		
		private Symbol(string name, Color color, SymbolFlags flags)
		{
			_name = name;
			_hash = KleiUtil.HashString(name);
			_color = color;
			_flags = flags;
		}
		
		internal static Symbol MakeStandalone(string name, IEnumerable<Sprite> sprites = null, Color? color = null, SymbolFlags? flags = null)
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace(name);
			
			Symbol symbol = new Symbol(name, color ?? Color.White, flags ?? SymbolFlags.None);

			if (sprites != null)
			{
				foreach (var sprite in sprites)
				{
					symbol.AddSprite(sprite, false);
				}
			}
			
			return symbol;
		}
		
		internal void SetSpriteTexturesFromAtlasTexture(Bitmap texture)
		{
			foreach (var sprite in _sprites)
			{
				sprite.SetFromAtlas(texture, sprite.GetTextureRectangle(texture.Width, texture.Height), false);
			}
		}
		
		internal void WriteToKleiBuildBytes(Stream stream, int buildVersion)
		{
			using BinaryWriter writer = new BinaryWriter(stream, Encoding.ASCII, true);

			writer.Write(_hash);
			if (buildVersion > 9) writer.Write(_path);
			writer.Write(_color);
			writer.Write(_flags);
			writer.Write(_sprites.Count);
			
			foreach (var frame in _sprites)
			{
				frame.WriteToKleiBuildBytes(stream);
			}
		}
		
		internal void ExportSprites(string outputFolder, JsonObject spritesMetadata = null)
		{
			Directory.CreateDirectory(outputFolder);
			
			foreach (var sprite in _sprites)
			{
				string fileName = $"{_name}_{sprite.Index}.png";
				string filePath = System.IO.Path.Combine(outputFolder, fileName);
				using FileStream fs = File.Create(filePath);
				sprite.WritePng(fs);
				
				if (spritesMetadata != null)
				{
					JsonObject spriteJson = new JsonObject();
					spriteJson["width"] = sprite.Width;
					spriteJson["height"] = sprite.Height;
					spriteJson["pivotX"] = sprite.PixelPivotX;
					spriteJson["pivotY"] = sprite.PixelPivotY;
					spritesMetadata[fileName] = spriteJson;
				}
			}
		}
		
		internal void SetName(string name, bool invoke)
		{
			_hash = KleiUtil.HashString(name);
			_name = name;
			
			if (invoke)
			{
				InvokePropertyChanged(nameof(Hash));
				InvokePropertyChanged(nameof(Name));
			}
		}
		
		internal void AutoFlag(bool invoke)
		{
			string lowerName = _name.ToLowerInvariant();
			if (lowerName.Contains("_bloom"))
			{
				_flags.SetFlag(SymbolFlags.Bloom, true);
			}
			if (lowerName.Contains("_fg"))
			{
				_flags.SetFlag(SymbolFlags.Foreground, true);
			}
			
			if (invoke)
			{
				InvokePropertyChanged(nameof(Flags));
				InvokePropertyChanged(nameof(Bloom));
				InvokePropertyChanged(nameof(Foreground));
			}
		}

		internal void AddSprite(Sprite sprite, bool invoke, bool setIndex = true)
		{
			_sprites.Add(sprite);
			if (setIndex)
			{
				sprite.SetIndex(_sprites.Count - 1, false);
			}
			sprite.PropertyChanged += OnPropertyChanged;
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Sprites));
		}
		
		internal void RemoveSprite(Sprite sprite, bool invoke)
		{
			if (_sprites.Remove(sprite))
			{
				sprite.PropertyChanged -= OnPropertyChanged;
				sprite.SetIndex(-1, false);
				for (int i=0; i<_sprites.Count; i++)
				{
					_sprites[i].SetIndex(i, false);
				}
				
				if (!invoke) return;
				InvokePropertyChanged(nameof(Sprites));
			}
		}
		
		internal void MoveSpriteUp(Sprite sprite, bool invoke)
		{
			int index = _sprites.IndexOf(sprite);
			if (index <= 0) return;
			
			var swapped = _sprites[index - 1];
			_sprites[index - 1] = sprite;
			_sprites[index] = swapped;
			
			sprite.SetIndex(index - 1, false);
			swapped.SetIndex(index, false);
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Sprites));
		}

		internal void MoveFrameDown(Sprite sprite, bool invoke)
		{
			int index = _sprites.IndexOf(sprite);
			if (index < 0) return;
			if (index ==  _sprites.Count - 1) return;

			var swapped = _sprites[index + 1];
			_sprites[index + 1] = sprite;
			_sprites[index] = swapped;

			sprite.SetIndex(index + 1, false);
			swapped.SetIndex(index, false);
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Sprites));
		}

		internal void ReplaceSprite(int index, Bitmap newImage, bool adjustForPadding, bool invoke)
		{
			if (index < 0 || index >= _sprites.Count) throw new ArgumentOutOfRangeException(nameof(index));
			
			var sprite = _sprites[index];
			sprite.SetFromImage(newImage, adjustForPadding, false);
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Sprites));
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			InvokePropertyChanged(nameof(Sprites));
		}
	}
}
