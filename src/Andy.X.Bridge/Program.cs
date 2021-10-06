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
            Console.WriteLine("Buildersoft Andy X Bridge");
            Console.WriteLine("Andy X Bridge is an open-source distributed solution for integrating clusters of Andy X, Kafka, Pulsar and Message Queue Systems together.\n");

            globalService = new GlobalService();
            Logger.LogInformation("Andy X Bridge is ready");


            while (true)
            {
                Console.ReadLine();
            }
        }
    }
}
