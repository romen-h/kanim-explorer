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
	public partial class StringPromptForm : Form
	{
		private readonly Func<string,bool> _validator;
		
		public string Value
		{ get; private set; }

		public StringPromptForm(string prompt, string currentValue = null, Func<string,bool> stringValidator = null)
		{
			InitializeComponent();
			
			labelPrompt.Text = prompt;
			
			if (currentValue != null)
			{
				textBox.Text = currentValue;
			}
			
			_validator = stringValidator;
		}

		private void StringPromptForm_Load(object sender, EventArgs e)
		{
			DialogResult = DialogResult.None;
			buttonOK.Enabled = false;
		}

		private void buttonOK_Click(object sender, EventArgs e)
		{
			Value = textBox.Text;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void textBox_TextChanged(object sender, EventArgs e)
		{
			if (_validator == null || _validator(textBox.Text))
			{
				textBox.BackColor = Color.White;
				buttonOK.Enabled = true;
			}
			else
			{
				textBox.BackColor = Color.Red;
				buttonOK.Enabled = false;
			}
		}

		private void textBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (_validator == null || _validator(textBox.Text))
				{
					textBox.BackColor = Color.White;
					Value = textBox.Text;
					DialogResult = DialogResult.OK;
					Close();
				}
				else
				{
					textBox.BackColor = Color.Red;
				}
			}
		}
	}
}
