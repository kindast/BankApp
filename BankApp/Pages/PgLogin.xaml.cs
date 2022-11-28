using BankApp.Model;
using BankApp.Views;
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

namespace BankApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PgLogin.xaml
    /// </summary>
    public partial class PgLogin : Page
    {
        public WndMain parentWindow;
        public Bank bank;

        public PgLogin()
        {
            InitializeComponent();
        }

        private void ShowMessageBox(string message)
        {
            WndMessageBox messageBox = new WndMessageBox();
            messageBox.Message = message;
            messageBox.ShowDialog();
        }

        private void OpenRegisterPage(object sender, MouseButtonEventArgs e)
        {
            PgRegister register = new PgRegister();
            register.parentWindow = parentWindow;
            register.bank = bank;
            parentWindow.frame.Navigate(register);
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            if (tbLogin.Text == String.Empty || tbPassword.Password == String.Empty)
            {
                ShowMessageBox("Заполните все поля");
                return;
            }
            try
            {
                bank.Login(tbLogin.Text, tbPassword.Password);
                PgMain main = new PgMain();
                main.parentWindow = parentWindow;
                main.bank = bank;
                parentWindow.frame.Navigate(main);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }
    }
}
