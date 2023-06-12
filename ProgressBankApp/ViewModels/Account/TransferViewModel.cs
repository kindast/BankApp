using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class TransferViewModel : ViewModel
    {
        private Window _transferWindow;
        private BankAccountRepository _accountRepository = new BankAccountRepository();
        private DepositRepository _depositRepository = new DepositRepository();
        private List<BankAccount> _bankAccounts;
        private List<Deposit> _deposits;
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
                var accounts = value;
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
                BankAccountsTo = new ObservableCollection<BankAccount>(_bankAccounts.Where(a => a.Number != BankAccountFrom.Number)
                    .Concat(_deposits.Where(d => d.DepositRate.IsWithdrawal || d.DepositRate.IsReplenishment).Select(d => d.BankAccount).ToList()));
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

        public TransferViewModel(Window transferWindow, BankAccount bankAccountFrom, BankAccount bankAccountTo = null)
        {
            _transferWindow = transferWindow;
            _deposits = _depositRepository.GetDeposits(CurrentUser.Id).Where(d => d.DepositRate.IsReplenishment || d.DepositRate.IsWithdrawal).ToList();
            _bankAccounts = (List<BankAccount>)_accountRepository.GetAccounts(CurrentUser.Id);
            _bankAccountsFrom = new ObservableCollection<BankAccount>(_bankAccounts.Concat(_deposits.Where(d => d.DepositRate.IsWithdrawal).Select(d => d.BankAccount).ToList()));

            BankAccountFrom = bankAccountFrom == null ? BankAccountsFrom.FirstOrDefault() : bankAccountFrom;
            BankAccountTo = bankAccountTo == null ? BankAccountsTo.FirstOrDefault() : bankAccountTo;
        }

        public ICommand TransferCommand { get => new Command(Transfer, CanTransfer); }

        private void Transfer(object parameter)
        {
            if (_accountRepository.Transfer(BankAccountFrom, BankAccountTo, Amount))
                _transferWindow.Close();
        }

        private bool CanTransfer(object parameter)
        {
            if (Amount <= 0 || BankAccountFrom == null || BankAccountTo == null)
                return false;

            return true;
        }
    }
}
