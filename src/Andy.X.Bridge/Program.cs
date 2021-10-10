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

            Logger.ShowWelcomeTest();

            globalService = new GlobalService();
            Logger.LogInformation("Andy X Bridge is ready");


            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
