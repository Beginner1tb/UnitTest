using System;
using InterfaceTest1;

namespace CalcTest1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ITempCalcInterface1 tempCalcInterface1= new TempCalcInterface1();
            Processor processor=new Processor(tempCalcInterface1);
            Console.WriteLine(processor.AddValue(1));
            

        }
    }
}