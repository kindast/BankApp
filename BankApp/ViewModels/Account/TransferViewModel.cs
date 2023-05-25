using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace BankApp.ViewModels
{
    public class TransferViewModel : ViewModel
    {
        private Window _transferWindow;
        private AccountRepository _accountRepository = new AccountRepository();
        private ICollection<BankAccount> _bankAccounts;
        private ObservableCollection<BankAccount> _bankAccountsFrom;
        private ObservableCollection<BankAccount> _bankAccountsTo;
        private BankAccount _bankAccountFrom;
        private BankAccount _bankAccountTo;
        private decimal _amount;

        public ObservableCollection<BankAccount> BankAccountsFrom
        {
            get => _bankAccountsFrom;
            set
            {
                _bankAccountsFrom = value;
                OnPropertyChanged(nameof(BankAccountsFrom));
            }
        }

        public BankAccount BankAccountFrom
        {
            get => _bankAccountFrom;
            set
            {
                _bankAccountFrom = value;
                OnPropertyChanged(nameof(BankAccountFrom));
                BankAccountsTo = new ObservableCollection<BankAccount>(_bankAccounts.Where(a => a.Number != BankAccountFrom.Number).ToList());
                BankAccountTo = BankAccountsTo.FirstOrDefault();
            }
        }

        public ObservableCollection<BankAccount> BankAccountsTo
        {
            get => _bankAccountsTo;
            set
            {
                _bankAccountsTo = value;
                OnPropertyChanged(nameof(BankAccountsTo));
            }
        }

        public BankAccount BankAccountTo
        {
            get => _bankAccountTo;
            set
            {
                _bankAccountTo = value;
                OnPropertyChanged(nameof(BankAccountTo));
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
            }
        }

        [Range(0.01, 99999999.99)]
        [RegularExpression(@"^\d{1,8}(\.\d{0,2})?₽")]
        public string AmountString
        {
            get { return _amount.ToString("F2"); }
            set
            {
                decimal result;
                if (decimal.TryParse(value, out result))
                {
                    Amount = result;
                }
            }
        }

        public TransferViewModel(Window transferWindow, BankAccount bankAccountFrom)
        {
            _transferWindow = transferWindow;
            _bankAccounts = _accountRepository.GetAccounts(CurrentUser.Id);
            _bankAccountsFrom = new ObservableCollection<BankAccount>(_bankAccounts);
            BankAccountFrom = bankAccountFrom;
            BankAccountTo = BankAccountsTo.FirstOrDefault();
        }

        public ICommand TransferCommand { get => new Command(Transfer); }

        private void Transfer(object parameter)
        {
            if (_accountRepository.Transfer(BankAccountFrom, BankAccountTo, Amount))
                _transferWindow.Close();
        }
    }
}
