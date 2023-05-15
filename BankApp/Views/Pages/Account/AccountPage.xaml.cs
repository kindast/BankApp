using BankApp.Models;
using BankApp.ViewModels;
using System.Windows.Controls;

namespace BankApp.Views
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
