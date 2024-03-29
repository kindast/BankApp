﻿using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using ProgressBankApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class AccountViewModel : ViewModel
    {
        private BankAccount _bankAccount;
        private AccountsViewModel _accountsViewModel;
        private BankAccountRepository _accountRepository = new BankAccountRepository();

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

            MainWindow.SetTitle("Счет " + account.Name);
        }

        public ICommand OpenTransferWindowCommand { get => new Command(OpenTransferWindow); }

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
    }
}
