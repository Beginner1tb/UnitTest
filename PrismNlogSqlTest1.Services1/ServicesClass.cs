using PrismNlogSqlTest1.Services1.Interfaces;
using PrismNlogSqlTest1.Services1.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Prism.Ioc;

namespace PrismNlogSqlTest1.Services1
{
    public class ServicesClass
    {
        private readonly IConfiguration _configuration;

        public ServicesClass(IConfiguration configuration)
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
