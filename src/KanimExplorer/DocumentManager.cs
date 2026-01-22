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
		
		public KanimPackage Data
		{ get; private set; }

		public bool FilesAreOpen => Data?.HasAnyData ?? false;
		
		public event EventHandler LoadedTextureChanged;
		private void InvokeLoadedTextureChanged()
		{
			_log.LogTrace("Invoking LoadedTextureChanged...");
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
			_log.LogTrace("Invoking LoadedBuildChanged...");
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
			_log.LogTrace("Invoking LoadedAnimChanged...");
			try
			{
				LoadedAnimChanged?.Invoke(this, EventArgs.Empty);
			}
			catch (Exception ex)
			{
				_log.LogError(ex, "Error in LoadedAnimChanged handler.");
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
				_loadedTextureFile = data.HasTexture ? string.Empty : null;
				_loadedBuildFile = data.HasBuild ? string.Empty : null;
				_loadedAnimFile = data.HasAnim ? string.Empty : null;
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

		public static void SelectSupportedFiles(IEnumerable<string> inputFiles, out string textureFile, out string buildFile, out string animFile, out IReadOnlyList<string> invalidFiles)
		{
			textureFile = null;
			buildFile = null;
			animFile = null;

			List<string> invalid = new List<string>();

			foreach (string file in inputFiles)
			{
				if (file.EndsWith(".png"))
				{
					if (textureFile == null)
					{
						textureFile = file;
					}
				}
				else if (file.EndsWith("build.bytes") || file.EndsWith("build.txt") || file.EndsWith("build.prefab"))
				{
					if (buildFile == null)
					{
						buildFile = file;
					}
				}
				else if (file.EndsWith("anim.bytes") || file.EndsWith("anim.txt") || file.EndsWith("anim.prefab"))
				{
					if (animFile == null)
					{
						animFile = file;
					}
				}
				else
				{
					invalid.Add(file);
				}
			}

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
	}
}
