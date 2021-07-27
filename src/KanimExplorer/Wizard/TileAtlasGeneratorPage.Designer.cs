
namespace KanimExplorer.Wizard
{
	partial class TileAtlasGeneratorPage
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.buttonBrowseBorder = new System.Windows.Forms.Button();
			this.textBoxBorderPath = new System.Windows.Forms.TextBox();
			this.buttonBrowseFill = new System.Windows.Forms.Button();
			this.textBoxFillPath = new System.Windows.Forms.TextBox();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.textBoxOutput = new System.Windows.Forms.TextBox();
			this.buttonBrowseOutput = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxBorderPath);
			this.groupBox1.Controls.Add(this.buttonBrowseBorder);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(13, 76);
			this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.groupBox1.Size = new System.Drawing.Size(374, 90);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Border Texture";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textBoxFillPath);
			this.groupBox2.Controls.Add(this.buttonBrowseFill);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(13, 166);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(374, 90);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Fill Texture";
			// 
			// buttonBrowseBorder
			// 
			this.buttonBrowseBorder.Location = new System.Drawing.Point(7, 22);
			this.buttonBrowseBorder.Name = "buttonBrowseBorder";
			this.buttonBrowseBorder.Size = new System.Drawing.Size(100, 30);
			this.buttonBrowseBorder.TabIndex = 0;
			this.buttonBrowseBorder.Text = "Browse";
			this.buttonBrowseBorder.UseVisualStyleBackColor = true;
			this.buttonBrowseBorder.Click += new System.EventHandler(this.buttonBrowseBorder_Click);
			// 
			// textBoxBorderPath
			// 
			this.textBoxBorderPath.Location = new System.Drawing.Point(7, 58);
			this.textBoxBorderPath.Name = "textBoxBorderPath";
			this.textBoxBorderPath.ReadOnly = true;
			this.textBoxBorderPath.Size = new System.Drawing.Size(360, 22);
			this.textBoxBorderPath.TabIndex = 1;
			// 
			// buttonBrowseFill
			// 
			this.buttonBrowseFill.Location = new System.Drawing.Point(7, 21);
			this.buttonBrowseFill.Name = "buttonBrowseFill";
			this.buttonBrowseFill.Size = new System.Drawing.Size(100, 30);
			this.buttonBrowseFill.TabIndex = 1;
			this.buttonBrowseFill.Text = "Browse";
			this.buttonBrowseFill.UseVisualStyleBackColor = true;
			this.buttonBrowseFill.Click += new System.EventHandler(this.buttonBrowseFill_Click);
			// 
			// textBoxFillPath
			// 
			this.textBoxFillPath.Location = new System.Drawing.Point(6, 57);
			this.textBoxFillPath.Name = "textBoxFillPath";
			this.textBoxFillPath.ReadOnly = true;
			this.textBoxFillPath.Size = new System.Drawing.Size(361, 22);
			this.textBoxFillPath.TabIndex = 2;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textBoxName);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.Location = new System.Drawing.Point(13, 12);
			this.groupBox3.Margin = new System.Windows.Forms.Padding(0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(10);
			this.groupBox3.Size = new System.Drawing.Size(374, 64);
			this.groupBox3.TabIndex = 6;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Name";
			// 
			// textBoxName
			// 
			this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxName.Location = new System.Drawing.Point(10, 25);
			this.textBoxName.MaximumSize = new System.Drawing.Size(250, 30);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(250, 22);
			this.textBoxName.TabIndex = 0;
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.textBoxOutput);
			this.groupBox4.Controls.Add(this.buttonBrowseOutput);
			this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox4.Location = new System.Drawing.Point(13, 256);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Padding = new System.Windows.Forms.Padding(10);
			this.groupBox4.Size = new System.Drawing.Size(374, 100);
			this.groupBox4.TabIndex = 7;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Output Directory:";
			// 
			// textBoxOutput
			// 
			this.textBoxOutput.Location = new System.Drawing.Point(13, 64);
			this.textBoxOutput.Name = "textBoxOutput";
			this.textBoxOutput.ReadOnly = true;
			this.textBoxOutput.Size = new System.Drawing.Size(354, 22);
			this.textBoxOutput.TabIndex = 1;
			// 
			// buttonBrowseOutput
			// 
			this.buttonBrowseOutput.Location = new System.Drawing.Point(13, 28);
			this.buttonBrowseOutput.Name = "buttonBrowseOutput";
			this.buttonBrowseOutput.Size = new System.Drawing.Size(100, 30);
			this.buttonBrowseOutput.TabIndex = 0;
			this.buttonBrowseOutput.Text = "Browse";
			this.buttonBrowseOutput.UseVisualStyleBackColor = true;
			this.buttonBrowseOutput.Click += new System.EventHandler(this.buttonBrowseOutput_Click);
			// 
			// TileAtlasGeneratorPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox3);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "TileAtlasGeneratorPage";
			this.Padding = new System.Windows.Forms.Padding(13, 12, 13, 12);
			this.Size = new System.Drawing.Size(400, 400);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxBorderPath;
		private System.Windows.Forms.Button buttonBrowseBorder;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button buttonBrowseFill;
		private System.Windows.Forms.TextBox textBoxFillPath;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox textBoxName;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.TextBox textBoxOutput;
		private System.Windows.Forms.Button buttonBrowseOutput;
	}
}
