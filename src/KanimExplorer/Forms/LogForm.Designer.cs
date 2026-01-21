namespace KanimExplorer.Forms
{
	partial class LogForm
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
			richTextBox = new System.Windows.Forms.RichTextBox();
			SuspendLayout();
			// 
			// richTextBox
			// 
			richTextBox.BackColor = System.Drawing.Color.Black;
			richTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
			richTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			richTextBox.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			richTextBox.ForeColor = System.Drawing.Color.White;
			richTextBox.Location = new System.Drawing.Point(0, 0);
			richTextBox.Name = "richTextBox";
			richTextBox.ReadOnly = true;
			richTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			richTextBox.Size = new System.Drawing.Size(484, 461);
			richTextBox.TabIndex = 1;
			richTextBox.Text = "";
			// 
			// LogForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(484, 461);
			Controls.Add(richTextBox);
			Name = "LogForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Event Log";
			FormClosing += LogForm_FormClosing;
			VisibleChanged += LogForm_VisibleChanged;
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox;
	}
}