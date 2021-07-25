
namespace KanimExplorer.Forms
{
	partial class AnimationForm
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
			this.panelExplorer = new System.Windows.Forms.Panel();
			this.listBoxBanks = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.closeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panelDisplayArea = new System.Windows.Forms.Panel();
			this.panelProperties = new System.Windows.Forms.Panel();
			this.panelTimeline = new System.Windows.Forms.Panel();
			this.checkBoxPlay = new System.Windows.Forms.CheckBox();
			this.textBoxFrameNumber = new System.Windows.Forms.TextBox();
			this.buttonNextFrame = new System.Windows.Forms.Button();
			this.buttonPrevFrame = new System.Windows.Forms.Button();
			this.panelExplorer.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.panelTimeline.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelExplorer
			// 
			this.panelExplorer.Controls.Add(this.listBoxBanks);
			this.panelExplorer.Controls.Add(this.label1);
			this.panelExplorer.Dock = System.Windows.Forms.DockStyle.Left;
			this.panelExplorer.Location = new System.Drawing.Point(0, 24);
			this.panelExplorer.Name = "panelExplorer";
			this.panelExplorer.Size = new System.Drawing.Size(200, 642);
			this.panelExplorer.TabIndex = 0;
			// 
			// listBoxBanks
			// 
			this.listBoxBanks.Dock = System.Windows.Forms.DockStyle.Top;
			this.listBoxBanks.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.listBoxBanks.FormattingEnabled = true;
			this.listBoxBanks.ItemHeight = 16;
			this.listBoxBanks.Location = new System.Drawing.Point(0, 30);
			this.listBoxBanks.Name = "listBoxBanks";
			this.listBoxBanks.Size = new System.Drawing.Size(200, 388);
			this.listBoxBanks.TabIndex = 1;
			this.listBoxBanks.SelectedIndexChanged += new System.EventHandler(this.listBoxBanks_SelectedIndexChanged);
			// 
			// label1
			// 
			this.label1.Dock = System.Windows.Forms.DockStyle.Top;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(200, 30);
			this.label1.TabIndex = 0;
			this.label1.Text = "Animations:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1312, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.closeToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// closeToolStripMenuItem
			// 
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new System.EventHandler(this.closeToolStripMenuItem_Click);
			// 
			// panelDisplayArea
			// 
			this.panelDisplayArea.BackColor = System.Drawing.SystemColors.ControlDarkDark;
			this.panelDisplayArea.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panelDisplayArea.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelDisplayArea.Location = new System.Drawing.Point(200, 24);
			this.panelDisplayArea.Name = "panelDisplayArea";
			this.panelDisplayArea.Size = new System.Drawing.Size(912, 642);
			this.panelDisplayArea.TabIndex = 2;
			// 
			// panelProperties
			// 
			this.panelProperties.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelProperties.Location = new System.Drawing.Point(1112, 24);
			this.panelProperties.Name = "panelProperties";
			this.panelProperties.Size = new System.Drawing.Size(200, 642);
			this.panelProperties.TabIndex = 0;
			this.panelProperties.Visible = false;
			// 
			// panelTimeline
			// 
			this.panelTimeline.Controls.Add(this.checkBoxPlay);
			this.panelTimeline.Controls.Add(this.textBoxFrameNumber);
			this.panelTimeline.Controls.Add(this.buttonNextFrame);
			this.panelTimeline.Controls.Add(this.buttonPrevFrame);
			this.panelTimeline.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelTimeline.Location = new System.Drawing.Point(0, 666);
			this.panelTimeline.Name = "panelTimeline";
			this.panelTimeline.Size = new System.Drawing.Size(1312, 100);
			this.panelTimeline.TabIndex = 0;
			// 
			// checkBoxPlay
			// 
			this.checkBoxPlay.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxPlay.AutoSize = true;
			this.checkBoxPlay.Location = new System.Drawing.Point(12, 35);
			this.checkBoxPlay.Name = "checkBoxPlay";
			this.checkBoxPlay.Size = new System.Drawing.Size(37, 23);
			this.checkBoxPlay.TabIndex = 3;
			this.checkBoxPlay.Text = "Play";
			this.checkBoxPlay.UseVisualStyleBackColor = true;
			this.checkBoxPlay.CheckedChanged += new System.EventHandler(this.checkBoxPlay_CheckedChanged);
			// 
			// textBoxFrameNumber
			// 
			this.textBoxFrameNumber.Location = new System.Drawing.Point(93, 8);
			this.textBoxFrameNumber.Name = "textBoxFrameNumber";
			this.textBoxFrameNumber.ReadOnly = true;
			this.textBoxFrameNumber.Size = new System.Drawing.Size(100, 20);
			this.textBoxFrameNumber.TabIndex = 2;
			this.textBoxFrameNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// buttonNextFrame
			// 
			this.buttonNextFrame.Location = new System.Drawing.Point(199, 6);
			this.buttonNextFrame.Name = "buttonNextFrame";
			this.buttonNextFrame.Size = new System.Drawing.Size(75, 23);
			this.buttonNextFrame.TabIndex = 1;
			this.buttonNextFrame.Text = "> >";
			this.buttonNextFrame.UseVisualStyleBackColor = true;
			this.buttonNextFrame.Click += new System.EventHandler(this.buttonNextFrame_Click);
			// 
			// buttonPrevFrame
			// 
			this.buttonPrevFrame.Location = new System.Drawing.Point(12, 6);
			this.buttonPrevFrame.Name = "buttonPrevFrame";
			this.buttonPrevFrame.Size = new System.Drawing.Size(75, 23);
			this.buttonPrevFrame.TabIndex = 0;
			this.buttonPrevFrame.Text = "< <";
			this.buttonPrevFrame.UseVisualStyleBackColor = true;
			this.buttonPrevFrame.Click += new System.EventHandler(this.buttonPrevFrame_Click);
			// 
			// AnimationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1312, 766);
			this.Controls.Add(this.panelDisplayArea);
			this.Controls.Add(this.panelProperties);
			this.Controls.Add(this.panelExplorer);
			this.Controls.Add(this.panelTimeline);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "AnimationForm";
			this.Text = "Animation Viewer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AnimationForm_FormClosing);
			this.Load += new System.EventHandler(this.AnimationForm_Load);
			this.panelExplorer.ResumeLayout(false);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.panelTimeline.ResumeLayout(false);
			this.panelTimeline.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panelExplorer;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;
		private System.Windows.Forms.Panel panelDisplayArea;
		private System.Windows.Forms.Panel panelProperties;
		private System.Windows.Forms.Panel panelTimeline;
		private System.Windows.Forms.ListBox listBoxBanks;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox textBoxFrameNumber;
		private System.Windows.Forms.Button buttonNextFrame;
		private System.Windows.Forms.Button buttonPrevFrame;
		private System.Windows.Forms.CheckBox checkBoxPlay;
	}
}