using BankApp.Models;
using BankApp.Repository;
using System.ComponentModel;

namespace BankApp.ViewModels
{
    public class DepositViewModel : INotifyPropertyChanged
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
            //BankAccount.Histories = BankAccount.Histories.OrderByDescending(h => h.DateTime).ToList();
            _depositsViewModel = depositsViewModel;
            //OpenTransferWindowCommand = new Command(OpenTransferWindow);
        }

        //public ICommand OpenTransferWindowCommand { get; set; }

        //private void OpenTransferWindow(object paremeter)
        //{
        //    if (_accountsViewModel.BankAccounts.Count < 2)
        //    {
        //        DialogWindow.Show("Для перевода между счетами, у вас должно быть открыто хотя бы 2 счета.");
        //        return;
        //    }
        //    TransferWindow transferWindow = new TransferWindow(Deposit.Account);
        //    transferWindow.ShowDialog();
        //    _accountsViewModel.BankAccounts = new ObservableCollection<BankAccount>(_accountRepository.GetAccounts(CurrentUser.Id));
        //    _accountsViewModel.CurrentPage = new AccountPage(_accountRepository.GetAccount(BankAccount.Number), _accountsViewModel);
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
