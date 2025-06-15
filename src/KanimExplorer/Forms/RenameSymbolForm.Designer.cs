namespace KanimExplorer.Forms
{
	partial class RenameSymbolForm
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
			promptLabel = new System.Windows.Forms.Label();
			buttonOK = new System.Windows.Forms.Button();
			buttonCancel = new System.Windows.Forms.Button();
			textBox = new System.Windows.Forms.TextBox();
			oldNamesComboBox = new System.Windows.Forms.ComboBox();
			label1 = new System.Windows.Forms.Label();
			SuspendLayout();
			// 
			// promptLabel
			// 
			promptLabel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			promptLabel.AutoSize = true;
			promptLabel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			promptLabel.Location = new System.Drawing.Point(12, 9);
			promptLabel.Name = "promptLabel";
			promptLabel.Size = new System.Drawing.Size(174, 20);
			promptLabel.TabIndex = 0;
			promptLabel.Text = "Pick a symbol to rename:";
			// 
			// buttonOK
			// 
			buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
			buttonOK.Location = new System.Drawing.Point(126, 119);
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
			buttonCancel.Location = new System.Drawing.Point(232, 119);
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
			textBox.Location = new System.Drawing.Point(12, 81);
			textBox.Name = "textBox";
			textBox.Size = new System.Drawing.Size(320, 23);
			textBox.TabIndex = 3;
			textBox.TextChanged += textBox_TextChanged;
			// 
			// oldNamesComboBox
			// 
			oldNamesComboBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			oldNamesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			oldNamesComboBox.FormattingEnabled = true;
			oldNamesComboBox.Location = new System.Drawing.Point(12, 32);
			oldNamesComboBox.Name = "oldNamesComboBox";
			oldNamesComboBox.Size = new System.Drawing.Size(320, 23);
			oldNamesComboBox.TabIndex = 4;
			oldNamesComboBox.SelectedValueChanged += oldNamesComboBox_SelectedValueChanged;
			// 
			// label1
			// 
			label1.AutoSize = true;
			label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
			label1.Location = new System.Drawing.Point(12, 58);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(130, 20);
			label1.TabIndex = 5;
			label1.Text = "Enter a new name:";
			// 
			// RenameSymbolForm
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(344, 161);
			ControlBox = false;
			Controls.Add(label1);
			Controls.Add(oldNamesComboBox);
			Controls.Add(textBox);
			Controls.Add(buttonCancel);
			Controls.Add(buttonOK);
			Controls.Add(promptLabel);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			MaximizeBox = false;
			MinimizeBox = false;
			Name = "RenameSymbolForm";
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Text = "Rename Symbol";
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
		private System.Windows.Forms.Label label1;
	}
}