using Andy.X.Bridge.Core.Services;
using Andy.X.Bridge.Core.Utilities.Logging;
using System;
using System.Diagnostics;

namespace Andy.X.Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalService globalService;
            LoggerSink loggerSink;
            // Sinking Console logs to log files
            loggerSink = new LoggerSink();
            loggerSink.InitializeSink();

            Trace.WriteLine("Buildersoft Andy X Bridge");
            Trace.WriteLine("Version 1.0.0-alpha");
            Trace.WriteLine("Andy X Bridge is an open-source distributed solution for integrating clusters of Andy X, Kafka, Pulsar and Message Queue Systems together.\n");

            globalService = new GlobalService();
            Logger.LogInformation("Andy X Bridge is ready");


            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
