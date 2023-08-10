namespace KanimExplorer.Wizard
{
	partial class SymbolDuplicatorPage
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
			this.groupBoxSymbols = new System.Windows.Forms.GroupBox();
			this.listViewSymbols = new System.Windows.Forms.ListView();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxSuffix = new System.Windows.Forms.TextBox();
			this.textBoxPrefix = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
			this.groupBoxSymbols.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
			this.SuspendLayout();
			// 
			// groupBoxSymbols
			// 
			this.groupBoxSymbols.Controls.Add(this.listViewSymbols);
			this.groupBoxSymbols.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBoxSymbols.Location = new System.Drawing.Point(0, 0);
			this.groupBoxSymbols.Name = "groupBoxSymbols";
			this.groupBoxSymbols.Size = new System.Drawing.Size(470, 224);
			this.groupBoxSymbols.TabIndex = 0;
			this.groupBoxSymbols.TabStop = false;
			this.groupBoxSymbols.Text = "Symbols To Duplicate";
			// 
			// listViewSymbols
			// 
			this.listViewSymbols.CheckBoxes = true;
			this.listViewSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewSymbols.Location = new System.Drawing.Point(3, 19);
			this.listViewSymbols.Name = "listViewSymbols";
			this.listViewSymbols.Size = new System.Drawing.Size(464, 202);
			this.listViewSymbols.TabIndex = 0;
			this.listViewSymbols.UseCompatibleStateImageBehavior = false;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.textBoxSuffix);
			this.groupBox1.Controls.Add(this.textBoxPrefix);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.numericUpDown1);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
			this.groupBox1.Location = new System.Drawing.Point(0, 224);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(470, 116);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Options";
			// 
			// textBoxSuffix
			// 
			this.textBoxSuffix.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textBoxSuffix.Location = new System.Drawing.Point(69, 81);
			this.textBoxSuffix.Name = "textBoxSuffix";
			this.textBoxSuffix.Size = new System.Drawing.Size(120, 25);
			this.textBoxSuffix.TabIndex = 5;
			// 
			// textBoxPrefix
			// 
			this.textBoxPrefix.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textBoxPrefix.Location = new System.Drawing.Point(69, 50);
			this.textBoxPrefix.Name = "textBoxPrefix";
			this.textBoxPrefix.Size = new System.Drawing.Size(120, 25);
			this.textBoxPrefix.TabIndex = 4;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label3.Location = new System.Drawing.Point(6, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(42, 17);
			this.label3.TabIndex = 3;
			this.label3.Text = "Suffix:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label2.Location = new System.Drawing.Point(6, 53);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(43, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "Prefix:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.label1.Location = new System.Drawing.Point(6, 19);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 17);
			this.label1.TabIndex = 1;
			this.label1.Text = "Z Offset:";
			// 
			// numericUpDown1
			// 
			this.numericUpDown1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.numericUpDown1.Location = new System.Drawing.Point(69, 19);
			this.numericUpDown1.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDown1.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numericUpDown1.Name = "numericUpDown1";
			this.numericUpDown1.Size = new System.Drawing.Size(120, 25);
			this.numericUpDown1.TabIndex = 0;
			// 
			// SymbolDuplicatorPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.groupBoxSymbols);
			this.Name = "SymbolDuplicatorPage";
			this.Size = new System.Drawing.Size(470, 452);
			this.groupBoxSymbols.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBoxSymbols;
		private System.Windows.Forms.ListView listViewSymbols;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDown1;
		private System.Windows.Forms.TextBox textBoxSuffix;
		private System.Windows.Forms.TextBox textBoxPrefix;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
	}
}
