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
			toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			splitTextureAtlasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			rebuildTextureAtlasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			exportAtlasBoxesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			autoFlagToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			duplicateSymbolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			wizardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			previewAnimToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			splitContainer1 = new System.Windows.Forms.SplitContainer();
			splitContainer2 = new System.Windows.Forms.SplitContainer();
			buildTreeView = new System.Windows.Forms.TreeView();
			label1 = new System.Windows.Forms.Label();
			propertyGrid = new System.Windows.Forms.PropertyGrid();
			label2 = new System.Windows.Forms.Label();
			atlasView = new System.Windows.Forms.PictureBox();
			toolsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			renameSymbolToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
			splitContainer2.Panel1.SuspendLayout();
			splitContainer2.Panel2.SuspendLayout();
			splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)atlasView).BeginInit();
			SuspendLayout();
			// 
			// menuStrip1
			// 
			menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, toolsToolStripMenuItem, wizardToolStripMenuItem, previewAnimToolStripMenuItem });
			menuStrip1.Location = new System.Drawing.Point(0, 0);
			menuStrip1.Name = "menuStrip1";
			menuStrip1.Padding = new System.Windows.Forms.Padding(5, 1, 0, 1);
			menuStrip1.Size = new System.Drawing.Size(668, 24);
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
			toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { saveBlankAnimbytesToolStripMenuItem, toolStripSeparator3, renameSymbolToolStripMenuItem, splitTextureAtlasToolStripMenuItem, rebuildTextureAtlasToolStripMenuItem, exportAtlasBoxesToolStripMenuItem, autoFlagToolStripMenuItem, duplicateSymbolsToolStripMenuItem, toolsToolStripMenuItem1 });
			toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			toolsToolStripMenuItem.Size = new System.Drawing.Size(47, 22);
			toolsToolStripMenuItem.Text = "Tools";
			// 
			// saveBlankAnimbytesToolStripMenuItem
			// 
			saveBlankAnimbytesToolStripMenuItem.Name = "saveBlankAnimbytesToolStripMenuItem";
			saveBlankAnimbytesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			saveBlankAnimbytesToolStripMenuItem.Text = "Save Blank Anim.bytes";
			saveBlankAnimbytesToolStripMenuItem.Click += saveBlankAnimbytesToolStripMenuItem_Click;
			// 
			// toolStripSeparator3
			// 
			toolStripSeparator3.Name = "toolStripSeparator3";
			toolStripSeparator3.Size = new System.Drawing.Size(190, 6);
			// 
			// splitTextureAtlasToolStripMenuItem
			// 
			splitTextureAtlasToolStripMenuItem.Name = "splitTextureAtlasToolStripMenuItem";
			splitTextureAtlasToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			splitTextureAtlasToolStripMenuItem.Text = "Split Texture Atlas";
			splitTextureAtlasToolStripMenuItem.Click += splitTextureAtlasToolStripMenuItem_Click;
			// 
			// rebuildTextureAtlasToolStripMenuItem
			// 
			rebuildTextureAtlasToolStripMenuItem.Name = "rebuildTextureAtlasToolStripMenuItem";
			rebuildTextureAtlasToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			rebuildTextureAtlasToolStripMenuItem.Text = "Rebuild Texture Atlas";
			rebuildTextureAtlasToolStripMenuItem.Click += rebuildTextureAtlasToolStripMenuItem_Click;
			// 
			// exportAtlasBoxesToolStripMenuItem
			// 
			exportAtlasBoxesToolStripMenuItem.Name = "exportAtlasBoxesToolStripMenuItem";
			exportAtlasBoxesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			exportAtlasBoxesToolStripMenuItem.Text = "Export Atlas Boxes";
			exportAtlasBoxesToolStripMenuItem.Click += exportAtlasBoxesToolStripMenuItem_Click;
			// 
			// autoFlagToolStripMenuItem
			// 
			autoFlagToolStripMenuItem.Name = "autoFlagToolStripMenuItem";
			autoFlagToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			autoFlagToolStripMenuItem.Text = "Auto Flag";
			autoFlagToolStripMenuItem.Click += autoFlagToolStripMenuItem_Click;
			// 
			// duplicateSymbolsToolStripMenuItem
			// 
			duplicateSymbolsToolStripMenuItem.Name = "duplicateSymbolsToolStripMenuItem";
			duplicateSymbolsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			duplicateSymbolsToolStripMenuItem.Text = "Duplicate Symbols";
			duplicateSymbolsToolStripMenuItem.Click += duplicateSymbolsToolStripMenuItem_Click;
			// 
			// wizardToolStripMenuItem
			// 
			wizardToolStripMenuItem.Name = "wizardToolStripMenuItem";
			wizardToolStripMenuItem.Size = new System.Drawing.Size(55, 22);
			wizardToolStripMenuItem.Text = "Wizard";
			wizardToolStripMenuItem.Click += wizardToolStripMenuItem_Click;
			// 
			// previewAnimToolStripMenuItem
			// 
			previewAnimToolStripMenuItem.Enabled = false;
			previewAnimToolStripMenuItem.Name = "previewAnimToolStripMenuItem";
			previewAnimToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			previewAnimToolStripMenuItem.Text = "Animation Viewer";
			previewAnimToolStripMenuItem.Click += previewAnimToolStripMenuItem_Click;
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer1.Location = new System.Drawing.Point(0, 24);
			splitContainer1.Margin = new System.Windows.Forms.Padding(2);
			splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(atlasView);
			splitContainer1.Size = new System.Drawing.Size(668, 631);
			splitContainer1.SplitterDistance = 221;
			splitContainer1.TabIndex = 1;
			// 
			// splitContainer2
			// 
			splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			splitContainer2.Location = new System.Drawing.Point(0, 0);
			splitContainer2.Margin = new System.Windows.Forms.Padding(2);
			splitContainer2.Name = "splitContainer2";
			splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			splitContainer2.Panel1.Controls.Add(buildTreeView);
			splitContainer2.Panel1.Controls.Add(label1);
			splitContainer2.Panel1MinSize = 200;
			// 
			// splitContainer2.Panel2
			// 
			splitContainer2.Panel2.Controls.Add(propertyGrid);
			splitContainer2.Panel2.Controls.Add(label2);
			splitContainer2.Size = new System.Drawing.Size(221, 631);
			splitContainer2.SplitterDistance = 231;
			splitContainer2.SplitterWidth = 3;
			splitContainer2.TabIndex = 4;
			// 
			// buildTreeView
			// 
			buildTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			buildTreeView.Location = new System.Drawing.Point(0, 30);
			buildTreeView.Margin = new System.Windows.Forms.Padding(2);
			buildTreeView.Name = "buildTreeView";
			buildTreeView.Size = new System.Drawing.Size(221, 201);
			buildTreeView.TabIndex = 1;
			buildTreeView.AfterSelect += buildTreeView_AfterSelect;
			// 
			// label1
			// 
			label1.Dock = System.Windows.Forms.DockStyle.Top;
			label1.Location = new System.Drawing.Point(0, 0);
			label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label1.Name = "label1";
			label1.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			label1.Size = new System.Drawing.Size(221, 30);
			label1.TabIndex = 0;
			label1.Text = "Symbol Tree:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// propertyGrid
			// 
			propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			propertyGrid.HelpVisible = false;
			propertyGrid.Location = new System.Drawing.Point(0, 30);
			propertyGrid.Margin = new System.Windows.Forms.Padding(2);
			propertyGrid.Name = "propertyGrid";
			propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
			propertyGrid.Size = new System.Drawing.Size(221, 367);
			propertyGrid.TabIndex = 2;
			propertyGrid.ToolbarVisible = false;
			propertyGrid.PropertyValueChanged += propertyGrid_PropertyValueChanged;
			// 
			// label2
			// 
			label2.Dock = System.Windows.Forms.DockStyle.Top;
			label2.Location = new System.Drawing.Point(0, 0);
			label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			label2.Name = "label2";
			label2.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
			label2.Size = new System.Drawing.Size(221, 30);
			label2.TabIndex = 3;
			label2.Text = "Symbol Inspector:";
			label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// atlasView
			// 
			atlasView.BackColor = System.Drawing.Color.Gray;
			atlasView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			atlasView.Dock = System.Windows.Forms.DockStyle.Fill;
			atlasView.Location = new System.Drawing.Point(0, 0);
			atlasView.Margin = new System.Windows.Forms.Padding(2);
			atlasView.Name = "atlasView";
			atlasView.Size = new System.Drawing.Size(443, 631);
			atlasView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			atlasView.TabIndex = 0;
			atlasView.TabStop = false;
			// 
			// toolsToolStripMenuItem1
			// 
			toolsToolStripMenuItem1.Name = "toolsToolStripMenuItem1";
			toolsToolStripMenuItem1.Size = new System.Drawing.Size(193, 22);
			toolsToolStripMenuItem1.Text = "Tools";
			// 
			// renameSymbolToolStripMenuItem
			// 
			renameSymbolToolStripMenuItem.Name = "renameSymbolToolStripMenuItem";
			renameSymbolToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
			renameSymbolToolStripMenuItem.Text = "Rename Symbol";
			renameSymbolToolStripMenuItem.Click += renameSymbolToolStripMenuItem_Click;
			// 
			// MainForm
			// 
			AllowDrop = true;
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(668, 655);
			Controls.Add(splitContainer1);
			Controls.Add(menuStrip1);
			MainMenuStrip = menuStrip1;
			Margin = new System.Windows.Forms.Padding(2);
			Name = "MainForm";
			Text = "Kanim Explorer";
			DragDrop += MainForm_DragDrop;
			DragEnter += MainForm_DragEnter;
			menuStrip1.ResumeLayout(false);
			menuStrip1.PerformLayout();
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			splitContainer2.Panel1.ResumeLayout(false);
			splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
			splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)atlasView).EndInit();
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.PictureBox atlasView;
		private System.Windows.Forms.TreeView buildTreeView;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.PropertyGrid propertyGrid;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.SplitContainer splitContainer2;
		private System.Windows.Forms.ToolStripMenuItem saveBuildFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem splitTextureAtlasToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAnimFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveBlankAnimbytesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem rebuildTextureAtlasToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripMenuItem saveTextureAtlasToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem previewAnimToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem wizardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openSCMLToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exportAtlasBoxesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem autoFlagToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem duplicateSymbolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem saveSCMLToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem renameSymbolToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem1;
	}
}

