
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

using KanimExplorer.Wizard;

using KanimLib;
using KanimLib.Sprites;

namespace KanimExplorer.Forms
{
	public partial class MainForm : Form
	{
		private Font fnt = new Font(FontFamily.GenericSansSerif, 10f);

		private string currentAtlasFile = null;
		private string currentBuildFile = null;
		private string currentAnimFile = null;
		private KAnimPackage data;

		public bool FilesAreOpen => data?.HasAnyData ?? false;

		public MainForm()
		{
			InitializeComponent();

			closeToolStripMenuItem.Enabled = false;
			convertToSCMLToolStripMenuItem.Enabled = false;
			splitTextureAtlasToolStripMenuItem.Enabled = false;
			rebuildTextureAtlasToolStripMenuItem.Enabled = false;
			saveTextureAtlasToolStripMenuItem.Enabled = false;
			saveBuildFileToolStripMenuItem.Enabled = false;
		}

		private void OpenFiles()
		{
			Bitmap atlas = null;
			KBuild build = null;
			KAnim anim = null;

			if (currentAtlasFile != null)
			{
				using (FileStream fs = new FileStream(currentAtlasFile, FileMode.Open))
				{
					Bitmap bmp = new Bitmap(fs);
					atlas = (Bitmap)bmp.Clone();
				}
			}

			if (currentBuildFile != null)
			{
				build = KAnimUtils.ReadBuild(currentBuildFile);
			}

			if (currentAnimFile != null)
			{
				anim = KAnimUtils.ReadAnim(currentAnimFile);
			}

			OpenData(atlas, build, anim);
		}

		private void OpenData(Bitmap atlas, KBuild build, KAnim anim)
		{
			data = new KAnimPackage(); 
			data.Texture = atlas;
			UpdateAtlasView(data.Texture);

			data.Build = build;
			data.Anim = anim;
			UpdateBuildTree(data);

			closeToolStripMenuItem.Enabled = FilesAreOpen;
			convertToSCMLToolStripMenuItem.Enabled = data.IsComplete;
			splitTextureAtlasToolStripMenuItem.Enabled = data.IsValidAtlas;
			rebuildTextureAtlasToolStripMenuItem.Enabled = data.IsValidAtlas;
			saveTextureAtlasToolStripMenuItem.Enabled = data.HasTexture;
			saveBuildFileToolStripMenuItem.Enabled = data.HasBuild;
			saveAnimFileToolStripMenuItem.Enabled = data.HasAnim;
			previewAnimToolStripMenuItem.Enabled = data.IsComplete;
		}

		private void UpdateAtlasView(Bitmap img, RectangleF[] frames = null, PointF[] pivots = null)
		{
			if (img != null)
			{
				Bitmap bmp = new Bitmap(img.Width, img.Height);
				using (Graphics g = Graphics.FromImage(bmp))
				{
					g.Clear(Color.FromArgb(128, 128, 128));
					g.DrawImage(img, 0, 0, img.Width, img.Height);

					if (frames != null)
					{
						foreach (RectangleF frame in frames)
						{
							if (frame != RectangleF.Empty)
							{
								using (Pen pen = new Pen(Color.Red, 2f))
								{
									g.DrawRectangle(pen, frame.Left, frame.Top, frame.Width, frame.Height);
								}
							}
						}
					}

					if (pivots != null)
					{
						foreach (PointF pivot in pivots)
						{
							if (pivot != PointF.Empty)
							{
								g.FillRectangle(Brushes.Lime, pivot.X - 1f, pivot.Y - 1f, 3f, 3f);
							}
						}
					}

					if (data.Build != null && data.Build.NeedsRepack)
					{
						g.DrawString("Requires Rebuild", fnt, Brushes.Orange, 5, 5);
					}
				}

				atlasView.Image = bmp;
			}
			else
			{
				atlasView.Image = null;
			}
		}

		private void UpdateBuildTree(KAnimPackage data)
		{
			buildTreeView.Nodes.Clear();

			if (data == null) return;

			if (data.Build != null)
			{
				TreeNode buildNode = new TreeNode(data.Build.ToString());
				buildNode.Tag = data.Build;

				foreach (KSymbol symbol in data.Build.Symbols)
				{
					TreeNode symbolNode = new TreeNode(symbol.Name);
					symbolNode.Tag = symbol;

					foreach (KFrame frame in symbol.Frames)
					{
						TreeNode frameNode = new TreeNode(frame.Index.ToString());
						frameNode.Tag = frame;

						symbolNode.Nodes.Add(frameNode);
					}

					buildNode.Nodes.Add(symbolNode);
				}

				buildTreeView.Nodes.Add(buildNode);
			}

			if (data.Anim != null)
			{
				TreeNode animNode = new TreeNode("Animations");
				animNode.Tag = data.Anim;

				foreach (KAnimBank bank in data.Anim.Banks)
				{
					TreeNode bankNode = new TreeNode(bank.Name);
					bankNode.Tag = bank;

					animNode.Nodes.Add(bankNode);
				}

				buildTreeView.Nodes.Add(animNode);
			}

			//buildTreeView.ExpandAll();
		}

		private void CloseFiles()
		{
			currentAtlasFile = null;
			if (data.Texture != null)
			{
				data.Texture.Dispose();
				data.Texture = null;
			}
			atlasView.Image = null;

			currentBuildFile = null;
			data.Build = null;

			currentAnimFile = null;
			data.Anim = null;

			data = null;

			UpdateBuildTree(null);

			propertyGrid.SelectedObject = null;

			closeToolStripMenuItem.Enabled = false;
			convertToSCMLToolStripMenuItem.Enabled = false;
			splitTextureAtlasToolStripMenuItem.Enabled = false;
			rebuildTextureAtlasToolStripMenuItem.Enabled = false;
			saveTextureAtlasToolStripMenuItem.Enabled = false;
			saveBuildFileToolStripMenuItem.Enabled = false;
			previewAnimToolStripMenuItem.Enabled = false;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FilesAreOpen) CloseFiles();

			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Multiselect = true;
			dlg.Filter = "Kanim files|*.png;*.bytes";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (dlg.FileNames.Length > 3)
				{
					MessageBox.Show(this, "Too many files selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				bool invalidFiles = false;

				foreach (string file in dlg.FileNames)
				{
					if (file.EndsWith(".png"))
					{
						currentAtlasFile = file;
					}
					else if (file.EndsWith("build.bytes"))
					{
						currentBuildFile = file;
					}
					else if (file.EndsWith("anim.bytes"))
					{
						currentAnimFile = file;
					}
					else
					{
						invalidFiles = true;
					}
				}

				if (invalidFiles)
				{
					MessageBox.Show(this, "Invalid files were selected.\nThey will not be loaded.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				OpenFiles();
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseFiles();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseFiles();
			Close();
		}

		private void buildTreeView_AfterSelect(object sender, TreeViewEventArgs e)
		{
			TreeView tree = sender as TreeView;
			if (tree == null) return;

			List<RectangleF> frames = new List<RectangleF>();
			List<PointF> pivots = new List<PointF>();

			if (e.Node != null)
			{
				propertyGrid.SelectedObject = e.Node.Tag;

				switch (e.Node.Tag)
				{
					case KBuild build:
					case KAnim anim:
						break;

					case KSymbol symbol:
						if (data.Texture != null)
						{
							foreach (KFrame frame in symbol.Frames)
							{
								frames.Add(frame.GetUVRectangle(data.Texture.Width, data.Texture.Height));
								pivots.Add(frame.GetPivotPoint(data.Texture.Width, data.Texture.Height));
							}
						}
						break;

					case KFrame frame:
						if (data.Texture != null)
						{
							frames.Add(frame.GetUVRectangle(data.Texture.Width, data.Texture.Height));
							pivots.Add(frame.GetPivotPoint(data.Texture.Width, data.Texture.Height));
						}
						break;

					default:
						break;
				}
			}

			if (data.Texture != null)
			{
				UpdateAtlasView(data.Texture, frames.ToArray(), pivots.ToArray());
			}
		}

		private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			List<RectangleF> frames = new List<RectangleF>();
			List<PointF> pivots = new List<PointF>();

			switch (propertyGrid.SelectedObject)
			{
				case KBuild build:
					break;

				case KSymbol symbol:
					if (data.Texture != null)
					{
						foreach (KFrame frame in symbol.Frames)
						{
							frames.Add(frame.GetUVRectangle(data.Texture.Width, data.Texture.Height));
							pivots.Add(frame.GetPivotPoint(data.Texture.Width, data.Texture.Height));
						}
					}
					break;

				case KFrame frame:
					if (data.Texture != null)
					{
						frames.Add(frame.GetUVRectangle(data.Texture.Width, data.Texture.Height));
						pivots.Add(frame.GetPivotPoint(data.Texture.Width, data.Texture.Height));
					}
					break;

				default:
					break;
			}

			if (data.Texture != null)
			{
				UpdateAtlasView(data.Texture, frames.ToArray(), pivots.ToArray());
			}
		}

		private bool VerifyKanimalSEPath()
		{
			string kanimalPath = Settings.Default.KanimalCLIPath;

			if (File.Exists(kanimalPath)) return true;

			DialogResult r = MessageBox.Show(this, "Kanim Explorer depends on Kanimal-SE to convert to SCML.\nPlease navigate to the kanimal-cli executable.", "Kanimal-SE Path", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
			if (r == DialogResult.OK)
			{
				OpenFileDialog dlg = new OpenFileDialog();
				dlg.Filter = "Kanimal-SE Executable|kanimal-cli.exe";

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					kanimalPath = dlg.FileName;
					Settings.Default.KanimalCLIPath = kanimalPath;
					Settings.Default.Save();
					return true;
				}
			}

			return false;
		}

		private void saveTextureAtlasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				try
				{
					data.Texture.Save(dlg.FileName, ImageFormat.Png);
					MessageBox.Show(this, "Texture atlas saved successfully.", "Save Success", MessageBoxButtons.OK);
				}
				catch
				{
					MessageBox.Show(this, "Failed to save texture atlas.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void saveBuildFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (data.Build.NeedsRepack)
			{
				MessageBox.Show("The texture atlas needs to be rebuilt first.");
				// Repack
				return;
			}

			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (KAnimUtils.WriteBuild(dlg.FileName, data.Build))
				{
					MessageBox.Show(this, "Build file saved successfully.", "Save Success", MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(this, "Failed to save build file.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void saveAnimFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (KAnimUtils.WriteAnim(dlg.FileName, data.Anim))
				{
					MessageBox.Show(this, "Anim file saved successfully.", "Save Success", MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(this, "Failed to save anim file.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void convertToSCMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (VerifyKanimalSEPath())
			{
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				dlg.ShowNewFolderButton = true;
				dlg.Description = "Select a folder to save the converted files...";
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					string outPath = dlg.SelectedPath;

					StringBuilder kanimalOutput = new StringBuilder();

					Process kanimal = new Process();
					ProcessStartInfo ps = new ProcessStartInfo();
					ps.RedirectStandardOutput = true;
					ps.FileName = Settings.Default.KanimalCLIPath;
					ps.Arguments = $"scml --output {outPath} {currentAtlasFile} {currentBuildFile} {currentAnimFile}";
					ps.UseShellExecute = false;
					kanimal.StartInfo = ps;
					kanimal.OutputDataReceived += (p, a) =>
					{
						kanimalOutput.AppendLine(a.Data);
					};
					kanimal.Start();
					kanimal.BeginOutputReadLine();
					kanimal.WaitForExit();

					Trace.Write(kanimalOutput.ToString());

					Process.Start("explorer.exe", outPath);
				}
			}
		}

		private void convertFromSCMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (VerifyKanimalSEPath())
			{
				OpenFileDialog scmlDlg = new OpenFileDialog();
				scmlDlg.Filter = "Spriter Projects (*.scml)|*.scml";
				scmlDlg.Title = "Select a Spriter Project...";
				if (scmlDlg.ShowDialog() == DialogResult.OK)
				{
					DialogResult r = MessageBox.Show(this, "Do you want to interpolate frames for this conversion?", "Interpolate Frames?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (r == DialogResult.Cancel) return;
					bool interpolate = (r == DialogResult.Yes);

					FolderBrowserDialog dlg = new FolderBrowserDialog();
					dlg.ShowNewFolderButton = true;
					dlg.Description = "Select a folder to save the converted files...";
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						string outPath = dlg.SelectedPath;

						StringBuilder kanimalOutput = new StringBuilder();

						Process kanimal = new Process();
						ProcessStartInfo ps = new ProcessStartInfo();
						ps.RedirectStandardOutput = true;
						ps.FileName = Settings.Default.KanimalCLIPath;
						ps.Arguments = $"kanim {scmlDlg.FileName} --output {outPath}" + (interpolate ? " --interpolate" : "");
						ps.UseShellExecute = false;
						kanimal.StartInfo = ps;
						kanimal.OutputDataReceived += (p, a) =>
						{
							kanimalOutput.AppendLine(a.Data);
						};
						kanimal.Start();
						kanimal.BeginOutputReadLine();
						kanimal.WaitForExit();

						Trace.Write(kanimalOutput.ToString());

						Process.Start("explorer.exe", outPath);
					}
				}
			}
		}

		private void splitTextureAtlasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.ShowNewFolderButton = true;
			dlg.Description = "Select a folder to save the exported frames...";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				Sprite[] sprites = SpriteUtils.BuildSprites(data.Texture, data.Build);
				foreach (Sprite sprite in sprites)
				{
					string frameFileName = $"{sprite.SymbolData.Name}_{sprite.FrameData.Index}.png";
					string framePath = Path.Combine(dlg.SelectedPath, frameFileName);
					sprite.Image.Save(framePath, ImageFormat.Png);
				}
				Process.Start("explorer.exe", dlg.SelectedPath);
			}
		}

		private void saveBlankAnimbytesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (KAnimUtils.WriteAnim(dlg.FileName, KAnimUtils.CreateEmptyAnim()))
				{
					MessageBox.Show(this, "Blank anim file saved successfully.", "Save Success", MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(this, "Failed to save blank anim file.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void rebuildTextureAtlasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!data.Build.NeedsRepack)
			{
				MessageBox.Show("The build data does not have any changes that require repacking.");
			}
			else
			{
				try
				{
					Sprite[] sprites = SpriteUtils.BuildSprites(data.Texture, data.Build);
					SpriteUtils.ResizeSprites(sprites);
					data.Texture = SpriteUtils.RebuildAtlas(sprites);
					foreach (var spr in sprites)
					{
						spr.FrameData.NeedsRepack = false;
					}
				}
				catch
				{ }

				UpdateAtlasView(data.Texture);
				propertyGrid.Refresh();
			}
		}


		private void previewAnimToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (data != null && data.IsComplete)
			{
				AnimationForm animForm = new AnimationForm();
				animForm.SetData(data);
				animForm.Show(this);
			}
		}

		private void wizardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WizardForm f = new WizardForm();
			f.ShowDialog(this);
		}
	}
}
