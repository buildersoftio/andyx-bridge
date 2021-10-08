using Andy.X.Bridge.Core.Services;
using Andy.X.Bridge.Core.Utilities.Logging;
using System;

namespace Andy.X.Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            GlobalService globalService;
            LoggerSink loggerSink;

            Console.WriteLine("Buildersoft Andy X Bridge");
            Console.WriteLine("Version 1.0.0-alpha");
            Console.WriteLine("Andy X Bridge is an open-source distributed solution for integrating clusters of Andy X, Kafka, Pulsar and Message Queue Systems together.\n");

            // Sinking Console logs to log files
            loggerSink = new LoggerSink();
            loggerSink.InitializeSink();
            
            globalService = new GlobalService();
            Logger.LogInformation("Andy X Bridge is ready");


            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
