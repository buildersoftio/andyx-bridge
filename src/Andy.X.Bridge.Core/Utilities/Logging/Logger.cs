using System;

namespace Andy.X.Bridge.Core.Utilities.Logging
{
    public static class Logger
    {
        public static void LogInformation(string log)
        {
            Console.WriteLine($"{DateTime.Now:yyyy-MM-dd HH-mm-ss} andyx-bridge [info]     |   {log}");
        }
        public static void LogWarning(string log)
        {
            var generalColor = Console.ForegroundColor;
            Console.Write($"{DateTime.Now:yyyy-MM-dd HH-mm-ss} andyx-bridge ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[warning]");
            Console.ForegroundColor = generalColor;
            Console.WriteLine($"  |   {log}");
        }

        public static void LogError(string log)
        {
            var generalColor = Console.ForegroundColor;
            Console.Write($"{DateTime.Now:yyyy-MM-dd HH-mm-ss} andyx-bridge ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[error]");
            Console.ForegroundColor = generalColor;
            Console.WriteLine($"    |   {log}");
        }

        public static void LogError(string log, string logWithRed)
        {
            var generalColor = Console.ForegroundColor;
            Console.Write($"{DateTime.Now:yyyy-MM-dd HH-mm-ss} andyx-bridge ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[error]");
            Console.ForegroundColor = generalColor;
            Console.Write($"    |   {log}");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(logWithRed);
            Console.ForegroundColor = generalColor;
        }
    }

}
