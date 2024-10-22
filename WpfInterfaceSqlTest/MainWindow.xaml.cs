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
using WpfInterfaceSqlTest.Services;
using WpfInterfaceSqlTest.Interfaces;
using WpfInterfaceSqlTest.Models;

namespace WpfInterfaceSqlTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IUserRepositories _userRepositories;
        private readonly DataBaseServices _databaseServices;
        public MainWindow(DataBaseServices dataBaseServices)
        {
            _databaseServices = dataBaseServices;
            InitializeComponent();
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            User user = new User() { Id = new Guid(), Password = null, Username = "u6", CreateTime = DateTime.Now, Role = 2 };
            int roleNum = 999;
            roleNum = _databaseServices.GetPriorityNum(user.Username);
            MessageBox.Show(roleNum.ToString());
        }
    }
}
