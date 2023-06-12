using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using ProgressBankApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace ProgressBankApp.ViewModels
{
    public class AccountsViewModel : ViewModel
    {
        private AccountRepository _accountRepository = new AccountRepository();
        private ObservableCollection<BankAccount> _bankAccounts;
        private BankAccount _selectedBankAccount;
        private Page _currentPage;

        public ObservableCollection<BankAccount> BankAccounts
        {
            get => _bankAccounts;
            set
            {
                _bankAccounts = value;
                OnPropertyChanged(nameof(BankAccounts));
            }
        }

        public BankAccount SelectedBankAccount
        {
            get => _selectedBankAccount;
            set
            {
                _selectedBankAccount = value;
                OnPropertyChanged(nameof(SelectedBankAccount));
                if (SelectedBankAccount != null)
                    CurrentPage = new AccountPage(SelectedBankAccount, this);
            }
        }

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public AccountsViewModel()
        {
            BankAccounts = new ObservableCollection<BankAccount>(_accountRepository.GetAccounts(CurrentUser.Id));
            SelectedBankAccount = BankAccounts.FirstOrDefault();
        }
    }
}
