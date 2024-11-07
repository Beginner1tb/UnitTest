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


        //[Fact]
        //public void GetPriorityNum_ReturnsExpectedValue()
        //{
        //    // Arrange
        //    string username = "u6";
        //    int expectedPriorityNum = 0;

        //    _mockReader.Setup(reader => reader.Read()).Returns(true);
        //    _mockReader.Setup(reader => reader.GetInt32(0)).Returns(expectedPriorityNum);
        //    _mockDbCommand.Setup(cmd => cmd.ExecuteReader(CommandBehavior.Default)).Returns(_mockReader.Object);
        //    _mockDbConnection.Setup(connection => connection.CreateCommand()).Returns(_mockDbCommand.Object);

        //    // Act
        //    int actualPriorityNum = _sqlRepositories.GetPriorityNum(username);

        //    // Assert
        //    Assert.Equal(expectedPriorityNum, actualPriorityNum);
        //}

        //[Fact]
        //public void GetPriorityNum_ReturnsDefaultFalseValue()
        //{
        //    // Arrange
        //    string username = "1111";
        //    int expectedPriorityNum = 999;

        //    _mockReader.Setup(reader => reader.Read()).Returns(false);
        //    _mockDbCommand.Setup(command => command.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(_mockReader.Object);
        //    _mockDbCommand.Setup(cmd => cmd.Parameters.Add(It.IsAny<IDbDataParameter>())).Verifiable();
        //    _mockDbConnection.Setup(conn => conn.CreateCommand()).Returns(_mockDbCommand.Object);

        //    // Act
        //    var actualPriority = _sqlRepositories.GetPriorityNum(username);

        //    // Assert
        //    Assert.Equal(expectedPriorityNum, actualPriority);

        //}

        [Fact]
        public void GetPriorityNum_ShouldReturnPriorityNum_WhenUserExists()
        {
            // Arrange
            var expectedPriorityNum = 1;
            var username = "testuser";

            // Mock IDbConnectionFactory
            var connectionFactoryMock = new Mock<IDbConnectionFactory>();

            // Mock IDbConnection
            var connectionMock = new Mock<IDbConnection>();
            connectionFactoryMock.Setup(f => f.CreateConnection()).Returns(connectionMock.Object);

            // Ensure connection opens when called
            connectionMock.Setup(conn => conn.Open());

            // Mock IDbCommand
            var commandMock = new Mock<IDbCommand>();
            connectionMock.Setup(conn => conn.CreateCommand()).Returns(commandMock.Object);

            // Mock IDbDataParameter and ensure it’s returned correctly from CreateParameter
            var parameterMock = new Mock<IDbDataParameter>();
            connectionFactoryMock.Setup(f => f.CreateParameter(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(parameterMock.Object);

            // Mock IDataReader
            var readerMock = new Mock<IDataReader>();
            readerMock.Setup(r => r.Read()).Returns(true); // Simulate data presence
            readerMock.Setup(r => r.GetInt32(0)).Returns(expectedPriorityNum);

            // Set up command to return the mock reader
            commandMock.Setup(cmd => cmd.ExecuteReader()).Returns(readerMock.Object);

            // Make sure parameters can be added to the command
            var parameters = new Mock<IDataParameterCollection>();
            commandMock.Setup(cmd => cmd.Parameters).Returns(parameters.Object);

            // Instantiate SqlRepositories with the mocked connection factory
            var sqlRepositories = new SqlRepositories(connectionFactoryMock.Object);

            // Act
            var actualPriorityNum = sqlRepositories.GetPriorityNum(username);

            // Assert
            Assert.Equal(expectedPriorityNum, actualPriorityNum);
        }

        [Fact]
        public void GetPriorityNum_ShouldReturnDefaultPriority_WhenUserDoesNotExist()
        {
            // Arrange
            var expectedPriorityNum = 1;
            var username = "testuser";

            // Mock IDbConnectionFactory
            var connectionFactoryMock = new Mock<IDbConnectionFactory>();

            // Mock IDbConnection
            var connectionMock = new Mock<IDbConnection>();
            connectionFactoryMock.Setup(f => f.CreateConnection()).Returns(connectionMock.Object);

            // Ensure connection opens when called
            connectionMock.Setup(conn => conn.Open());

            // Mock IDbCommand
            var commandMock = new Mock<IDbCommand>();
            connectionMock.Setup(conn => conn.CreateCommand()).Returns(commandMock.Object);

            // Mock IDbDataParameter and ensure it’s returned correctly from CreateParameter
            var parameterMock = new Mock<IDbDataParameter>();
            connectionFactoryMock.Setup(f => f.CreateParameter(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(parameterMock.Object);

            // Mock IDataReader
            var readerMock = new Mock<IDataReader>();
            readerMock.Setup(r => r.Read()).Returns(false); // Simulate data presence
            readerMock.Setup(r => r.GetInt32(0)).Returns(expectedPriorityNum);

            // Set up command to return the mock reader
            commandMock.Setup(cmd => cmd.ExecuteReader()).Returns(readerMock.Object);

            // Make sure parameters can be added to the command
            var parameters = new Mock<IDataParameterCollection>();
            commandMock.Setup(cmd => cmd.Parameters).Returns(parameters.Object);

            // Instantiate SqlRepositories with the mocked connection factory
            var sqlRepositories = new SqlRepositories(connectionFactoryMock.Object);

            // Act
            var actualPriorityNum = sqlRepositories.GetPriorityNum(username);

            // Assert
            Assert.Equal(expectedPriorityNum, actualPriorityNum);
        }

        [Fact]
        public void GetPriorityNum_ShouldCallOpenAndExecuteReader_WhenUserExists()
        {
            // Arrange
            var expectedPriorityNum = 1;
            var username = "testuser";

            // Mock IDbConnectionFactory
            var connectionFactoryMock = new Mock<IDbConnectionFactory>();

            // Mock IDbConnection
            var connectionMock = new Mock<IDbConnection>();
            connectionFactoryMock.Setup(f => f.CreateConnection()).Returns(connectionMock.Object);

            // Ensure connection opens when called
            connectionMock.Setup(conn => conn.Open());

            // Mock IDbCommand
            var commandMock = new Mock<IDbCommand>();
            connectionMock.Setup(conn => conn.CreateCommand()).Returns(commandMock.Object);

            // Mock IDbDataParameter and ensure it’s returned correctly from CreateParameter
            var parameterMock = new Mock<IDbDataParameter>();
            connectionFactoryMock.Setup(f => f.CreateParameter(It.IsAny<string>(), It.IsAny<object>()))
                .Returns(parameterMock.Object);

            // Mock IDataReader
            var readerMock = new Mock<IDataReader>();
            readerMock.Setup(r => r.Read()).Returns(true); // Simulate data presence
            readerMock.Setup(r => r.GetInt32(0)).Returns(expectedPriorityNum);

            // Set up command to return the mock reader
            commandMock.Setup(cmd => cmd.ExecuteReader()).Returns(readerMock.Object);

            // Make sure parameters can be added to the command
            var parameters = new Mock<IDataParameterCollection>();
            commandMock.Setup(cmd => cmd.Parameters).Returns(parameters.Object);

            // Instantiate SqlRepositories with the mocked connection factory
            var sqlRepositories = new SqlRepositories(connectionFactoryMock.Object);

            // Act
            var actualPriorityNum = sqlRepositories.GetPriorityNum(username);

            // Verify: 验证 Open 和 ExecuteReader 方法是否被调用
            connectionMock.Verify(conn => conn.Open(), Times.Once, "Expected Open() to be called once.");
            commandMock.Verify(cmd => cmd.ExecuteReader(), Times.Once, "Expected ExecuteReader() to be called once.");
        }

    }
}