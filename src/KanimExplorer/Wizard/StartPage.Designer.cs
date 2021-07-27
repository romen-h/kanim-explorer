
namespace KanimExplorer.Wizard
{
	partial class StartPage
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
			this.label1 = new System.Windows.Forms.Label();
			this.radioButtonBuildingPlaceholder = new System.Windows.Forms.RadioButton();
			this.radioButtonPackSprites = new System.Windows.Forms.RadioButton();
			this.radioButtonTileGenerator = new System.Windows.Forms.RadioButton();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(13, 12);
			this.label1.Margin = new System.Windows.Forms.Padding(13, 12, 13, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(105, 20);
			this.label1.TabIndex = 0;
			this.label1.Text = "Select a Tool:";
			// 
			// radioButtonBuildingPlaceholder
			// 
			this.radioButtonBuildingPlaceholder.AutoSize = true;
			this.radioButtonBuildingPlaceholder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioButtonBuildingPlaceholder.Location = new System.Drawing.Point(17, 48);
			this.radioButtonBuildingPlaceholder.Margin = new System.Windows.Forms.Padding(4);
			this.radioButtonBuildingPlaceholder.Name = "radioButtonBuildingPlaceholder";
			this.radioButtonBuildingPlaceholder.Size = new System.Drawing.Size(238, 20);
			this.radioButtonBuildingPlaceholder.TabIndex = 1;
			this.radioButtonBuildingPlaceholder.TabStop = true;
			this.radioButtonBuildingPlaceholder.Text = "Generate Building Placeholder";
			this.radioButtonBuildingPlaceholder.UseVisualStyleBackColor = true;
			// 
			// radioButtonPackSprites
			// 
			this.radioButtonPackSprites.AutoSize = true;
			this.radioButtonPackSprites.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioButtonPackSprites.Location = new System.Drawing.Point(17, 116);
			this.radioButtonPackSprites.Margin = new System.Windows.Forms.Padding(4);
			this.radioButtonPackSprites.Name = "radioButtonPackSprites";
			this.radioButtonPackSprites.Size = new System.Drawing.Size(114, 20);
			this.radioButtonPackSprites.TabIndex = 2;
			this.radioButtonPackSprites.TabStop = true;
			this.radioButtonPackSprites.Text = "Pack Sprites";
			this.radioButtonPackSprites.UseVisualStyleBackColor = true;
			// 
			// radioButtonTileGenerator
			// 
			this.radioButtonTileGenerator.AutoSize = true;
			this.radioButtonTileGenerator.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.radioButtonTileGenerator.Location = new System.Drawing.Point(17, 184);
			this.radioButtonTileGenerator.Margin = new System.Windows.Forms.Padding(4);
			this.radioButtonTileGenerator.Name = "radioButtonTileGenerator";
			this.radioButtonTileGenerator.Size = new System.Drawing.Size(126, 20);
			this.radioButtonTileGenerator.TabIndex = 3;
			this.radioButtonTileGenerator.TabStop = true;
			this.radioButtonTileGenerator.Text = "Tile Generator";
			this.radioButtonTileGenerator.UseVisualStyleBackColor = true;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(25, 72);
			this.label2.Margin = new System.Windows.Forms.Padding(25, 0, 10, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(374, 40);
			this.label2.TabIndex = 4;
			this.label2.Text = "Generates the kanim files for a basic building with \"on\" and \"off\" animations.";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(25, 140);
			this.label3.Margin = new System.Windows.Forms.Padding(25, 0, 10, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(374, 40);
			this.label3.TabIndex = 5;
			this.label3.Text = "Generates the kanim files from a folder of sprites.";
			// 
			// StartPage
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.radioButtonTileGenerator);
			this.Controls.Add(this.radioButtonPackSprites);
			this.Controls.Add(this.radioButtonBuildingPlaceholder);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "StartPage";
			this.Size = new System.Drawing.Size(409, 354);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.RadioButton radioButtonBuildingPlaceholder;
		private System.Windows.Forms.RadioButton radioButtonPackSprites;
		private System.Windows.Forms.RadioButton radioButtonTileGenerator;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
	}
}
