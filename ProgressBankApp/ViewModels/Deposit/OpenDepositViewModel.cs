using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class OpenDepositViewModel : ViewModel
    {
        private BankAccountRepository _accountRepository = new BankAccountRepository();
        private DepositRepository _depositRepository = new DepositRepository();

        private ObservableCollection<BankAccount> _bankAccounts;
        private BankAccount _selectedBankAccount;

        private DepositRate _depositRate;
        private int _months;
        private decimal _amount;

        private Window _window;

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
                if (Amount > _selectedBankAccount.Balance)
                    Amount = _selectedBankAccount.Balance;
            }
        }

        public DepositRate DepositRate
        {
            get => _depositRate;
            set
            {
                _depositRate = value;
                OnPropertyChanged(nameof(DepositRate));
            }
        }

        public int Months
        {
            get => _months;
            set
            {
                _months = value;
                OnPropertyChanged(nameof(Months));
                UpdateDepositRate();
            }
        }

        public decimal Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                OnPropertyChanged(nameof(Amount));
                UpdateDepositRate();
            }
        }

        public OpenDepositViewModel(DepositRate depositRate, int months, decimal amount, Window window)
        {
            BankAccounts = new ObservableCollection<BankAccount>(_accountRepository.GetAccounts(CurrentUser.Id).Where(a => a.Balance >= 50000).ToList());
            SelectedBankAccount = BankAccounts.FirstOrDefault();
            DepositRate = depositRate;
            Months = months;
            if (amount > SelectedBankAccount.Balance)
                Amount = SelectedBankAccount.Balance;
            else
                Amount = amount;
            _window = window;
        }

        private void UpdateDepositRate()
        {
            DepositRate.Period = Months;
            DepositRate.Amount = Amount;
            OnPropertyChanged(nameof(DepositRate));
        }

        public ICommand OpenDepositCommand { get => new Command(OpenDeposit); }

        private void OpenDeposit(object parameter)
        {
            _depositRepository.CreateDeposit(SelectedBankAccount.Number, DepositRate.Id, CurrentUser.Id, Months, Amount);
            _window.Close();
        }
    }
}
