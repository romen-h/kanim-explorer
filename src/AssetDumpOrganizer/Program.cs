using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AssetDumpOrganizer
{
	class Program
	{
		[STAThread]
		static void Main(string[] args)
		{
			FolderBrowserDialog dlgSrc = new FolderBrowserDialog()
			{
				ShowNewFolderButton = false,
				Description = "Select the asset dump folder:"
			};
			FolderBrowserDialog dlgDest = new FolderBrowserDialog()
			{
				ShowNewFolderButton = false,
				Description = "Select the destination folder:"
			};

			if (dlgSrc.ShowDialog() == DialogResult.OK)
			{
				if (dlgDest.ShowDialog() == DialogResult.OK)
				{
					try
					{
						OrganizeAssets(dlgSrc.SelectedPath, dlgDest.SelectedPath);	
					}
					catch (Exception ex)
					{
						Console.WriteLine("Unrecoverable while organizing assets:");
						Console.WriteLine(ex.ToString());
					}
				}
			}
		}

		private static void OrganizeAssets(string dumpFolder, string orgFolder)
		{
			try
			{
				OrganizeKanims(dumpFolder, orgFolder);
			}
			catch (Exception ex)
			{
				Console.WriteLine("Unrecoverable error while organizing kanims:");
				Console.WriteLine(ex.ToString());
			}
		}

		private static void OrganizeKanims(string dumpFolder, string orgFolder)
		{
			string textureDumpFolder = Path.Combine(dumpFolder, "Texture2D");
			if (!Directory.Exists(textureDumpFolder)) throw new Exception("Texture2D folder not found.");

			string textDumpFolder = Path.Combine(dumpFolder, "TextAsset");
			if (!Directory.Exists(textDumpFolder)) throw new Exception("TextAsset folder not found.");

			string kanimsFolder = Path.Combine(orgFolder, "Kanims");
			if (!Directory.Exists(kanimsFolder))
			{
				Directory.CreateDirectory(kanimsFolder);
			}

			foreach (var textureFile in Directory.GetFiles(textureDumpFolder, "*.png"))
			{
				if (!textureFile.EndsWith("_0.png")) continue;

				string fileNameExt = Path.GetFileName(textureFile);
				string fileName = Path.GetFileNameWithoutExtension(textureFile);
				string[] cmps = fileName.Split('_');
				string[] kanimNameCmps = cmps.Take(cmps.Length - 1).ToArray();
				string kanimName = string.Join("_", kanimNameCmps);
				try
				{
					string dumpBuildFile = Path.Combine(textDumpFolder, $"{kanimName}_build.txt");
					bool buildExists = File.Exists(dumpBuildFile);
					string dumpAnimFile = Path.Combine(textDumpFolder, $"{kanimName}_anim.txt");
					bool animExists = File.Exists(dumpAnimFile);

					if (buildExists || animExists)
					{
						string kanimDestFolder = Path.Combine(kanimsFolder, kanimName);
						if (!Directory.Exists(kanimDestFolder))
							Directory.CreateDirectory(kanimDestFolder);

						string orgTextureFile = Path.Combine(kanimDestFolder, fileNameExt);
						File.Copy(textureFile, orgTextureFile);

						if (buildExists)
						{
							string orgBuildFile = Path.Combine(kanimDestFolder, $"{kanimName}_build.bytes");
							File.Copy(dumpBuildFile, orgBuildFile);
						}
						else
						{
							Console.WriteLine("Texture has no matching build file: " + kanimName);
						}

						if (animExists)
						{
							string orgAnimFile = Path.Combine(kanimDestFolder, $"{kanimName}_anim.bytes");
							File.Copy(dumpAnimFile, orgAnimFile);
						}
						else
						{
							Console.WriteLine("Texture has no matching anim file: " + kanimName);
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Failed to copy kanim data: " + kanimName);
					Debug.WriteLine(ex.ToString());
				}
			}
		}
	}
}
