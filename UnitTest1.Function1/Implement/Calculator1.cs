using System;
using System.Collections.Generic;
using System.Text;
using UnitTest1.Library.Interface;

namespace UnitTest1.Library.Implement
{
    public class Calculator1 : ICalculator1
    {
        public int Add(int param1, int param2)
        {
            return param1 + param2;
        }

        public double AddDouble(double param1,double param2)
        {
            return param1 + param2;
        }
    }
}