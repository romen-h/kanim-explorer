using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using kanimal.KBuild;
using KanimExplorer.Forms;
using KanimExplorer.Logging;
using KanimExplorer.OpenGL.Objects;

using KanimLib;
using KanimLib.Converters;
using KanimLib.KanimModel;
using KanimLib.Serialization;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.Logging;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KanimExplorer
{
	/// <summary>
	/// Provides methods that require modal interaction before running an operation on kanim data.
	/// </summary>
	internal static class ModalTasks
	{
		/// <summary>
		/// Prompts the user to select a texture file and opens it in the DocumentManager.
		/// </summary>
		internal static bool OpenTexture(ILogger? log = null)
		{
			using var function = log?.BeginFunction();

			OpenFileDialog dlg = new OpenFileDialog()
			{
				Title = "Opening Texture...",
				Filter = "Texture files|*.png"
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;
			
			return OpenFilesImpl(dlg.FileNames, "Opening Texture", log);
		}
		
		/// <summary>
		/// Prompts the user to select a build.bytes file and opens it in the DocumentManager.
		/// </summary>
		internal static bool OpenBuild(ILogger? log = null)
		{
			using var function = log?.BeginFunction();

			OpenFileDialog dlg = new OpenFileDialog()
			{
				Title = "Opening Build...",
				Filter = "Build files|*.bytes;*.prefab;*.txt"
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;

			return OpenFilesImpl(dlg.FileNames, "Opening Build", log);
		}

		/// <summary>
		/// Prompts the user to select an anim.bytes file and opens it in the DocumentManager.
		/// </summary>
		internal static bool OpenAnim(ILogger? log = null)
		{
			using var function = log?.BeginFunction();

			OpenFileDialog dlg = new OpenFileDialog()
			{
				Title = "Opening Anim...",
				Filter = "Anim files|*.bytes;*.prefab;*.txt"
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;

			return OpenFilesImpl(dlg.FileNames, "Opening Anim", log);
		}

		/// <summary>
		/// Prompts the user to select multiple files and opens them all in the DocumentManager.
		/// </summary>
		internal static bool OpenMultiple(ILogger? log = null)
		{
			using var function = log?.BeginFunction();
			
			OpenFileDialog dlg = new OpenFileDialog()
			{
				Title = "Opening Kanim Files...",
				Multiselect = true,
				Filter = "Kanim Files|*.png;*.bytes;*.prefab;*.txt|All Files|*.*"
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;

			if (dlg.FileNames.Length > 3)
			{
				MessageBox.Show("A maximum of 3 files can be selected.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			return OpenFilesImpl(dlg.FileNames, "Opening Files", log);
		}

		/// <summary>
		/// Opens a list of files in the DocumentManager with prompts when specific cases are encountered.
		/// </summary>
		public static bool OpenFilesImpl(IEnumerable<string> files, string messageBoxContext, ILogger? log = null)
		{
			ArgumentNullException.ThrowIfNull(files);
			ArgumentNullException.ThrowIfNull(messageBoxContext);
			
			DocumentManager.GetSupportedFiles(files, out var textureFiles, out var buildFiles, out var animFiles, out var invalidFiles);

			var textureFile = textureFiles.FirstOrDefault();
			var buildFile = buildFiles.FirstOrDefault();
			var animFile = animFiles.FirstOrDefault();
			
			bool anythingSupported = textureFile != null || buildFile != null || animFile != null;
			if (!anythingSupported)
			{
				MessageBox.Show("No supported files were selected.", messageBoxContext, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			if (invalidFiles.Count > 0)
			{
				StringBuilder sb = new StringBuilder();
				sb.AppendLine("Invalid files were selected.");
				sb.AppendLine("These files will not be loaded:");
				foreach (var file in invalidFiles)
				{
					string fileName = Path.GetFileName(file);
					sb.AppendLine(fileName);
				}
				MessageBox.Show(sb.ToString(), messageBoxContext, MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}

			if (DocumentManager.Instance.FilesAreOpen)
			{
				bool overloading = false;
				overloading |= textureFile != null && DocumentManager.Instance.Data.HasTexture;
				overloading |= buildFile != null && DocumentManager.Instance.Data.HasBuild;
				overloading |= animFile != null && DocumentManager.Instance.Data.HasAnim;

				if (overloading)
				{
					var rc = MessageBox.Show("Files are already open.\nDo you want to close everything before opening the new ones?", "Opening Selected Files", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
					if (rc == DialogResult.No)
					{
						log.LogTrace("User selected yes.");
						if (textureFile != null && DocumentManager.Instance.Data.HasTexture)
						{
							DocumentManager.Instance.CloseTexture();
						}
						if (buildFile != null && DocumentManager.Instance.Data.HasBuild)
						{
							DocumentManager.Instance.CloseBuild();
						}
						if (animFile != null && DocumentManager.Instance.Data.HasAnim)
						{
							DocumentManager.Instance.CloseAnim();
						}
					}
					else if (rc == DialogResult.Yes)
					{
						log.LogTrace("User selected No.");
						DocumentManager.Instance.CloseEverything();
					}
					else if (rc == DialogResult.Cancel)
					{
						log.LogTrace("User selected Cancel.");
						return false;
					}
				}
			}
			
			bool anythingOpened = false;

			if (textureFile != null)
			{
				if (DocumentManager.Instance.OpenTexture(textureFile))
				{
					anythingOpened = true;
				}
				else
				{
					MessageBox.Show("Failed to load texture.\nPlease check the log for more details.", messageBoxContext, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			if (buildFile != null)
			{
				if (DocumentManager.Instance.OpenBuild(buildFile))
				{
					anythingOpened = true;
				}
				else
				{
					MessageBox.Show("Failed to load build.\nPlease check the log for more details.", messageBoxContext, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}

			if (animFile != null)
			{
				if (DocumentManager.Instance.OpenAnim(animFile))
				{
					anythingOpened = true;
				}
				else
				{
					MessageBox.Show("Failed to load anim.\nPlease check the log for more details.", messageBoxContext, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
			
			return anythingOpened;
		}
		
		internal static bool SaveTexture(ILogger log = null)
		{
			using var function = log?.BeginFunction();
			
			var texture = DocumentManager.Instance.GetSelectedTexture();
			if (texture == null) throw new InvalidOperationException("No texture is selected.");

			string filePath = DocumentManager.Instance.TextureFilePath;
			return SaveTextureImpl(texture, filePath, log);
		}

		internal static bool SaveTextureAs(ILogger log = null)
		{
			using var function = log?.BeginFunction();
			
			var texture = DocumentManager.Instance.GetSelectedTexture();
			if (texture == null) throw new InvalidOperationException("No texture is selected.");
			
			SaveFileDialog dlg = new SaveFileDialog()
			{
				Title = "Saving Texture...",
				AddExtension = true,
				DefaultExt = "png"
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;

			return SaveTextureImpl(texture, dlg.FileName, log);
		}
		
		private static bool SaveTextureImpl(Bitmap texture, string filePath, ILogger log = null)
		{
			using var path = log?.BeginScope(filePath);
			texture.Save(filePath, ImageFormat.Png);
			return true;
		}
		
		internal static bool SaveBuild(ILogger log = null)
		{
			using var function = log?.BeginFunction();

			var build = DocumentManager.Instance.GetSelectedBuild();
			if (build == null) throw new InvalidOperationException("No build is selected.");

			string filePath = DocumentManager.Instance.BuildFilePath;
			return SaveBuildImpl(build, filePath, log);
		}
		
		internal static bool SaveBuildAs(ILogger log = null)
		{
			using var function = log?.BeginFunction();
			
			var build = DocumentManager.Instance.GetSelectedBuild();
			if (build == null) throw new InvalidOperationException("No build is selected.");

			SaveFileDialog dlg = new SaveFileDialog()
			{
				Title = "Saving build.bytes...",
				AddExtension = true,
				DefaultExt = "bytes"
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;

			string fileName = Path.GetFileName(dlg.FileName).ToLowerInvariant();
			if (!fileName.EndsWith("build.bytes")) throw new Exception("Build data must be saved with a file name that ends in \"build.bytes\".");

			return SaveBuildImpl(build, dlg.FileName, log);
		}
		
		private static bool SaveBuildImpl(KBuild build, string filePath, ILogger log = null)
		{
			using var path = log?.BeginScope(filePath);
			KanimWriter.WriteBuild(filePath, build);
			return true;
		}

		internal static bool SaveAnim(ILogger log = null)
		{
			using var function = log?.BeginFunction();

			var anim = DocumentManager.Instance.GetSelectedAnim();
			if (anim == null) throw new InvalidOperationException("No anim selected.");

			string filePath = DocumentManager.Instance.AnimFilePath;
			return SaveAnimImpl(anim, filePath, log);
		}

		internal static bool SaveAnimAs(ILogger log = null)
		{
			using var function = log?.BeginFunction();
			
			var anim = DocumentManager.Instance.GetSelectedAnim();
			if (anim == null) throw new InvalidOperationException("No anim selected.");

			SaveFileDialog dlg = new SaveFileDialog()
			{
				Title = "Saving anim.bytes...",
				AddExtension = true,
				DefaultExt = "bytes"
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;
			
			string fileName = Path.GetFileName(dlg.FileName).ToLowerInvariant();
			if (!fileName.EndsWith("anim.bytes")) throw new Exception("Anim data must be saved with a file name that ends in \"anim.bytes\".");
			
			return SaveAnimImpl(anim, dlg.FileName, log);
		}
		
		private static bool SaveAnimImpl(KAnim anim, string filePath, ILogger log = null)
		{
			if (!anim.ValidateUILastOrAbsent())
			{
				log?.LogTrace("UI animation is not last. Prompting for user decision...");
				var rc = MessageBox.Show(
					"The ui animation should be moved to the bottom of the animations list to avoid a bug in ONI.\nDo you want Kanim Explorer to fix this now?",
					"Saving anim.bytes", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
				if (rc == DialogResult.Cancel)
				{
					log?.LogTrace("User selected cancel.");
					return false;
				}
				else if (rc == DialogResult.Yes)
				{
					log?.LogTrace("User selected yes.");
					anim.EnsureUILast();
				}
				else
				{
					log?.LogTrace("User selected no.");
				}
			}

			using var path = log?.BeginScope(filePath);
			KanimWriter.WriteAnim(filePath, anim);
			return true;
		}
		
		internal static bool SaveEmptyAnim(ILogger log = null)
		{
			using var function = log?.BeginFunction();

			KAnim anim = KAnimUtils.CreateEmptyAnim();

			SaveFileDialog dlg = new SaveFileDialog()
			{
				Title = "Saving anim.bytes...",
				AddExtension = true,
				DefaultExt = "bytes"
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;

			return SaveAnimImpl(anim, dlg.FileName, log);
		}
		
		internal static bool SaveAll(KanimPackage kanim, ILogger log)
		{
			ArgumentNullException.ThrowIfNull(kanim);
			
			using var function = log?.BeginFunction();
			
			bool savedAnything = false;
			if (kanim.Texture != null)
			{
				savedAnything |= SaveTextureImpl(kanim.Texture, DocumentManager.Instance.TextureFilePath, log);
			}
			if (kanim.Build != null)
			{
				savedAnything |= SaveBuildImpl(kanim.Build, DocumentManager.Instance.BuildFilePath, log);
			}
			if (kanim.Anim != null)
			{
				savedAnything |= SaveAnimImpl(kanim.Anim, DocumentManager.Instance.AnimFilePath, log);
			}
			return savedAnything;
		}
		
		internal static bool SaveAllAs(KanimPackage kanim, ILogger log)
		{
			ArgumentNullException.ThrowIfNull(kanim);
			
			using var function = log?.BeginFunction();

			FolderBrowserDialog dlg = new FolderBrowserDialog()
			{
				Description = "Saving All Files...",
				UseDescriptionForTitle = true
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;
			
			string exportName = null;
			if (kanim.HasBuild)
			{
				exportName = kanim.Build.Name;
			}
			
			if (exportName == null)
			{
				StringPromptForm stringPrompt = new StringPromptForm(
					"Enter a name for the kanim files:",
					"kanim_name");
				if (stringPrompt.ShowDialog() != DialogResult.OK) return false;
				exportName = stringPrompt.Value;
			}

			string textureFile = Path.Combine(dlg.SelectedPath, $"{exportName}_0.png");
			string buildFile = Path.Combine(dlg.SelectedPath, $"{exportName}_build.bytes");
			string animFile = Path.Combine(dlg.SelectedPath, $"{exportName}_anim.bytes");

			bool savedAnything = false;
			if (kanim.Texture != null)
			{
				savedAnything |= SaveTextureImpl(kanim.Texture, textureFile, log);
			}
			if (kanim.Build != null)
			{
				savedAnything |= SaveBuildImpl(kanim.Build, buildFile, log);
			}
			if (kanim.Anim != null)
			{
				savedAnything |= SaveAnimImpl(kanim.Anim, animFile, log);
			}
			
			return savedAnything;
		}

		internal static bool ImportSCML(ILogger log = null)
		{
			using var function = log?.BeginFunction();

			OpenFileDialog scmlDlg = new OpenFileDialog()
			{
				Title = "Select a Spriter Project...",
				Filter = "Spriter Projects (*.scml)|*.scml"
			};
			if (scmlDlg.ShowDialog() != DialogResult.OK) return false;

			DocumentManager.Instance.CloseEverything();
			var pkg = SCMLImporter.Convert(scmlDlg.FileName);
			DocumentManager.Instance.OpenConvertedData(pkg);
			return true;
		}

		internal static bool ExportSCML(ILogger log = null)
		{
			using var function = log?.BeginFunction();

			FolderBrowserDialog dlg = new FolderBrowserDialog()
			{
				ShowNewFolderButton = true
			};
			if (dlg.ShowDialog() != DialogResult.OK) return false;
				
			SCMLExporter.Convert(DocumentManager.Instance.Data, dlg.SelectedPath);
			return true;
		}

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
