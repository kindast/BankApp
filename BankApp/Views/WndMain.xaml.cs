using BankApp.Pages;
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
using BankApp.Model;

namespace BankApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class WndMain : Window
    {
        public Bank bank = new Bank();

        public WndMain()
        {
            InitializeComponent();
            PgLogin login = new PgLogin();
            login.parentWindow = this;
            login.bank = bank;
            frame.Navigate(login);
        }
    }
}
