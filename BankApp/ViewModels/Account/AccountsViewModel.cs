using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using BankApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;

namespace BankApp.ViewModels
{
    public class AccountsViewModel : INotifyPropertyChanged
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
                CurrentPage = new AccountPage(_selectedBankAccount, this);
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
