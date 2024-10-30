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
using Npgsql;
using DllSqlTest1.Interfaces;
using DllSqlTest1.Repositories;

namespace WpfDllTest2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ISqlRepositories _sqlRepositories;
        public MainWindow(ISqlRepositories sqlRepositories)
        {
            InitializeComponent();
            _sqlRepositories = sqlRepositories;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = "u6";
            int priorityNum = _sqlRepositories.GetPriorityNum(username);
            MessageBox.Show(priorityNum.ToString());
        }
    }
}