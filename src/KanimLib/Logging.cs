using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using kanimal;
using Microsoft.Extensions.Logging;

namespace KanimLib
{
	public static class Logging
	{
		private static ILoggerFactory s_loggerFactory;
		
		public static ILoggerFactory Factory
		{
			get => s_loggerFactory;
			set
			{
				if (s_loggerFactory != null) throw new InvalidOperationException("A logging factory has already been set.");
				
				s_loggerFactory = value;
				kanimal.Kanimal.Logger = s_loggerFactory.CreateLogger("Kanimal");
				kanimal.DebonerProcessor.Logger = s_loggerFactory.CreateLogger("Kanimal.Deboner");
				kanimal.KeyFrameInterpolateProcessor.Logger = s_loggerFactory.CreateLogger("Kanimal.KeyFrameInterpolator");
				kanimal.KanimReader.Logger = s_loggerFactory.CreateLogger("Kanimal.KanimReader");
				kanimal.KanimWriter.Logger = s_loggerFactory.CreateLogger("Kanimal.KanimWriter");
				kanimal.ScmlReader.Logger = s_loggerFactory.CreateLogger("Kanimal.ScmlReader");
				kanimal.ScmlWriter.Logger = s_loggerFactory.CreateLogger("Kanimal.ScmlWriter");
				kanimal.TexturePacker.Logger = s_loggerFactory.CreateLogger("Kanimal.TexturePacker");
				kanimal.Writer.Logger = s_loggerFactory.CreateLogger("Kanimal.AbstractWriter");
			}
		}

		public static IDisposable? BeginFunction(this ILogger log, [CallerMemberName] string methodName = null)
		{
			return log.BeginScope(methodName);
		}
	}
}
