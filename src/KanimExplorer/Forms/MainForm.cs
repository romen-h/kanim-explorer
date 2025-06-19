
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Windows.Forms;

using KanimExplorer.Wizard;

using KanimLib;
using KanimLib.Converters;
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
			splitTextureAtlasToolStripMenuItem.Enabled = false;
			rebuildTextureAtlasToolStripMenuItem.Enabled = false;
			exportAtlasBoxesToolStripMenuItem.Enabled = false;
			saveTextureAtlasToolStripMenuItem.Enabled = false;
			saveBuildFileToolStripMenuItem.Enabled = false;
			saveAllToolStripMenuItem.Enabled = false;
			saveSCMLToolStripMenuItem.Enabled = false;
			renameSymbolToolStripMenuItem.Enabled = false;
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

				if (build != null)
				{
					anim.RepairStringsFromBuild(build);
				}
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
			splitTextureAtlasToolStripMenuItem.Enabled = data.IsValidAtlas;
			rebuildTextureAtlasToolStripMenuItem.Enabled = data.IsValidAtlas;
			saveTextureAtlasToolStripMenuItem.Enabled = data.HasTexture;
			exportAtlasBoxesToolStripMenuItem.Enabled = data.HasTexture && data.HasBuild;
			autoFlagToolStripMenuItem.Enabled = data.HasBuild;
			saveBuildFileToolStripMenuItem.Enabled = data.HasBuild;
			saveAnimFileToolStripMenuItem.Enabled = data.HasAnim;
			saveAllToolStripMenuItem.Enabled = data.HasTexture || data.HasBuild || data.HasAnim;
			previewAnimToolStripMenuItem.Enabled = data.IsComplete;
			saveSCMLToolStripMenuItem.Enabled = data.HasTexture && data.HasBuild;
			renameSymbolToolStripMenuItem.Enabled = data.HasBuild;
		}

		private void UpdateAtlasView(Bitmap img, Rectangle[] frames = null, PointF[] pivots = null)
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
						foreach (Rectangle frame in frames)
						{
							if (frame != Rectangle.Empty)
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

					for (int i = 0; i < bank.Frames.Count; i++)
					{
						KAnimFrame frame = bank.Frames[i];
						TreeNode frameNode = new TreeNode($"Frame {i}");
						frameNode.Tag = frame;

						for (int j = 0; j < frame.Elements.Count; j++)
						{
							KAnimElement element = frame.Elements[j];
							TreeNode elementNode = new TreeNode($"Element {j}");
							elementNode.Tag = element;

							frameNode.Nodes.Add(elementNode);
						}

						bankNode.Nodes.Add(frameNode);
					}

					animNode.Nodes.Add(bankNode);
				}

				buildTreeView.Nodes.Add(animNode);
			}

			//buildTreeView.ExpandAll();
		}

		private void CloseFiles()
		{
			if (data == null) return;

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
			splitTextureAtlasToolStripMenuItem.Enabled = false;
			rebuildTextureAtlasToolStripMenuItem.Enabled = false;
			saveTextureAtlasToolStripMenuItem.Enabled = false;
			exportAtlasBoxesToolStripMenuItem.Enabled = false;
			autoFlagToolStripMenuItem.Enabled = false;
			saveBuildFileToolStripMenuItem.Enabled = false;
			saveAllToolStripMenuItem.Enabled = false;
			previewAnimToolStripMenuItem.Enabled = false;
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FilesAreOpen) CloseFiles();

			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Multiselect = true;
			dlg.Filter = "Kanim files|*.png;*.bytes;*.prefab;*.txt";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (dlg.FileNames.Length > 3)
				{
					MessageBox.Show(this, "Too many files selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return;
				}

				if (SelectSupportedFiles(dlg.FileNames, out bool invalidFiles))
				{
					if (invalidFiles)
					{
						MessageBox.Show(this, "Invalid files were selected.\nThey will not be loaded.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
					}

					OpenFiles();
				}
				else
				{
					MessageBox.Show(this, "No supported files were selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
				}
			}
		}

		private bool SelectSupportedFiles(IEnumerable<string> files, out bool invalidFiles)
		{
			invalidFiles = false;

			bool selectedPNG = false;
			bool selectedBuild = false;
			bool selectedAnim = false;

			foreach (string file in files)
			{
				if (file.EndsWith(".png"))
				{
					if (!selectedPNG)
					{
						currentAtlasFile = file;
						selectedPNG = true;
					}
				}
				else if (file.EndsWith("build.bytes") || file.EndsWith("build.txt") || file.EndsWith("build.prefab"))
				{
					if (!selectedBuild)
					{
						currentBuildFile = file;
						selectedBuild = true;
					}
				}
				else if (file.EndsWith("anim.bytes") || file.EndsWith("anim.txt") || file.EndsWith("anim.prefab"))
				{
					if (!selectedAnim)
					{
						currentAnimFile = file;
						selectedAnim = true;
					}
				}
				else
				{
					invalidFiles = true;
				}
			}

			return (selectedPNG || selectedBuild || selectedAnim);
		}

		private void openSCMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (FilesAreOpen) CloseFiles();

			OpenFileDialog scmlDlg = new OpenFileDialog();
			scmlDlg.Filter = "Spriter Projects (*.scml)|*.scml";
			scmlDlg.Title = "Select a Spriter Project...";
			if (scmlDlg.ShowDialog() == DialogResult.OK)
			{
				var pkg = SCMLImporter.Convert(scmlDlg.FileName);
				if (pkg != null)
				{
					OpenData(pkg.Texture, pkg.Build, pkg.Anim);
				}
			}
		}

		private void saveSCMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (!FilesAreOpen) return;

			try
			{
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				dlg.ShowNewFolderButton = true;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					SCMLExporter.Convert(data, dlg.SelectedPath);
					MessageBox.Show(this, "SCML project saved successfully.", "Save Success", MessageBoxButtons.OK);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex.ToString());
				MessageBox.Show("Failed to export SCML", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

			List<Rectangle> frames = new List<Rectangle>();
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
								frames.Add(frame.GetTextureRectangle(data.Texture.Width, data.Texture.Height));
								pivots.Add(frame.GetPivotPoint(data.Texture.Width, data.Texture.Height));
							}
						}
						break;

					case KFrame frame:
						if (data.Texture != null)
						{
							frames.Add(frame.GetTextureRectangle(data.Texture.Width, data.Texture.Height));
							pivots.Add(frame.GetPivotPoint(data.Texture.Width, data.Texture.Height));
						}
						break;

					case KAnimFrame animFrame:
						if (data.Texture != null && data.Build != null)
						{
							foreach (KAnimElement element in animFrame.Elements)
							{
								KSymbol symbol = data.Build.GetSymbol(element.SymbolHash);
								if (symbol != null)
								{
									if (symbol.FrameCount > element.FrameNumber)
									{
										KFrame frame = symbol.Frames[element.FrameNumber];
										frames.Add(frame.GetTextureRectangle(data.Texture.Width, data.Texture.Height));
										pivots.Add(frame.GetPivotPoint(data.Texture.Width, data.Texture.Height));
									}
								}
							}
						}
						break;

					case KAnimElement element:
						if (data.Texture != null && data.Build != null)
						{
							KSymbol symbol = data.Build.GetSymbol(element.SymbolHash);
							if (symbol != null)
							{
								if (symbol.FrameCount > element.FrameNumber)
								{
									KFrame frame = symbol.Frames[element.FrameNumber];
									frames.Add(frame.GetTextureRectangle(data.Texture.Width, data.Texture.Height));
									pivots.Add(frame.GetPivotPoint(data.Texture.Width, data.Texture.Height));
								}
							}
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
			List<Rectangle> frames = new List<Rectangle>();
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
							frames.Add(frame.GetTextureRectangle(data.Texture.Width, data.Texture.Height));
							pivots.Add(frame.GetPivotPoint(data.Texture.Width, data.Texture.Height));
						}
					}
					break;

				case KFrame frame:
					if (data.Texture != null)
					{
						frames.Add(frame.GetTextureRectangle(data.Texture.Width, data.Texture.Height));
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

		private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FolderBrowserDialog dlg = new FolderBrowserDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				string exportName = "name";

				if (data.HasBuild)
				{
					exportName = data.Build.Name;
				}

				string textureFile = Path.Combine(dlg.SelectedPath, $"{exportName}_0.png");
				string buildFile = Path.Combine(dlg.SelectedPath, $"{exportName}_build.bytes");
				string animFile = Path.Combine(dlg.SelectedPath, $"{exportName}_anim.bytes");

				try
				{
					if (data.HasTexture)
					{
						data.Texture.Save(textureFile, ImageFormat.Png);
					}
					if (data.HasBuild)
					{
						if (!KAnimUtils.WriteBuild(buildFile, data.Build)) throw new Exception();
					}
					if (data.HasAnim)
					{
						if (!KAnimUtils.WriteAnim(animFile, data.Anim)) throw new Exception();
					}

					MessageBox.Show(this, "Kanim files saved successfully.", "Save Success", MessageBoxButtons.OK);

					Process.Start("explorer.exe", dlg.SelectedPath);
				}
				catch
				{
					MessageBox.Show(this, "Failed to save kanim files.", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void renameSymbolToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (data != null && data.HasBuild)
			{
				RenameSymbolForm dlg = new RenameSymbolForm(data.Build.SymbolNames.Values);
				if (dlg.ShowDialog(this) == DialogResult.OK)
				{
					try
					{
						KAnimUtils.RenameSymbol(data, dlg.OldName, dlg.NewName);
						UpdateBuildTree(data);
						MessageBox.Show(this, "Symbol renamed successfully.", "Rename Success", MessageBoxButtons.OK);
					}
					catch (Exception ex)
					{
						MessageBox.Show(this, "Failed to rename symbol.", "Rename Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					}

				}
			}
		}

		private void editPivotToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (data != null && data.HasBuild && data.HasTexture)
			{
				if (propertyGrid.SelectedObject is KFrame frame)
				{
					PivotEditorForm dlg = new PivotEditorForm(frame, data.Texture);
					if (dlg.ShowDialog() == DialogResult.OK)
					{
						frame.SpriterPivotX = dlg.PivotX;
						frame.SpriterPivotY = dlg.PivotY;
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
			SaveFileDialog dlg = new SaveFileDialog()
			{
				Filter = "*.bytes|*.bytes"
			};

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

		private void exportAtlasBoxesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SaveFileDialog dlg = new SaveFileDialog()
			{
				Filter = "*.png|*.png"
			};

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				Bitmap boxes = SpriteUtils.GetHelperImage(data.Texture.Width, data.Texture.Height, data.Build, true, true);
				boxes.Save(dlg.FileName);
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
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Things went wrong...", MessageBoxButtons.OK, MessageBoxIcon.Question);
				}

				UpdateAtlasView(data.Texture);
				propertyGrid.Refresh();
			}
		}

		private void autoFlagToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (data != null && data.HasBuild)
			{
				foreach (var symbol in data.Build.Symbols)
				{
					string lowerName = symbol.Name.ToLowerInvariant();
					if (lowerName.Contains("_bloom"))
					{
						symbol.Flags = symbol.Flags.SetFlag(SymbolFlags.Bloom, true);
					}

					if (lowerName.Contains("_fg"))
					{
						symbol.Flags = symbol.Flags.SetFlag(SymbolFlags.Foreground, true);
					}
				}

				propertyGrid.Refresh();
			}
		}

		private void previewAnimToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (data != null && data.IsComplete)
			{
				AnimationForm animForm = new AnimationForm(data);
				animForm.Show(this);
			}
		}

		private void wizardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WizardForm f = new WizardForm();
			f.ShowDialog(this);
		}

		private void duplicateSymbolsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SymbolDuplicatorForm f = new SymbolDuplicatorForm(data);
			f.ShowDialog(this);
			UpdateBuildTree(data);
			propertyGrid.Refresh();
		}

		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

			if (SelectSupportedFiles(files, out bool invalidFiles))
			{
				if (FilesAreOpen) CloseFiles();

				if (invalidFiles)
				{
					MessageBox.Show(this, "Invalid files were dropped.\nThey will not be loaded.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}

				OpenFiles();
			}
			else
			{
				MessageBox.Show(this, "No supported files were dropped.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
	}
}
