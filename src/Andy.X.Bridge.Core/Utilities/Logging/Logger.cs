using System;
using System.Diagnostics;

namespace Andy.X.Bridge.Core.Utilities.Logging
{
    public static class Logger
    {
        public static void LogInformation(string log)
        {
            Trace.WriteLine($"{DateTime.Now:yyyy-MM-dd HH-mm-ss} andyx-bridge [info]     |   {log}");
        }
        public static void LogWarning(string log)
        {
            var generalColor = Console.ForegroundColor;
            Trace.Write($"{DateTime.Now:yyyy-MM-dd HH-mm-ss} andyx-bridge ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Trace.Write($"[warning]");
            Console.ForegroundColor = generalColor;
            Trace.WriteLine($"  |   {log}");
        }

        public static void LogError(string log)
        {
            var generalColor = Console.ForegroundColor;
            Trace.Write($"{DateTime.Now:yyyy-MM-dd HH-mm-ss} andyx-bridge ");
            Console.ForegroundColor = ConsoleColor.Red;
            Trace.Write($"[error]");
            Console.ForegroundColor = generalColor;
            Trace.WriteLine($"    |   {log}");
        }

        public static void LogError(string log, string logWithRed)
        {
            var generalColor = Console.ForegroundColor;
            Trace.Write($"{DateTime.Now:yyyy-MM-dd HH-mm-ss} andyx-bridge ");
            Console.ForegroundColor = ConsoleColor.Red;
            Trace.Write($"[error]");
            Console.ForegroundColor = generalColor;
            Trace.Write($"    |   {log}");
            Console.ForegroundColor = ConsoleColor.Red;
            Trace.WriteLine(logWithRed);
            Console.ForegroundColor = generalColor;
        }

        public static void ShowWelcomeTest()
        {
            var generalColor = Console.ForegroundColor;
            Trace.WriteLine("                   Starting Buildersoft Andy X Bridge");
            Trace.WriteLine("                   Copyright (C) 2021 Buildersoft LLC");
            Trace.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Trace.Write("  ###"); Console.ForegroundColor = generalColor; Trace.WriteLine("      ###");
            Console.ForegroundColor = ConsoleColor.Red;
            Trace.Write("    ###"); Console.ForegroundColor = generalColor; Trace.Write("  ###");
            Trace.WriteLine("       Andy X Bridge 1.0.0-alpha. Copyright (C) 2021 Buildersoft LLC");
            Console.ForegroundColor = ConsoleColor.Red;
            Trace.Write("      ####         "); Console.ForegroundColor = generalColor; Trace.WriteLine("Licensed under the Apache License 2.0.  See https://bit.ly/3DqVQbx");
            Console.ForegroundColor = ConsoleColor.Red;
            Trace.WriteLine("    ###  ###");
            Trace.Write("  ###      ###     "); Console.ForegroundColor = generalColor; Trace.WriteLine("Andy X Bridge is an open-source distributed solution for integrating clusters of Andy X, Kafka, Pulsar and Message Queue Systems together");
            Trace.WriteLine("");


            Trace.WriteLine("                   Starting Buildersoft Andy X Bridge...");
            Trace.WriteLine("\n");
        }
    }

}
