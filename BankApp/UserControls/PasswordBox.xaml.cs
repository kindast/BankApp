using System;
using System.Windows;
using System.Windows.Controls;

namespace BankApp.UserControls
{
    /// <summary>
    /// Interaction logic for PasswordBox.xaml
    /// </summary>
    public partial class PasswordBox : UserControl
    {
        public PasswordBox()
        {
            InitializeComponent();
            DataContext = this;
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

        public string Password
        {
            get
            {
                if ((bool)btnShow.IsChecked)
                    return tbText.Text;
                else
                    return tbPassword.Password;
            }
        }

        public event EventHandler PasswordChanged;

        private void OnPasswordChanged()
        {
            PasswordChanged?.Invoke(this, EventArgs.Empty);
        }

        private void tbText_TextChanged(object sender, TextChangedEventArgs e)
        {
            OnPasswordChanged();
        }

        private void tbPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            OnPasswordChanged();
        }
    }
}
