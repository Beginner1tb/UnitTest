using System;
using NLog;
using NLog.Config;

namespace ConsoleNlogTest1
{
    class Program
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            logger.Error("1111");
            Console.ReadKey();
        }
    }
}
