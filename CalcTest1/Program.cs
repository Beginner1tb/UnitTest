using System;
using Microsoft.Extensions.DependencyInjection;
using InterfaceTest1;

namespace CalcTest1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITempCalcInterface1 tempCalcInterface1 = new TempCalcInterface1();
            Processor processor = new Processor(tempCalcInterface1);
            Console.WriteLine("Interface Invoke Result is " + processor.AddValue(1));
            Console.WriteLine("----------------");
            var services = new ServiceCollection();
            services.AddScoped(typeof(ITempCalcInterface1), typeof(TempCalcInterface1));
            var serviceProvider = services.BuildServiceProvider();
            var tempCalcAddTest = serviceProvider.GetService<ITempCalcInterface1>();
            Console.WriteLine("Dependency Injection Result is " + tempCalcAddTest.AddValue(2));

        }
    }
}