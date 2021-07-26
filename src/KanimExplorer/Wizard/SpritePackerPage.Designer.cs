
namespace KanimExplorer.Wizard
{
	partial class SpritePackerPage
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
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textBoxInput = new System.Windows.Forms.TextBox();
			this.buttonBrowseInput = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxOutput = new System.Windows.Forms.TextBox();
			this.buttonBrowseOutput = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBoxName = new System.Windows.Forms.TextBox();
			this.groupBox3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textBoxInput);
			this.groupBox3.Controls.Add(this.buttonBrowseInput);
			this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox3.Location = new System.Drawing.Point(10, 74);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Padding = new System.Windows.Forms.Padding(10);
			this.groupBox3.Size = new System.Drawing.Size(380, 100);
			this.groupBox3.TabIndex = 3;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Input Directory:";
			// 
			// textBoxInput
			// 
			this.textBoxInput.Location = new System.Drawing.Point(13, 64);
			this.textBoxInput.Name = "textBoxInput";
			this.textBoxInput.ReadOnly = true;
			this.textBoxInput.Size = new System.Drawing.Size(354, 20);
			this.textBoxInput.TabIndex = 1;
			// 
			// buttonBrowseInput
			// 
			this.buttonBrowseInput.Location = new System.Drawing.Point(13, 28);
			this.buttonBrowseInput.Name = "buttonBrowseInput";
			this.buttonBrowseInput.Size = new System.Drawing.Size(100, 30);
			this.buttonBrowseInput.TabIndex = 0;
			this.buttonBrowseInput.Text = "Browse";
			this.buttonBrowseInput.UseVisualStyleBackColor = true;
			this.buttonBrowseInput.Click += new System.EventHandler(this.buttonBrowseInput_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxOutput);
			this.groupBox1.Controls.Add(this.buttonBrowseOutput);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(10, 174);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Padding = new System.Windows.Forms.Padding(10);
			this.groupBox1.Size = new System.Drawing.Size(380, 100);
			this.groupBox1.TabIndex = 4;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Output Directory:";
			// 
			// textBoxOutput
			// 
			this.textBoxOutput.Location = new System.Drawing.Point(13, 64);
			this.textBoxOutput.Name = "textBoxOutput";
			this.textBoxOutput.ReadOnly = true;
			this.textBoxOutput.Size = new System.Drawing.Size(354, 20);
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
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textBoxName);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox2.Location = new System.Drawing.Point(10, 10);
			this.groupBox2.Margin = new System.Windows.Forms.Padding(0);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Padding = new System.Windows.Forms.Padding(10);
			this.groupBox2.Size = new System.Drawing.Size(380, 64);
			this.groupBox2.TabIndex = 5;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Name";
			// 
			// textBoxName
			// 
			this.textBoxName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxName.Location = new System.Drawing.Point(10, 23);
			this.textBoxName.MaximumSize = new System.Drawing.Size(250, 30);
			this.textBoxName.Name = "textBoxName";
			this.textBoxName.Size = new System.Drawing.Size(250, 20);
			this.textBoxName.TabIndex = 0;
			// 
			// SpritePackerPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Name = "SpritePackerPage";
			this.Padding = new System.Windows.Forms.Padding(10);
			this.Size = new System.Drawing.Size(400, 400);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox textBoxInput;
		private System.Windows.Forms.Button buttonBrowseInput;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxOutput;
		private System.Windows.Forms.Button buttonBrowseOutput;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBoxName;
	}
}
