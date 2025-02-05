using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KanimLib;

namespace KanimExplorer
{
	public static class KanimLoader
	{
		public static KAnimPackage BrowseForKanimFiles()
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Multiselect = true;
			dlg.Filter = "Kanim files|*.png;*.bytes";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (dlg.FileNames.Length > 3)
				{
					MessageBox.Show("Too many files selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return null;
				}

				bool invalidFiles = false;
				string atlasFile = null;
				string buildFile = null;
				string animFile = null;

				foreach (string file in dlg.FileNames)
				{
					if (file.EndsWith(".png"))
					{
						atlasFile = file;
					}
					else if (file.EndsWith("build.bytes"))
					{
						buildFile = file;
					}
					else if (file.EndsWith("anim.bytes"))
					{
						animFile = file;
					}
					else
					{
						invalidFiles = true;
					}
				}

				if (invalidFiles)
				{
					MessageBox.Show("Invalid files were selected.\nThey will not be loaded.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				return OpenKanimFiles(atlasFile, buildFile, animFile);
			}

			return null;
		}

		public static KAnimPackage OpenKanimFiles(string atlasFile, string buildFile, string animFile)
		{
			KAnimPackage pkg = new KAnimPackage();

			if (atlasFile != null)
			{
				try
				{
					using (FileStream fs = new FileStream(atlasFile, FileMode.Open))
					{
						Bitmap bmp = new Bitmap(fs);
						pkg.Texture = (Bitmap)bmp.Clone();
					}
				}
				catch (Exception ex)
				{ }
			}

			if (buildFile != null)
			{
				try
				{
					pkg.Build = KAnimUtils.ReadBuild(buildFile);
				}
				catch (Exception ex)
				{ }
			}

			if (animFile != null)
			{
				try
				{
					pkg.Anim = KAnimUtils.ReadAnim(animFile);

					if (pkg.Build != null)
					{
						pkg.Anim.RepairStringsFromBuild(pkg.Build);
					}
				}
				catch (Exception ex)
				{ }
			}

			return pkg;
		}
	}
}
