using PrismNlogSqlTest1.Services1.Interfaces;
using PrismNlogSqlTest1.Services1;
using PrismNlogSqlTest1.Services1.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Prism.Ioc;


using Microsoft.Extensions.DependencyInjection;


namespace PrismNlogSqlTest1.Nlog.Test1
{
    public class NlogUnitTest1
    {
        private ServicesClass _servicesClass;
        private Mock<IContainerRegistry> _containerRegistryMock;
        public NlogUnitTest1()
        {
            // 创建一个 Mock 的 IContainerRegistry 实例
            _containerRegistryMock = new Mock<IContainerRegistry>();

            // 初始化 ServicesClass
            _servicesClass = new ServicesClass(new ConfigurationBuilder().Build());
        }
        [Fact]
        public void NlogRepositories_Should_Be_Registered()
        {
            // Act
            _servicesClass.RegisterServices(_containerRegistryMock.Object);

            // Assert
            _containerRegistryMock.Verify(cr => cr.Register(typeof(INlogRepositories), typeof(NlogRepositories)), Times.Once);
        }
    }

    public class PrismContainerRegistry : IContainerRegistry
    {
        private readonly IServiceCollection _services;

        public PrismContainerRegistry(IServiceCollection services)
        {
            _services = services;
        }

        public void Register<TFrom, TTo>() where TFrom : class where TTo : class, TFrom
        {
            _services.AddTransient<TFrom, TTo>();
        }

        // 实现 IContainerRegistry 其他方法
        public IContainerRegistry RegisterInstance(Type type, object instance)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterInstance(Type type, object instance, string name)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterSingleton(Type from, Type to)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterSingleton(Type from, Type to, string name)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterSingleton(Type type, Func<object> factoryMethod)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterSingleton(Type type, Func<IContainerProvider, object> factoryMethod)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterManySingleton(Type type, params Type[] serviceTypes)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry Register(Type from, Type to)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry Register(Type from, Type to, string name)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry Register(Type type, Func<object> factoryMethod)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry Register(Type type, Func<IContainerProvider, object> factoryMethod)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterMany(Type type, params Type[] serviceTypes)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterScoped(Type from, Type to)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterScoped(Type type, Func<object> factoryMethod)
        {
            throw new NotImplementedException();
        }

        public IContainerRegistry RegisterScoped(Type type, Func<IContainerProvider, object> factoryMethod)
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered(Type type)
        {
            throw new NotImplementedException();
        }

        public bool IsRegistered(Type type, string name)
        {
            throw new NotImplementedException();
        }
    }




    public class NlogRepositoriesTests
    {
        private readonly Mock<INlogRepositories> _mockNlogRepositories = new();

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



}