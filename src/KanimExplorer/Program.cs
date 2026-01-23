using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using KanimExplorer.Forms;
using KanimExplorer.Logging;
using KanimExplorer.Settings;
using KanimLib;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace KanimExplorer
{
	static class Program
	{
		public static DateTime LaunchTime
		{ get; private set; }
		
		public static string DataFolder
		{ get; private set; }
		
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			LaunchTime = DateTime.Now;

			bool logFileEnabled = false;
#if DEBUG
			LogLevel logLevel = LogLevel.Trace;
#else
			LogLevel logLevel = LogLevel.Information;
#endif
			foreach (var arg in args)
			{
				string lowerArg = arg.ToLowerInvariant();
				if (lowerArg == "-log")
				{
					logFileEnabled = true;
				}
				else if (lowerArg.StartsWith("-loglevel="))
				{
					string logLevelStr = lowerArg.Substring(10);
					switch (logLevelStr)
					{
						case "error":
							logLevel = LogLevel.Error;
							break;
						case "warn":
							logLevel = LogLevel.Warning;
							break;
						case "debug":
							logLevel = LogLevel.Debug;
							break;
						case "trace":
							logLevel = LogLevel.Trace;
							break;
						case "info":
						default:
							logLevel = LogLevel.Information;
							break;
					}
				}
			}
			
			string programDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
			DataFolder = Path.Combine(programDataFolder, "KanimExplorer");
			Directory.CreateDirectory(DataFolder);
			
			string logsFolder = Path.Combine(DataFolder, "Logs");
			Directory.CreateDirectory(logsFolder);
			
			string logFileName = $"KanimExplorerLog_{LaunchTime:yyyy-MM-dd-HH-mm-ss}.txt";
			string logFile = Path.Combine(logsFolder, logFileName);
			
			KanimLib.Logging.Factory = LoggerFactory.Create(builder =>
			{
				builder.AddProvider(new MemoryLoggerProvider());
				if (logFileEnabled)
				{
					builder.AddProvider(new FileLoggerProvider(logFile));
				}
#if DEBUG
				builder.AddProvider(new DebugLoggerProvider());
#endif
				builder.SetMinimumLevel(logLevel);
			});
			
			var log = KanimLib.Logging.Factory.CreateLogger("Program");
#if DEBUG
			using (log.BeginScope("Testing Log Levels"))
			{
				log.LogTrace("Trace Test.");
				log.LogDebug("Debug Test.");
				log.LogInformation("Info Test.");
				log.LogWarning("Warning Test.");
				log.LogError("Error Test.");
				log.LogError(new Exception("Exception Message"), "Exception Test.");
				log.LogCritical("Critical Test.");
			}
#endif
			log.LogTrace("Initializing ApplicationSettings...");
			ApplicationSettings.SettingFilePath = Path.Combine(DataFolder, "Settings.json");
			ApplicationSettings.Load();
			Debug.Assert(ApplicationSettings.Instance != null);

			log.LogTrace("Initializing DocumentManager...");
			var documentManager = DocumentManager.Instance;
			Debug.Assert(DocumentManager.Instance != null);
			
			log.LogTrace("Initializing main window...");
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
			
			log.LogTrace("Main window exited.");
		}
	}
}
