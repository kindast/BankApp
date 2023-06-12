using ProgressBankApp.UserControls;
using ProgressBankApp.ViewModels;
using System;
using System.Windows;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            _viewModel = new LoginViewModel(this);
            DataContext = _viewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, EventArgs e)
        {
            _viewModel.Password = (sender as PasswordBox).Password;
        }
    }
}
