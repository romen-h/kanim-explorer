
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
using Microsoft.VisualBasic.Logging;

using static System.Runtime.InteropServices.JavaScript.JSType;

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

		public MainForm()
		{
			_log.LogTrace("Constructing MainForm...");

			InitializeComponent();

			kanimDataTreeControl = new KanimDataTreeControl();
			kanimDataTreeControl.Dock = DockStyle.Fill;
			splitContainerOuter.Panel1.Controls.Add(kanimDataTreeControl);

			atlasControl = new AtlasControl();
			atlasControl.Dock = DockStyle.Fill;
			tabPageAtlas.Controls.Add(atlasControl);

			spriteControl = new SpriteControl();
			spriteControl.Dock = DockStyle.Fill;
			spriteControl.FramePivotUpdated += SpriteControl_FramePivotUpdated;
			tabPageSprite.Controls.Add(spriteControl);

			_logForm = new LogForm();

			DocumentManager.Instance.LoadedTextureChanged += DocumentManager_LoadedDocumentsChanged;
			DocumentManager.Instance.LoadedBuildChanged += DocumentManager_LoadedDocumentsChanged;
			DocumentManager.Instance.LoadedAnimChanged += DocumentManager_LoadedDocumentsChanged;
			DocumentManager.Instance.SelectedObjectChanged += DocumentManager_SelectedObjectChanged;
		}

		/// <summary>
		/// Called when the form first loads.
		/// </summary>
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

			ResolveControls();
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
				ModalTasks.OpenFilesImpl(files, "Opening Dropped Files", _log);
			}
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

		/// <summary>
		/// Called when any texture, build, or anim file is opened or closed.
		/// </summary>
		private void DocumentManager_LoadedDocumentsChanged(object sender, EventArgs e)
		{
			ResolveControls();
		}

		/// <summary>
		/// Called when the user focuses an object in the tree view.
		/// </summary>
		private void DocumentManager_SelectedObjectChanged(object sender, SelectedObjectChangedEventArgs e)
		{
			propertyGrid.SelectedObject = e.Object;
			ResolveControls();
		}

		private void SpriteControl_FramePivotUpdated(object sender, EventArgs e)
		{
			propertyGrid.Refresh();
		}

		private void tabControl_TabIndexChanged(object sender, EventArgs e)
		{
			spriteControl.ResetPivotEditing();
		}

		private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			string? propertyName = e.ChangedItem?.PropertyDescriptor.Name;

			switch (propertyGrid.SelectedObject)
			{
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

		/* File Menu */

		private void openTextureToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIUtils.TryWithErrorMessage(() => ModalTasks.OpenTexture(_log), "Opening Texture", _log);
		}

		private void openBuildToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIUtils.TryWithErrorMessage(() => ModalTasks.OpenBuild(_log), "Opening Build", _log);
		}

		private void openAnimToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIUtils.TryWithErrorMessage(() => ModalTasks.OpenAnim(_log), "Opening Anim", _log);
		}

		private void openMultipleToolStripMenuItem_Click(object sender, EventArgs e)
		{
			UIUtils.TryWithErrorMessage(() => ModalTasks.OpenMultiple(_log), "Opening Multiple Files", _log);
		}

		private void openSCMLToolStripMenuItem_Click_1(object sender, EventArgs e)
		{
			UIUtils.TryWithErrorMessage(() => ModalTasks.OpenSCML(_log), "Opening SCML", _log);
		}

		private void saveAllToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (UIUtils.TryWithErrorMessage(() => ModalTasks.SaveAll(DocumentManager.Instance.Data, _log), "Saving All", _log))
			{
				if (ApplicationSettings.Instance.ShowSuccessDialogs)
				{
					MessageBox.Show("Files saved successfully.", "Saving All Files", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void SaveTextureAtlasAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (UIUtils.TryWithErrorMessage(() => ModalTasks.SaveTexture(DocumentManager.Instance.Data?.Texture, _log), "Saving Texture", _log))
			{
				if (ApplicationSettings.Instance.ShowSuccessDialogs)
				{
					MessageBox.Show("Texture file saved successfully.", "Saving Texture", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void SaveBuildAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (UIUtils.TryWithErrorMessage(() => ModalTasks.SaveBuild(DocumentManager.Instance.Data?.Build, _log), "Saving build.bytes", _log))
			{
				if (ApplicationSettings.Instance.ShowSuccessDialogs)
				{
					MessageBox.Show("Build file saved successfully.", "Saving build.bytes", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void SaveAnimAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (UIUtils.TryWithErrorMessage(() => ModalTasks.SaveAnim(DocumentManager.Instance.Data?.Anim, _log), "Saving anim.bytes", _log))
			{
				if (ApplicationSettings.Instance.ShowSuccessDialogs)
				{
					MessageBox.Show("Anim file saved successfully.", "Saving anim.bytes", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void saveAllAsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (UIUtils.TryWithErrorMessage(() => ModalTasks.SaveAllAs(DocumentManager.Instance.Data, _log), "Saving All Files", _log))
			{
				if (ApplicationSettings.Instance.ShowSuccessDialogs)
				{
					MessageBox.Show("Files saved successfully.", "Saving All Files", MessageBoxButtons.OK, MessageBoxIcon.Information);
				}
			}
		}

		private void exportSCMLToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Save SCML menu item clicked.");

			if (!DocumentManager.Instance.FilesAreOpen) return;

			try
			{
				FolderBrowserDialog dlg = new FolderBrowserDialog();
				dlg.ShowNewFolderButton = true;
				if (dlg.ShowDialog() == DialogResult.OK)
				{
					SCMLExporter.Convert(DocumentManager.Instance.Data, dlg.SelectedPath);
					MessageBox.Show(this, "SCML project saved successfully.", "Save Success", MessageBoxButtons.OK);
				}
			}
			catch (Exception ex)
			{
				_log.LogError(ex, "Failed to export SCML.");
				MessageBox.Show("Failed to export SCML", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void exportEmptyAnimbytesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (UIUtils.TryWithErrorMessage(() => ModalTasks.SaveEmptyAnim(_log), "Saving Empty anim.bytes", _log))
			{
				if (ApplicationSettings.Instance.ShowSuccessDialogs)
				{
					MessageBox.Show(this, "Empty anim file saved successfully.", "Saving Empty anim.bytes", MessageBoxButtons.OK);
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
				var data = DocumentManager.Instance.Data;
				Bitmap boxes = SpriteUtils.GetHelperImage(data.Texture.Width, data.Texture.Height, data.Build, true, true);
				boxes.Save(dlg.FileName);
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

		/* Tools Menu */

		private void ExportTextureAtlasSpritesToolStripMenuItem_Click(object sender, EventArgs e)
		{
			_log.LogTrace("Split Texture Atlas menu item clicked.");

			FolderBrowserDialog dlg = new FolderBrowserDialog();
			dlg.ShowNewFolderButton = true;
			dlg.Description = "Select a folder to save the exported frames...";

			if (dlg.ShowDialog() == DialogResult.OK)
			{
				KAnimUtils.SplitTextureAtlas(DocumentManager.Instance.Data, dlg.SelectedPath);
				Process.Start("explorer.exe", dlg.SelectedPath);
			}
		}

		private void autoFlagToolStripMenuItem_Click(object sender, EventArgs e)
		{
			using (_log.BeginFunction())
			{
				try
				{
					var data = DocumentManager.Instance.Data;
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
			var data = DocumentManager.Instance.Data;
			SymbolDuplicatorForm dlg = new SymbolDuplicatorForm(data);
			dlg.ShowDialog(this);

			kanimDataTreeControl.RebuildTree();
			propertyGrid.Refresh();
		}

		private void wizardToolStripMenuItem_Click(object sender, EventArgs e)
		{
			WizardForm f = new WizardForm();
			f.ShowDialog(this);
		}
		
		/* Window Menu */

		private void logToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (_logForm == null)
			{
				_logForm = new LogForm();
			}

			_logForm.Show();
		}

		private void oldAnimationViewerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			var data = DocumentManager.Instance.Data;
			if (data == null || !data.IsComplete) return;
			
			AnimationForm animForm = new AnimationForm(data);
			animForm.Show(this);
		}

		private void ResolveControls()
		{
			var data = DocumentManager.Instance.Data;

			openTextureToolStripMenuItem.Enabled = true;
			openBuildToolStripMenuItem.Enabled = true;
			openAnimToolStripMenuItem.Enabled = true;
			openMultipleToolStripMenuItem.Enabled = true;
			openSCMLToolStripMenuItem.Enabled = true;

			//saveAllToolStripMenuItem.Enabled = data != null && (data.HasTexture || data.HasBuild || data.HasAnim);
			saveTextureAtlasAsToolStripMenuItem.Enabled = data?.HasTexture ?? false;
			saveBuildAsToolStripMenuItem.Enabled = data?.HasBuild ?? false;
			saveAnimAsToolStripMenuItem.Enabled = data?.HasAnim ?? false;
			saveAllAsToolStripMenuItem.Enabled = data != null && (data.HasTexture || data.HasBuild || data.HasAnim);

			exportSCMLToolStripMenuItem.Enabled = data != null && (data.HasTexture && data.HasBuild);
			exportTextureAtlasSpritesToolStripMenuItem.Enabled = data?.IsValidAtlas ?? false;
			exportAtlasBoxesToolStripMenuItem.Enabled = data != null && (data.HasTexture && data.HasBuild);

			closeToolStripMenuItem.Enabled = data?.HasAnyData ?? false;

			autoFlagToolStripMenuItem.Enabled = data?.HasBuild ?? false;
			duplicateSymbolsToolStripMenuItem.Enabled = data?.HasBuild ?? false;
			
			oldAnimationViewerToolStripMenuItem.Enabled = data?.IsComplete ?? false;
		}
	}
}
