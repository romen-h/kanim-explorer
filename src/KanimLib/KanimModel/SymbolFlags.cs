using System;

namespace KanimLib.KanimModel
{
	[Flags]
	public enum SymbolFlags : int
	{
		None = 0,
		/// <summary>
		/// A flag that determines whether the symbol will be rendered with a bloom shader.
		/// </summary>
		Bloom = 1,
		/// <summary>
		/// A flag that determines whether the symbol will be displayed based on whether a building is operational.
		/// </summary>
		OnLight = 2,
		/// <summary>
		/// A flag that determines whether the symbol is a snapping point for other objects.
		/// </summary>
		SnapTo = 4,
		/// <summary>
		/// A flag that determines whether the symbol will be drawn on a foreground layer (in front of dupes).
		/// </summary>
		Foreground = 8
	}

	public static class SymbolFlagsExtensions
	{
		public static SymbolFlags SetFlag(this SymbolFlags flags, SymbolFlags flag, bool value)
		{
			int flagsInt = (int)flags;
			int flagInt = (int)flag;
			if (value)
			{
				flagsInt |= flagInt;
			}
			else
			{
				flagsInt &= ~flagInt;
			}
			return (SymbolFlags)flagsInt;
		}
	}
}
