using System.Windows.Forms;
using System.Drawing;

namespace KanimView
{
	partial class AnimPreviewForm
	{
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
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
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			menuStrip = new MenuStrip();
			fileToolStripMenuItem = new ToolStripMenuItem();
			openKanimFilesToolStripMenuItem = new ToolStripMenuItem();
			openSCMLToolStripMenuItem = new ToolStripMenuItem();
			toolStripSeparator2 = new ToolStripSeparator();
			closeAllToolStripMenuItem = new ToolStripMenuItem();
			toolStripSeparator1 = new ToolStripSeparator();
			exitToolStripMenuItem = new ToolStripMenuItem();
			editToolStripMenuItem = new ToolStripMenuItem();
			viewToolStripMenuItem = new ToolStripMenuItem();
			toggleBloomPreviewToolStripMenuItem = new ToolStripMenuItem();
			toolsToolStripMenuItem = new ToolStripMenuItem();
			statusStrip = new StatusStrip();
			splitContainer = new SplitContainer();
			propertyGrid = new PropertyGrid();
			tabControl = new TabControl();
			filesPage = new TabPage();
			symbolsPage = new TabPage();
			animsPage = new TabPage();
			animDisplay = new SkiaSharp.Views.Desktop.SKControl();
			timelinePanel = new Panel();
			flowLayoutPanel2 = new FlowLayoutPanel();
			preToWorkingCheckbox = new CheckBox();
			workingToPstCheckbox = new CheckBox();
			flowLayoutPanel1 = new FlowLayoutPanel();
			playButton = new Button();
			pauseButton = new Button();
			beginningButton = new Button();
			prevFrameButton = new Button();
			playbackTrackBar = new TrackBar();
			nextFrameButton = new Button();
			endingButton = new Button();
			loopCheckbox = new CheckBox();
			numRepeatsTextBox = new TextBox();
			label1 = new Label();
			splitContainer1 = new SplitContainer();
			filesListView = new ListView();
			symbolsListView = new ListView();
			animationsListView = new ListView();
			menuStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
			splitContainer.Panel1.SuspendLayout();
			splitContainer.Panel2.SuspendLayout();
			splitContainer.SuspendLayout();
			tabControl.SuspendLayout();
			filesPage.SuspendLayout();
			symbolsPage.SuspendLayout();
			animsPage.SuspendLayout();
			timelinePanel.SuspendLayout();
			flowLayoutPanel2.SuspendLayout();
			flowLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)playbackTrackBar).BeginInit();
			((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
			splitContainer1.Panel1.SuspendLayout();
			splitContainer1.Panel2.SuspendLayout();
			splitContainer1.SuspendLayout();
			SuspendLayout();
			// 
			// menuStrip
			// 
			menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem, viewToolStripMenuItem, toolsToolStripMenuItem });
			menuStrip.Location = new Point(0, 0);
			menuStrip.Name = "menuStrip";
			menuStrip.Size = new Size(1264, 24);
			menuStrip.TabIndex = 0;
			menuStrip.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openKanimFilesToolStripMenuItem, openSCMLToolStripMenuItem, toolStripSeparator2, closeAllToolStripMenuItem, toolStripSeparator1, exitToolStripMenuItem });
			fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			fileToolStripMenuItem.Size = new Size(37, 20);
			fileToolStripMenuItem.Text = "File";
			// 
			// openKanimFilesToolStripMenuItem
			// 
			openKanimFilesToolStripMenuItem.Name = "openKanimFilesToolStripMenuItem";
			openKanimFilesToolStripMenuItem.Size = new Size(166, 22);
			openKanimFilesToolStripMenuItem.Text = "Open Kanim Files";
			// 
			// openSCMLToolStripMenuItem
			// 
			openSCMLToolStripMenuItem.Name = "openSCMLToolStripMenuItem";
			openSCMLToolStripMenuItem.Size = new Size(166, 22);
			openSCMLToolStripMenuItem.Text = "Open SCML";
			// 
			// toolStripSeparator2
			// 
			toolStripSeparator2.Name = "toolStripSeparator2";
			toolStripSeparator2.Size = new Size(163, 6);
			// 
			// closeAllToolStripMenuItem
			// 
			closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
			closeAllToolStripMenuItem.Size = new Size(166, 22);
			closeAllToolStripMenuItem.Text = "Close All";
			// 
			// toolStripSeparator1
			// 
			toolStripSeparator1.Name = "toolStripSeparator1";
			toolStripSeparator1.Size = new Size(163, 6);
			// 
			// exitToolStripMenuItem
			// 
			exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			exitToolStripMenuItem.Size = new Size(166, 22);
			exitToolStripMenuItem.Text = "Exit";
			// 
			// editToolStripMenuItem
			// 
			editToolStripMenuItem.Name = "editToolStripMenuItem";
			editToolStripMenuItem.Size = new Size(39, 20);
			editToolStripMenuItem.Text = "Edit";
			// 
			// viewToolStripMenuItem
			// 
			viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { toggleBloomPreviewToolStripMenuItem });
			viewToolStripMenuItem.Name = "viewToolStripMenuItem";
			viewToolStripMenuItem.Size = new Size(44, 20);
			viewToolStripMenuItem.Text = "View";
			// 
			// toggleBloomPreviewToolStripMenuItem
			// 
			toggleBloomPreviewToolStripMenuItem.Name = "toggleBloomPreviewToolStripMenuItem";
			toggleBloomPreviewToolStripMenuItem.Size = new Size(192, 22);
			toggleBloomPreviewToolStripMenuItem.Text = "Toggle Bloom Preview";
			// 
			// toolsToolStripMenuItem
			// 
			toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			toolsToolStripMenuItem.Size = new Size(47, 20);
			toolsToolStripMenuItem.Text = "Tools";
			// 
			// statusStrip
			// 
			statusStrip.Location = new Point(0, 899);
			statusStrip.Name = "statusStrip";
			statusStrip.Size = new Size(1264, 22);
			statusStrip.TabIndex = 1;
			statusStrip.Text = "statusStrip1";
			// 
			// splitContainer
			// 
			splitContainer.Dock = DockStyle.Fill;
			splitContainer.Location = new Point(0, 24);
			splitContainer.Name = "splitContainer";
			// 
			// splitContainer.Panel1
			// 
			splitContainer.Panel1.Controls.Add(splitContainer1);
			// 
			// splitContainer.Panel2
			// 
			splitContainer.Panel2.Controls.Add(animDisplay);
			splitContainer.Panel2.Controls.Add(timelinePanel);
			splitContainer.Size = new Size(1264, 875);
			splitContainer.SplitterDistance = 420;
			splitContainer.TabIndex = 2;
			// 
			// propertyGrid
			// 
			propertyGrid.Dock = DockStyle.Fill;
			propertyGrid.Location = new Point(0, 0);
			propertyGrid.Name = "propertyGrid";
			propertyGrid.Size = new Size(420, 673);
			propertyGrid.TabIndex = 1;
			// 
			// tabControl
			// 
			tabControl.Controls.Add(filesPage);
			tabControl.Controls.Add(symbolsPage);
			tabControl.Controls.Add(animsPage);
			tabControl.Dock = DockStyle.Fill;
			tabControl.Location = new Point(0, 0);
			tabControl.Name = "tabControl";
			tabControl.SelectedIndex = 0;
			tabControl.Size = new Size(420, 198);
			tabControl.TabIndex = 0;
			// 
			// filesPage
			// 
			filesPage.Controls.Add(filesListView);
			filesPage.Location = new Point(4, 24);
			filesPage.Name = "filesPage";
			filesPage.Padding = new Padding(3);
			filesPage.Size = new Size(412, 170);
			filesPage.TabIndex = 0;
			filesPage.Text = "Files";
			filesPage.UseVisualStyleBackColor = true;
			// 
			// symbolsPage
			// 
			symbolsPage.Controls.Add(symbolsListView);
			symbolsPage.Location = new Point(4, 24);
			symbolsPage.Name = "symbolsPage";
			symbolsPage.Padding = new Padding(3);
			symbolsPage.Size = new Size(412, 170);
			symbolsPage.TabIndex = 1;
			symbolsPage.Text = "Symbols";
			symbolsPage.UseVisualStyleBackColor = true;
			// 
			// animsPage
			// 
			animsPage.Controls.Add(animationsListView);
			animsPage.Location = new Point(4, 24);
			animsPage.Name = "animsPage";
			animsPage.Size = new Size(412, 170);
			animsPage.TabIndex = 2;
			animsPage.Text = "Animations";
			animsPage.UseVisualStyleBackColor = true;
			// 
			// animDisplay
			// 
			animDisplay.BackColor = Color.Gray;
			animDisplay.Dock = DockStyle.Fill;
			animDisplay.Location = new Point(0, 0);
			animDisplay.Name = "animDisplay";
			animDisplay.Size = new Size(840, 815);
			animDisplay.TabIndex = 0;
			// 
			// timelinePanel
			// 
			timelinePanel.Controls.Add(flowLayoutPanel2);
			timelinePanel.Controls.Add(flowLayoutPanel1);
			timelinePanel.Dock = DockStyle.Bottom;
			timelinePanel.Location = new Point(0, 815);
			timelinePanel.Name = "timelinePanel";
			timelinePanel.Size = new Size(840, 60);
			timelinePanel.TabIndex = 1;
			// 
			// flowLayoutPanel2
			// 
			flowLayoutPanel2.Controls.Add(preToWorkingCheckbox);
			flowLayoutPanel2.Controls.Add(workingToPstCheckbox);
			flowLayoutPanel2.Dock = DockStyle.Top;
			flowLayoutPanel2.Location = new Point(0, 36);
			flowLayoutPanel2.Name = "flowLayoutPanel2";
			flowLayoutPanel2.Size = new Size(840, 24);
			flowLayoutPanel2.TabIndex = 1;
			// 
			// preToWorkingCheckbox
			// 
			preToWorkingCheckbox.AutoSize = true;
			preToWorkingCheckbox.Location = new Point(3, 3);
			preToWorkingCheckbox.Name = "preToWorkingCheckbox";
			preToWorkingCheckbox.Size = new Size(182, 19);
			preToWorkingCheckbox.TabIndex = 0;
			preToWorkingCheckbox.Text = "working_pre -> working_loop";
			preToWorkingCheckbox.UseVisualStyleBackColor = true;
			// 
			// workingToPstCheckbox
			// 
			workingToPstCheckbox.AutoSize = true;
			workingToPstCheckbox.Location = new Point(191, 3);
			workingToPstCheckbox.Name = "workingToPstCheckbox";
			workingToPstCheckbox.Size = new Size(181, 19);
			workingToPstCheckbox.TabIndex = 1;
			workingToPstCheckbox.Text = "working_loop -> working_pst";
			workingToPstCheckbox.UseVisualStyleBackColor = true;
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.Controls.Add(playButton);
			flowLayoutPanel1.Controls.Add(pauseButton);
			flowLayoutPanel1.Controls.Add(beginningButton);
			flowLayoutPanel1.Controls.Add(prevFrameButton);
			flowLayoutPanel1.Controls.Add(playbackTrackBar);
			flowLayoutPanel1.Controls.Add(nextFrameButton);
			flowLayoutPanel1.Controls.Add(endingButton);
			flowLayoutPanel1.Controls.Add(loopCheckbox);
			flowLayoutPanel1.Controls.Add(numRepeatsTextBox);
			flowLayoutPanel1.Controls.Add(label1);
			flowLayoutPanel1.Dock = DockStyle.Top;
			flowLayoutPanel1.Location = new Point(0, 0);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new Size(840, 36);
			flowLayoutPanel1.TabIndex = 0;
			// 
			// playButton
			// 
			playButton.Location = new Point(3, 3);
			playButton.Name = "playButton";
			playButton.Size = new Size(30, 30);
			playButton.TabIndex = 0;
			playButton.Text = "⏵";
			playButton.UseVisualStyleBackColor = true;
			playButton.Click += button1_Click;
			// 
			// pauseButton
			// 
			pauseButton.Location = new Point(39, 3);
			pauseButton.Name = "pauseButton";
			pauseButton.Size = new Size(30, 30);
			pauseButton.TabIndex = 1;
			pauseButton.Text = "⏸";
			pauseButton.UseVisualStyleBackColor = true;
			// 
			// beginningButton
			// 
			beginningButton.Location = new Point(75, 3);
			beginningButton.Name = "beginningButton";
			beginningButton.Size = new Size(30, 30);
			beginningButton.TabIndex = 6;
			beginningButton.Text = "⏮";
			beginningButton.UseVisualStyleBackColor = true;
			// 
			// prevFrameButton
			// 
			prevFrameButton.Location = new Point(111, 3);
			prevFrameButton.Name = "prevFrameButton";
			prevFrameButton.Size = new Size(30, 30);
			prevFrameButton.TabIndex = 2;
			prevFrameButton.Text = "<";
			prevFrameButton.UseVisualStyleBackColor = true;
			// 
			// playbackTrackBar
			// 
			playbackTrackBar.AutoSize = false;
			playbackTrackBar.Location = new Point(147, 6);
			playbackTrackBar.Margin = new Padding(3, 6, 3, 3);
			playbackTrackBar.Name = "playbackTrackBar";
			playbackTrackBar.Size = new Size(300, 24);
			playbackTrackBar.TabIndex = 3;
			playbackTrackBar.TickStyle = TickStyle.None;
			// 
			// nextFrameButton
			// 
			nextFrameButton.Location = new Point(453, 3);
			nextFrameButton.Name = "nextFrameButton";
			nextFrameButton.Size = new Size(30, 30);
			nextFrameButton.TabIndex = 4;
			nextFrameButton.Text = ">";
			nextFrameButton.UseVisualStyleBackColor = true;
			// 
			// endingButton
			// 
			endingButton.Location = new Point(489, 3);
			endingButton.Name = "endingButton";
			endingButton.Size = new Size(30, 30);
			endingButton.TabIndex = 7;
			endingButton.Text = "⏭";
			endingButton.UseVisualStyleBackColor = true;
			// 
			// loopCheckbox
			// 
			loopCheckbox.Appearance = Appearance.Button;
			loopCheckbox.Location = new Point(525, 3);
			loopCheckbox.Name = "loopCheckbox";
			loopCheckbox.Size = new Size(30, 30);
			loopCheckbox.TabIndex = 5;
			loopCheckbox.Text = "🔁";
			loopCheckbox.UseVisualStyleBackColor = true;
			// 
			// numRepeatsTextBox
			// 
			numRepeatsTextBox.Location = new Point(561, 6);
			numRepeatsTextBox.Margin = new Padding(3, 6, 3, 3);
			numRepeatsTextBox.Name = "numRepeatsTextBox";
			numRepeatsTextBox.Size = new Size(80, 23);
			numRepeatsTextBox.TabIndex = 9;
			numRepeatsTextBox.TextAlign = HorizontalAlignment.Center;
			// 
			// label1
			// 
			label1.Location = new Point(647, 6);
			label1.Margin = new Padding(3, 6, 3, 3);
			label1.Name = "label1";
			label1.Size = new Size(40, 23);
			label1.TabIndex = 8;
			label1.Text = "times";
			label1.TextAlign = ContentAlignment.MiddleLeft;
			// 
			// splitContainer1
			// 
			splitContainer1.Dock = DockStyle.Fill;
			splitContainer1.Location = new Point(0, 0);
			splitContainer1.Name = "splitContainer1";
			splitContainer1.Orientation = Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			splitContainer1.Panel1.Controls.Add(tabControl);
			// 
			// splitContainer1.Panel2
			// 
			splitContainer1.Panel2.Controls.Add(propertyGrid);
			splitContainer1.Size = new Size(420, 875);
			splitContainer1.SplitterDistance = 198;
			splitContainer1.TabIndex = 2;
			// 
			// filesListView
			// 
			filesListView.Dock = DockStyle.Fill;
			filesListView.Location = new Point(3, 3);
			filesListView.Name = "filesListView";
			filesListView.Size = new Size(406, 164);
			filesListView.TabIndex = 0;
			filesListView.UseCompatibleStateImageBehavior = false;
			// 
			// symbolsListView
			// 
			symbolsListView.Dock = DockStyle.Fill;
			symbolsListView.Location = new Point(3, 3);
			symbolsListView.Name = "symbolsListView";
			symbolsListView.Size = new Size(406, 164);
			symbolsListView.TabIndex = 0;
			symbolsListView.UseCompatibleStateImageBehavior = false;
			// 
			// animationsListView
			// 
			animationsListView.Dock = DockStyle.Fill;
			animationsListView.Location = new Point(0, 0);
			animationsListView.Name = "animationsListView";
			animationsListView.Size = new Size(412, 170);
			animationsListView.TabIndex = 0;
			animationsListView.UseCompatibleStateImageBehavior = false;
			// 
			// AnimPreviewForm
			// 
			AutoScaleDimensions = new SizeF(7F, 15F);
			AutoScaleMode = AutoScaleMode.Font;
			ClientSize = new Size(1264, 921);
			Controls.Add(splitContainer);
			Controls.Add(statusStrip);
			Controls.Add(menuStrip);
			MainMenuStrip = menuStrip;
			Name = "AnimPreviewForm";
			Text = "Kanim View";
			menuStrip.ResumeLayout(false);
			menuStrip.PerformLayout();
			splitContainer.Panel1.ResumeLayout(false);
			splitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
			splitContainer.ResumeLayout(false);
			tabControl.ResumeLayout(false);
			filesPage.ResumeLayout(false);
			symbolsPage.ResumeLayout(false);
			animsPage.ResumeLayout(false);
			timelinePanel.ResumeLayout(false);
			flowLayoutPanel2.ResumeLayout(false);
			flowLayoutPanel2.PerformLayout();
			flowLayoutPanel1.ResumeLayout(false);
			flowLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)playbackTrackBar).EndInit();
			splitContainer1.Panel1.ResumeLayout(false);
			splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
			splitContainer1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private MenuStrip menuStrip;
		private StatusStrip statusStrip;
		private SplitContainer splitContainer;
		private ToolStripMenuItem fileToolStripMenuItem;
		private ToolStripMenuItem editToolStripMenuItem;
		private ToolStripMenuItem viewToolStripMenuItem;
		private ToolStripMenuItem toolsToolStripMenuItem;
		private SkiaSharp.Views.Desktop.SKControl animDisplay;
		private Panel timelinePanel;
		private ToolStripMenuItem openKanimFilesToolStripMenuItem;
		private ToolStripMenuItem openSCMLToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator2;
		private ToolStripMenuItem closeAllToolStripMenuItem;
		private ToolStripSeparator toolStripSeparator1;
		private ToolStripMenuItem exitToolStripMenuItem;
		private PropertyGrid propertyGrid;
		private TabControl tabControl;
		private TabPage filesPage;
		private TabPage symbolsPage;
		private TabPage animsPage;
		private FlowLayoutPanel flowLayoutPanel1;
		private Button playButton;
		private Button pauseButton;
		private Button prevFrameButton;
		private TrackBar playbackTrackBar;
		private ToolStripMenuItem toggleBloomPreviewToolStripMenuItem;
		private Button beginningButton;
		private Button nextFrameButton;
		private Button endingButton;
		private CheckBox loopCheckbox;
		private FlowLayoutPanel flowLayoutPanel2;
		private CheckBox preToWorkingCheckbox;
		private CheckBox workingToPstCheckbox;
		private TextBox numRepeatsTextBox;
		private Label label1;
		private SplitContainer splitContainer1;
		private ListView filesListView;
		private ListView symbolsListView;
		private ListView animationsListView;
	}
}
