using System;
using System.Collections.Generic;
using System.Text;
using UnitTest1.Library.Interface;
using UnitTest1.Library.Implement;

namespace UnitTest
{
    public class Calculator1Service
    {
        private readonly ICalculator1 _calculator1;

        public Calculator1Service(ICalculator1 calculator1)
        {
            _calculator1 = calculator1;
        }

        public int CalAdd(int a, int b)
        {
            return _calculator1.Add(a, b);
        }
    }
}
