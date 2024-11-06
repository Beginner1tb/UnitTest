using Microsoft.Extensions.DependencyInjection;
using PrismNlogSqlTest1.Services1.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismNlogSqlTest1.Services1.Interfaces;

namespace PrismNlogSqlTest1.Nlog.Test1
{
    public class ScopedSqliteTests
    {
        private readonly ServiceProvider _serviceProvider;

        public ScopedSqliteTests()
        {
            var services = new ServiceCollection();

            // 注册 IDbConnectionFactory，并传入 SQLite 内存数据库连接字符串
            var connectionString = "Data Source=:memory:";
            services.AddScoped<IDbConnectionFactory>(provider => new SQLiteConnectionFactory(connectionString));
            services.AddScoped<ISqlRepositories, SqlRepositories>();

            // 构建服务提供者
            _serviceProvider = services.BuildServiceProvider();
        }

        [Fact]
        public void GetPriorityNum_ReturnsCorrectValue_WithScopedSQLite()
        {
            int result;
            // 创建作用域，确保数据库在当前作用域内有效
            using var scope = _serviceProvider.CreateScope();
            var connectionFactory = scope.ServiceProvider.GetRequiredService<IDbConnectionFactory>();

            // 初始化数据库并保持连接打开
            using (var conn = connectionFactory.CreateConnection())
            {
                
                InitializeDatabase(conn);

                // 获取 SqlRepositories 实例并进行查询
                var sqlRepositories = new SqlRepositories(connectionFactory);
                result = sqlRepositories.GetPriorityNum("test_user");

                Assert.Equal(1, result);
            }

           
        }

        private void InitializeDatabase(IDbConnection connection)
        {
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CREATE TABLE users (user_name TEXT, role INTEGER);";
                command.ExecuteNonQuery();

                command.CommandText = "INSERT INTO users (user_name, role) VALUES ('test_user', 1);";
                command.ExecuteNonQuery();
            }
        }
    }
}
