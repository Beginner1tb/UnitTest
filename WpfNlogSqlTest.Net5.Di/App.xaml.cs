using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WpfNlogSqlTest.Net5.Di.Services;


namespace WpfNlogSqlTest.Net5.Di
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }
        private  IConfiguration _configurationSql;
        public App()
        {
           
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./Settings/appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();

            // 将配置存储到Application.Properties
            this.Properties["Configuration"] = configuration;

            var builderSql= new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("./Settings/postgres_settings.json", optional: false, reloadOnChange: true);

            _configurationSql=builderSql.Build();


            var serviceCollection = new ServiceCollection();
            
            
            ConfigureServices(serviceCollection);
            
            ServiceProvider = serviceCollection.BuildServiceProvider();
            
             var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            // 注册 MainWindow
            services.AddTransient<MainWindow>();

            // 通过 ServiceRegistrar 注册所有服务模块，传递 IConfiguration
            ServiceRegistrar.RegisterServices(services, _configurationSql);
        }
    }
}