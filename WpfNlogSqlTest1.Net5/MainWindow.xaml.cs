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
using WpfNlogSqlTest1.Net5.Services;

namespace WpfNlogSqlTest1.Net5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SqlServices _sqlServices;
        private readonly NlogServices _nlogServices;

        public MainWindow(SqlServices sqlServices, NlogServices nlogServices)
        {
            InitializeComponent();
            _sqlServices = sqlServices;
            _nlogServices = nlogServices;
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            string username = "u6";
            int roleNum = _sqlServices.GetPriorityNum(username);
            MessageBox.Show(roleNum.ToString());
        }


        private void ButtonNlog_OnClick(object sender, RoutedEventArgs e)
        {
            _nlogServices.LogInfo("This is a Test Nlog");
        }
    }
}