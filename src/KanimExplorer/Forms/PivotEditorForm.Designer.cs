namespace KanimExplorer.Forms
{
	partial class PivotEditorForm
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
			label1 = new System.Windows.Forms.Label();
			label2 = new System.Windows.Forms.Label();
			textBox1 = new System.Windows.Forms.TextBox();
			textBox2 = new System.Windows.Forms.TextBox();
			okButton = new System.Windows.Forms.Button();
			cancelButton = new System.Windows.Forms.Button();
			tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			pixelPictureBox = new PixelPictureBox();
			tableLayoutPanel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)pixelPictureBox).BeginInit();
			SuspendLayout();
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Dock = System.Windows.Forms.DockStyle.Fill;
			label1.Location = new System.Drawing.Point(3, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(94, 30);
			label1.TabIndex = 0;
			label1.Text = "Pivot X:";
			label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label2
			// 
			label2.AutoSize = true;
			label2.Dock = System.Windows.Forms.DockStyle.Fill;
			label2.Location = new System.Drawing.Point(3, 60);
			label2.Name = "label2";
			label2.Size = new System.Drawing.Size(94, 30);
			label2.TabIndex = 1;
			label2.Text = "Pivot Y:";
			label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBox1
			// 
			textBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			textBox1.Location = new System.Drawing.Point(3, 33);
			textBox1.Name = "textBox1";
			textBox1.ReadOnly = true;
			textBox1.Size = new System.Drawing.Size(94, 23);
			textBox1.TabIndex = 2;
			// 
			// textBox2
			// 
			textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			textBox2.Location = new System.Drawing.Point(3, 93);
			textBox2.Name = "textBox2";
			textBox2.ReadOnly = true;
			textBox2.Size = new System.Drawing.Size(94, 23);
			textBox2.TabIndex = 3;
			// 
			// okButton
			// 
			okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			okButton.Location = new System.Drawing.Point(429, 417);
			okButton.Name = "okButton";
			okButton.Size = new System.Drawing.Size(100, 30);
			okButton.TabIndex = 5;
			okButton.Text = "OK";
			okButton.UseVisualStyleBackColor = true;
			okButton.Click += okButton_Click;
			// 
			// cancelButton
			// 
			cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			cancelButton.Location = new System.Drawing.Point(535, 417);
			cancelButton.Name = "cancelButton";
			cancelButton.Size = new System.Drawing.Size(100, 30);
			cancelButton.TabIndex = 6;
			cancelButton.Text = "Cancel";
			cancelButton.UseVisualStyleBackColor = true;
			cancelButton.Click += cancelButton_Click;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(label1, 0, 0);
			tableLayoutPanel1.Controls.Add(textBox1, 0, 1);
			tableLayoutPanel1.Controls.Add(label2, 0, 2);
			tableLayoutPanel1.Controls.Add(textBox2, 0, 3);
			tableLayoutPanel1.Controls.Add(pixelPictureBox, 1, 0);
			tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 5;
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			tableLayoutPanel1.Size = new System.Drawing.Size(647, 411);
			tableLayoutPanel1.TabIndex = 7;
			// 
			// pixelPictureBox
			// 
			pixelPictureBox.BackColor = System.Drawing.Color.Gray;
			pixelPictureBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			pixelPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
			pixelPictureBox.Location = new System.Drawing.Point(103, 3);
			pixelPictureBox.Name = "pixelPictureBox";
			tableLayoutPanel1.SetRowSpan(pixelPictureBox, 5);
			pixelPictureBox.Size = new System.Drawing.Size(541, 405);
			pixelPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			pixelPictureBox.TabIndex = 4;
			pixelPictureBox.TabStop = false;
			pixelPictureBox.MouseDown += pictureBox_MouseDown;
			pixelPictureBox.MouseLeave += pictureBox_MouseLeave;
			pixelPictureBox.MouseMove += pictureBox_MouseMove;
			pixelPictureBox.MouseUp += pictureBox_MouseUp;
			// 
			// PivotEditorForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(647, 459);
			Controls.Add(tableLayoutPanel1);
			Controls.Add(cancelButton);
			Controls.Add(okButton);
			Name = "PivotEditorForm";
			Text = "PivotEditorForm";
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)pixelPictureBox).EndInit();
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private PixelPictureBox pixelPictureBox;
	}
}