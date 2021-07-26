using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KanimExplorer.Wizard
{
	public partial class WizardForm : Form
	{
		private IWizardPage CurrentPage;

		public WizardForm()
		{
			InitializeComponent();
			SetPage(new StartPage());
		}

		private void SetPage(IWizardPage page)
		{
			panelPageArea.Controls.Clear();

			UserControl ctrl = page as UserControl;
			if (ctrl == null) return;

			ctrl.Dock = DockStyle.Fill;
			panelPageArea.Controls.Add(ctrl);

			CurrentPage = page;
		}

		private void WizardForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (CurrentPage != null)
			{
				CurrentPage.Cancel();
				CurrentPage = null;
			}
		}

		private void buttonCancel_Click(object sender, EventArgs e)
		{
			if (CurrentPage != null)
			{
				CurrentPage.Cancel();
				CurrentPage = null;
			}
			Close();
		}

		private void buttonNext_Click(object sender, EventArgs e)
		{
			if (CurrentPage != null)
			{
				IWizardPage nextPage;
				try
				{
					nextPage = CurrentPage.Next();
				}
				catch (Exception ex)
				{
					MessageBox.Show(this, $"Error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}

				if (nextPage != null)
				{
					SetPage(nextPage);
				}
				else
				{
					MessageBox.Show("Complete.");
					Close();
				}
			}
		}
	}
}
