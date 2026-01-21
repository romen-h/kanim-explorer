using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.Extensions.Logging;

namespace kanimal
{
    public class DebonerProcessor : Processor
    {
        public static ILogger Logger
        { get; set; }

        public override XmlDocument Process(XmlDocument original)
        {
            Logger.LogWarning("Deboning is not currently supported.");
            return original;
        }
    }
}
