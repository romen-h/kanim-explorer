using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KanimExplorer.Forms
{
	public partial class RenameSymbolForm : Form
	{
		private readonly IEnumerable<string> _originalSymbolNames;

		public string OldName
		{ get; private set; }

		public string NewName
		{ get; private set; }

		public RenameSymbolForm(IEnumerable<string> originalSymbolNames)
		{
			_originalSymbolNames = originalSymbolNames;

			InitializeComponent();

			foreach (var name in originalSymbolNames)
			{
				oldNamesComboBox.Items.Add(name);
			}
		}

		private void StringPromptForm_Load(object sender, EventArgs e)
		{
			DialogResult = DialogResult.None;
			buttonOK.Enabled = false;
		}

		private void oldNamesComboBox_SelectedValueChanged(object sender, EventArgs e)
		{
			OldName = oldNamesComboBox.SelectedItem as string;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			NewName = textBox.Text;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private bool ValidateName(string str)
		{
			if (OldName == null) return false;
			if (string.IsNullOrWhiteSpace(str)) return false;
			if (str.Contains(' ')) return false;
			if (_originalSymbolNames.Contains(str)) return false;

			return true;
		}

		private void textBox_TextChanged(object sender, EventArgs e)
		{
			if (ValidateName(textBox.Text))
			{
				buttonOK.Enabled = true;
			}
			else
			{
				buttonOK.Enabled = false;
			}
		}
	}
}
