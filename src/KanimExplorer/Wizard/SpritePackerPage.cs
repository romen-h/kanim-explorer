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
	public partial class SpritePackerPage : UserControl, IWizardPage
	{
		public SpritePackerPage()
		{
			InitializeComponent();
		}

		public IWizardPage Next()
		{
			string name = textBoxName.Text;
			string inDir = textBoxInput.Text;
			string outDir = textBoxOutput.Text;

			if (string.IsNullOrWhiteSpace(name)) throw new Exception("Name must not be empty.");
			if (string.IsNullOrWhiteSpace(inDir) || !Directory.Exists(inDir)) throw new Exception("Input path is invalid.");
			if (string.IsNullOrWhiteSpace(outDir) || !Directory.Exists(outDir)) throw new Exception("Output path is invalid.");

			string texFile = Path.Combine(outDir, $"{name}_0.png");
			string buildFile = Path.Combine(outDir, $"{name}_build.bytes");
			string animFile = Path.Combine(outDir, $"{name}_anim.bytes");

			try
			{
				AnimFactory.MakeSpritePack(name, inDir, out Bitmap tex, out KBuild build, out KAnim anim);
				tex.Save(texFile, ImageFormat.Png);
				KAnimUtils.WriteBuild(buildFile, build);
				KAnimUtils.WriteAnim(animFile, anim);
			}
			catch (Exception ex)
			{
				throw new Exception("Failed to pack sprites.", ex);
			}

			return null;
		}

		public void Cancel()
		{ }

		private void buttonBrowseInput_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.ShowNewFolderButton = true;
			dlg.Description = "Select a folder to load sprites from:";
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				textBoxInput.Text = dlg.SelectedPath;
			}
		}

		private void buttonBrowseOutput_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.ShowNewFolderButton = true;
			dlg.Description = "Select a folder to save the kanim files to:";
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				textBoxOutput.Text = dlg.SelectedPath;
			}
		}
	}
}
