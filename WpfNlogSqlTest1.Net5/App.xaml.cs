using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfNlogSqlTest1.Net5.Repositories;
using WpfNlogSqlTest1.Net5.Services;

namespace WpfNlogSqlTest1.Net5
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
            
            var SqlRepositories = new SqlRepositories(connectionString);
            var sqlServices = new SqlServices(SqlRepositories);
            
            var nlogRepositories = new NlogRepositories();
            var nlogServices = new NlogServices(nlogRepositories);
            
            var mainWindow = new MainWindow(sqlServices, nlogServices);
            mainWindow.Show();
            
        }
    }
}
