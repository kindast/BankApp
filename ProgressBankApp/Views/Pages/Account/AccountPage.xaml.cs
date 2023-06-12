using ProgressBankApp.Models;
using ProgressBankApp.ViewModels;
using System.Windows.Controls;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for AccountPage.xaml
    /// </summary>
    public partial class AccountPage : Page
    {
        public AccountPage(BankAccount account, AccountsViewModel accountsViewModel)
        {
            InitializeComponent();
            DataContext = new AccountViewModel(account, accountsViewModel);
        }
    }
}
