using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using BankApp.Views;
using System.Windows.Input;

namespace BankApp.ViewModels
{
    public class DepositViewModel : ViewModel
    {
        private Deposit _deposit;
        private DepositsViewModel _depositsViewModel;
        private AccountRepository _accountRepository = new AccountRepository();

        public Deposit Deposit
        {
            get => _deposit;
            set
            {
                _deposit = value;
                OnPropertyChanged(nameof(Deposit));
            }
        }

        public DepositViewModel(Deposit deposit, DepositsViewModel depositsViewModel)
        {
            Deposit = deposit;
            _depositsViewModel = depositsViewModel;
        }

        public ICommand ReplenishDepositCommand { get => new Command(ReplenishDeposit); }
        public ICommand TransferCommand { get => new Command(ReplenishDeposit); }

        private void ReplenishDeposit(object parameter)
        {
            DialogWindow.ShowDevelopmentMessage();
        }

        private void Transfer(object parameter)
        {
            DialogWindow.ShowDevelopmentMessage();
        }
    }
}
