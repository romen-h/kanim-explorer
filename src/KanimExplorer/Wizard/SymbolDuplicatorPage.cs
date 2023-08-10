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

namespace KanimExplorer.Wizard
{
	public partial class SymbolDuplicatorPage : UserControl, IWizardPage
	{
		public SymbolDuplicatorPage()
		{
			InitializeComponent();
		}

		internal void Load(KAnimPackage pkg)
		{
		}

		public IWizardPage Next()
		{
			return null;
		}

		public void Cancel()
		{
			
		}
	}
}
