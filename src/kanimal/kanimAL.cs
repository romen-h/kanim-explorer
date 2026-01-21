using System.IO;
using Microsoft.Extensions.Logging;

namespace kanimal
{
    public static class Kanimal
    {
        // TODO: Injecting a logger instance here isn't ideal
        public static ILogger Logger
        { get; set; }
    }
}