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
	public partial class StartPage : UserControl, IWizardPage
	{
		public IWizardPage Next()
		{
			if (radioButtonBuildingPlaceholder.Checked)
			{
				return new BuildingPlaceholderGeneratorPage();
			}
			else if (radioButtonPackSprites.Checked)
			{
				return new SpritePackerPage();
			}
			else if (radioButtonTileGenerator.Checked)
			{
				return new TileAtlasGeneratorPage();
			}

			return null;
		}

		public void Cancel()
		{ }

		public StartPage()
		{
			InitializeComponent();
		}
	}
}
