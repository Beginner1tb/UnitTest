using PrismNlogSqlTest1.Services1.Interfaces;
using PrismNlogSqlTest1.Services1;
using PrismNlogSqlTest1.Services1.Repositories;
using Microsoft.Extensions.Configuration;
using Moq;
using Prism.Ioc;
namespace PrismNlogSqlTest1.Nlog.Test1
{
    public class NlogUnitTest1
    {


        [Fact]
        public void RegisterServices_ShouldRegisterINlogRepositories()
        {
            //var containerRegistryMock = new Mock<IContainerRegistry>();

            //var configurationMock=new Mock<IConfiguration>();

            //var servicesClass=new ServicesClass(configurationMock.Object);

            //servicesClass.RegisterServices(containerRegistryMock.Object);

            //containerRegistryMock.Verify(cr=>cr.Register<INlogRepositories,NlogRepositories>(),Times.Once);

            // 创建 TestContainerRegistry 实例
            var containerRegistry = new TestContainerRegistry();

            // 创建 IConfiguration 的 Mock 对象（如有需要）
            var configurationMock = new Mock<IConfiguration>();

            // 实例化 ServicesClass
            var servicesClass = new ServicesClass(configurationMock.Object);

            // 调用 RegisterServices 注册方法
            servicesClass.RegisterServices(containerRegistry);

            // 解析 INlogRepositories，验证是否注册成功
            var resolvedRepo = containerRegistry.Resolve<INlogRepositories>();

            // 验证解析出的对象是 NlogRepositories 类型
            Assert.IsType<NlogRepositories>(resolvedRepo);
        }
    }


    public class NlogRepositoriesTests
    {
        private readonly Mock<INlogRepositories> _mockNlogRepositories;

        public NlogRepositoriesTests()
        {
            _mockNlogRepositories = new Mock<INlogRepositories>();
        }

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


    public class TestContainerRegistry : IContainerRegistry
    {
        private readonly Dictionary<Type, Func<object>> _registrations = new Dictionary<Type, Func<object>>();

        public void Register<TFrom, TTo>() where TTo : TFrom
        {
            _registrations[typeof(TFrom)] = () => Activator.CreateInstance(typeof(TTo));
        }

        public IContainerRegistry Register<T>(Func<IContainerProvider, object> factoryMethod)
        {
            _registrations[typeof(T)] = () => factoryMethod(null);
            return this;
        }

        public T Resolve<T>()
        {
            if (_registrations.ContainsKey(typeof(T)))
            {
                return (T)_registrations[typeof(T)]();
            }

            throw new InvalidOperationException($"Type {typeof(T).Name} not registered.");
        }

        // 其他 IContainerRegistry 方法的空实现
        public IContainerRegistry Register(Type from, Type to) => this;
        public IContainerRegistry RegisterInstance(Type type, object instance) => this;
        public IContainerRegistry RegisterInstance<T>(T instance) => this;
        public IContainerRegistry RegisterSingleton(Type from, Type to) => this;
        public IContainerRegistry RegisterSingleton<TFrom, TTo>() where TTo : TFrom => this;
        public IContainerRegistry RegisterSingleton(Type type) => this;
        public IContainerRegistry RegisterSingleton<T>() => this;
        public IContainerRegistry Register(Type from, Type to, string name) => this;
        public IContainerRegistry Register(Type type) => this;
        public IContainerRegistry Register<T>() => this;
        public IContainerRegistry Register(Type type, string name) => this;
        public IContainerRegistry Register<T>(string name) => this;
        public bool IsRegistered(Type type) => _registrations.ContainsKey(type);
        public bool IsRegistered(Type type, string name) => false;
        public bool IsRegistered<T>() => _registrations.ContainsKey(typeof(T));
        public bool IsRegistered<T>(string name) => false;

        public IContainerRegistry RegisterInstance(Type type, object instance, string name)
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
    }
}