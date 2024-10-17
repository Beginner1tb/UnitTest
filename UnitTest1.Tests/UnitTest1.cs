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
            var calculatorMock = new Mock<ICalculator1>(); // ���� ICalculator1 �� Mock
            calculatorMock
                .Setup(c => c.Add(5, 3)) // ģ�� Add �������� 8
                .Returns(8);

            var service = new Calculator1Service(calculatorMock.Object); // ע�� Mock ����

            //// Act
            var result = service.CalAdd(5, 3); // ���ñ����Եķ���

            //// Assert
            //Assert.Equal(5, result); // ��֤���ؽ��
            // Assert
            calculatorMock.Verify(c => c.Add(5, 3), Times.Once); // ��֤ Add �����Ƿ񱻵���һ��

            _output.WriteLine("�������");
        }
    }
}
