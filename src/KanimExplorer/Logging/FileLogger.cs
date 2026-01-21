using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace KanimExplorer.Logging
{
	internal class FileLogger : ILogger
	{
		private readonly string _category;
		private readonly string _logFile;
		private readonly IExternalScopeProvider? _scopeProvider;

		private static readonly object _fileLock = new object();

		public FileLogger(string category, string logFile, IExternalScopeProvider? scopeProvider)
		{
			_logFile = logFile;
			_scopeProvider = scopeProvider;
		}

		public IDisposable BeginScope<TState>(TState state) where TState : notnull
		{
			return _scopeProvider?.Push(state);
		}

		public bool IsEnabled(LogLevel logLevel) => true;

		public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
		{
			DateTime now = DateTime.UtcNow;
			lock (_fileLock)
			{
				using var writer = File.AppendText(_logFile);
				
				writer.Write("[");
				writer.Write(now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
				writer.Write("] [");
				writer.Write(logLevel.ToString());
				writer.Write("] ");
				writer.Write(_category);
				writer.WriteLine();
				
				writer.Write(formatter(state, exception));
				writer.WriteLine();
				
				if (exception != null)
				{
					writer.WriteLine("Exception:");
					writer.WriteLine(exception.ToString());
				}
				
				writer.WriteLine();
				
				writer.Flush();
			}
		}
	}
	
	internal class FileLoggerProvider : ILoggerProvider, ISupportExternalScope
	{
		private string _path;
		private IExternalScopeProvider? _scopeProvider;
		
		public FileLoggerProvider(string path)
		{
			_path = path;
		}

		public void SetScopeProvider(IExternalScopeProvider scopeProvider)
		{
			_scopeProvider = scopeProvider;
		}

		public ILogger CreateLogger(string categoryName)
		{
			return new FileLogger(categoryName, _path, _scopeProvider);
		}

		public void Dispose()
		{ }
	}
}
