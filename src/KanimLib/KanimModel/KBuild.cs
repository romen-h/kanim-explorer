using System;
using System.Collections.Generic;
using System.ComponentModel;

using KanimLib.KanimModel;

namespace KanimLib
{
	/// <summary>
	/// Contains a hierarchy of data representing the sprites used in an animation.
	/// </summary>
	public class KBuild : INotifyPropertyChanged
	{
		/// <summary>
		/// A 4 character sequence used to identify whether a file contains build data.
		/// </summary>
		public const string BUILD_HEADER = @"BILD";
		/// <summary>
		/// The current version number of build files included with Oxygen Not Included.
		/// </summary>
		public const int CURRENT_BUILD_VERSION = 10;

		public KanimPackage Parent
		{ get; internal set; }
		
		/// <summary>
		/// Gets or sets the name of the animation.
		/// </summary>
		/// <remarks>This does not seem to be used by ONI code.</remarks>
		[ReadOnly(true)]
		public string Name
		{ get; set; } = "Uninitialized_Name";

		/// <summary>
		/// Gets or sets the version of the build data.
		/// </summary>
		[ReadOnly(true)]
		public int Version
		{ get; set; } = CURRENT_BUILD_VERSION;

		/// <summary>
		/// Gets or sets the number of symbols in the build data.
		/// </summary>
		[ReadOnly(true)]
		public int SymbolCount
		{ get; set; } = 0;

		/// <summary>
		/// Gets or sets the total number of frames in the build data.
		/// </summary>
		[ReadOnly(true)]
		public int FrameCount
		{ get; set; } = 0;

		/// <summary>
		/// A list of symbols used by this animation.
		/// </summary>
		public readonly List<KSymbol> Symbols = new List<KSymbol>();

		/// <summary>
		/// A dictionary of names for the symbols indexed by their KHash.
		/// </summary>
		public readonly Dictionary<int, string> SymbolNames = new Dictionary<int, string>();
		
		public event PropertyChangedEventHandler PropertyChanged;
		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public KBuild()
		{ }
		
		public void TriggerAtlasRebuild()
		{
			Parent?.RebuildAtlas();
		}

		/// <summary>
		/// Returns the symbol name for the given hash.
		/// </summary>
		/// <returns>Null if the name is not found.</returns>
		public string GetSymbolName(int hash)
		{
			if (SymbolNames.ContainsKey(hash))
			{
				return SymbolNames[hash];
			}

			return null;
		}

		public bool SymbolExists(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));

			foreach (var symbol in Symbols)
			{
				if (symbol.Name == name) return true;
			}
			
			return false;
		}
		
		/// <summary>
		/// Returns the symbol for the given name.
		/// </summary>
		/// <returns>Null if the symbol is not found.</returns>
		public KSymbol GetSymbol(string name)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException(nameof(name));

			foreach (var symbol in Symbols)
			{
				if (symbol.Name == name) return symbol;
			}

			return null;
		}

		public KSymbol GetSymbol(int hash)
		{
			foreach (var symbol in Symbols)
			{
				if (symbol.Hash == hash) return symbol;
			}

			return null;
		}

		/// <summary>
		/// Returns the KFrame for the given name and sub-image index.
		/// </summary>
		/// <returns>Null if the frame is not found.</returns>
		public KFrame GetFrame(string name, int index)
		{
			if (string.IsNullOrEmpty(name)) throw new ArgumentException("name");

			foreach (var symbol in Symbols)
			{
				if (symbol.Name == name)
				{
					foreach (var frame in symbol.Frames)
					{
						if (frame.Index == index) return frame;
					}
				}
			}

			return null;
		}

		internal void AddSymbol(KSymbol symbol)
		{
			symbol.Parent = this;
			Symbols.Add(symbol);
			int hash = symbol.Name.KHash();
			SymbolNames[hash] = symbol.Name;
			SymbolCount = Symbols.Count;
		}
		
		internal void InsertSymbolAfter(KSymbol inserted, KSymbol after)
		{
			inserted.Parent = this;
			
			int insertIndex = Symbols.IndexOf(after) + 1;
			Symbols.Insert(insertIndex, inserted);
			
			int hash = inserted.Name.KHash();
			SymbolNames[hash] = inserted.Name;
			SymbolCount = Symbols.Count;
		}
	}
}
