using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DllSqlTest1.Interfaces;
using DllSqlTest1.Repositories;

namespace WpfDllTest2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            string connectionString = "Host=localhost;Username=postgres;Password=613;Database=CoinCodeTest2;";
            var SqlRepositories = new SqlRepositories(connectionString);
            var mainWindow = new MainWindow(SqlRepositories);
            mainWindow.Show();
        }
    }
}