
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using KanimExplorer.Controls;
using KanimExplorer.Settings;
using KanimExplorer.Wizard;

using KanimLib;
using KanimLib.Converters;
using KanimLib.KanimModel;
using KanimLib.Serialization;
using KanimLib.Sprites;

using Microsoft.Extensions.Logging;

namespace KanimExplorer.Forms
{
	public partial class MainForm : Form
	{
		private readonly ILogger _log = KanimLib.Logging.Factory.CreateLogger("MainForm");
		
		private bool _loaded = false;

		private readonly Controls.KanimDataTreeControl kanimDataTreeControl;
		private readonly Controls.AtlasControl atlasControl;
		private readonly Controls.SpriteControl spriteControl;

		private LogForm _logForm;

		private KanimPackage data => DocumentManager.Instance.Data;

		public MainForm()
		{
			_log.LogTrace("Constructing MainForm...");

			InitializeComponent();

			kanimDataTreeControl = new KanimDataTreeControl();
			kanimDataTreeControl.Dock = DockStyle.Fill;
			kanimDataTreeControl.SelectedObjectChanged += KanimDataTreeControl_SelectedObjectChanged;
			splitContainerOuter.Panel1.Controls.Add(kanimDataTreeControl);

			atlasControl = new AtlasControl();
			atlasControl.Dock = DockStyle.Fill;
			tabPageAtlas.Controls.Add(atlasControl);

			spriteControl = new SpriteControl();
			spriteControl.Dock = DockStyle.Fill;
			spriteControl.FramePivotUpdated += SpriteControl_FramePivotUpdated;
			tabPageSprite.Controls.Add(spriteControl);

			_logForm = new LogForm();

			DocumentManager.Instance.LoadedTextureChanged += DocumentManager_LoadedTextureChanged;
			DocumentManager.Instance.LoadedBuildChanged += DocumentManager_LoadedBuildChanged;
			DocumentManager.Instance.LoadedAnimChanged += DocumentManager_LoadedAnimChanged;

			ResolveControls();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			var windowState = ApplicationSettings.Instance.MainWindow;
			
			if (windowState.Maximized ?? false)
			{
				WindowState = FormWindowState.Maximized;
			}
			else
			{
				int left = windowState.Left ?? Left;
				int top = windowState.Top ?? Top;
				Location = new Point(left, top);

				Width = windowState.Width ?? Width;
				Height = windowState.Height ?? Height;
			}
			
			_loaded = true;
		}

		/// <summary>
		/// Called when the DocumentManager has a new texture loaded.
		/// </summary>
		private void DocumentManager_LoadedTextureChanged(object sender, EventArgs e)
		{
			var texture = DocumentManager.Instance.Data?.Texture;
			atlasControl.Texture = texture;
			if (texture == null)
			{
				spriteControl.Frame = null;
			}
			ResolveControls();
		}

		/// <summary>
		/// Called when the DocumentManager has a new build.bytes loaded.
		/// </summary>
		private void DocumentManager_LoadedBuildChanged(object sender, EventArgs e)
		{
			kanimDataTreeControl.SetKanim(DocumentManager.Instance.Data);
			var build = DocumentManager.Instance.Data?.Build;
			atlasControl.Build = build;
			if (build == null)
			{
				spriteControl.Frame = null;
			}
			ResolveControls();
		}

		/// <summary>
		/// Called when the DocumentManager has a new anim.bytes loaded.
		/// </summary>
		private void DocumentManager_LoadedAnimChanged(object sender, EventArgs e)
		{
			kanimDataTreeControl.SetKanim(DocumentManager.Instance.Data);
			ResolveControls();
		}

		/// <summary>
		/// Called when the tree view on the sidebar selects an object.
		/// </summary>
		private void KanimDataTreeControl_SelectedObjectChanged(object sender, EventArgs e)
		{
			var selected = kanimDataTreeControl.SelectedObject;
			if (selected == null) return;

			propertyGrid.SelectedObject = selected;

			switch (selected)
			{
				case KBuild build:
				case KAnim anim:
					spriteControl.Frame = null;
					break;

				case KSymbol symbol:
					atlasControl.SelectedFrames = symbol.Frames;
					spriteControl.Frame = symbol.Frames.FirstOrDefault();
					break;

				case KFrame frame:
					atlasControl.SelectedFrames = [frame];
					spriteControl.Frame = frame;
					break;

				case KAnimFrame animFrame:
					List<KFrame> framesInAnim = new List<KFrame>();
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
									framesInAnim.Add(frame);
								}
							}
						}
					}
					atlasControl.SelectedFrames = framesInAnim;
					spriteControl.Frame = framesInAnim.FirstOrDefault();
					break;

				case KAnimElement element:
					KFrame frameInElement = null;
					if (data.Texture != null && data.Build != null)
					{
						KSymbol symbol = data.Build.GetSymbol(element.SymbolHash);
						if (symbol != null)
						{
							if (symbol.FrameCount > element.FrameNumber)
							{
								frameInElement = symbol.Frames[element.FrameNumber];
							}
						}
					}
					atlasControl.SelectedFrames = [frameInElement];
					spriteControl.Frame = frameInElement;
					break;

				default:
					break;
			}
		}

		private void SpriteControl_FramePivotUpdated(object sender, EventArgs e)
		{
			propertyGrid.Refresh();
		}

		private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			string? propertyName = e.ChangedItem?.PropertyDescriptor.Name;
			List<Rectangle> frames = new List<Rectangle>();
			List<PointF> pivots = new List<PointF>();

			switch (propertyGrid.SelectedObject)
			{
				case KBuild build:
					break;

				case KSymbol symbol:
					atlasControl.OnBuildUpdated();
					break;

				case KFrame frame:
					if (propertyName == nameof(KFrame.SpriterPivotX) || propertyName == nameof(KFrame.SpriterPivotY))
					{
						spriteControl.OnFrameUpdated();
					}
					atlasControl.OnBuildUpdated();
					break;

				default:
					break;
			}
		}

		private void openToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (_log.BeginFunction())
			{
				OpenFileDialog dlg = new OpenFileDialog();
				dlg.Multiselect = true;
				dlg.Filter = "Kanim files|*.png;*.bytes;*.prefab;*.txt";

				if (dlg.ShowDialog() != DialogResult.OK) return;

				if (dlg.FileNames.Length > 3)
				{
					MessageBox.Show(this, "A maximum of 3 files can be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				OpenFilesList(dlg.FileNames, "Opening Files");
			}
		}

		private void openSCMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Open SCML menu item clicked.");

			DocumentManager.Instance.CloseEverything();

			OpenFileDialog scmlDlg = new OpenFileDialog();
			scmlDlg.Filter = "Spriter Projects (*.scml)|*.scml";
			scmlDlg.Title = "Select a Spriter Project...";
			if (scmlDlg.ShowDialog() == DialogResult.OK)
			{
				var pkg = SCMLImporter.Convert(scmlDlg.FileName);
				DocumentManager.Instance.OpenConvertedData(pkg);
			}
		}

		private void saveSCMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Save SCML menu item clicked.");

			if (!DocumentManager.Instance.FilesAreOpen) return;

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
				_log.LogError(ex, "Failed to export SCML.");
				MessageBox.Show("Failed to export SCML", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void closeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Close menu item clicked.");

			DocumentManager.Instance.CloseEverything();
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Exit menu item clicked.");

			DocumentManager.Instance.CloseEverything();
			Close();
		}

		private void saveTextureAtlasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Save Texture Atlas menu item clicked.");

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
			_log.LogTrace("Save Build menu item clicked.");

			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (KanimWriter.WriteBuild(dlg.FileName, data.Build))
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
			_log.LogTrace("Save Anim menu item clicked.");

			SaveFileDialog dlg = new SaveFileDialog();
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (KanimWriter.WriteAnim(dlg.FileName, data.Anim))
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
			_log.LogTrace("Save All menu item clicked.");

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
						if (!KanimWriter.WriteBuild(buildFile, data.Build)) throw new Exception();
					}
					if (data.HasAnim)
					{
						if (!KanimWriter.WriteAnim(animFile, data.Anim)) throw new Exception();
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

		private void splitTextureAtlasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Split Texture Atlas menu item clicked.");

			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.ShowNewFolderButton = true;
			dlg.Description = "Select a folder to save the exported frames...";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				KAnimUtils.SplitTextureAtlas(data.Texture, data.Build, dlg.SelectedPath);
				Process.Start("explorer.exe", dlg.SelectedPath);
			}
		}

		private void saveBlankAnimbytesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Save Blank Anim menu item clicked.");

			SaveFileDialog dlg = new SaveFileDialog()
			{
				Filter = "*.bytes|*.bytes"
			};

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				if (KanimWriter.WriteAnim(dlg.FileName, KAnimUtils.CreateEmptyAnim()))
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
			_log.LogTrace("Export Texture Atlas Boxes menu item clicked.");

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

#if false
		private void rebuildTextureAtlasToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Rebuild Texture Atlas menu item clicked.");
			
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
#endif

		private void autoFlagToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (_log.BeginFunction())
			{
				try
				{
					if (data == null) throw new InvalidOperationException("No kanim data is loaded yet.");
					if (!data.HasBuild) throw new InvalidOperationException("No build file loaded yet.");
					KAnimUtils.AutoFlagSymbols(data);
					propertyGrid.Refresh();
				}
				catch (Exception ex)
				{
					_log.LogError(ex, "Failed to auto-flag symbols.");
					MessageBox.Show("Failed to auto-flag symbols.\nPlease check the log for more details.", "Auto-Flagging Symbols", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void duplicateSymbolsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SymbolDuplicatorForm dlg = new SymbolDuplicatorForm(data);
			dlg.ShowDialog(this);

			kanimDataTreeControl.RebuildTree();
			propertyGrid.Refresh();
		}

		private void oldAnimationViewerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Animation Viewer menu item clicked.");

			if (data != null && data.IsComplete)
			{
				AnimationForm animForm = new AnimationForm(data);
				animForm.Show(this);
			}
		}

		private void wizardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Wizards menu item clicked.");

			WizardForm f = new WizardForm();
			f.ShowDialog(this);
		}

		private void logToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Log menu item clicked.");

			if (_logForm == null)
			{
				_logForm = new LogForm();
			}

			_logForm.Show();
		}

		private void MainForm_DragEnter(object sender, DragEventArgs e)
		{
			_log.LogTrace("Item drag entered window.");

			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				e.Effect = DragDropEffects.Copy;
			}
		}

		private void MainForm_DragDrop(object sender, DragEventArgs e)
		{
			using (_log.BeginFunction())
			{
				string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
				OpenFilesList(files, "Opening Dropped Files");
			}
		}

		private void OpenFilesList(IEnumerable<string> files, string messageBoxContext)
		{
			using (_log.BeginFunction())
			{
				DocumentManager.SelectSupportedFiles(files, out string textureFile, out string buildFile, out string animFile, out var invalidFiles);

				bool anythingSupported = textureFile != null || buildFile != null || animFile != null;
				if (!anythingSupported)
				{
					MessageBox.Show(this, "No supported files were selected.", messageBoxContext, MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}

				if (invalidFiles.Count > 0)
				{
					StringBuilder sb = new StringBuilder();
					sb.AppendLine("Invalid files were selected.");
					sb.AppendLine("These files will not be loaded:");
					foreach (var file in invalidFiles)
					{
						string fileName = Path.GetFileName(file);
						sb.AppendLine(fileName);
					}
					MessageBox.Show(this, sb.ToString(), messageBoxContext, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}

				if (DocumentManager.Instance.FilesAreOpen)
				{
					bool overloading = false;
					overloading |= textureFile != null && DocumentManager.Instance.Data.HasTexture;
					overloading |= buildFile != null && DocumentManager.Instance.Data.HasBuild;
					overloading |= animFile != null && DocumentManager.Instance.Data.HasAnim;

					if (overloading)
					{
						var rc = MessageBox.Show(this, "Files are already open.\nDo you want to close everything before opening the new ones?", "Opening Selected Files", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
						if (rc == DialogResult.No)
						{
							_log.LogTrace("User selected yes.");
							if (textureFile != null && DocumentManager.Instance.Data.HasTexture)
							{
								DocumentManager.Instance.CloseTexture();
							}
							if (buildFile != null && DocumentManager.Instance.Data.HasBuild)
							{
								DocumentManager.Instance.CloseBuild();
							}
							if (animFile != null && DocumentManager.Instance.Data.HasAnim)
							{
								DocumentManager.Instance.CloseAnim();
							}
						}
						else if (rc == DialogResult.Yes)
						{
							_log.LogTrace("User selected No.");
							DocumentManager.Instance.CloseEverything();
						}
						else if (rc == DialogResult.Cancel)
						{
							_log.LogTrace("User selected Cancel.");
							return;
						}
					}
				}

				if (textureFile != null)
				{
					anythingSupported = true;
					if (!DocumentManager.Instance.OpenTexture(textureFile))
					{
						MessageBox.Show(this, "Failed to load texture.\nPlease check the log for more details.", messageBoxContext, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}

				if (buildFile != null)
				{
					anythingSupported = true;
					if (!DocumentManager.Instance.OpenBuild(buildFile))
					{
						MessageBox.Show(this, "Failed to load build.\nPlease check the log for more details.", messageBoxContext, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}

				if (animFile != null)
				{
					anythingSupported = true;
					if (!DocumentManager.Instance.OpenAnim(animFile))
					{
						MessageBox.Show(this, "Failed to load anim.\nPlease check the log for more details.", messageBoxContext, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
				}
			}
		}

		private void ResolveControls()
		{
			closeToolStripMenuItem.Enabled = data != null || (data?.HasAnyData ?? false);
			splitTextureAtlasToolStripMenuItem.Enabled = data?.IsValidAtlas ?? false;
			saveTextureAtlasToolStripMenuItem.Enabled = data?.HasTexture ?? false;
			exportAtlasBoxesToolStripMenuItem.Enabled = data != null && data.HasTexture && data.HasBuild;
			autoFlagToolStripMenuItem.Enabled = data?.HasBuild ?? false;
			saveBuildFileToolStripMenuItem.Enabled = data?.HasBuild ?? false;
			saveAnimFileToolStripMenuItem.Enabled = data?.HasAnim ?? false;
			duplicateSymbolsToolStripMenuItem.Enabled = data?.HasBuild ?? false;
			saveAllToolStripMenuItem.Enabled = data != null && (data.HasTexture || data.HasBuild || data.HasAnim);
			oldAnimationViewerToolStripMenuItem.Enabled = data?.IsComplete ?? false;
			saveSCMLToolStripMenuItem.Enabled = data != null && data.HasTexture && data.HasBuild;
		}

		private void tabControl_TabIndexChanged(object sender, EventArgs e)
		{
			spriteControl.ResetPivotEditing();
		}

		private void MainForm_ResizeEnd(object sender, EventArgs e)
		{
			if (!_loaded) return;
			ApplicationSettings.Instance.MainWindow.Maximized = WindowState == FormWindowState.Maximized;
			ApplicationSettings.Instance.SetMainWindowRectangle(Left, Top, Width, Height);
		}

		private void MainForm_LocationChanged(object sender, EventArgs e)
		{
			if (!_loaded) return;
			ApplicationSettings.Instance.MainWindow.Maximized = WindowState == FormWindowState.Maximized;
			ApplicationSettings.Instance.SetMainWindowRectangle(Left, Top, Width, Height);
		}
	}
}
