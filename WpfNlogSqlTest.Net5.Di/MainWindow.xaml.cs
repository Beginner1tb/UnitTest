using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DllNlogTest1.Interfaces;
using DllSqlTest1.Interfaces;
using NLog;

namespace WpfNlogSqlTest.Net5.Di
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IConfiguration _configuration;
        private readonly INlogRepositories _nlogRepositories;
        private readonly ISqlRepositories _sqlRepositories;
        public MainWindow(INlogRepositories nlogRepositories, ISqlRepositories sqlRepositories)
        {
            InitializeComponent();
            _nlogRepositories = nlogRepositories;
            _sqlRepositories = sqlRepositories;
            //_configuration = (IConfiguration)Application.Current.Properties["Configuration"];
            //string title = _configuration["应用程序设置1111:标题"];

            //MessageBox.Show(title);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _nlogRepositories.LogInfo("MainWindow initialized.");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string username = "u6";
            int priorityNum = _sqlRepositories.GetPriorityNum(username);
            _nlogRepositories.LogInfo($"Priority number for user {username} is {priorityNum}.");
        }
    }
}