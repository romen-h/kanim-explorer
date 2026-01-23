using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using KanimExplorer.Forms;
using KanimExplorer.Logging;

using KanimLib;
using KanimLib.KanimModel;

using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.Logging;

namespace KanimExplorer
{
	/// <summary>
	/// Provides methods that require modal interaction before running an operation on kanim data.
	/// </summary>
	internal static class ModalTasks
	{
		/// <summary>
		/// Shows a dialog prompting for a string to rename the given symbol.
		/// </summary>
		internal static bool RenameSymbol(KanimPackage kanim, string symbolName, ILogger log = null)
		{
			ArgumentNullException.ThrowIfNull(kanim);
			ArgumentNullException.ThrowIfNull(symbolName);

			using var function = log?.BeginFunction();

			StringPromptForm dlg = new StringPromptForm("Enter a new name for the symbol:", symbolName);
			if (dlg.ShowDialog() != DialogResult.OK) return false;
			
			KAnimUtils.RenameSymbol(kanim, symbolName, dlg.Value);
			return true;
		}

		/// <summary>
		/// Shows a dialog (todo) prompting for Z offset before duplicating the given symbol.
		/// </summary>
		internal static bool DuplicateSymbol(KanimPackage kanim, string symbolName, ILogger log = null)
		{
			ArgumentNullException.ThrowIfNull(kanim);
			ArgumentNullException.ThrowIfNull(symbolName);

			using var function = log?.BeginFunction();
			
			KAnimUtils.DuplicateSymbols(kanim, [symbolName], kanim.Anim.BankNames.ToArray(), null, "_copy", -1, false);
			return true;
		}
		
		internal static bool RemoveSymbol(KanimPackage kanim, string symbolName, ILogger log = null)
		{
			ArgumentNullException.ThrowIfNull(kanim);
			ArgumentNullException.ThrowIfNull(symbolName);
			
			using var function = log?.BeginFunction();
			
			KAnimUtils.DeleteSymbol(kanim, symbolName, true, true);
			return true;
		}
		
		internal static bool ExportSprite(KanimPackage kanim, KFrame frame, ILogger log = null)
		{
			ArgumentNullException.ThrowIfNull(kanim);
			ArgumentNullException.ThrowIfNull(frame);
			if (kanim.Texture == null) throw new InvalidOperationException("There is no texture atlas loaded.");
			if (frame.Sprite == null) throw new InvalidOperationException("Sprites have not been built from the atlas yet.");

			using var function = log?.BeginFunction();

			SaveFileDialog dlg = new SaveFileDialog()
			{
				Filter = "PNG Files (*.png)|*.png",
				AddExtension = true,
				DefaultExt = "png"
			};
			
			if (dlg.ShowDialog() != DialogResult.OK) return false;
			
			frame.Sprite.Image.Save(dlg.FileName, ImageFormat.Png);
			return true;
		}

		/// <summary>
		/// Shows a dialog for editing the pivot point of the given frame.
		/// </summary>
		internal static bool EditPivot(KanimPackage kanim, KFrame frame, ILogger log = null)
		{
			ArgumentNullException.ThrowIfNull(kanim);
			ArgumentNullException.ThrowIfNull(frame);
			if (kanim.Texture == null) throw new InvalidOperationException("There is no texture atlas loaded.");

			using var function = log?.BeginFunction();

			PivotEditorForm dlg = new PivotEditorForm(frame, kanim.Texture);
			if (dlg.ShowDialog() != DialogResult.OK) return false;

			frame.SpriterPivotX = dlg.PivotX;
			frame.SpriterPivotY = dlg.PivotY;
			return true;
		}
		
		internal static bool ReplaceSprite(KanimPackage kanim, KFrame frame, ILogger log = null)
		{
			ArgumentNullException.ThrowIfNull(kanim);
			ArgumentNullException.ThrowIfNull(frame);

			using var function = log?.BeginFunction();
			
			OpenFileDialog dlg = new OpenFileDialog()
			{
				Filter = "PNG Files (*.png)|*.png",
				CheckFileExists = true
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;

			Bitmap newSprite = new Bitmap(dlg.FileName);

			bool adjustForSymmetricPadding = false;
				
			if (frame.Sprite.Width != newSprite.Width || frame.Sprite.Height != newSprite.Height)
			{
				var rc = MessageBox.Show("The new sprite does not match the original size.\n\nDo you want to adjust the pivot assuming the difference is padding or cropping? (Yes)\n\nOr adjust the pivot proportionally? (No)", "Adjust Pivot?", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (rc == DialogResult.Cancel) return false;
					
				adjustForSymmetricPadding = rc == DialogResult.Yes;
			}

			KAnimUtils.ReplaceSprite(kanim, frame, newSprite, adjustForSymmetricPadding);
			return true;
		}
	}
}
