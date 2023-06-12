using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using ProgressBankApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class DepositViewModel : ViewModel
    {
        private DepositsViewModel _depositsViewModel;
        private UserRepository _userRepository = new UserRepository();
        private AccountRepository _accountRepository = new AccountRepository();
        private DepositRepository _depositRepository = new DepositRepository();

        private Deposit _deposit;
        public Deposit Deposit
        {
            get => _deposit;
            set
            {
                _deposit = value;
                OnPropertyChanged(nameof(Deposit));
            }
        }

        private ObservableCollection<BankAccount> _bankAccounts;
        public ObservableCollection<BankAccount> BankAccounts
        {
            get => _bankAccounts;
            set
            {
                _bankAccounts = value;
                OnPropertyChanged(nameof(BankAccounts));
            }
        }

        private BankAccount _selectedBankAccount;
        public BankAccount SelectedBankAccount
        {
            get => _selectedBankAccount;
            set
            {
                _selectedBankAccount = value;
                OnPropertyChanged(nameof(SelectedBankAccount));
            }
        }

        private int _months = 3;
        public int Months
        {
            get => _months;
            set
            {
                _months = value;
                OnPropertyChanged(nameof(Months));
            }
        }

        public DepositViewModel(Deposit deposit, DepositsViewModel depositsViewModel)
        {
            _depositsViewModel = depositsViewModel;
            Deposit = deposit;
            if (Deposit.BankAccount.Histories != null)
                Deposit.BankAccount.Histories = Deposit.BankAccount.Histories.OrderByDescending(h => h.DateTime).ToList();
            BankAccounts = new ObservableCollection<BankAccount>(_accountRepository.GetAccounts(CurrentUser.Id));
            SelectedBankAccount = BankAccounts.FirstOrDefault();

            MainWindow.SetTitle("Вклад " + Deposit.BankAccount.Name);
        }

        public ICommand ReplenishDepositCommand { get => new Command(ReplenishDeposit); }
        public ICommand OpenTransferWindowCommand { get => new Command(OpenTransferWindow); }
        public ICommand TransferCommand { get => new Command(Transfer); }
        public ICommand RenewDepositCommand { get => new Command(RenewDeposit); }
        public ICommand PrintDepositAgreementCommand { get => new Command(PrintDepositAgreement); }

        private void ReplenishDeposit(object parameter)
        {
            TransferWindow transferWindow = new TransferWindow(null, Deposit.BankAccount);
            transferWindow.ShowDialog();
            UpdateDeposits();
        }

        private void OpenTransferWindow(object parameter)
        {
            TransferWindow transferWindow = new TransferWindow(Deposit.BankAccount);
            transferWindow.ShowDialog();
            UpdateDeposits();
        }

        private void Transfer(object parameter)
        {
            _accountRepository.Transfer(Deposit.BankAccount, SelectedBankAccount, Deposit.BankAccount.Balance);
            _depositRepository.DeleteDeposit(Deposit);

            if (_depositRepository.GetDeposits(CurrentUser.Id).Count() == 0)
                MainWindow.Navigate(new DepositCalculatorPage());
            else
            {
                _depositsViewModel.Deposits = new ObservableCollection<Deposit>(_depositRepository.GetDeposits(CurrentUser.Id));
                _depositsViewModel.SelectedDeposit = _depositsViewModel.Deposits.FirstOrDefault();
            }
        }

        private void RenewDeposit(object parameter)
        {
            Deposit.BankAccount.DateOpen = DateTime.Now;
            Deposit.DateExpiration = DateTime.Now.AddMonths(Months);
            Deposit.Months = Months;
            Deposit.MonthsPassed = 0;
            _depositRepository.Save();
            UpdateDeposits();
        }

        private void UpdateDeposits()
        {
            int indexDeposit = _depositsViewModel.Deposits.IndexOf(_depositsViewModel.SelectedDeposit);
            _depositsViewModel.Deposits = new ObservableCollection<Deposit>(_depositRepository.GetDeposits(CurrentUser.Id));
            _depositsViewModel.SelectedDeposit = _depositsViewModel.Deposits.LastOrDefault();
        }

        private void PrintDepositAgreement(object parameter)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
                printDialog.PrintVisual(new DepositAgreement(_userRepository.GetUser(CurrentUser.Id), Deposit), "Договор");
        }
    }
}
