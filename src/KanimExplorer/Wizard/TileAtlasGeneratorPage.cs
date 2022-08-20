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
	public partial class TileAtlasGeneratorPage : UserControl, IWizardPage
	{
		public IWizardPage Next()
		{
			string name = textBoxName.Text;
			string borderFile = textBoxBorderPath.Text;
			string fillFile = textBoxFillPath.Text;
			string outDir = textBoxOutput.Text;

			if (string.IsNullOrWhiteSpace(name)) throw new Exception("Name must not be empty.");
			//if (string.IsNullOrWhiteSpace(borderFile) || !File.Exists(borderFile)) throw new Exception("Invalid border file.");
			//if (string.IsNullOrWhiteSpace(fillFile) || !File.Exists(fillFile)) throw new Exception("Invalid fill file.");
			if (string.IsNullOrWhiteSpace(outDir) || !Directory.Exists(outDir)) throw new Exception("Invalid output directory.");

			string atlasFile = Path.Combine(outDir, $"tiles_{name}.png");

			using (Bitmap border = (!string.IsNullOrWhiteSpace(borderFile)) ? new Bitmap(borderFile) : null)
			using (Bitmap fill = (!string.IsNullOrWhiteSpace(fillFile)) ? new Bitmap(fillFile) : null)
			{
				if (border != null && (border.Width != TileFactory.BORDER_IMG_SIZE || border.Height != TileFactory.BORDER_IMG_SIZE)) throw new Exception("Border texture must be 336x336.");
				if (fill != null && (fill.Width != TileFactory.SEAMLESS_SIZE || fill.Height != TileFactory.SEAMLESS_SIZE)) throw new Exception("Fill texture must be 128x128.");

				try
				{
					Bitmap atlas = TileFactory.RenderTileAtlas(border, fill);
					atlas.Save(atlasFile, ImageFormat.Png);
					atlas.Dispose();
					return null;
				}
				catch (Exception ex)
				{
					throw new Exception("Failed to make tile atlas.", ex);
				}
			}
		}

		public void Cancel()
		{ }

		public TileAtlasGeneratorPage()
		{
			InitializeComponent();
		}

		private void buttonBrowseBorder_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "PNG Images (*.png)|*.png";
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				textBoxBorderPath.Text = dlg.FileName;
			}
		}

		private void buttonBrowseFill_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "PNG Images (*.png)|*.png";
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				textBoxFillPath.Text = dlg.FileName;
			}
		}

		private void buttonBrowseOutput_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.ShowNewFolderButton = true;
			dlg.Description = "Select a folder to save the tile atlas to:";
			if (dlg.ShowDialog(this) == DialogResult.OK)
			{
				textBoxOutput.Text = dlg.SelectedPath;
			}
		}
	}
}
