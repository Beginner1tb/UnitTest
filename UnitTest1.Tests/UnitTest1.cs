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
                                                           //calculatorMock
                                                           //    .Setup(c => c.Add(5, 3)) // ģ�� Add �������� 8
                                                           //    .Returns(8);

            //calculatorMock.Setup(c => c.Add(5, 3)).Returns(8);

            calculatorMock.Setup(x => x.Add(It.IsAny<int>(), It.IsAny<int>())).Returns(8);

          

           
            var service = new Calculator1Service(calculatorMock.Object); // ע�� Mock ����


            ////// Act
            var result = service.CalAdd(5, 3); // ���ñ����Եķ���

            //// Assert
            Assert.Equal(8, result); // ��֤���ؽ��

            // Assert

            calculatorMock.Verify(c => c.Add(It.IsAny<int>(), It.IsAny<int>()), Times.Once, "������һ��");

            _output.WriteLine("�������");
        }

        [Fact]
        public void Execute_ShouldCallAddMethod_ThrowNegativeValueException()
        {
            var calculatorMock = new Mock<ICalculator1>();
            calculatorMock.Setup(x => x.Add(It.Is<int>(a => a < 0), It.IsAny<int>()))
                .Throws(new ArgumentException("��������"));
            var service = new Calculator1Service(calculatorMock.Object); // ע�� Mock ����
            // Act
            var exception = Record.Exception(() => service.CalAdd(-1, 2));

            // Assert
            Assert.Null(exception);
            // Assert
            calculatorMock.Verify(c => c.Add(It.IsAny<int>(), It.IsAny<int>()), Times.Once, "������һ��");

            _output.WriteLine("�������");

        }

        [Fact]
        public void Execute_Exception()
        {
            var calculatorMock = new Mock<ICalculator1>();
            calculatorMock.Setup(x => x.Add(It.IsAny<int>(), It.IsAny<int>())).Throws(new ArgumentException("�����׳��쳣"));

            var service = new Calculator1Service(calculatorMock.Object); // ע�� Mock ����
            // Act
            var exception = Record.Exception(() => service.CalAdd(-1, 2));

            //// Assert
            //Assert.Null(exception);
            Assert.Throws<ApplicationException>(() => service.CalAdd(1, 2));
            // Assert
            //calculatorMock.Verify(c => c.Add(It.IsAny<int>(), It.IsAny<int>()), Times.Once, "������һ��");

            _output.WriteLine("�������");

        }
    }
}
