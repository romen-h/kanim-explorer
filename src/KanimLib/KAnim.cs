using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace KanimLib
{
	public class KAnim
	{
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
		}
	}
}
