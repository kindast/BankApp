using ProgressBankApp.Models;
using ProgressBankApp.ViewModels;
using System.Windows.Controls;

namespace ProgressBankApp.Views
{
    public partial class ProfileEditPage : Page
    {
        private ProfileEditViewModel _viewModel;

        public ProfileEditPage(int userId)
        {
            InitializeComponent();
            _viewModel = new ProfileEditViewModel(userId);
            DataContext = _viewModel;
        }

        private void btnDeleteAccount_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.DeleteBankAccount((sender as Button).DataContext as BankAccount);
        }

        private void btnDeleteDeposit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            _viewModel.DeleteDeposit((sender as Button).DataContext as Deposit);
        }
    }
}
