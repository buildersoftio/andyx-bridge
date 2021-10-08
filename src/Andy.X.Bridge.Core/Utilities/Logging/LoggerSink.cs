using Andy.X.Bridge.IO.Locations;
using System;
using System.IO;

namespace Andy.X.Bridge.Core.Utilities.Logging
{
    public class LoggerSink
    {
        private FileStream fileStream;
        private StreamWriter streamWriter;
        private TextWriter textWriter;
        public LoggerSink()
        {
            if (Directory.Exists(AppLocations.LogsDirectory()) != true)
            {
                Directory.CreateDirectory(AppLocations.LogsDirectory());
            }
        }

        public void InitializeSink()
        {
            fileStream = new FileStream(AppLocations.GetLogConfigurationFile(), FileMode.OpenOrCreate);
            textWriter = Console.Out;
            streamWriter = new StreamWriter(fileStream);
            streamWriter.AutoFlush = true;

            Console.SetOut(streamWriter);
        }
    }
}
