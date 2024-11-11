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
            // 创建一个 Mock 的 ICalculator
            var mockCalculator = new Mock<ICalculator>();

            // 设置 mockCalculator 的 Calculate 方法返回一个固定值
            mockCalculator.Setup(c => c.Calculate(It.IsAny<int>(), It.IsAny<int>())).Returns(15);

            // 创建 Mock 的 ICalculatorFactory
            var mockFactory = new Mock<ICalculatorFactory>();

            // 设置工厂的 CreateCalculator 方法返回我们 Mock 的 ICalculator
            mockFactory.Setup(f => f.CreateCalculator(It.IsAny<IOperation>())).Returns(mockCalculator.Object);

            // 使用工厂创建 Calculator
            var calculator = mockFactory.Object.CreateCalculator(new Mock<IOperation>().Object);

            // 调用 Calculate 方法
            int result = calculator.Calculate(5, 3);

            // 验证返回结果是否为 42
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