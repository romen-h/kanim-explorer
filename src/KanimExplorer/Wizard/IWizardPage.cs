using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanimExplorer.Wizard
{
	public interface IWizardPage
	{
		IWizardPage Next();
		void Cancel();
	}
}
