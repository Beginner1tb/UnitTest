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
            Console.WriteLine("-------------------");
            var services2 = new ServiceCollection();
            services2.AddScoped<ITempCalcInterface1, TempCalcInterface1>();
            //后续注册服务会覆盖前面注册的服务
            //services2.AddScoped<ITempCalcInterface1, TempCalcInterface2>();
            services2.AddScoped<Processor>();
            var serviceProvider2 = services2.BuildServiceProvider();
            //但是在BuildServoceProvider之后，再进行注册就没有用了
            services2.AddScoped<ITempCalcInterface1, TempCalcInterface2>();
            var AddTest2 = serviceProvider2.GetService<Processor>();
            Console.WriteLine(AddTest2.AddValue(3));

        }
    }
}