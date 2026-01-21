using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KanimLib;

namespace KanimExplorer.Forms
{
	public partial class SymbolDuplicatorForm : Form
	{
		private readonly KanimPackage kAnimPackage;

		public SymbolDuplicatorForm(KanimPackage pkg)
		{
			InitializeComponent();

			kAnimPackage = pkg;

			buttonOK.Enabled = false;

			if (pkg.HasBuild)
			{
				foreach (var symbol in pkg.Build.Symbols)
				{
					ListViewItem lvi = new ListViewItem();
					lvi.Text = symbol.Name;
					listViewSymbols.Items.Add(lvi);
				}
			}

			if (pkg.HasAnim)
			{
				foreach (var bank in pkg.Anim.Banks)
				{
					ListViewItem lvi = new ListViewItem();
					lvi.Text = bank.Name;
					listViewAnimations.Items.Add(lvi);
				}
			}

			ValidateOptions();
		}

		private void buttonSelectAllSymbols_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in listViewSymbols.Items)
			{
				lvi.Checked = true;
			}
			
			ValidateOptions();
		}

		private void buttonSelectNoneSymbols_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in listViewSymbols.Items)
			{
				lvi.Checked = false;
			}

			ValidateOptions();
		}

		private void listViewSymbols_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			ValidateOptions();
		}

		private void buttonSelectAllAnims_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in listViewAnimations.Items)
			{
				lvi.Checked = true;
			}

			ValidateOptions();
		}

		private void buttonSelectNoneAnims_Click(object sender, EventArgs e)
		{
			foreach (ListViewItem lvi in listViewAnimations.Items)
			{
				lvi.Checked = false;
			}

			ValidateOptions();
		}

		private void listViewAnimations_ItemChecked(object sender, ItemCheckedEventArgs e)
		{
			ValidateOptions();
		}

		private void numericUpDownZOffset_ValueChanged(object sender, EventArgs e)
		{
			ValidateOptions();
		}

		private void textBoxPrefix_TextChanged(object sender, EventArgs e)
		{
			ValidateOptions();
		}

		private void textBoxSuffix_TextChanged(object sender, EventArgs e)
		{
			ValidateOptions();
		}

		private void checkBoxInvisibleCopies_CheckedChanged(object sender, EventArgs e)
		{
			ValidateOptions();
		}

		private void ValidateOptions()
		{
			buttonOK.Enabled = false;

			bool symbolsSelected = false;
			foreach (ListViewItem lvi in listViewSymbols.Items)
			{
				if (lvi.Checked)
				{
					symbolsSelected = true;
					break;
				}
			}
			if (!symbolsSelected) return;

			bool animsSelected = false;
			foreach (ListViewItem lvi in listViewAnimations.Items)
			{
				if (lvi.Checked)
				{
					animsSelected = true;
					break;
				}
			}
			if (!animsSelected) return;

			int zOffset = (int)numericUpDownZOffset.Value;
			if (zOffset == 0) return;

			string prefix = textBoxPrefix.Text;
			string suffix = textBoxSuffix.Text;
			if (string.IsNullOrWhiteSpace(prefix) && string.IsNullOrWhiteSpace(suffix)) return;

			buttonOK.Enabled = true;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			// Do Duplicate
			List<string> selectedSymbols = new List<string>();
			foreach (ListViewItem lvi in listViewSymbols.Items)
			{
				if (lvi.Checked)
				{
					selectedSymbols.Add(lvi.Text);
				}
			}
			List<string> selectedBanks = new List<string>();
			foreach (ListViewItem lvi in listViewAnimations.Items)
			{
				if (lvi.Checked)
				{
					selectedBanks.Add(lvi.Text);
				}
			}
			int zOffset = (int)numericUpDownZOffset.Value;
			string prefix = textBoxPrefix.Text;
			string suffix = textBoxSuffix.Text;
			bool invisible = checkBoxInvisibleCopies.Checked;

			try
			{
				KAnimUtils.DuplicateSymbols(kAnimPackage, selectedSymbols, selectedBanks, prefix, suffix, zOffset, invisible);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			Close();
		}
	}
}
