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
            calculatorMock
                .Setup(c => c.Add(5, 3)) // 模拟 Add 方法返回 8
                .Returns(8);

            var service = new Calculator1Service(calculatorMock.Object); // 注入 Mock 对象

            //// Act
            var result = service.CalAdd(5, 3); // 调用被测试的方法

            //// Assert
            //Assert.Equal(5, result); // 验证返回结果
            // Assert
            calculatorMock.Verify(c => c.Add(5, 3), Times.Once); // 验证 Add 方法是否被调用一次

            _output.WriteLine("测试完成");
        }
    }
}
