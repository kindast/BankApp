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
    /// Логика взаимодействия для PgRegister.xaml
    /// </summary>
    public partial class PgRegister : Page
    {
        public WndMain parentWindow;
        public Bank bank;

        public PgRegister()
        {
            InitializeComponent();
        }

        private void ShowMessageBox(string message)
        {
            WndMessageBox messageBox = new WndMessageBox();
            messageBox.Message = message;
            messageBox.ShowDialog();
        }

        private void OpenLoginPage(object sender, MouseButtonEventArgs e)
        {
            PgLogin login = new PgLogin();
            login.parentWindow = parentWindow;
            login.bank = bank;
            parentWindow.frame.Navigate(login);
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            if (tbLastName.Text == "" || tbFirstName.Text == "" || tbMiddleName.Text == "" 
                || tbLogin.Text == "" || tbPassword.Password == "" || tbPasswordRepeat.Password == "")
            {
                ShowMessageBox("Заполните все поля");
                return;
            }
            if (tbPassword.Password != tbPasswordRepeat.Password)
            {
                ShowMessageBox("Пароли не совпадают");
                return;
            }
            if (tbPassword.Password.Length < 8)
            {
                ShowMessageBox("Пароль должен состоять хотя бы из 8 символов");
                return;
            }
            bank.Register(tbLastName.Text, tbFirstName.Text, tbMiddleName.Text, tbLogin.Text, tbPassword.Password);
            OpenLoginPage(null, null);
        }

        private void tbLastName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!char.IsLetter(c))
                    e.Handled = true;
            }
        }

        private void tbLastName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}
