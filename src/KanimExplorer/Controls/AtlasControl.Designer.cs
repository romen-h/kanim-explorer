namespace KanimExplorer.Controls
{
	partial class AtlasControl
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
			imageBoxAtlas = new Cyotek.Windows.Forms.ImageBox();
			SuspendLayout();
			// 
			// imageBoxAtlas
			// 
			imageBoxAtlas.BackColor = System.Drawing.Color.Gray;
			imageBoxAtlas.BorderStyle = System.Windows.Forms.BorderStyle.None;
			imageBoxAtlas.Dock = System.Windows.Forms.DockStyle.Fill;
			imageBoxAtlas.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
			imageBoxAtlas.GridColor = System.Drawing.Color.Gray;
			imageBoxAtlas.GridColorAlternate = System.Drawing.Color.Gray;
			imageBoxAtlas.GridDisplayMode = Cyotek.Windows.Forms.ImageBoxGridDisplayMode.None;
			imageBoxAtlas.GridScale = Cyotek.Windows.Forms.ImageBoxGridScale.Large;
			imageBoxAtlas.Location = new System.Drawing.Point(0, 0);
			imageBoxAtlas.Name = "imageBoxAtlas";
			imageBoxAtlas.Size = new System.Drawing.Size(422, 425);
			imageBoxAtlas.TabIndex = 0;
			imageBoxAtlas.TextAlign = System.Drawing.ContentAlignment.TopLeft;
			imageBoxAtlas.VirtualMode = true;
			imageBoxAtlas.VirtualDraw += imageBoxAtlas_VirtualDraw;
			// 
			// AtlasControl
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			Controls.Add(imageBoxAtlas);
			Name = "AtlasControl";
			Size = new System.Drawing.Size(422, 425);
			ResumeLayout(false);
		}

		#endregion

		private Cyotek.Windows.Forms.ImageBox imageBoxAtlas;
	}
}
