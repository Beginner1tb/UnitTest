namespace InterfaceTest1
{
    public interface ITempCalcInterface1
    {
        int AddValue(int value);
    }

    public class TempCalcInterface1 : ITempCalcInterface1
    {
        public int AddValue(int value)
        {
            return value + 1;
        }
    }

    public class Processor
    {
        private readonly ITempCalcInterface1 _tempCalcInterface1;
        public Processor(ITempCalcInterface1 tempCalcInterface1)
        {
            _tempCalcInterface1 = tempCalcInterface1;
        }

        public int AddValue(int value)
        {
            return _tempCalcInterface1.AddValue(value);
        }
    }
    //操作接口
    public interface IOperation
    {
        int Calculate(int a, int b);
    }

    public class Addition : IOperation
    {

        public int Calculate(int a, int b)
        {
            return a + b;
        }
    }

    public class Subtraction:IOperation
    {
        public int Calculate(int a, int b)
        {
            return a - b;
        }
    }

    public class Multiplication:IOperation
    {
        public int Calculate(int a, int b)
        {
            return a * b;
        }
    }

    //计算器接口
    public interface ICalculator
    {
        int Calculate(int a, int b);
    }

    public class Calculator : ICalculator
    {
        private readonly IOperation _operation;

        public Calculator(IOperation operation) { _operation = operation; }
        public int Calculate(int a, int b)
        {
           return _operation.Calculate(a, b);
        }
    }
    //工厂接口
    public interface ICalculatorFactory
    {
        ICalculator CreateCalculator(IOperation operation);
    }

    public class CalculatorFactory : ICalculatorFactory
    {
        public ICalculator CreateCalculator(IOperation operation)
        {
           return new Calculator(operation);
        }
    }


}
