using System.Data;
using PrismNlogSqlTest1.Services1.Interfaces;
using PrismNlogSqlTest1.Services1;
using PrismNlogSqlTest1.Services1.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Prism.Ioc;


using Microsoft.Extensions.DependencyInjection;
using Npgsql;


namespace PrismNlogSqlTest1.Nlog.Test1
{
    public class NlogUnitTest1
    {
        //传统构造函数对初始化的写法
        private readonly ServicesClass _servicesClass;
        private readonly Mock<IContainerRegistry> _containerRegistryMock;
        public NlogUnitTest1()
        {
            // 创建一个 Mock 的 IContainerRegistry 实例
            _containerRegistryMock = new Mock<IContainerRegistry>();
        
            // 初始化 ServicesClass
            _servicesClass = new ServicesClass(new ConfigurationBuilder().Build());
        }
                
        //现代C#9之后的写法
        //private readonly ServicesClass _servicesClass = new(new ConfigurationBuilder().Build());
        //private readonly Mock<IContainerRegistry> _containerRegistryMock = new();
       
        [Fact]
        public void NlogRepositories_Should_Be_Registered()
        {
            // Act
            _servicesClass.RegisterServices(_containerRegistryMock.Object);

            // Assert
            _containerRegistryMock.Verify(cr => cr.Register(typeof(INlogRepositories), typeof(NlogRepositories)), Times.Once);
        }
    }
    public class NlogRepositoriesTests
    {
        //传统构造函数对初始化的写法
        private readonly Mock<INlogRepositories> _mockNlogRepositories;
        public NlogRepositoriesTests()
        {
            _mockNlogRepositories = new Mock<INlogRepositories>();
        }
        
        //现代C#9之后的写法
        //private readonly Mock<INlogRepositories> _mockNlogRepositories = new();
        
        [Fact]
        public void LogInfo_CallsLoggerWithCorrectMessage()
        {
            // Arrange
            string testMessage = "Test log message";

            // Act
            _mockNlogRepositories.Object.LogInfo(testMessage);

            // Assert
            _mockNlogRepositories.Verify(m => m.LogInfo(testMessage), Times.Once);
        }
    }

    public class SqlRepositoriesTests
    {
        private Mock<IDbCommand> _mockDbCommand;
        private Mock<IDbConnection> _mockDbConnection;
        private Mock<IDataReader> _mockReader;
        private SqlRepositories _sqlRepositories;

        public SqlRepositoriesTests()
        {
            _mockDbConnection = new Mock<IDbConnection>();
            _mockDbCommand = new Mock<IDbCommand>();
            _mockReader = new Mock<IDataReader>();
            string connectionString = "Host=localhost;Username=postgres;Password=613;Database=CoinCodeTest2;";
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();

            _sqlRepositories = new SqlRepositories(mockConnectionFactory.Object);
        }


        [Fact]
        public void GetPriorityNum_ReturnsExpectedValue()
        {
            // Arrange
            string username = "u6";
            int expectedPriorityNum = 0;

            _mockReader.Setup(reader => reader.Read()).Returns(true);
            _mockReader.Setup(reader => reader.GetInt32(0)).Returns(expectedPriorityNum);
            _mockDbCommand.Setup(cmd => cmd.ExecuteReader(CommandBehavior.Default)).Returns(_mockReader.Object);
            _mockDbConnection.Setup(connection => connection.CreateCommand()).Returns(_mockDbCommand.Object);

            // Act
            int actualPriorityNum = _sqlRepositories.GetPriorityNum(username);

            // Assert
            Assert.Equal(expectedPriorityNum, actualPriorityNum);
        }

        [Fact]
        public void GetPriorityNum_ReturnsDefaultFalseValue()
        {
            // Arrange
            string username = "1111";
            int expectedPriorityNum = 999;

            _mockReader.Setup(reader => reader.Read()).Returns(false);
            _mockDbCommand.Setup(command => command.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(_mockReader.Object);
            _mockDbCommand.Setup(cmd => cmd.Parameters.Add(It.IsAny<IDbDataParameter>())).Verifiable();
            _mockDbConnection.Setup(conn => conn.CreateCommand()).Returns(_mockDbCommand.Object);

            // Act
            var actualPriority = _sqlRepositories.GetPriorityNum(username);

            // Assert
            Assert.Equal(expectedPriorityNum, actualPriority);

        }

    }
}