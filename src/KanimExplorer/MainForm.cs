using KanimalExplorer.Properties;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace KanimalExplorer
{
	public partial class MainForm : Form
	{
		private string currentAtlasFile = null;
		private Bitmap atlas = null;

		private string currentBuildFile = null;
		private KBuild buildData = null;

		private string currentAnimFile = null;
		private KAnim animData = null;

		public bool FilesAreOpen => (atlas != null || buildData != null || animData != null);

		public MainForm()
		{
			InitializeComponent();
		}

		private void OpenFiles()
		{
			if (currentAtlasFile != null)
			{
				using (FileStream fs = new FileStream(currentAtlasFile, FileMode.Open))
				{
					Bitmap bmp = new Bitmap(fs);
					atlas = (Bitmap)bmp.Clone();
				}
				UpdateAtlasView(atlas);
			}

			if (currentBuildFile != null)
			{
				buildData = KAnimUtils.ReadBuild(currentBuildFile);
			}

			if (currentAnimFile != null)
			{
				animData = KAnimUtils.ReadAnim(currentAnimFile);
			}

			UpdateBuildTree(buildData, animData);

			closeToolStripMenuItem.Enabled = (atlas != null || buildData != null || animData != null);
			convertSCMLToolStripMenuItem.Enabled = (atlas != null && buildData != null && animData != null);
			exportFramesToolStripMenuItem.Enabled = (atlas != null && buildData != null);
			saveBuildFileToolStripMenuItem.Enabled = (buildData != null);
		}

		private void UpdateAtlasView(Bitmap img, RectangleF[] frames = null, PointF[] pivots = null)
		{
			if (img != null)
			{
				Bitmap bmp = new Bitmap(img.Width, img.Height);
				using (Graphics g = Graphics.FromImage(bmp))
				{
					g.Clear(Color.FromArgb(128,128,128));
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
								g.FillRectangle(Brushes.Red, pivot.X - 1f, pivot.Y - 1f, 3f, 3f);
							}
						}
					}
				}

				atlasView.Image = bmp;
			}
			else
			{
				atlasView.Image = null;
			}
		}

		private void UpdateBuildTree(KBuild build, KAnim anim)
		{
			buildTreeView.Nodes.Clear();

			if (build != null)
			{
				TreeNode buildNode = new TreeNode(build.Name);
				buildNode.Tag = build;

				foreach (KSymbol symbol in build.Symbols)
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

			if (anim != null)
			{
				TreeNode animNode = new TreeNode("Animations");
				animNode.Tag = anim;

				foreach (KAnimBank bank in anim.Banks)
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
			if (atlas != null)
			{
				atlas.Dispose();
				atlas = null;
			}
			atlasView.Image = null;

			currentBuildFile = null;
			buildData = null;
			
			currentAnimFile = null;
			animData = null;

			UpdateBuildTree(null, null);

			propertyGrid.SelectedObject = null;

			closeToolStripMenuItem.Enabled = false;
			convertSCMLToolStripMenuItem.Enabled = false;
			exportFramesToolStripMenuItem.Enabled = false;
			saveBuildFileToolStripMenuItem.Enabled = false;
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
						break;

					case KSymbol symbol:
						if (atlas != null)
						{
							foreach (KFrame frame in symbol.Frames)
							{
								frames.Add(frame.GetUVRectangle(atlas.Width, atlas.Height));
								pivots.Add(frame.GetPivotPoint(atlas.Width, atlas.Height));
							}
						}
						break;

					case KFrame frame:
						if (atlas != null)
						{
							frames.Add(frame.GetUVRectangle(atlas.Width, atlas.Height));
							pivots.Add(frame.GetPivotPoint(atlas.Width, atlas.Height));
						}
						break;

					default:
						break;
				}
			}

			if (atlas != null)
			{
				UpdateAtlasView(atlas, frames.ToArray(), pivots.ToArray());
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

		private void convertSCMLToolStripMenuItem_Click(object sender, EventArgs e)
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

		private void exportFramesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.ShowNewFolderButton = true;
			dlg.Description = "Select a folder to save the exported frames...";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				foreach (KSymbol symbol in buildData.Symbols)
				{
					foreach (KFrame frame in symbol.Frames)
					{
						string frameFileName = $"{symbol.Name}_{frame.Index}.png";

						Bitmap croppedImg = atlas.Clone(frame.GetUVRectangle(atlas.Width, atlas.Height), atlas.PixelFormat);

						string framePath = Path.Combine(dlg.SelectedPath, frameFileName);

						croppedImg.Save(framePath, ImageFormat.Png);
					}
				}

				Process.Start("explorer.exe", dlg.SelectedPath);
			}
		}

		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{

		}

		private void saveBuildFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (KAnimUtils.WriteBuild(dlg.FileName, buildData))
				{
					MessageBox.Show(this, "Build file saved successfully.", "Save Success", MessageBoxButtons.OK);
				}
				else
				{
					MessageBox.Show(this, "Failed to save build file.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}
	}
}
