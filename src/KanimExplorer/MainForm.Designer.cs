namespace KanimalExplorer
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveBuildFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAnimFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.convertToSCMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitTextureAtlasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.buildTreeView = new System.Windows.Forms.TreeView();
			this.label1 = new System.Windows.Forms.Label();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.label2 = new System.Windows.Forms.Label();
			this.atlasView = new System.Windows.Forms.PictureBox();
			this.saveBlankAnimbytesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.atlasView)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(860, 33);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.saveBuildFileToolStripMenuItem,
            this.saveAnimFileToolStripMenuItem,
            this.toolStripSeparator2,
            this.closeToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(54, 29);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveBuildFileToolStripMenuItem
			// 
			this.saveBuildFileToolStripMenuItem.Enabled = false;
			this.saveBuildFileToolStripMenuItem.Name = "saveBuildFileToolStripMenuItem";
			this.saveBuildFileToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
			this.saveBuildFileToolStripMenuItem.Text = "Save Build File...";
			this.saveBuildFileToolStripMenuItem.Click += new System.EventHandler(this.saveBuildFileToolStripMenuItem_Click);
			// 
			// saveAnimFileToolStripMenuItem
			// 
			this.saveAnimFileToolStripMenuItem.Enabled = false;
			this.saveAnimFileToolStripMenuItem.Name = "saveAnimFileToolStripMenuItem";
			this.saveAnimFileToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
			this.saveAnimFileToolStripMenuItem.Text = "Save Anim File...";
			this.saveAnimFileToolStripMenuItem.Click += new System.EventHandler(this.saveAnimFileToolStripMenuItem_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(267, 6);
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Enabled = false;
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(267, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// toolsToolStripMenuItem
			// 
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.convertToSCMLToolStripMenuItem,
            this.splitTextureAtlasToolStripMenuItem,
            this.saveBlankAnimbytesToolStripMenuItem});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new System.Drawing.Size(69, 29);
			this.toolsToolStripMenuItem.Text = "Tools";
			// 
			// convertToSCMLToolStripMenuItem
			// 
			this.convertToSCMLToolStripMenuItem.Name = "convertToSCMLToolStripMenuItem";
			this.convertToSCMLToolStripMenuItem.Size = new System.Drawing.Size(292, 34);
			this.convertToSCMLToolStripMenuItem.Text = "Convert to SCML";
			this.convertToSCMLToolStripMenuItem.Click += new System.EventHandler(this.convertToSCMLToolStripMenuItem_Click);
			// 
			// splitTextureAtlasToolStripMenuItem
			// 
			this.splitTextureAtlasToolStripMenuItem.Name = "splitTextureAtlasToolStripMenuItem";
			this.splitTextureAtlasToolStripMenuItem.Size = new System.Drawing.Size(292, 34);
			this.splitTextureAtlasToolStripMenuItem.Text = "Split Texture Atlas";
			this.splitTextureAtlasToolStripMenuItem.Click += new System.EventHandler(this.splitTextureAtlasToolStripMenuItem_Click);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 33);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.atlasView);
			this.splitContainer1.Size = new System.Drawing.Size(860, 841);
			this.splitContainer1.SplitterDistance = 286;
			this.splitContainer1.TabIndex = 1;
			// 
			// splitContainer2
			// 
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.Controls.Add(this.buildTreeView);
			this.splitContainer2.Panel1.Controls.Add(this.label1);
			this.splitContainer2.Panel1MinSize = 200;
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.Controls.Add(this.propertyGrid);
			this.splitContainer2.Panel2.Controls.Add(this.label2);
			this.splitContainer2.Size = new System.Drawing.Size(286, 841);
			this.splitContainer2.SplitterDistance = 200;
			this.splitContainer2.TabIndex = 4;
			// 
			// buildTreeView
			// 
			this.buildTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.buildTreeView.Location = new System.Drawing.Point(0, 40);
			this.buildTreeView.Name = "buildTreeView";
			this.buildTreeView.Size = new System.Drawing.Size(286, 160);
			this.buildTreeView.TabIndex = 1;
			this.buildTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.buildTreeView_AfterSelect);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Padding = new System.Windows.Forms.Padding(5);
			this.label1.Size = new System.Drawing.Size(286, 40);
			this.label1.TabIndex = 0;
			this.label1.Text = "Symbol Tree:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// propertyGrid
			// 
			this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.propertyGrid.HelpVisible = false;
			this.propertyGrid.Location = new System.Drawing.Point(0, 40);
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
			this.propertyGrid.Size = new System.Drawing.Size(286, 597);
			this.propertyGrid.TabIndex = 2;
			this.propertyGrid.ToolbarVisible = false;
			// 
			// label2
			// 
			this.label2.Dock = System.Windows.Forms.DockStyle.Top;
			this.label2.Location = new System.Drawing.Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Padding = new System.Windows.Forms.Padding(5);
			this.label2.Size = new System.Drawing.Size(286, 40);
			this.label2.TabIndex = 3;
			this.label2.Text = "Symbol Inspector:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// atlasView
			// 
			this.atlasView.BackColor = System.Drawing.Color.Gray;
			this.atlasView.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.atlasView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.atlasView.Location = new System.Drawing.Point(0, 0);
			this.atlasView.Name = "atlasView";
			this.atlasView.Size = new System.Drawing.Size(570, 841);
			this.atlasView.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.atlasView.TabIndex = 0;
			this.atlasView.TabStop = false;
			// 
			// saveBlankAnimbytesToolStripMenuItem
			// 
			this.saveBlankAnimbytesToolStripMenuItem.Name = "saveBlankAnimbytesToolStripMenuItem";
			this.saveBlankAnimbytesToolStripMenuItem.Size = new System.Drawing.Size(292, 34);
			this.saveBlankAnimbytesToolStripMenuItem.Text = "Save Blank Anim.bytes";
			this.saveBlankAnimbytesToolStripMenuItem.Click += new System.EventHandler(this.saveBlankAnimbytesToolStripMenuItem_Click);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(860, 874);
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "Kanim Explorer";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.atlasView)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.ToolStripMenuItem convertToSCMLToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem splitTextureAtlasToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAnimFileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveBlankAnimbytesToolStripMenuItem;
	}
}

