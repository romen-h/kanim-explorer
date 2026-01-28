namespace KanimExplorer.Forms
{
	partial class MainForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			menuStrip1 = new System.Windows.Forms.MenuStrip();
			fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openMultipleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openSCMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			individualKAnimFilesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			openTextureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openBuildToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			openAnimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAllAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			exportSCMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			individualKAnimFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAnimAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveTextureAtlasAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveBuildAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			exportEmptyAnimbytesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			exportTextureAtlasSpritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			exportAtlasBoxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			autoFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			duplicateSymbolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			wizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			windowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			oldAnimationViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			splitContainerOuter = new System.Windows.Forms.SplitContainer();
			splitContainerInner = new System.Windows.Forms.SplitContainer();
			tabControl = new System.Windows.Forms.TabControl();
			tabPageAtlas = new System.Windows.Forms.TabPage();
			tabPageSprite = new System.Windows.Forms.TabPage();
			propertyGrid = new System.Windows.Forms.PropertyGrid();
			label2 = new System.Windows.Forms.Label();
			imageListIcons = new System.Windows.Forms.ImageList(components);
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerOuter).BeginInit();
			splitContainerOuter.Panel2.SuspendLayout();
			splitContainerOuter.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainerInner).BeginInit();
			splitContainerInner.Panel1.SuspendLayout();
			splitContainerInner.Panel2.SuspendLayout();
			splitContainerInner.SuspendLayout();
			tabControl.SuspendLayout();
			SuspendLayout();
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, wizardToolStripMenuItem, windowToolStripMenuItem });
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Padding = new System.Windows.Forms.Padding(5, 1, 0, 1);
			menuStrip1.Size = new System.Drawing.Size(1264, 24);
			menuStrip1.TabIndex = 0;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openToolStripMenuItem, saveAsToolStripMenuItem, toolStripSeparator5, exportToolStripMenuItem, toolStripSeparator2, closeToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
			fileToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			openToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openMultipleToolStripMenuItem, openSCMLToolStripMenuItem, individualKAnimFilesToolStripMenuItem1 });
			openToolStripMenuItem.Name = "openToolStripMenuItem";
			openToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			openToolStripMenuItem.Text = "Open...";
			// 
			// openMultipleToolStripMenuItem
			// 
			openMultipleToolStripMenuItem.Name = "openMultipleToolStripMenuItem";
			openMultipleToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			openMultipleToolStripMenuItem.Text = "KAnim Files...";
			openMultipleToolStripMenuItem.Click += openMultipleToolStripMenuItem_Click;
			// 
			// openSCMLToolStripMenuItem
			// 
			openSCMLToolStripMenuItem.Name = "openSCMLToolStripMenuItem";
			openSCMLToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			openSCMLToolStripMenuItem.Text = "SCML Project...";
			openSCMLToolStripMenuItem.Click += openSCMLToolStripMenuItem_Click_1;
			// 
			// individualKAnimFilesToolStripMenuItem1
			// 
			individualKAnimFilesToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openTextureToolStripMenuItem, openBuildToolStripMenuItem, openAnimToolStripMenuItem });
			individualKAnimFilesToolStripMenuItem1.Name = "individualKAnimFilesToolStripMenuItem1";
			individualKAnimFilesToolStripMenuItem1.Size = new System.Drawing.Size(191, 22);
			individualKAnimFilesToolStripMenuItem1.Text = "Individual KAnim Files";
			// 
			// openTextureToolStripMenuItem
			// 
			openTextureToolStripMenuItem.Name = "openTextureToolStripMenuItem";
			openTextureToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			openTextureToolStripMenuItem.Text = "Texture...";
			// 
			// openBuildToolStripMenuItem
			// 
			openBuildToolStripMenuItem.Name = "openBuildToolStripMenuItem";
			openBuildToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			openBuildToolStripMenuItem.Text = "Build...";
			// 
			// openAnimToolStripMenuItem
			// 
			openAnimToolStripMenuItem.Name = "openAnimToolStripMenuItem";
			openAnimToolStripMenuItem.Size = new System.Drawing.Size(121, 22);
			openAnimToolStripMenuItem.Text = "Anim...";
			// 
			// saveAsToolStripMenuItem
			// 
			saveAsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveAllAsToolStripMenuItem, exportSCMLToolStripMenuItem, individualKAnimFilesToolStripMenuItem });
			saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			saveAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			saveAsToolStripMenuItem.Text = "Save As...";
			// 
			// saveAllAsToolStripMenuItem
			// 
			saveAllAsToolStripMenuItem.Enabled = false;
			saveAllAsToolStripMenuItem.Name = "saveAllAsToolStripMenuItem";
			saveAllAsToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			saveAllAsToolStripMenuItem.Text = "KAnim Files...";
			saveAllAsToolStripMenuItem.Click += saveAllAsToolStripMenuItem_Click;
			// 
			// exportSCMLToolStripMenuItem
			// 
			exportSCMLToolStripMenuItem.Enabled = false;
			exportSCMLToolStripMenuItem.Name = "exportSCMLToolStripMenuItem";
			exportSCMLToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			exportSCMLToolStripMenuItem.Text = "SCML Project...";
			// 
			// individualKAnimFilesToolStripMenuItem
			// 
			individualKAnimFilesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveTextureAtlasAsToolStripMenuItem, saveBuildAsToolStripMenuItem, saveAnimAsToolStripMenuItem });
			individualKAnimFilesToolStripMenuItem.Name = "individualKAnimFilesToolStripMenuItem";
			individualKAnimFilesToolStripMenuItem.Size = new System.Drawing.Size(191, 22);
			individualKAnimFilesToolStripMenuItem.Text = "Individual KAnim Files";
			// 
			// saveAnimAsToolStripMenuItem
			// 
			saveAnimAsToolStripMenuItem.Enabled = false;
			saveAnimAsToolStripMenuItem.Name = "saveAnimAsToolStripMenuItem";
			saveAnimAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			saveAnimAsToolStripMenuItem.Text = "Save Anim As...";
			// 
			// saveTextureAtlasAsToolStripMenuItem
			// 
			saveTextureAtlasAsToolStripMenuItem.Enabled = false;
			saveTextureAtlasAsToolStripMenuItem.Name = "saveTextureAtlasAsToolStripMenuItem";
			saveTextureAtlasAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			saveTextureAtlasAsToolStripMenuItem.Text = "Save Texture As...";
			// 
			// saveBuildAsToolStripMenuItem
			// 
			saveBuildAsToolStripMenuItem.Enabled = false;
			saveBuildAsToolStripMenuItem.Name = "saveBuildAsToolStripMenuItem";
			saveBuildAsToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			saveBuildAsToolStripMenuItem.Text = "Save Build As...";
			// 
			// toolStripSeparator5
			// 
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(177, 6);
			// 
			// exportToolStripMenuItem
			// 
			exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { exportEmptyAnimbytesToolStripMenuItem, exportTextureAtlasSpritesToolStripMenuItem, exportAtlasBoxesToolStripMenuItem });
			exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			exportToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			exportToolStripMenuItem.Text = "Export...";
			// 
			// exportEmptyAnimbytesToolStripMenuItem
			// 
			exportEmptyAnimbytesToolStripMenuItem.Name = "exportEmptyAnimbytesToolStripMenuItem";
			exportEmptyAnimbytesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			exportEmptyAnimbytesToolStripMenuItem.Text = "Empty Anim.bytes...";
			exportEmptyAnimbytesToolStripMenuItem.Click += exportEmptyAnimbytesToolStripMenuItem_Click;
			// 
			// exportTextureAtlasSpritesToolStripMenuItem
			// 
			exportTextureAtlasSpritesToolStripMenuItem.Enabled = false;
			exportTextureAtlasSpritesToolStripMenuItem.Name = "exportTextureAtlasSpritesToolStripMenuItem";
			exportTextureAtlasSpritesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			exportTextureAtlasSpritesToolStripMenuItem.Text = "Texture Atlas Sprites...";
			exportTextureAtlasSpritesToolStripMenuItem.Click += ExportTextureAtlasSpritesToolStripMenuItem_Click;
			// 
			// exportAtlasBoxesToolStripMenuItem
			// 
			exportAtlasBoxesToolStripMenuItem.Enabled = false;
			exportAtlasBoxesToolStripMenuItem.Name = "exportAtlasBoxesToolStripMenuItem";
			exportAtlasBoxesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
			exportAtlasBoxesToolStripMenuItem.Text = "Atlas Boxes...";
			exportAtlasBoxesToolStripMenuItem.Click += exportAtlasBoxesToolStripMenuItem_Click;
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
			// 
			// closeToolStripMenuItem
			// 
			closeToolStripMenuItem.Enabled = false;
			closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			closeToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			closeToolStripMenuItem.Text = "Close";
			closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
			exitToolStripMenuItem.Text = "Exit";
			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
			// 
			// toolsToolStripMenuItem
			// 
			toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { autoFlagToolStripMenuItem, duplicateSymbolsToolStripMenuItem });
			toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			toolsToolStripMenuItem.Size = new System.Drawing.Size(46, 22);
			toolsToolStripMenuItem.Text = "Tools";
			// 
			// autoFlagToolStripMenuItem
			// 
			autoFlagToolStripMenuItem.Name = "autoFlagToolStripMenuItem";
			autoFlagToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			autoFlagToolStripMenuItem.Text = "Auto-Flag Symbols";
			autoFlagToolStripMenuItem.Click += autoFlagToolStripMenuItem_Click;
			// 
			// duplicateSymbolsToolStripMenuItem
			// 
			duplicateSymbolsToolStripMenuItem.Name = "duplicateSymbolsToolStripMenuItem";
			duplicateSymbolsToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			duplicateSymbolsToolStripMenuItem.Text = "Duplicate Multiple Symbols...";
			duplicateSymbolsToolStripMenuItem.Click += duplicateSymbolsToolStripMenuItem_Click;
			// 
			// wizardToolStripMenuItem
			// 
			wizardToolStripMenuItem.Name = "wizardToolStripMenuItem";
			wizardToolStripMenuItem.Size = new System.Drawing.Size(55, 22);
			wizardToolStripMenuItem.Text = "Wizard";
			wizardToolStripMenuItem.Click += wizardToolStripMenuItem_Click;
			// 
			// windowToolStripMenuItem
			// 
			windowToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { logToolStripMenuItem, oldAnimationViewerToolStripMenuItem });
			windowToolStripMenuItem.Name = "windowToolStripMenuItem";
			windowToolStripMenuItem.Size = new System.Drawing.Size(63, 22);
			windowToolStripMenuItem.Text = "Window";
			// 
			// logToolStripMenuItem
			// 
			logToolStripMenuItem.Name = "logToolStripMenuItem";
			logToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			logToolStripMenuItem.Text = "Event Log";
			logToolStripMenuItem.Click += logToolStripMenuItem_Click;
			// 
			// oldAnimationViewerToolStripMenuItem
			// 
			oldAnimationViewerToolStripMenuItem.Name = "oldAnimationViewerToolStripMenuItem";
			oldAnimationViewerToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
			oldAnimationViewerToolStripMenuItem.Text = "Old Animation Viewer";
			oldAnimationViewerToolStripMenuItem.Click += oldAnimationViewerToolStripMenuItem_Click;
			// 
			// splitContainerOuter
			// 
			splitContainerOuter.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainerOuter.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
			splitContainerOuter.Location = new System.Drawing.Point(0, 24);
			splitContainerOuter.Margin = new System.Windows.Forms.Padding(2);
			splitContainerOuter.Name = "splitContainerOuter";
			splitContainerOuter.Panel1MinSize = 200;
			// 
			// splitContainerOuter.Panel2
			// 
			splitContainerOuter.Panel2.Controls.Add(splitContainerInner);
			splitContainerOuter.Size = new System.Drawing.Size(1264, 961);
			splitContainerOuter.SplitterDistance = 200;
			splitContainerOuter.TabIndex = 1;
			// 
			// splitContainerInner
			// 
			splitContainerInner.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainerInner.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			splitContainerInner.Location = new System.Drawing.Point(0, 0);
			splitContainerInner.Name = "splitContainerInner";
			// 
			// splitContainerInner.Panel1
			// 
			splitContainerInner.Panel1.Controls.Add(tabControl);
			// 
			// splitContainerInner.Panel2
			// 
			splitContainerInner.Panel2.Controls.Add(propertyGrid);
			splitContainerInner.Panel2.Controls.Add(label2);
			splitContainerInner.Panel2MinSize = 300;
			splitContainerInner.Size = new System.Drawing.Size(1060, 961);
			splitContainerInner.SplitterDistance = 756;
			splitContainerInner.TabIndex = 0;
			// 
			// tabControl
			// 
			tabControl.Controls.Add(tabPageAtlas);
			tabControl.Controls.Add(tabPageSprite);
			tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			tabControl.ItemSize = new System.Drawing.Size(100, 22);
			tabControl.Location = new System.Drawing.Point(0, 0);
			tabControl.Margin = new System.Windows.Forms.Padding(0);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.Size = new System.Drawing.Size(756, 961);
			tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
			tabControl.TabIndex = 1;
			tabControl.TabIndexChanged += tabControl_TabIndexChanged;
			// 
			// tabPageAtlas
			// 
			tabPageAtlas.Location = new System.Drawing.Point(4, 26);
			tabPageAtlas.Name = "tabPageAtlas";
			tabPageAtlas.Size = new System.Drawing.Size(748, 931);
			tabPageAtlas.TabIndex = 0;
			tabPageAtlas.Text = "Atlas";
			tabPageAtlas.UseVisualStyleBackColor = true;
			// 
			// tabPageSprite
			// 
			tabPageSprite.Location = new System.Drawing.Point(4, 26);
			tabPageSprite.Name = "tabPageSprite";
			tabPageSprite.Size = new System.Drawing.Size(748, 931);
			tabPageSprite.TabIndex = 2;
			tabPageSprite.Text = "Sprite";
			tabPageSprite.UseVisualStyleBackColor = true;
			// 
			// propertyGrid
			// 
			propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			propertyGrid.HelpVisible = false;
			propertyGrid.Location = new System.Drawing.Point(0, 25);
			propertyGrid.Margin = new System.Windows.Forms.Padding(2);
			propertyGrid.Name = "propertyGrid";
			propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
			propertyGrid.Size = new System.Drawing.Size(300, 936);
			propertyGrid.TabIndex = 2;
			propertyGrid.ToolbarVisible = false;
			propertyGrid.PropertyValueChanged += propertyGrid_PropertyValueChanged;
			// 
			// label2
			// 
			label2.Dock = System.Windows.Forms.DockStyle.Top;
			label2.Location = new System.Drawing.Point(0, 0);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(300, 25);
			label2.TabIndex = 3;
			label2.Text = "Symbol Inspector:";
			label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// imageListIcons
			// 
			imageListIcons.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			imageListIcons.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageListIcons.ImageStream");
			imageListIcons.TransparentColor = System.Drawing.Color.Transparent;
			imageListIcons.Images.SetKeyName(0, "build");
			imageListIcons.Images.SetKeyName(1, "sprite");
			// 
			// MainForm
			// 
			AllowDrop = true;
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(1264, 985);
			Controls.Add(splitContainerOuter);
			Controls.Add(menuStrip1);
			Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
			MainMenuStrip = menuStrip1;
			Margin = new System.Windows.Forms.Padding(2);
			Name = "MainForm";
			Text = "Kanim Explorer";
			Load += MainForm_Load;
			ResizeEnd += MainForm_ResizeEnd;
			LocationChanged += MainForm_LocationChanged;
			DragDrop += MainForm_DragDrop;
			DragEnter += MainForm_DragEnter;
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			splitContainerOuter.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerOuter).EndInit();
			splitContainerOuter.ResumeLayout(false);
			splitContainerInner.Panel1.ResumeLayout(false);
			splitContainerInner.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainerInner).EndInit();
			splitContainerInner.ResumeLayout(false);
			tabControl.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainerOuter;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wizardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem autoFlagToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem duplicateSymbolsToolStripMenuItem;
		private System.Windows.Forms.ImageList imageListIcons;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageAtlas;
		private System.Windows.Forms.TabPage tabPageSprite;
		private System.Windows.Forms.SplitContainer splitContainerInner;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ToolStripMenuItem openMultipleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openSCMLToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportEmptyAnimbytesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportTextureAtlasSpritesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem windowToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem oldAnimationViewerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportAtlasBoxesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAllAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem individualKAnimFilesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportSCMLToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAnimAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveTextureAtlasAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveBuildAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem individualKAnimFilesToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem openTextureToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openBuildToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openAnimToolStripMenuItem;
	}
}

