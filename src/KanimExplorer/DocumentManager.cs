using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KanimExplorer.Logging;

using KanimLib;
using KanimLib.KanimModel;
using KanimLib.Serialization;

using Microsoft.Extensions.Logging;

namespace KanimExplorer
{
	internal class DocumentManager
	{
		private static DocumentManager s_instance;
		
		public static DocumentManager Instance
		{
			get
			{
				if (s_instance == null)
				{
					s_instance = new DocumentManager();
				}
				return s_instance;
			}
		}
		
		private readonly ILogger _log = KanimLib.Logging.Factory.CreateLogger("DocumentManager");
		
		private string _loadedTextureFile = null;
		private string _loadedBuildFile = null;
		private string _loadedAnimFile = null;
		
		public string TextureFilePath => _loadedTextureFile;
		
		public string BuildFilePath => _loadedBuildFile;
		
		public string AnimFilePath => _loadedAnimFile;
		
		public KanimPackage Data
		{ get; private set; }
		
		public object SelectedObject
		{ get; private set; }

		public bool FilesAreOpen => Data?.HasAnyData ?? false;
		
		public event EventHandler LoadedTextureChanged;
		private void InvokeLoadedTextureChanged()
		{
			try
			{
				LoadedTextureChanged?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				_log.LogError(ex, "Error in LoadedTextureChanged handler.");
			}
		}
		
		public event EventHandler LoadedBuildChanged;
		private void InvokeLoadedBuildChanged()
		{
			try
			{
				LoadedBuildChanged?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				_log.LogError(ex, "Error in LoadedBuildChanged handler.");
			}
		}

		public event EventHandler LoadedAnimChanged;
		private void InvokeLoadedAnimChanged()
		{
			try
			{
				LoadedAnimChanged?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				_log.LogError(ex, "Error in LoadedAnimChanged handler.");
			}
		}
		
		public event EventHandler<SelectedObjectChangedEventArgs> SelectedObjectChanged;
		private void InvokeSelectedObjectChanged()
		{
			try
			{
				SelectedObjectChanged?.Invoke(this, new SelectedObjectChangedEventArgs(SelectedObject));
			}
			catch (Exception ex)
			{
				_log.LogError(ex, "Error in SelectedItemChanged handler.");
			}
		}

		private DocumentManager()
		{ }

		internal bool OpenConvertedData(KanimPackage data)
		{
			using var function = _log.BeginFunction();
			try
			{
				if (data == null) throw new ArgumentNullException(nameof(data));
				if (Data != null) throw new InvalidOperationException("Kanim data is already loaded.");

				Data = data;
				_loadedTextureFile = null;
				_loadedBuildFile = null;
				_loadedAnimFile = null;
				_log.LogInformation("Opened kanim data.");
			}
			catch (Exception ex)
			{
				_log.LogError(ex, "Failed to open kanim data.");
				return false;
			}

			InvokeLoadedTextureChanged();
			InvokeLoadedBuildChanged();
			InvokeLoadedAnimChanged();
			return true;
		}

		public static void GetSupportedFiles(IEnumerable<string> inputFiles, out IReadOnlyList<string> textureFiles, out IReadOnlyList<string> buildFiles, out IReadOnlyList<string> animFiles, out IReadOnlyList<string> invalidFiles)
		{
			List<string> textures = new List<string>();
			List<string> builds = new List<string>();
			List<string> anims = new List<string>();
			List<string> invalid = new List<string>();

			foreach (string file in inputFiles)
			{
				string fileName = Path.GetFileName(file).ToLowerInvariant();
				if (fileName.EndsWith(".png"))
				{
					textures.Add(file);
				}
				else if (fileName.EndsWith("build.bytes") || fileName.EndsWith("build.txt") || fileName.EndsWith("build.prefab"))
				{
					builds.Add(file);
				}
				else if (fileName.EndsWith("anim.bytes") || fileName.EndsWith("anim.txt") || fileName.EndsWith("anim.prefab"))
				{
					anims.Add(file);
				}
				else
				{
					invalid.Add(file);
				}
			}

			textureFiles = textures;
			buildFiles = builds;
			animFiles = anims;
			invalidFiles = invalid;
		}

		public bool OpenTexture(string file)
		{
			using var function = _log.BeginFunction();
			using var scope = _log.BeginScope(file);
			try
			{
				if (_loadedTextureFile != null || Data?.Texture != null) throw new InvalidOperationException("A texture is already loaded.");
				if (string.IsNullOrWhiteSpace(file)) throw new ArgumentNullException(nameof(file));
				if (!File.Exists(file)) throw new ArgumentException("File does not exist.");

				EnsurePackageCreated();

				_log.LogTrace("Opening texture file...");
				using (FileStream fs = new FileStream(file, FileMode.Open))
				{
					Bitmap bmp = new Bitmap(fs);
					Data.SetTexture((Bitmap)bmp.Clone(), false);
				}
				_loadedTextureFile = file;
				_log.LogInformation("Opened texture.");
			}
			catch (Exception ex)
			{
				_log.LogError(ex, "Failed to open texture file.");
				return false;
			}

			InvokeLoadedTextureChanged();
			return true;
		}
		
		public void CloseTexture()
		{
			using var function = _log.BeginFunction();
			_loadedTextureFile = null;
			if (Data == null) return;
			if (Data.Texture == null) return;

			var texture = Data.Texture;
			Data.SetTexture(null, false);
			_log.LogTrace("Disposing texture...");
			texture.Dispose();

			_log.LogInformation("Closed texture.");

			_log.LogTrace("Invoking PropertyChanged...");
			InvokeLoadedTextureChanged();
		}
		
		public bool OpenBuild(string file)
		{
			using var function = _log.BeginFunction();
			using var scope = _log.BeginScope(file);
			try
			{
				if (_loadedBuildFile != null || Data?.Build != null) throw new InvalidOperationException("A build is already loaded.");
				if (string.IsNullOrWhiteSpace(file)) throw new ArgumentNullException(nameof(file));
				if (!File.Exists(file)) throw new ArgumentException("File does not exist.");
					
				EnsurePackageCreated();

				_log.LogTrace("Opening build file...");
				var build = KanimReader.ReadBuild(file);
				Data.SetBuild(build, false);
					
				_loadedBuildFile = file;
				_log.LogInformation("Opened build.");
			}
			catch (Exception ex)
			{
				_log.LogError(ex, $"Failed to open build file: {file}");
				return false;
			}

			InvokeLoadedBuildChanged();
			return true;
		}
		
		public void CloseBuild()
		{
			using var function = _log.BeginFunction();
			_loadedBuildFile = null;

			if (Data == null) return;
			if (Data.Build == null) return;
				
			Data.SetBuild(null, false);
			_log.LogInformation("Closed build.");

			_log.LogTrace("Invoking PropertyChanged...");
				
			InvokeLoadedBuildChanged();
		}
		
		public bool OpenAnim(string file)
		{
			using var function = _log.BeginFunction();
			using var scope = _log.BeginScope(file);
			try
			{
				if (_loadedAnimFile != null || Data?.Anim != null) throw new InvalidOperationException("An anim bank is already loaded.");
				if (string.IsNullOrWhiteSpace(file)) throw new ArgumentNullException(nameof(file));
				if (!File.Exists(file)) throw new ArgumentException("File does not exist.");

				EnsurePackageCreated();

				_log.LogTrace("Opening anim file...");
				var anim = KanimReader.ReadAnim(file);
				Data.SetAnim(anim, false);

				_loadedAnimFile = file;
				_log.LogInformation("Opened anim bank.");
			}
			catch (Exception ex)
			{
				_log.LogError(ex, $"Failed to open anim file: {file}");
				return false;
			}

			InvokeLoadedAnimChanged();
			return true;
		}
		
		public void CloseAnim()
		{
			using var function = _log.BeginFunction();
			
			if (Data == null) return;
			if (Data.Anim == null) return;

			Data.SetAnim(null, false);
			_loadedAnimFile = null;
			_log.LogInformation("Closed anim bank.");
				
			InvokeLoadedAnimChanged();
		}
		
		public void CloseEverything()
		{
			using var function = _log.BeginFunction();

			_loadedTextureFile = null;
			_loadedBuildFile = null;
			_loadedAnimFile = null;

			if (Data == null) return;
			
			if (Data.Texture != null)
			{
				Data.Texture.Dispose();
			}
			
			Data = null;
			
			InvokeLoadedTextureChanged();
			InvokeLoadedBuildChanged();
			InvokeLoadedAnimChanged();
		}
		
		public void SelectObject(object selection)
		{
			if (SelectedObject != selection)
			{
				SelectedObject = selection;
				InvokeSelectedObjectChanged();
			}
		}
		
		private void EnsurePackageCreated()
		{
			using var function = _log.BeginFunction();
			if (Data == null)
			{
				Data = new KanimPackage();
				_log.LogTrace("Created new KanimPackage because nothing was loaded yet.");
				Data.TextureChanged += Data_TextureChanged;
				Data.BuildChanged += Data_BuildChanged;
				Data.AnimChanged += Data_AnimChanged;
			}
		}
		
		private void CleanupPackage()
		{
			if (Data == null) return;

			Data.TextureChanged -= Data_TextureChanged;
			Data.BuildChanged -= Data_BuildChanged;
			Data.AnimChanged -= Data_AnimChanged;
			Data = null;
		}

		private void Data_TextureChanged(object sender, EventArgs e)
		{
			LoadedTextureChanged?.Invoke(this, EventArgs.Empty);
		}

		private void Data_BuildChanged(object sender, EventArgs e)
		{
			LoadedBuildChanged?.Invoke(this, EventArgs.Empty);
		}

		private void Data_AnimChanged(object sender, EventArgs e)
		{
			LoadedAnimChanged?.Invoke(this, EventArgs.Empty);
		}
		
		public Bitmap GetSelectedTexture()
		{
			if (Data == null) return null;
			if (!IsTexture(SelectedObject)) return null;
			
			return Data.Texture;
		}
		
		public KBuild GetSelectedBuild()
		{
			if (Data == null) return null;
			if (!IsPartOfBuild(SelectedObject)) return null;
			
			return Data.Build;
		}
		
		public KAnim GetSelectedAnim()
		{
			if (Data == null) return null;
			if (!IsPartOfAnim(SelectedObject)) return null;
			
			return Data.Anim;
		}
		
		public static bool IsTexture(object obj)
		{
			switch (obj)
			{
				case Bitmap:
					return true;
				
				default:
					return false;
			}
		}

		public static bool IsPartOfBuild(object obj)
		{
			switch (obj)
			{
				case KBuild:
				case KSymbol:
				case KFrame:
					return true;

				default:
					return false;
			}
		}

		public bool IsSelectedObjectPartOfAnim => IsPartOfAnim(SelectedObject);
		
		public static bool IsPartOfAnim(object obj)
		{
			switch (obj)
			{
				case KAnim:
				case KAnimBank:
				case KAnimFrame:
				case KAnimElement:
					return true;

				default:
					return false;
			}
		}
	}
}
