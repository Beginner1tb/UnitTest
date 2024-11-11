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
        public void CalculatorFactory_AdditionTest()
        {

            var mockCalculator = new Mock<ICalculator>();
            mockCalculator.Setup(c => c.Calculate(It.IsAny<int>(), It.IsAny<int>())).Returns(8);
            var mockFactory = new Mock<ICalculatorFactory>();

            mockFactory.Setup(f => f.CreateCalculator(It.IsAny<IOperation>())).Returns(mockCalculator.Object);

            var calculator = mockFactory.Object.CreateCalculator(new Addition());

            int result = calculator.Calculate(5, 3);

            Assert.Equal(8, result);
        }

        [Fact]
        public void CalculatorFactory_SubtractionTest()
        {
            // ����һ�� Mock �� ICalculator
            var mockCalculator = new Mock<ICalculator>();

            // ���� mockCalculator �� Calculate ��������һ���̶�ֵ
            mockCalculator.Setup(c => c.Calculate(It.IsAny<int>(), It.IsAny<int>())).Returns(15);

            // ���� Mock �� ICalculatorFactory
            var mockFactory = new Mock<ICalculatorFactory>();

            // ���ù����� CreateCalculator ������������ Mock �� ICalculator
            mockFactory.Setup(f => f.CreateCalculator(It.IsAny<IOperation>())).Returns(mockCalculator.Object);

            // ʹ�ù������� Calculator
            var calculator = mockFactory.Object.CreateCalculator(new Mock<IOperation>().Object);

            // ���� Calculate ����
            int result = calculator.Calculate(5, 3);

            // ��֤���ؽ���Ƿ�Ϊ 42
            Assert.Equal(15, result);
        }

        public void Calculator()
        {
            //var factory = new CalculatorFactory();
            //var calculator = factory.CreateCalculator();

            //Console.WriteLine(calculator.Calculate(10, 10));
            var factory = new CalculatorFactory();
            var operation = new Addition();
            var calculator = factory.CreateCalculator(operation);
            Console.WriteLine(calculator.Calculate(10, 10));


        }
    }
}