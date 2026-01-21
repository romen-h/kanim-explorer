namespace KanimExplorer.Controls
{
	partial class SpriteControl
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
			panel1 = new System.Windows.Forms.Panel();
			buttonResetPivot = new System.Windows.Forms.Button();
			buttonApplyPivot = new System.Windows.Forms.Button();
			checkBoxEditPivot = new System.Windows.Forms.CheckBox();
			buttonExportSprite = new System.Windows.Forms.Button();
			buttonZoomInSprite = new System.Windows.Forms.Button();
			buttonResetZoomSprite = new System.Windows.Forms.Button();
			buttonZoomOutSprite = new System.Windows.Forms.Button();
			imageBoxSprite = new Cyotek.Windows.Forms.ImageBox();
			buttonChangePivotColor = new System.Windows.Forms.Button();
			panel1.SuspendLayout();
			SuspendLayout();
			// 
			// panel1
			// 
			panel1.Controls.Add(buttonChangePivotColor);
			panel1.Controls.Add(buttonResetPivot);
			panel1.Controls.Add(buttonApplyPivot);
			panel1.Controls.Add(checkBoxEditPivot);
			panel1.Controls.Add(buttonExportSprite);
			panel1.Controls.Add(buttonZoomInSprite);
			panel1.Controls.Add(buttonResetZoomSprite);
			panel1.Controls.Add(buttonZoomOutSprite);
			panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			panel1.Location = new System.Drawing.Point(0, 643);
			panel1.Name = "panel1";
			panel1.Size = new System.Drawing.Size(912, 42);
			panel1.TabIndex = 2;
			// 
			// buttonResetPivot
			// 
			buttonResetPivot.Location = new System.Drawing.Point(316, 6);
			buttonResetPivot.Name = "buttonResetPivot";
			buttonResetPivot.Size = new System.Drawing.Size(75, 30);
			buttonResetPivot.TabIndex = 7;
			buttonResetPivot.Text = "Reset";
			buttonResetPivot.UseVisualStyleBackColor = true;
			buttonResetPivot.Click += buttonResetPivot_Click;
			// 
			// buttonApplyPivot
			// 
			buttonApplyPivot.Location = new System.Drawing.Point(235, 6);
			buttonApplyPivot.Name = "buttonApplyPivot";
			buttonApplyPivot.Size = new System.Drawing.Size(75, 30);
			buttonApplyPivot.TabIndex = 6;
			buttonApplyPivot.Text = "Apply";
			buttonApplyPivot.UseVisualStyleBackColor = true;
			buttonApplyPivot.Click += buttonApplyPivot_Click;
			// 
			// checkBoxEditPivot
			// 
			checkBoxEditPivot.Appearance = System.Windows.Forms.Appearance.Button;
			checkBoxEditPivot.Location = new System.Drawing.Point(156, 6);
			checkBoxEditPivot.Name = "checkBoxEditPivot";
			checkBoxEditPivot.Size = new System.Drawing.Size(73, 30);
			checkBoxEditPivot.TabIndex = 5;
			checkBoxEditPivot.Text = "Edit Pivot";
			checkBoxEditPivot.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			checkBoxEditPivot.UseVisualStyleBackColor = true;
			checkBoxEditPivot.CheckedChanged += checkBoxEditPivot_CheckedChanged;
			// 
			// buttonExportSprite
			// 
			buttonExportSprite.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
			buttonExportSprite.Location = new System.Drawing.Point(834, 6);
			buttonExportSprite.Name = "buttonExportSprite";
			buttonExportSprite.Size = new System.Drawing.Size(75, 30);
			buttonExportSprite.TabIndex = 3;
			buttonExportSprite.Text = "Export";
			buttonExportSprite.UseVisualStyleBackColor = true;
			buttonExportSprite.Click += buttonExportSprite_Click;
			// 
			// buttonZoomInSprite
			// 
			buttonZoomInSprite.Location = new System.Drawing.Point(120, 6);
			buttonZoomInSprite.Name = "buttonZoomInSprite";
			buttonZoomInSprite.Size = new System.Drawing.Size(30, 30);
			buttonZoomInSprite.TabIndex = 2;
			buttonZoomInSprite.Text = "+";
			buttonZoomInSprite.UseVisualStyleBackColor = true;
			buttonZoomInSprite.Click += buttonZoomInSprite_Click;
			// 
			// buttonResetZoomSprite
			// 
			buttonResetZoomSprite.Location = new System.Drawing.Point(39, 6);
			buttonResetZoomSprite.Name = "buttonResetZoomSprite";
			buttonResetZoomSprite.Size = new System.Drawing.Size(75, 30);
			buttonResetZoomSprite.TabIndex = 1;
			buttonResetZoomSprite.Text = "100%";
			buttonResetZoomSprite.UseVisualStyleBackColor = true;
			buttonResetZoomSprite.Click += buttonResetZoomSprite_Click;
			// 
			// buttonZoomOutSprite
			// 
			buttonZoomOutSprite.Location = new System.Drawing.Point(3, 6);
			buttonZoomOutSprite.Name = "buttonZoomOutSprite";
			buttonZoomOutSprite.Size = new System.Drawing.Size(30, 30);
			buttonZoomOutSprite.TabIndex = 0;
			buttonZoomOutSprite.Text = "-";
			buttonZoomOutSprite.UseVisualStyleBackColor = true;
			buttonZoomOutSprite.Click += buttonZoomOutSprite_Click;
			// 
			// imageBoxSprite
			// 
			imageBoxSprite.Dock = System.Windows.Forms.DockStyle.Fill;
			imageBoxSprite.Location = new System.Drawing.Point(0, 0);
			imageBoxSprite.Name = "imageBoxSprite";
			imageBoxSprite.ShowPixelGrid = true;
			imageBoxSprite.Size = new System.Drawing.Size(912, 643);
			imageBoxSprite.TabIndex = 3;
			imageBoxSprite.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			imageBoxSprite.MouseDown += imageBoxSprite_MouseDown;
			// 
			// buttonChangePivotColor
			// 
			buttonChangePivotColor.Location = new System.Drawing.Point(397, 6);
			buttonChangePivotColor.Name = "buttonChangePivotColor";
			buttonChangePivotColor.Size = new System.Drawing.Size(75, 30);
			buttonChangePivotColor.TabIndex = 8;
			buttonChangePivotColor.Text = "Pivot Color";
			buttonChangePivotColor.UseVisualStyleBackColor = true;
			buttonChangePivotColor.Click += buttonChangePivotColor_Click;
			// 
			// SpriteControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(imageBoxSprite);
			Controls.Add(panel1);
			Name = "SpriteControl";
			Size = new System.Drawing.Size(912, 685);
			panel1.ResumeLayout(false);
			ResumeLayout(false);
		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Button buttonExportSprite;
		private System.Windows.Forms.Button buttonZoomInSprite;
		private System.Windows.Forms.Button buttonResetZoomSprite;
		private System.Windows.Forms.Button buttonZoomOutSprite;
		private Cyotek.Windows.Forms.ImageBox imageBoxSprite;
		private System.Windows.Forms.CheckBox checkBoxEditPivot;
		private System.Windows.Forms.Button buttonApplyPivot;
		private System.Windows.Forms.Button buttonResetPivot;
		private System.Windows.Forms.Button buttonChangePivotColor;
	}
}
