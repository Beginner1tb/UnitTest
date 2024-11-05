using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using PrismNlogSqlTest1.Services1.Interfaces;
using PrismNlogSqlTest1.Services1.Repositories;

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

            //var connectionString = _configuration["Postgresql:connectionString"];
            //containerRegistry.Register<ISqlRepositories>(provider => new SqlRepositories(connectionString));

            var connectionString = _configuration["Database:connectionString"];
            var databaseType= _configuration["Database:Type"];
            // 根据数据库类型注册相应的工厂实例
            //这里如果选错，则会在ModelView.AutoWired里报错
            if (connectionString == null)
            {
                throw new ArgumentException("connectionString 为空");
            }
            if (databaseType == "Postgres")
            {
                var postgresFactory = new PostgreSqlConnectionFactory(connectionString);
                containerRegistry.RegisterInstance<IDbConnectionFactory>(postgresFactory);
            }
            else if (databaseType == "SQLite")
            {
                var sqliteFactory = new SQLiteConnectionFactory(connectionString);
                containerRegistry.RegisterInstance<IDbConnectionFactory>(sqliteFactory);
            }

            containerRegistry.Register<ISqlRepositories, SqlRepositories>();
        }
    }
    
}
