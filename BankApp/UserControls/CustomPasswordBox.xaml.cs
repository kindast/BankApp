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

namespace BankApp.UserControls
{
    /// <summary>
    /// Логика взаимодействия для PasswordBox.xaml
    /// </summary>
    public partial class CustomPasswordBox : UserControl
    {

        public CustomPasswordBox()
        {
            InitializeComponent();
            DataContext = this;
            btnShow.IsChecked = false;
            btnShow_Click(null, null);
        }

        private void btnShow_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)btnShow.IsChecked)
            {
                tbText.Text = tbPassword.Password;
                tbPassword.Visibility = Visibility.Hidden;
                tbText.Visibility = Visibility.Visible;
            }
            else
            {
                tbPassword.Password = tbText.Text;
                tbPassword.Visibility = Visibility.Visible;
                tbText.Visibility = Visibility.Hidden;
            }
        }

        public string Password { 
            get {
                if ((bool)btnShow.IsChecked)
                    return tbText.Text;
                else
                    return tbPassword.Password;
            }
            set { }
        }
    }
}
