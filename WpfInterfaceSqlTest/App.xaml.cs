using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfInterfaceSqlTest.Repositories;
using WpfInterfaceSqlTest.Services;

namespace WpfInterfaceSqlTest
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            var userRepositories = new UserRepositories(connectionString);
            var databaseService = new DataBaseServices(userRepositories);
            var mainWindow = new MainWindow(databaseService);


            mainWindow.Show();
        }
    }
}
