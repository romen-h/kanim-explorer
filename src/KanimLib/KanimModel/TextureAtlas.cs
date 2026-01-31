using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

using KanimLib.Sprites;

namespace KanimLib.KanimModel
{
	/// <summary>
	/// Contains information about a texture and the sprites within it.
	/// </summary>
	/// <remarks>This class maintains the "KBuild" data structure, but also tightly couples the associated texture.</remarks>
	public class TextureAtlas : INotifyPropertyChanged
	{
		/// <summary>
		/// A 4 character sequence used to identify whether a file contains build data.
		/// </summary>
		private const string BUILD_HEADER = @"BILD";
		/// <summary>
		/// The current version number of build files included with Oxygen Not Included.
		/// </summary>
		private const int CURRENT_BUILD_VERSION = 10;
		
		private string _buildFilePath = null;
		private string _textureFilePath = null;

		private string _name;
		private readonly List<Symbol> _symbols = new List<Symbol>();
		private Bitmap _texture;
		
		public string BuildFilePath => _buildFilePath;
		
		public string TextureFilePath => _textureFilePath;
		
		public string Name => _name;

		private int SymbolCount => _symbols.Count;

		private int FrameCount => _symbols.Sum(s => s.Sprites.Count);

		public IReadOnlyList<Symbol> Symbols => _symbols;
		
		public IEnumerable<Sprite> AllSprites
		{
			get
			{
				foreach (var symbol in _symbols)
				{
					foreach (var sprite in symbol.Sprites)
					{
						yield return sprite;
					}
				}
			}
		}
		
		public Bitmap Texture => _texture;

		public event PropertyChangedEventHandler PropertyChanged;
		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public TextureAtlas(string buildFile, string textureFile)
		{
			if (buildFile == null && textureFile == null) throw new ArgumentException("buildFile and textureFile are both null.");
			if (buildFile != null && !File.Exists(buildFile)) throw new ArgumentException("Build file does not exist.");
			if (textureFile != null && !File.Exists(textureFile)) throw new ArgumentException("Texture file does not exist.");

			if (buildFile != null)
			{
				using FileStream fs = new FileStream(buildFile, FileMode.Open);
				ReadFromKleiBuildBytes(fs);
				fs.Close();
			}
			
			if (textureFile != null)
			{
				_texture = (Bitmap)(new Bitmap(textureFile)).Clone();
			}
			
			SetSpritesFromTexture();
		}

		private TextureAtlas(string name, IEnumerable<Symbol> symbols, Bitmap texture)
		{
			_buildFilePath = null;
			_textureFilePath = null;
			_name = name;
			if (symbols != null)
			{
				_symbols.AddRange(symbols);
			}
			_texture = texture;
		}

		internal static TextureAtlas MakeFromStandalone(string name, IEnumerable<Symbol> symbols)
		{
			TextureAtlas atlas = new TextureAtlas(name, null, null);
			
			foreach (var symbol in symbols)
			{
				atlas.AddSymbol(symbol, false, false);
			}
			
			atlas.SetTextureFromSprites();
			
			return atlas;
		}

		private void ReadFromKleiBuildBytes(Stream stream)
		{
			using BinaryReader reader = new BinaryReader(stream, Encoding.ASCII, true);
			string header = Encoding.ASCII.GetString(reader.ReadBytes(TextureAtlas.BUILD_HEADER.Length));
			if (header != TextureAtlas.BUILD_HEADER) throw new Exception("Header bytes are not valid for a build file.");

			int version = reader.ReadInt32(); // Throw away, we always re-save version 10 for ONI
			int symbolCount = reader.ReadInt32(); // Keep temporarily to validate we read everything properly
			int frameCount = reader.ReadInt32(); // "
			_name = reader.ReadKString();

			for (int i = 0; i < symbolCount; i++)
			{
				Symbol symbol = new Symbol(stream, version);
				symbol.PropertyChanged += OnPropertyChanged;
				_symbols.Add(symbol);
			}

			Debug.Assert(FrameCount == frameCount);

			int numHashes = reader.ReadInt32();
			for (int i = 0; i < numHashes; i++)
			{
				int hash = reader.ReadInt32();
				string str = reader.ReadKString();
				Symbol symbol = GetSymbol(hash);
				if (symbol != null)
				{
					symbol.SetName(str, false);
				}
				else
				{
					// String shouldn't be in the dictionary
					Debugger.Break();
				}
			}
		}

		private void SetSpritesFromTexture()
		{
			if (_texture == null) throw new InvalidOperationException("Texture is null.");

			foreach (var symbol in _symbols)
			{
				symbol.SetSpriteTexturesFromAtlasTexture(_texture);
			}
		}

		private void SetTextureFromSprites()
		{
			if (_texture != null)
			{
				_texture.Dispose();
				_texture = null;
			}

			var sprites = AllSprites;

			SpriteUtils.PackedSprite[] packedSprites = SpriteUtils.Pack(sprites, out int atlasWidth, out int atlasHeight);

			Bitmap newAtlas = new Bitmap(atlasWidth, atlasHeight, PixelFormat.Format32bppArgb);
			
			using Graphics g = Graphics.FromImage(newAtlas);
			g.Clear(Color.FromArgb(0, 0, 0, 0));
			
			foreach (SpriteUtils.PackedSprite packed in packedSprites)
			{
				g.DrawImage(packed.Sprite.Texture, packed.Position);
				packed.Sprite.SetFromAtlas(newAtlas, packed.BoundingBox, false, extractImage: false);
			}

			_texture = newAtlas;
		}

		internal void WriteToPng(Stream stream)
		{
			if (_texture == null) return;
		}
		
		public void Save(string filePath)
		{
			using FileStream fs = File.Create(filePath);
			WriteToKleiBuildBytes(fs);
		}
		
		internal void WriteToKleiBuildBytes(Stream stream)
		{
			using BinaryWriter writer = new BinaryWriter(stream, Encoding.ASCII, true);

			writer.Write(Encoding.ASCII.GetBytes(BUILD_HEADER));

			writer.Write(CURRENT_BUILD_VERSION);
			writer.Write(SymbolCount);
			writer.Write(FrameCount);
			writer.WriteKString(Name);
			
			foreach (var symbol in _symbols)
			{
				symbol.WriteToKleiBuildBytes(stream, CURRENT_BUILD_VERSION);
			}
			
			writer.Write(_symbols.Count);
			
			foreach (var symbol in _symbols)
			{
				writer.Write(symbol.Hash);
				writer.WriteKString(symbol.Name);
			}
		}
		
		public void ExportSprites(string outputFolder)
		{
			Directory.CreateDirectory(outputFolder);
			
			JsonObject spriteMetadata = new JsonObject();
			foreach (var symbol in _symbols)
			{
				symbol.ExportSprites(outputFolder, spriteMetadata);
			}
			
			string spritesFile = Path.Combine(outputFolder, "sprites.json");

			string jsonStr = spriteMetadata.ToJsonString(new JsonSerializerOptions()
			{
				WriteIndented = true
			});
			
			File.WriteAllText(spritesFile, jsonStr, Encoding.UTF8);
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			InvokePropertyChanged(nameof(Symbols));
		}

		/// <summary>
		/// Gets the symbol with the given name hash.
		/// </summary>
		/// <returns>Null if the symbol is not found.</returns>
		public Symbol GetSymbol(int hash)
		{
			foreach (var symbol in Symbols)
			{
				if (symbol.Hash == hash) return symbol;
			}

			return null;
		}

		/// <summary>
		/// Returns the symbol for the given name.
		/// </summary>
		/// <returns>Null if the symbol is not found.</returns>
		public Symbol GetSymbol(string name)
		{
			ArgumentNullException.ThrowIfNullOrWhiteSpace(name);

			return GetSymbol(KleiUtil.HashString(name));
		}

		/// <summary>
		/// Returns the symbol name for the given hash.
		/// </summary>
		/// <returns>Null if the name is not found.</returns>
		public string GetSymbolName(int hash)
		{
			if (GetSymbol(hash) is not Symbol symbol) return null;
			return symbol.Name;
		}
		
		/// <summary>
		/// Returns whether the symbol with the given hash exists.
		/// </summary>
		public bool SymbolExists(int hash) => GetSymbol(hash) != null;

		/// <summary>
		/// Returns whether the symbol with the given name exists.
		/// </summary>
		public bool SymbolExists(string name) => GetSymbol(name) != null;
		
		public bool TryGetSymbol(int hash, out Symbol symbol)
		{
			symbol = GetSymbol(hash);
			return symbol != null;
		}
		
		public bool TryGetSymbol(string name, out Symbol symbol)
		{
			symbol = GetSymbol(name);
			return symbol != null;
		}

		/// <summary>
		/// Returns the KFrame for the given name and sub-image index.
		/// </summary>
		/// <returns>Null if the frame is not found.</returns>
		public Sprite GetFrame(string name, int index)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("name");

			foreach (var symbol in Symbols)
			{
				if (symbol.Name == name)
				{
					foreach (var frame in symbol.Sprites)
					{
						if (frame.Index == index) return frame;
					}
				}
			}

			return null;
		}

		public void AddSymbol(Symbol symbol, bool invoke, bool repack = true)
		{
			_symbols.Add(symbol);
			symbol.PropertyChanged += OnPropertyChanged;
			
			if (repack)
			{
				SetTextureFromSprites();
			}
			
			if (!invoke) return;
			InvokePropertyChanged(null);
		}

		public void RemoveSymbol(string symbolName, bool invoke, bool repack = true)
		{
			if (!TryGetSymbol(symbolName, out Symbol symbolToRemove)) return; // Already gone!
			RemoveSymbol(symbolToRemove, invoke, repack);
		}

		public void RemoveSymbol(Symbol symbol, bool invoke, bool repack = true)
		{
			if (_symbols.Remove(symbol))
			{
				symbol.PropertyChanged -= OnPropertyChanged;
				
				if (repack)
				{
					SetTextureFromSprites();
				}
				
				if (!invoke) return;
				InvokePropertyChanged(null);
			}
		}

		public void RemoveMultipleSymbols(IEnumerable<Symbol> symbols, bool invoke, bool repack = true)
		{
			foreach (var symbolToRemove in symbols)
			{
				RemoveSymbol(symbolToRemove, invoke);
			}
			
			if (repack)
			{
				SetTextureFromSprites();
			}
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Symbols));
		}

		public void InsertSymbolAfter(Symbol inserted, Symbol after, bool invoke, bool repack = true)
		{
			int insertIndex = _symbols.IndexOf(after) + 1;
			_symbols.Insert(insertIndex, inserted);
			inserted.PropertyChanged += OnPropertyChanged;
			
			if (repack)
			{
				SetTextureFromSprites();
			}
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Symbols));
		}

		public void MoveSymbolUp(Symbol symbol, bool invoke)
		{
			int index = _symbols.IndexOf(symbol);
			if (index <= 0) return;
			
			Symbol swapped = _symbols[index - 1];
			_symbols[index - 1] = symbol;
			_symbols[index] = swapped;
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Symbols));
		}

		public void MoveSymbolDown(Symbol symbol, bool invoke)
		{
			int index = _symbols.IndexOf(symbol);
			if (index < 0) return;
			if (index >= _symbols.Count - 1) return;
			
			Symbol temp = _symbols[index + 1];
			_symbols[index + 1] = symbol;
			_symbols[index] = temp;
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Symbols));
		}
		
		public void AutoFlagSymbols(bool invoke)
		{
			foreach (var symbol in _symbols)
			{
				symbol.AutoFlag(invoke);
			}
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Symbols));
		}
		
		public void RenameSymbol(string oldSymbolName, string newSymbolName, bool invoke)
		{
			if (!TryGetSymbol(oldSymbolName, out Symbol symbolToRename)) throw new InvalidOperationException("Symbol with name \"oldSymbolName\" does not exist.");

			symbolToRename.SetName(newSymbolName, invoke);
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Symbols));
		}
		
		public void DuplicateSymbol(string symbolName, bool invoke)
		{
			throw new NotImplementedException();
		}
		
		public void ReplaceSprite(Sprite sprite, Bitmap newImage, bool adjustForPadding, bool invoke, bool repack = true)
		{
			bool found = false;
			foreach (var symbol in _symbols)
			{
				if (symbol.Sprites.Contains(sprite))
				{
					found = true;
					symbol.ReplaceSprite(sprite.Index, newImage, adjustForPadding, invoke);
					break;
				}
			}
			
			if (!found) return;

			if (repack)
			{
				SetTextureFromSprites();
			}

			if (!invoke) return;
			InvokePropertyChanged(nameof(Texture));
			InvokePropertyChanged(nameof(Symbols));
		}

		public void ReplaceSprite(string symbolName, int index, Bitmap newImage, bool adjustForPadding, bool invoke, bool repack = true)
		{
			if (!TryGetSymbol(symbolName, out Symbol symbol)) throw new InvalidOperationException("Symbol with name \"oldSymbolName\" does not exist.");
			symbol.ReplaceSprite(index, newImage, adjustForPadding, invoke);
			
			if (repack)
			{
				SetTextureFromSprites();
			}
			
			if (!invoke) return;
			InvokePropertyChanged(nameof(Texture));
			InvokePropertyChanged(nameof(Symbols));
		}
	}
}
