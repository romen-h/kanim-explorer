namespace KanimExplorer.Forms
{
	partial class StringPromptForm
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
			buttonOK = new System.Windows.Forms.Button();
			buttonCancel = new System.Windows.Forms.Button();
			textBox = new System.Windows.Forms.TextBox();
			labelPrompt = new System.Windows.Forms.Label();
			SuspendLayout();
			// 
			// buttonOK
			// 
			buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			buttonOK.Location = new System.Drawing.Point(126, 65);
			buttonOK.Name = "buttonOK";
			buttonOK.Size = new System.Drawing.Size(100, 30);
			buttonOK.TabIndex = 1;
			buttonOK.Text = "OK";
			buttonOK.UseVisualStyleBackColor = true;
			buttonOK.Click += buttonOK_Click;
			// 
			// buttonCancel
			// 
			buttonCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			buttonCancel.Location = new System.Drawing.Point(232, 65);
			buttonCancel.Name = "buttonCancel";
			buttonCancel.Size = new System.Drawing.Size(100, 30);
			buttonCancel.TabIndex = 2;
			buttonCancel.Text = "Cancel";
			buttonCancel.UseVisualStyleBackColor = true;
			buttonCancel.Click += buttonCancel_Click;
			// 
			// textBox
			// 
			textBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			textBox.Location = new System.Drawing.Point(12, 32);
			textBox.Name = "textBox";
			textBox.Size = new System.Drawing.Size(320, 23);
			textBox.TabIndex = 3;
			textBox.TextChanged += textBox_TextChanged;
			textBox.PreviewKeyDown += textBox_PreviewKeyDown;
			// 
			// labelPrompt
			// 
			labelPrompt.AutoSize = true;
			labelPrompt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			labelPrompt.Location = new System.Drawing.Point(12, 9);
			labelPrompt.Name = "labelPrompt";
			labelPrompt.Size = new System.Drawing.Size(18, 20);
			labelPrompt.TabIndex = 5;
			labelPrompt.Text = "...";
			// 
			// StringPromptForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(344, 107);
			ControlBox = false;
			Controls.Add(labelPrompt);
			Controls.Add(textBox);
			Controls.Add(buttonCancel);
			Controls.Add(buttonOK);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "StringPromptForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Enter String";
			Load += StringPromptForm_Load;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private System.Windows.Forms.Label promptLabel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.TextBox textBox;
		private System.Windows.Forms.ComboBox oldNamesComboBox;
		private System.Windows.Forms.Label labelPrompt;
	}
}