using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using DllNlogTest1.Interfaces;
using DllNlogTest1.Repositories;
using DllSqlTest1.Interfaces;
using DllSqlTest1.Repositories;
using Microsoft.Extensions.Configuration;
using Prism.Ioc;

namespace PrismNlogSqlTest1.Services
{
    public static class ServiceRegistrar
    {
        public static void RegisterServices(IContainerRegistry containerRegistry, IConfiguration configuration)
        {
            containerRegistry.Register<INlogRepositories, NlogRepositories>();

            var connectionString = configuration["Postgresql:connectionString"];
            containerRegistry.Register<ISqlRepositories>(provider => new SqlRepositories(connectionString));
        }

        //public static void RegisterTestServices(IServiceCollection services)
        //{
        //    // 注册 Mock 日志模块
        //    services.AddSingleton<INlogRepositories, MockNlogRepositories>();

        //    // 注册 Mock 数据库模块
        //    services.AddSingleton<ISqlRepositories, MockSqlRepositories>();

        //    // 其他测试服务模块的注册
        //    // services.AddSingleton<IOtherService, MockOtherService>();
        //}
    }
}
