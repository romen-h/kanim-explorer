using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

using KanimLib;

using Microsoft.Extensions.Logging;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace KanimExplorer.Settings
{
	public class ApplicationSettings
	{
		private static readonly ILogger s_log = KanimLib.Logging.Factory.CreateLogger("Settings");
		
		private static readonly JsonSerializerOptions s_serializerOptions = new JsonSerializerOptions()
			{
				WriteIndented = true
			};
		
		private static bool s_loading = false;

		[JsonIgnore]
		internal static string SettingFilePath
		{ get; set; }

		[JsonIgnore]
		public static ApplicationSettings Instance
		{ get; private set; }

		public static void Load()
		{
			using var function = s_log.BeginFunction();
			using var file = s_log.BeginScope(SettingFilePath);

			ApplicationSettings loaded = null;
			try
			{
				s_loading = true;
				string json = File.ReadAllText(SettingFilePath, Encoding.UTF8);
				loaded = JsonSerializer.Deserialize<ApplicationSettings>(json, s_serializerOptions);
				s_loading = false;
				s_log.LogTrace("Settings file loaded.");
			}
			catch (Exception ex)
			{
				s_log.LogError(ex, "Failed to load settings file. Using default settings...");
				loaded = new ApplicationSettings();
			}
			
			Instance = loaded;
		}

		public static void Save()
		{
			using var function = s_log.BeginFunction();
			using var file = s_log.BeginScope(SettingFilePath);

			try
			{
				string json = JsonSerializer.Serialize(Instance, s_serializerOptions);
				File.WriteAllText(SettingFilePath, json, Encoding.UTF8);
				s_log.LogTrace("Settings file saved.");
			}
			catch (Exception ex)
			{
				s_log.LogError(ex, "Failed to save settings file.");
			}
		}

		[JsonInclude]
		[JsonObjectCreationHandling(JsonObjectCreationHandling.Populate)]
		public WindowState MainWindow
		{ get; private set; }
		
		public void SetMainWindowRectangle(int left, int top, int width, int height)
		{
			MainWindow.SetRectangle(left, top, width, height);
			if (!s_loading) Save();
		}

		private bool _openExplorerAfterSaving;
		[JsonInclude]
		public bool OpenExplorerAfterSaving
		{
			get => _openExplorerAfterSaving;
			set => Set(ref _openExplorerAfterSaving, value);
		}
		
		private bool _showSuccessDialogs;
		[JsonInclude]
		public bool ShowSuccessDialogs
		{
			get => _showSuccessDialogs;
			set => Set(ref _showSuccessDialogs, value);
		}

		[JsonConstructor]
		private ApplicationSettings()
		{
			MainWindow = new WindowState();
			_openExplorerAfterSaving = true;
			_showSuccessDialogs = true;
		}

		private void Set<T>(ref T var, T value)
		{
			var = value;
			if (!s_loading) Save();
		}
	}
}
