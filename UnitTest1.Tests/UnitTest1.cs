using System;
using Xunit;
using Moq;
using UnitTest;
using UnitTest1.Library.Interface;
using Xunit.Abstractions;

namespace UnitTest1.Tests
{

    public class UnitTest1
    {

        private readonly ITestOutputHelper _output;

        public UnitTest1(ITestOutputHelper output)
        {
            _output = output;
        }
        [Fact]
        public void Execute_ShouldCallAddMethod_AndPrintCorrectResult()
        {
            // Arrange
            var calculatorMock = new Mock<ICalculator1>(); // 创建 ICalculator1 的 Mock
                                                           //calculatorMock
                                                           //    .Setup(c => c.Add(5, 3)) // 模拟 Add 方法返回 8
                                                           //    .Returns(8);

            //calculatorMock.Setup(c => c.Add(5, 3)).Returns(8);

            calculatorMock.Setup(x => x.Add(It.IsAny<int>(), It.IsAny<int>())).Returns(8);

          

           
            var service = new Calculator1Service(calculatorMock.Object); // 注入 Mock 对象


            ////// Act
            var result = service.CalAdd(5, 3); // 调用被测试的方法

            //// Assert
            Assert.Equal(8, result); // 验证返回结果

            // Assert

            calculatorMock.Verify(c => c.Add(It.IsAny<int>(), It.IsAny<int>()), Times.Once, "调用了一次");

            _output.WriteLine("测试完成");
        }

        [Fact]
        public void Execute_ShouldCallAddMethod_ThrowNegativeValueException()
        {
            var calculatorMock = new Mock<ICalculator1>();
            calculatorMock.Setup(x => x.Add(It.Is<int>(a => a < 0), It.IsAny<int>()))
                .Throws(new ArgumentException("不允许负数"));
            var service = new Calculator1Service(calculatorMock.Object); // 注入 Mock 对象
            // Act
            var exception = Record.Exception(() => service.CalAdd(-1, 2));

            // Assert
            Assert.Null(exception);
            // Assert
            calculatorMock.Verify(c => c.Add(It.IsAny<int>(), It.IsAny<int>()), Times.Once, "调用了一次");

            _output.WriteLine("测试完成");

        }

        [Fact]
        public void Execute_Exception()
        {
            var calculatorMock = new Mock<ICalculator1>();
            calculatorMock.Setup(x => x.Add(It.IsAny<int>(), It.IsAny<int>())).Throws(new ArgumentException("测试抛出异常"));

            var service = new Calculator1Service(calculatorMock.Object); // 注入 Mock 对象
            // Act
            var exception = Record.Exception(() => service.CalAdd(-1, 2));

            //// Assert
            //Assert.Null(exception);
            Assert.Throws<ApplicationException>(() => service.CalAdd(1, 2));
            // Assert
            //calculatorMock.Verify(c => c.Add(It.IsAny<int>(), It.IsAny<int>()), Times.Once, "调用了一次");

            _output.WriteLine("测试完成");

        }
    }
}
