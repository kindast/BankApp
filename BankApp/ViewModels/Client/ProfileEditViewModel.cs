using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using BankApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace BankApp.ViewModels
{
    public class ProfileEditViewModel : ViewModel
    {
        private UserRepository _userRepository = new UserRepository();
        private AccountRepository _accountRepository = new AccountRepository();
        private DepositRepository _depositRepository = new DepositRepository();

        public DateTime DateStart { get => DateTime.Now.AddYears(-100); set { } }
        public DateTime DateEnd { get => DateTime.Now.AddYears(-14); set { } }

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

        private ObservableCollection<Deposit> _deposits;
        public ObservableCollection<Deposit> Deposits
        {
            get => _deposits;
            set
            {
                _deposits = value;
                OnPropertyChanged(nameof(Deposits));
            }
        }

        public ProfileEditViewModel(int userId)
        {
            User = _userRepository.GetUser(userId);
            BankAccounts = new ObservableCollection<BankAccount>(_accountRepository.GetAccounts(userId));
            Deposits = new ObservableCollection<Deposit>(_depositRepository.GetDeposits(userId));
        }

        public ICommand SaveDataCommand { get => new Command(SaveData, CanSaveData); }

        private void SaveData(object parameter)
        {
            _userRepository.Save();
            DialogWindow.Show("Данные сохранены");
        }

        private bool CanSaveData(object parameter)
        {
            if (string.IsNullOrWhiteSpace(User.Surname) || string.IsNullOrWhiteSpace(User.Name)
                || User.Passport.Length != 10 || !long.TryParse(User.Passport, out _) || string.IsNullOrWhiteSpace(User.Address)
                || string.IsNullOrWhiteSpace(User.Phone) || string.IsNullOrWhiteSpace(User.Login) || string.IsNullOrWhiteSpace(User.Password))
            {
                return false;
            }

            return true;
        }

        public ICommand OpenNewAccountCommand { get => new Command(OpenNewAccount); }

        private void OpenNewAccount(object parameter)
        {
            int count = BankAccounts.Where(a => a.Name.Contains("Progress Standart")).Count();

            if (count == 5)
            {
                DialogWindow.Show("Клиент достиг максимального количества одновременно открытых счетов (5)");
                return;
            }

            BankAccount bankAccount = new BankAccount()
            {
                Number = BankAccount.GenerateUniqueNumber(10),
                Name = count > 0 ? $"Progress Standart #{count + 1}" : "Progress Standart",
                DateOpen = DateTime.Now,
                Balance = 0,
                User = User,
                Type = BankAccountType.Checking
            };

            _accountRepository.CreateAccount(bankAccount);
            BankAccounts.Add(bankAccount);
        }
    }
}
