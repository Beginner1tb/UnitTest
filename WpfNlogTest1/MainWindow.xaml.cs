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
using NLog;
using WpfNlogTest1.Repositories;
using WpfNlogTest1.Services;
using NLog.Config;

namespace WpfNlogTest1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NlogServices _nlogServices;
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public MainWindow(NlogServices nlogServices)
        {
            InitializeComponent();
           
            _nlogServices = nlogServices;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _nlogServices.LogInfo("123454");
            //_logger.Info("111111");
        }
    }
}
