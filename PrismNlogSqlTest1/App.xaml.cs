﻿using System.IO;
using Prism.Ioc;
using PrismNlogSqlTest1.Views;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Prism.DryIoc;
using PrismNlogSqlTest1.Services1;

namespace PrismNlogSqlTest1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : PrismApplication
    {
        private IConfiguration _configuration;
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // 设置配置
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./Settings/database_settings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
            // 注册服务
            var serviceRegistrar = new ServicesClass(_configuration);
            serviceRegistrar.RegisterServices(containerRegistry);
        }


    }
}
