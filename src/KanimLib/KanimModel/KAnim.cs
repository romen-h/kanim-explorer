using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace KanimLib.KanimModel
{
	public class KAnim
	{
		public const string ANIM_HEADER = @"ANIM";

		public const int CURRENT_ANIM_VERSION = 5;

		public KanimPackage Parent
		{ get; internal set; }
		
		[ReadOnly(true)]
		public int Version
		{ get; set; }

		[ReadOnly(true)]
		public int ElementCount
		{ get; set; } = 0;

		[ReadOnly(true)]
		public int FrameCount
		{ get; set; } = 0;

		[ReadOnly(true)]
		public int BankCount
		{ get; set; } = 0;

		[ReadOnly(true)]
		public int MaxVisSymbols
		{ get; set; }

		public readonly List<KAnimBank> Banks = new List<KAnimBank>();

		public IEnumerable<string> BankNames
		{
			get
			{
				foreach (var bank in Banks)
				{
					yield return bank.Name;
				}
			}
		}

		public readonly Dictionary<int, string> SymbolNames = new Dictionary<int, string>();

		public KAnimBank GetBank(string name)
		{
			foreach (var bank in Banks)
			{
				if (bank.Name == name) return bank;
			}

			return null;
		}

		internal void AddBank(KAnimBank bank)
		{
			bank.Parent = this;
			Banks.Add(bank);
			int hash = bank.Name.KHash();
			SymbolNames[hash] = bank.Name;
			BankCount = Banks.Count;
		}

		public void RepairStringsFromBuild(KBuild build)
		{
			foreach (var kvp in build.SymbolNames)
			{
				SymbolNames[kvp.Key] = kvp.Value;
			}
		}
	}
}
