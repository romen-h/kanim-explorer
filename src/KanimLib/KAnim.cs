using System.Collections.Generic;
using System.ComponentModel;

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

		public readonly Dictionary<int, string> BankNames = new Dictionary<int, string>();

		public string GetBankName(int hash)
		{
			if (BankNames.ContainsKey(hash))
			{
				return BankNames[hash];
			}

			return null;
		}

		internal void AddBank(KAnimBank bank)
		{
			bank.Parent = this;
			Banks.Add(bank);
			int hash = bank.Name.KHash();
			BankNames[hash] = bank.Name;
			BankCount = Banks.Count;
		}
	}
}
