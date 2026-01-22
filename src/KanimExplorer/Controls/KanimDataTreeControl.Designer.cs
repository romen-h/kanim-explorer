namespace KanimExplorer.Controls
{
	partial class KanimDataTreeControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KanimDataTreeControl));
			treeView = new System.Windows.Forms.TreeView();
			imageList = new System.Windows.Forms.ImageList(components);
			flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
			buttonAdd = new System.Windows.Forms.Button();
			buttonRemove = new System.Windows.Forms.Button();
			buttonRename = new System.Windows.Forms.Button();
			buttonDuplicate = new System.Windows.Forms.Button();
			buttonReplaceSprite = new System.Windows.Forms.Button();
			label1 = new System.Windows.Forms.Label();
			flowLayoutPanel1.SuspendLayout();
			SuspendLayout();
			// 
			// treeView
			// 
			treeView.Dock = System.Windows.Forms.DockStyle.Fill;
			treeView.ImageIndex = 0;
			treeView.ImageList = imageList;
			treeView.Location = new System.Drawing.Point(0, 25);
			treeView.Name = "treeView";
			treeView.SelectedImageIndex = 0;
			treeView.Size = new System.Drawing.Size(334, 450);
			treeView.TabIndex = 0;
			treeView.BeforeExpand += treeView_BeforeExpand;
			treeView.AfterSelect += treeView_AfterSelect;
			// 
			// imageList
			// 
			imageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
			imageList.ImageStream = (System.Windows.Forms.ImageListStreamer)resources.GetObject("imageList.ImageStream");
			imageList.TransparentColor = System.Drawing.Color.Transparent;
			imageList.Images.SetKeyName(0, "build");
			imageList.Images.SetKeyName(1, "symbol");
			imageList.Images.SetKeyName(2, "sprite");
			imageList.Images.SetKeyName(3, "bank");
			imageList.Images.SetKeyName(4, "animation");
			imageList.Images.SetKeyName(5, "frame");
			imageList.Images.SetKeyName(6, "element");
			// 
			// flowLayoutPanel1
			// 
			flowLayoutPanel1.AutoSize = true;
			flowLayoutPanel1.Controls.Add(buttonAdd);
			flowLayoutPanel1.Controls.Add(buttonRemove);
			flowLayoutPanel1.Controls.Add(buttonRename);
			flowLayoutPanel1.Controls.Add(buttonDuplicate);
			flowLayoutPanel1.Controls.Add(buttonReplaceSprite);
			flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			flowLayoutPanel1.Location = new System.Drawing.Point(0, 475);
			flowLayoutPanel1.Name = "flowLayoutPanel1";
			flowLayoutPanel1.Size = new System.Drawing.Size(334, 72);
			flowLayoutPanel1.TabIndex = 1;
			// 
			// buttonAdd
			// 
			buttonAdd.Location = new System.Drawing.Point(3, 3);
			buttonAdd.Name = "buttonAdd";
			buttonAdd.Size = new System.Drawing.Size(60, 30);
			buttonAdd.TabIndex = 0;
			buttonAdd.Text = "Add";
			buttonAdd.UseVisualStyleBackColor = true;
			buttonAdd.Click += buttonAdd_Click;
			// 
			// buttonRemove
			// 
			buttonRemove.Location = new System.Drawing.Point(69, 3);
			buttonRemove.Name = "buttonRemove";
			buttonRemove.Size = new System.Drawing.Size(60, 30);
			buttonRemove.TabIndex = 1;
			buttonRemove.Text = "Remove";
			buttonRemove.UseVisualStyleBackColor = true;
			buttonRemove.Click += buttonRemove_Click;
			// 
			// buttonRename
			// 
			buttonRename.Location = new System.Drawing.Point(135, 3);
			buttonRename.Name = "buttonRename";
			buttonRename.Size = new System.Drawing.Size(60, 30);
			buttonRename.TabIndex = 2;
			buttonRename.Text = "Rename";
			buttonRename.UseVisualStyleBackColor = true;
			buttonRename.Click += buttonRename_Click;
			// 
			// buttonDuplicate
			// 
			buttonDuplicate.Location = new System.Drawing.Point(201, 3);
			buttonDuplicate.Name = "buttonDuplicate";
			buttonDuplicate.Size = new System.Drawing.Size(75, 30);
			buttonDuplicate.TabIndex = 4;
			buttonDuplicate.Text = "Duplicate";
			buttonDuplicate.UseVisualStyleBackColor = true;
			buttonDuplicate.Click += buttonDuplicate_Click;
			// 
			// buttonReplaceSprite
			// 
			buttonReplaceSprite.Location = new System.Drawing.Point(3, 39);
			buttonReplaceSprite.Name = "buttonReplaceSprite";
			buttonReplaceSprite.Size = new System.Drawing.Size(100, 30);
			buttonReplaceSprite.TabIndex = 5;
			buttonReplaceSprite.Text = "Replace Sprite";
			buttonReplaceSprite.UseVisualStyleBackColor = true;
			buttonReplaceSprite.Click += buttonReplaceSprite_Click;
			// 
			// label1
			// 
			label1.Dock = System.Windows.Forms.DockStyle.Top;
			label1.Location = new System.Drawing.Point(0, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(334, 25);
			label1.TabIndex = 2;
			label1.Text = "File Contents:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// KanimDataTreeControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(treeView);
			Controls.Add(label1);
			Controls.Add(flowLayoutPanel1);
			Name = "KanimDataTreeControl";
			Size = new System.Drawing.Size(334, 547);
			flowLayoutPanel1.ResumeLayout(false);
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.TreeView treeView;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
		private System.Windows.Forms.Button buttonAdd;
		private System.Windows.Forms.Button buttonRemove;
		private System.Windows.Forms.Button buttonRename;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button buttonDuplicate;
		private System.Windows.Forms.Button buttonReplaceSprite;
	}
}
