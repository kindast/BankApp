using ProgressBankApp.ViewModels;
using System.Windows.Controls;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        private ProfileViewModel _viewModel;

        public ProfilePage()
        {
            InitializeComponent();
            _viewModel = new ProfileViewModel(chart);
            DataContext = _viewModel;
        }

        private void pbOldPassword_PasswordChanged(object sender, System.EventArgs e)
        {
            _viewModel.OldPassword = pbOldPassword.Password;
        }

        private void pbNewPassword_PasswordChanged(object sender, System.EventArgs e)
        {
            _viewModel.NewPassword = pbNewPassword.Password;
        }

        private void pbNewPasswordConfirm_PasswordChanged(object sender, System.EventArgs e)
        {
            _viewModel.NewPasswordConfirm = pbNewPasswordConfirm.Password;
        }
    }
}
