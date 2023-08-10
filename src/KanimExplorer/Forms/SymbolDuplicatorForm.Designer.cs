namespace KanimExplorer.Forms
{
	partial class SymbolDuplicatorForm
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonCancel = new System.Windows.Forms.Button();
			this.buttonOK = new System.Windows.Forms.Button();
			this.panel2 = new System.Windows.Forms.Panel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.listViewAnimations = new System.Windows.Forms.ListView();
			this.panel4 = new System.Windows.Forms.Panel();
			this.buttonSelectNoneAnims = new System.Windows.Forms.Button();
			this.buttonSelectAllAnims = new System.Windows.Forms.Button();
			this.groupBoxSymbols = new System.Windows.Forms.GroupBox();
			this.listViewSymbols = new System.Windows.Forms.ListView();
			this.panel3 = new System.Windows.Forms.Panel();
			this.buttonSelectNoneSymbols = new System.Windows.Forms.Button();
			this.buttonSelectAllSymbols = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBoxSuffix = new System.Windows.Forms.TextBox();
			this.textBoxPrefix = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.numericUpDownZOffset = new System.Windows.Forms.NumericUpDown();
			this.checkBoxInvisibleCopies = new System.Windows.Forms.CheckBox();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.panel4.SuspendLayout();
			this.groupBoxSymbols.SuspendLayout();
			this.panel3.SuspendLayout();
			this.groupBox1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownZOffset)).BeginInit();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.buttonCancel);
			this.panel1.Controls.Add(this.buttonOK);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(0, 400);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(510, 50);
			this.panel1.TabIndex = 0;
			// 
			// buttonCancel
			// 
			this.buttonCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonCancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.buttonCancel.Location = new System.Drawing.Point(398, 8);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new System.Drawing.Size(100, 30);
			this.buttonCancel.TabIndex = 1;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			this.buttonCancel.Click += new System.EventHandler(this.buttonCancel_Click);
			// 
			// buttonOK
			// 
			this.buttonOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonOK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.buttonOK.Location = new System.Drawing.Point(292, 8);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Size = new System.Drawing.Size(100, 30);
			this.buttonOK.TabIndex = 0;
			this.buttonOK.Text = "Duplicate";
			this.buttonOK.UseVisualStyleBackColor = true;
			this.buttonOK.Click += new System.EventHandler(this.buttonOK_Click);
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.tableLayoutPanel1);
			this.panel2.Controls.Add(this.groupBox1);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(510, 400);
			this.panel2.TabIndex = 1;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.Controls.Add(this.groupBox2, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.groupBoxSymbols, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 1;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 200F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(510, 284);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.listViewAnimations);
			this.groupBox2.Controls.Add(this.panel4);
			this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBox2.Location = new System.Drawing.Point(258, 3);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(249, 278);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Target Animations";
			// 
			// listViewAnimations
			// 
			this.listViewAnimations.CheckBoxes = true;
			this.listViewAnimations.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewAnimations.Location = new System.Drawing.Point(3, 49);
			this.listViewAnimations.Name = "listViewAnimations";
			this.listViewAnimations.Size = new System.Drawing.Size(243, 226);
			this.listViewAnimations.TabIndex = 0;
			this.listViewAnimations.UseCompatibleStateImageBehavior = false;
			this.listViewAnimations.View = System.Windows.Forms.View.List;
			this.listViewAnimations.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewAnimations_ItemChecked);
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.buttonSelectNoneAnims);
			this.panel4.Controls.Add(this.buttonSelectAllAnims);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(3, 19);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(243, 30);
			this.panel4.TabIndex = 1;
			// 
			// buttonSelectNoneAnims
			// 
			this.buttonSelectNoneAnims.Location = new System.Drawing.Point(84, 3);
			this.buttonSelectNoneAnims.Name = "buttonSelectNoneAnims";
			this.buttonSelectNoneAnims.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectNoneAnims.TabIndex = 1;
			this.buttonSelectNoneAnims.Text = "None";
			this.buttonSelectNoneAnims.UseVisualStyleBackColor = true;
			this.buttonSelectNoneAnims.Click += new System.EventHandler(this.buttonSelectNoneAnims_Click);
			// 
			// buttonSelectAllAnims
			// 
			this.buttonSelectAllAnims.Location = new System.Drawing.Point(3, 3);
			this.buttonSelectAllAnims.Name = "buttonSelectAllAnims";
			this.buttonSelectAllAnims.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectAllAnims.TabIndex = 0;
			this.buttonSelectAllAnims.Text = "Select All";
			this.buttonSelectAllAnims.UseVisualStyleBackColor = true;
			this.buttonSelectAllAnims.Click += new System.EventHandler(this.buttonSelectAllAnims_Click);
			// 
			// groupBoxSymbols
			// 
			this.groupBoxSymbols.Controls.Add(this.listViewSymbols);
			this.groupBoxSymbols.Controls.Add(this.panel3);
			this.groupBoxSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
			this.groupBoxSymbols.Location = new System.Drawing.Point(3, 3);
			this.groupBoxSymbols.Name = "groupBoxSymbols";
			this.groupBoxSymbols.Size = new System.Drawing.Size(249, 278);
			this.groupBoxSymbols.TabIndex = 2;
			this.groupBoxSymbols.TabStop = false;
			this.groupBoxSymbols.Text = "Symbols To Duplicate";
			// 
			// listViewSymbols
			// 
			this.listViewSymbols.CheckBoxes = true;
			this.listViewSymbols.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewSymbols.Location = new System.Drawing.Point(3, 49);
			this.listViewSymbols.Name = "listViewSymbols";
			this.listViewSymbols.Size = new System.Drawing.Size(243, 226);
			this.listViewSymbols.TabIndex = 0;
			this.listViewSymbols.UseCompatibleStateImageBehavior = false;
			this.listViewSymbols.View = System.Windows.Forms.View.List;
			this.listViewSymbols.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.listViewSymbols_ItemChecked);
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.buttonSelectNoneSymbols);
			this.panel3.Controls.Add(this.buttonSelectAllSymbols);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 19);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(243, 30);
			this.panel3.TabIndex = 1;
			// 
			// buttonSelectNoneSymbols
			// 
			this.buttonSelectNoneSymbols.Location = new System.Drawing.Point(84, 3);
			this.buttonSelectNoneSymbols.Name = "buttonSelectNoneSymbols";
			this.buttonSelectNoneSymbols.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectNoneSymbols.TabIndex = 1;
			this.buttonSelectNoneSymbols.Text = "None";
			this.buttonSelectNoneSymbols.UseVisualStyleBackColor = true;
			this.buttonSelectNoneSymbols.Click += new System.EventHandler(this.buttonSelectNoneSymbols_Click);
			// 
			// buttonSelectAllSymbols
			// 
			this.buttonSelectAllSymbols.Location = new System.Drawing.Point(3, 3);
			this.buttonSelectAllSymbols.Name = "buttonSelectAllSymbols";
			this.buttonSelectAllSymbols.Size = new System.Drawing.Size(75, 23);
			this.buttonSelectAllSymbols.TabIndex = 0;
			this.buttonSelectAllSymbols.Text = "Select All";
			this.buttonSelectAllSymbols.UseVisualStyleBackColor = true;
			this.buttonSelectAllSymbols.Click += new System.EventHandler(this.buttonSelectAllSymbols_Click);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.checkBoxInvisibleCopies);
			this.groupBox1.Controls.Add(this.textBoxSuffix);
			this.groupBox1.Controls.Add(this.textBoxPrefix);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.numericUpDownZOffset);
			this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.groupBox1.Location = new System.Drawing.Point(0, 284);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(510, 116);
			this.groupBox1.TabIndex = 3;
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
			this.textBoxSuffix.TextChanged += new System.EventHandler(this.textBoxSuffix_TextChanged);
			// 
			// textBoxPrefix
			// 
			this.textBoxPrefix.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.textBoxPrefix.Location = new System.Drawing.Point(69, 50);
			this.textBoxPrefix.Name = "textBoxPrefix";
			this.textBoxPrefix.Size = new System.Drawing.Size(120, 25);
			this.textBoxPrefix.TabIndex = 4;
			this.textBoxPrefix.TextChanged += new System.EventHandler(this.textBoxPrefix_TextChanged);
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
			// numericUpDownZOffset
			// 
			this.numericUpDownZOffset.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
			this.numericUpDownZOffset.Location = new System.Drawing.Point(69, 19);
			this.numericUpDownZOffset.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
			this.numericUpDownZOffset.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
			this.numericUpDownZOffset.Name = "numericUpDownZOffset";
			this.numericUpDownZOffset.Size = new System.Drawing.Size(120, 25);
			this.numericUpDownZOffset.TabIndex = 0;
			this.numericUpDownZOffset.ValueChanged += new System.EventHandler(this.numericUpDownZOffset_ValueChanged);
			// 
			// checkBoxInvisibleCopies
			// 
			this.checkBoxInvisibleCopies.AutoSize = true;
			this.checkBoxInvisibleCopies.Location = new System.Drawing.Point(204, 22);
			this.checkBoxInvisibleCopies.Name = "checkBoxInvisibleCopies";
			this.checkBoxInvisibleCopies.Size = new System.Drawing.Size(108, 19);
			this.checkBoxInvisibleCopies.TabIndex = 6;
			this.checkBoxInvisibleCopies.Text = "Invisible Copies";
			this.checkBoxInvisibleCopies.UseVisualStyleBackColor = true;
			this.checkBoxInvisibleCopies.CheckedChanged += new System.EventHandler(this.checkBoxInvisibleCopies_CheckedChanged);
			// 
			// SymbolDuplicatorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(510, 450);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.panel1);
			this.Name = "SymbolDuplicatorForm";
			this.Text = "SymbolDuplicatorForm";
			this.panel1.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.panel4.ResumeLayout(false);
			this.groupBoxSymbols.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.numericUpDownZOffset)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonCancel;
		private System.Windows.Forms.Button buttonOK;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBoxSuffix;
		private System.Windows.Forms.TextBox textBoxPrefix;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown numericUpDownZOffset;
		private System.Windows.Forms.GroupBox groupBoxSymbols;
		private System.Windows.Forms.ListView listViewSymbols;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Button buttonSelectNoneSymbols;
		private System.Windows.Forms.Button buttonSelectAllSymbols;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.ListView listViewAnimations;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Button buttonSelectNoneAnims;
		private System.Windows.Forms.Button buttonSelectAllAnims;
		private System.Windows.Forms.CheckBox checkBoxInvisibleCopies;
	}
}