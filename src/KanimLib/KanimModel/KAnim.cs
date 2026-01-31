using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;

namespace KanimLib.KanimModel
{
	public class KAnim : INotifyPropertyChanged
	{
		public const string ANIM_HEADER = @"ANIM";

		public const int CURRENT_ANIM_VERSION = 5;

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

		public event PropertyChangedEventHandler PropertyChanged;
		private void InvokePropertyChanged(string propertyName)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		
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

		internal void MoveBankUp(KAnimBank bank)
		{
			int index = Banks.IndexOf(bank);
			if (index <= 0) return;

			KAnimBank temp = Banks[index - 1];
			Banks[index - 1] = bank;
			Banks[index] = temp;
		}

		internal void MoveBankDown(KAnimBank bank)
		{
			int index = Banks.IndexOf(bank);
			if (index < 0) return;
			if (index >= Banks.Count - 1) return;

			KAnimBank temp = Banks[index + 1];
			Banks[index + 1] = bank;
			Banks[index] = temp;
		}
		
		public bool ValidateUILastOrAbsent()
		{
			for (int i=0; i<Banks.Count; i++)
			{
				var bank = Banks[i];
				if (bank.Name == "ui")
				{
					if (i != Banks.Count - 1) return false;
				}
			}
			
			return true;
		}
		
		public void EnsureUILast()
		{
			KAnimBank uiBank = GetBank("ui");
			if (uiBank == null) return;
			
			Banks.Remove(uiBank);
			Banks.Add(uiBank);
		}

		public void RepairStringsFromBuild(TextureAtlas build)
		{
			foreach (var symbol in build.Symbols)
			{
				SymbolNames[symbol.Hash] = symbol.Name;
			}
		}
	}
}
