using BankApp.Helpers;
using BankApp.Repository;
using BankApp.Views;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankApp.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private DepositRepository _depositRepository = new DepositRepository();
        private Page _currentPage = new Page();
        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public MainViewModel()
        {
            OpenClientsPageCommand = new Command(NavigateToClients);
            OpenAccountsPageCommand = new Command(NavigateToAccounts);
            OpenProfilePageCommand = new Command(NavigateToProfile);
            OpenCalculatorPageCommand = new Command(NavigateToDeposits);
        }

        public ICommand OpenClientsPageCommand { get; set; }
        public ICommand OpenAccountsPageCommand { get; set; }
        public ICommand OpenProfilePageCommand { get; set; }
        public ICommand OpenCalculatorPageCommand { get; set; }

        private void NavigateToClients(object parameter)
        {
            CurrentPage = new ClientsPage();
        }

        private void NavigateToAccounts(object parameter)
        {
            CurrentPage = new AccountsPage();
        }

        private void NavigateToProfile(object parameter)
        {
            CurrentPage = new ProfilePage();
        }

        private void NavigateToDeposits(object parameter)
        {
            if (_depositRepository.GetDeposits(CurrentUser.Id).Count == 0)
                CurrentPage = new DepositCalculatorPage();
            else
                CurrentPage = new DepositsPage();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
