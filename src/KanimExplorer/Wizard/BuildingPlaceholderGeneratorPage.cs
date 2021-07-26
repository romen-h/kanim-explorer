using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KanimLib;

namespace KanimExplorer.Wizard
{
	public partial class BuildingPlaceholderGeneratorPage : UserControl, IWizardPage
	{
		public BuildingPlaceholderGeneratorPage()
		{
			InitializeComponent();
		}

		public IWizardPage Next()
		{
			string buildingName = textBoxName.Text;
			int buildingWidth = (int)numericUpDownWidth.Value;
			int buildingHeight = (int)numericUpDownHeight.Value;
			string outDir = textBoxOutputPath.Text;

			if (string.IsNullOrWhiteSpace(buildingName)) throw new Exception("Building name must not be empty.");
			if (buildingWidth < 1) throw new Exception("Building width must not be less than 1.");
			if (buildingHeight < 1) throw new Exception("Building height must not be less than 1.");
			if (string.IsNullOrWhiteSpace(outDir) || !Directory.Exists(outDir)) throw new Exception("Output directory is not valid.");

			string texFile = Path.Combine(outDir, $"{buildingName}_0.png");
			string buildFile = Path.Combine(outDir, $"{buildingName}_build.bytes");
			string animFile = Path.Combine(outDir, $"{buildingName}_anim.bytes");

			try
			{
				AnimFactory.MakePlaceholderBuilding(buildingName, buildingWidth, buildingHeight, out Bitmap tex, out KBuild build, out KAnim anim);
				tex.Save(texFile, ImageFormat.Png);
				KAnimUtils.WriteBuild(buildFile, build);
				KAnimUtils.WriteAnim(animFile, anim);
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to generate building data.", ex);
			}

			return null;
		}

		public void Cancel()
		{ }

		private void buttonBrowse_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.ShowNewFolderButton = true;
			dlg.Description = "Select a folder to save the kanim files to:";
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				textBoxOutputPath.Text = dlg.SelectedPath;
			}
		}
	}
}
