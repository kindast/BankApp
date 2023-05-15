using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using BankApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace BankApp.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private BankAccount _bankAccount;
        private AccountsViewModel _accountsViewModel;
        private AccountRepository _accountRepository = new AccountRepository();

        public BankAccount BankAccount
        {
            get => _bankAccount;
            set
            {
                _bankAccount = value;
                OnPropertyChanged(nameof(BankAccount));
            }
        }

        public AccountViewModel(BankAccount account, AccountsViewModel accountsViewModel)
        {
            BankAccount = account;
            BankAccount.Histories = BankAccount.Histories.OrderByDescending(h => h.DateTime).ToList();
            _accountsViewModel = accountsViewModel;
            OpenTransferWindowCommand = new Command(OpenTransferWindow);
        }

        public ICommand OpenTransferWindowCommand { get; set; }

        private void OpenTransferWindow(object paremeter)
        {
            if (_accountsViewModel.BankAccounts.Count < 2)
            {
                DialogWindow.Show("Для перевода между счетами, у вас должно быть открыто хотя бы 2 счета.");
                return;
            }
            TransferWindow transferWindow = new TransferWindow(BankAccount);
            transferWindow.ShowDialog();
            _accountsViewModel.BankAccounts = new ObservableCollection<BankAccount>(_accountRepository.GetAccounts(CurrentUser.Id));
            _accountsViewModel.CurrentPage = new AccountPage(_accountRepository.GetAccount(BankAccount.Number), _accountsViewModel);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
