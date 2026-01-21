using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenTK.Compute.OpenCL;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace KanimExplorer.Logging
{
	internal class LogEntry
	{
		private readonly string _stringForm;
		
		internal readonly string headerString;
		
		public DateTime Time
		{ get; private set; }
		
		public LogLevel Level
		{ get; private set; }
		
		public string Category
		{ get; private set; }
		
		public IReadOnlyList<string> Scopes
		{ get; private set; }
		
		public string Message
		{ get; private set; }
		
		public Exception Exception
		{ get; private set; }
		
		internal LogEntry(DateTime time, LogLevel level, string category, IReadOnlyList<string> scopes, string message, Exception exception)
		{
			Time = time;
			Level = level;
			Category = category;
			Scopes = scopes;
			Message = message;
			Exception = exception;
			
			headerString = $"[{Time:HH:mm:ss.fff}] [{Level}] {Category}";
		}
	}
	
	internal class MemoryLogger : ILogger
	{
		private const int MAX_ENTRIES = 1000;
		
		private static readonly ObservableCollection<LogEntry> s_logEntries = new ObservableCollection<LogEntry>();
		public static readonly ReadOnlyObservableCollection<LogEntry> LogEntries = new ReadOnlyObservableCollection<LogEntry>(s_logEntries);
		private static readonly object s_lock = new object();
		
		private static readonly Timer s_cleanupTimeout = new Timer(Cleanup, null, Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);

		public static string LogText
		{ get; private set; }
		
		public static event EventHandler LogEntriesChanged;
		
		private readonly string _category;
		private readonly IExternalScopeProvider? _scopeProvider;
		
		internal MemoryLogger(string category, IExternalScopeProvider? scopeProvider)
		{
			_category = category;
			_scopeProvider = scopeProvider;
		}

		public IDisposable BeginScope<TState>(TState state) where TState : notnull
		{
			return _scopeProvider?.Push(state);
		}
		
		public bool IsEnabled(LogLevel logLevel) => true;

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			List<string> scopes = new List<string>();
			_scopeProvider?.ForEachScope((scope, state) =>
			{
				scopes.Add(scope?.ToString());
			}, state);
			LogEntry entry = new LogEntry(DateTime.UtcNow, logLevel, _category, scopes, formatter(state, exception), exception);
			lock (s_lock)
			{
				s_cleanupTimeout.Change(Timeout.InfiniteTimeSpan, Timeout.InfiniteTimeSpan);
				s_logEntries.Add(entry);
				LogText = BuildLogText();
				s_cleanupTimeout.Change(TimeSpan.FromMilliseconds(1000), Timeout.InfiniteTimeSpan);
			}
			LogEntriesChanged?.Invoke(this, EventArgs.Empty);
		}

		private static void Cleanup(object state)
		{
			lock (s_lock)
			{
				while (s_logEntries.Count > MAX_ENTRIES)
				{
					s_logEntries.RemoveAt(0);
				}
				LogText = BuildLogText();
			}
			LogEntriesChanged?.Invoke(null, EventArgs.Empty);
		}

		private static string BuildLogText()
		{
			StringBuilder sb = new StringBuilder();

			foreach (var entry in s_logEntries)
			{
				sb.AppendLine(entry.headerString);
				sb.AppendLine(entry.Message);

				if (entry.Scopes != null && entry.Scopes.Count > 0)
				{
					sb.AppendLine("---Scopes---");
					foreach (string scope in entry.Scopes)
					{
						sb.AppendLine(scope);
					}
					sb.AppendLine("---End of Scopes---");
				}
				
				if (entry.Exception != null)
				{
					sb.AppendLine("---Exception---");
					sb.AppendLine(entry.Exception.ToString());
					sb.AppendLine("---End of Exception---");
				}
				sb.AppendLine();
			}
			
			return sb.ToString();
		}
	}
	
	internal class MemoryLoggerProvider : ILoggerProvider, ISupportExternalScope
	{
		private IExternalScopeProvider? _scopeProvider;
		
		public MemoryLoggerProvider()
		{ }

		public void SetScopeProvider(IExternalScopeProvider scopeProvider)
		{
			_scopeProvider = scopeProvider;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new MemoryLogger(categoryName, _scopeProvider);
		}
		
		public void Dispose()
		{ }
	}
}
