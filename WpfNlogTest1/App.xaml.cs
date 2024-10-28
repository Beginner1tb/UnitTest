using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfNlogTest1.Interfaces;
using WpfNlogTest1.Repositories;
using WpfNlogTest1.Services;

namespace WpfNlogTest1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var userRepositories = new UserRepositories();
            var nlogServices = new NlogServices(userRepositories);
            var mainWindow = new MainWindow(nlogServices);


            mainWindow.Show();
        }
    }
}
