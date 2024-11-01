using PrismNlogSqlTest1.Core1.Interfaces;
using PrismNlogSqlTest1.Core1.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;

namespace PrismNlogSqlTest1.Core1
{
    public class SerivcesClass
    {
        private readonly IConfiguration _configuration;

        public SerivcesClass(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void RegisterServices(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<INlogRepositories, NlogRepositories>();

            var connectionString = _configuration["Postgresql:connectionString"];
            containerRegistry.Register<ISqlRepositories>(provider => new SqlRepositories(connectionString));
        }
    }
}
