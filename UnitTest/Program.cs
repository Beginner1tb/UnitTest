using System;
using UnitTest1.Library.Implement;
using UnitTest1.Library.Interface;

namespace UnitTest
{
    class Program
    {

        static void Main(string[] args)
        {
            ICalculator1 calculator1 = new Calculator1();

            Calculator1Service calculator1Service = new Calculator1Service(calculator1);
            Console.WriteLine(calculator1Service.CalAdd(1, 2));

        }
    }
}
