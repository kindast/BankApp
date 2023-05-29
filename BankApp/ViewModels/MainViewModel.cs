using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using BankApp.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankApp.ViewModels
{
    public class MainViewModel : ViewModel
    {
        private DepositRepository _depositRepository = new DepositRepository();
        private UserRepository _userRepository = new UserRepository();
        private Window _mainWindow;

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

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

        public MainViewModel(Window mainWindow)
        {
            User = _userRepository.GetUser(CurrentUser.Id);
            _mainWindow = mainWindow;
            if (User.Role == Role.Client)
                CurrentPage = new AccountsPage();
            else
                CurrentPage = new ClientsPage();
        }

        public ICommand OpenClientsPageCommand { get => new Command((p) => { CurrentPage = new ClientsPage(); }); }
        public ICommand OpenAccountsPageCommand { get => new Command((p) => { CurrentPage = new AccountsPage(); }); }
        public ICommand OpenProfilePageCommand { get => new Command((p) => { CurrentPage = new ProfilePage(); }); }
        public ICommand OpenDepositsPageCommand { get => new Command((p) => 
        {
            if (_depositRepository.GetDeposits(CurrentUser.Id).Count == 0)
                CurrentPage = new DepositCalculatorPage();
            else
                CurrentPage = new DepositsPage();
        }); }
        public ICommand OpenDepositRatesPageCommand { get => new Command((p) => { CurrentPage = new DepositRatesPage(); }); }
        public ICommand SignOutCommand { get => new Command(SignOut); }

        private void SignOut(object parameter)
        {
            CurrentUser.Id = 0;
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            _mainWindow.Close();
        }
    }
}
