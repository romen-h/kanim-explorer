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
			saveTextureAtlasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveBuildFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAnimFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			openSCMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveSCMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			saveBlankAnimbytesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			splitTextureAtlasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			exportAtlasBoxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			autoFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			duplicateSymbolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			oldAnimationViewerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			wizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			logToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, wizardToolStripMenuItem, logToolStripMenuItem });
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Padding = new System.Windows.Forms.Padding(5, 1, 0, 1);
			menuStrip1.Size = new System.Drawing.Size(1264, 24);
			menuStrip1.TabIndex = 0;
			menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { openToolStripMenuItem, saveTextureAtlasToolStripMenuItem, saveBuildFileToolStripMenuItem, saveAnimFileToolStripMenuItem, saveAllToolStripMenuItem, toolStripSeparator5, openSCMLToolStripMenuItem, saveSCMLToolStripMenuItem, toolStripSeparator2, closeToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new System.Drawing.Size(37, 22);
			fileToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			openToolStripMenuItem.Name = "openToolStripMenuItem";
			openToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			openToolStripMenuItem.Text = "Open";
			openToolStripMenuItem.Click += openToolStripMenuItem_Click;
			// 
			// saveTextureAtlasToolStripMenuItem
			// 
			saveTextureAtlasToolStripMenuItem.Enabled = false;
			saveTextureAtlasToolStripMenuItem.Name = "saveTextureAtlasToolStripMenuItem";
			saveTextureAtlasToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			saveTextureAtlasToolStripMenuItem.Text = "Save Texture Atlas...";
			saveTextureAtlasToolStripMenuItem.Click += saveTextureAtlasToolStripMenuItem_Click;
			// 
			// saveBuildFileToolStripMenuItem
			// 
			saveBuildFileToolStripMenuItem.Enabled = false;
			saveBuildFileToolStripMenuItem.Name = "saveBuildFileToolStripMenuItem";
			saveBuildFileToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			saveBuildFileToolStripMenuItem.Text = "Save Build File...";
			saveBuildFileToolStripMenuItem.Click += saveBuildFileToolStripMenuItem_Click;
			// 
			// saveAnimFileToolStripMenuItem
			// 
			saveAnimFileToolStripMenuItem.Enabled = false;
			saveAnimFileToolStripMenuItem.Name = "saveAnimFileToolStripMenuItem";
			saveAnimFileToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			saveAnimFileToolStripMenuItem.Text = "Save Anim File...";
			saveAnimFileToolStripMenuItem.Click += saveAnimFileToolStripMenuItem_Click;
			// 
			// saveAllToolStripMenuItem
			// 
			saveAllToolStripMenuItem.Name = "saveAllToolStripMenuItem";
			saveAllToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			saveAllToolStripMenuItem.Text = "Save All...";
			saveAllToolStripMenuItem.Click += saveAllToolStripMenuItem_Click;
			// 
			// toolStripSeparator5
			// 
			toolStripSeparator5.Name = "toolStripSeparator5";
			toolStripSeparator5.Size = new System.Drawing.Size(174, 6);
			// 
			// openSCMLToolStripMenuItem
			// 
			openSCMLToolStripMenuItem.Name = "openSCMLToolStripMenuItem";
			openSCMLToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			openSCMLToolStripMenuItem.Text = "Open SCML...";
			openSCMLToolStripMenuItem.Click += openSCMLToolStripMenuItem_Click;
			// 
			// saveSCMLToolStripMenuItem
			// 
			saveSCMLToolStripMenuItem.Name = "saveSCMLToolStripMenuItem";
			saveSCMLToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			saveSCMLToolStripMenuItem.Text = "Save SCML...";
			saveSCMLToolStripMenuItem.Click += saveSCMLToolStripMenuItem_Click;
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new System.Drawing.Size(174, 6);
			// 
			// closeToolStripMenuItem
			// 
			closeToolStripMenuItem.Enabled = false;
			closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			closeToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			closeToolStripMenuItem.Text = "Close";
			closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new System.Drawing.Size(174, 6);
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
			exitToolStripMenuItem.Text = "Exit";
			exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
			// 
			// toolsToolStripMenuItem
			// 
			toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveBlankAnimbytesToolStripMenuItem, splitTextureAtlasToolStripMenuItem, exportAtlasBoxesToolStripMenuItem, toolStripSeparator3, autoFlagToolStripMenuItem, duplicateSymbolsToolStripMenuItem, toolStripSeparator4, oldAnimationViewerToolStripMenuItem });
			toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 22);
			toolsToolStripMenuItem.Text = "Tools";
			// 
			// saveBlankAnimbytesToolStripMenuItem
			// 
			saveBlankAnimbytesToolStripMenuItem.Name = "saveBlankAnimbytesToolStripMenuItem";
			saveBlankAnimbytesToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			saveBlankAnimbytesToolStripMenuItem.Text = "Export Blank Anim.bytes";
			saveBlankAnimbytesToolStripMenuItem.Click += saveBlankAnimbytesToolStripMenuItem_Click;
			// 
			// splitTextureAtlasToolStripMenuItem
			// 
			splitTextureAtlasToolStripMenuItem.Name = "splitTextureAtlasToolStripMenuItem";
			splitTextureAtlasToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			splitTextureAtlasToolStripMenuItem.Text = "Export All Sprites";
			splitTextureAtlasToolStripMenuItem.Click += splitTextureAtlasToolStripMenuItem_Click;
			// 
			// exportAtlasBoxesToolStripMenuItem
			// 
			exportAtlasBoxesToolStripMenuItem.Name = "exportAtlasBoxesToolStripMenuItem";
			exportAtlasBoxesToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			exportAtlasBoxesToolStripMenuItem.Text = "Export Atlas Boxes";
			exportAtlasBoxesToolStripMenuItem.Click += exportAtlasBoxesToolStripMenuItem_Click;
			// 
			// toolStripSeparator3
			// 
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(225, 6);
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
			// toolStripSeparator4
			// 
			toolStripSeparator4.Name = "toolStripSeparator4";
			toolStripSeparator4.Size = new System.Drawing.Size(225, 6);
			// 
			// oldAnimationViewerToolStripMenuItem
			// 
			oldAnimationViewerToolStripMenuItem.Name = "oldAnimationViewerToolStripMenuItem";
			oldAnimationViewerToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
			oldAnimationViewerToolStripMenuItem.Text = "Old Animation Viewer";
			oldAnimationViewerToolStripMenuItem.Click += oldAnimationViewerToolStripMenuItem_Click;
			// 
			// wizardToolStripMenuItem
			// 
			wizardToolStripMenuItem.Name = "wizardToolStripMenuItem";
			wizardToolStripMenuItem.Size = new System.Drawing.Size(55, 22);
			wizardToolStripMenuItem.Text = "Wizard";
			wizardToolStripMenuItem.Click += wizardToolStripMenuItem_Click;
			// 
			// logToolStripMenuItem
			// 
			logToolStripMenuItem.Name = "logToolStripMenuItem";
			logToolStripMenuItem.Size = new System.Drawing.Size(39, 22);
			logToolStripMenuItem.Text = "Log";
			logToolStripMenuItem.Click += logToolStripMenuItem_Click;
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
		private System.Windows.Forms.ToolStripMenuItem saveBuildFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem splitTextureAtlasToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAnimFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveBlankAnimbytesToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem saveTextureAtlasToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wizardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openSCMLToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportAtlasBoxesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem autoFlagToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem duplicateSymbolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem saveSCMLToolStripMenuItem;
		private System.Windows.Forms.ImageList imageListIcons;
		private System.Windows.Forms.ToolStripMenuItem logToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripMenuItem oldAnimationViewerToolStripMenuItem;
		private System.Windows.Forms.TabControl tabControl;
		private System.Windows.Forms.TabPage tabPageAtlas;
		private System.Windows.Forms.TabPage tabPageSprite;
		private System.Windows.Forms.SplitContainer splitContainerInner;
		private System.Windows.Forms.Label label2;
	}
}

