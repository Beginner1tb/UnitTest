using Moq;
using InterfaceTest1;
namespace InterfaceTest1.Test
{
    public class TempInterfaceTest
    {
        [Fact]
        public void CalcMoqTest1()
        {
            var mocktempCalcInterface1 = new Mock<ITempCalcInterface1>();
            var processor = new Processor(mocktempCalcInterface1.Object);

            processor.AddValue(1);
            mocktempCalcInterface1.Verify(i => i.AddValue(1), Times.Once);

            mocktempCalcInterface1.Setup(service => service.AddValue(1)).Returns(2);

            var result = processor.AddValue(1);

            Assert.Equal(2, result);


        }
        [Fact]
        public void CalcFactoryTest()
        {
            var mockFactory = new Mock<ICalculatorFactory>();
            var mockCalculator= new Mock<ICalculator>();
            var mockOperation=new Mock<IOperation>();
            mockFactory.Setup(f=>f.CreateCalculator()).Returns(mockCalculator.Object);
        }

        [Fact]
        public void CalculatorTest()
        {

        }

        public void Calculator()
        {
            var factory=new CalculatorFactory();
            var calculator=factory.CreateCalculator();

            Console.WriteLine(calculator.Calculate(10,10));
        }
    }
}