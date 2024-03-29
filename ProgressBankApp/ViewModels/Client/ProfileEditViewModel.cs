﻿using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using ProgressBankApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class ProfileEditViewModel : ViewModel
    {
        private UserRepository _userRepository = new UserRepository();
        private BankAccountRepository _accountRepository = new BankAccountRepository();
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

        private ObservableCollection<Gender> _genders;
        public ObservableCollection<Gender> Genders
        {
            get => _genders;
            set
            {
                _genders = value;
                OnPropertyChanged(nameof(Genders));
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
            Genders = new ObservableCollection<Gender>(_userRepository.GetGenders());
            User = _userRepository.GetUser(userId);
            BankAccounts = new ObservableCollection<BankAccount>(_accountRepository.GetAccounts(userId));
            Deposits = new ObservableCollection<Deposit>(_depositRepository.GetDeposits(userId));

            MainWindow.SetTitle("Профиль " + User.ShortName);
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
        public ICommand OpenClientsPageCommand { get => new Command(OpenClientsPage); }

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
                Type = _accountRepository.GetCheckingType()
            };

            _accountRepository.CreateAccount(bankAccount);
            BankAccounts = new ObservableCollection<BankAccount>(_accountRepository.GetAccounts(User.Id));
        }

        public void DeleteBankAccount(BankAccount bankAccount)
        {
            if (!DialogWindow.ShowYesNo("Вы уверены что хотите закрыть этот счет?"))
                return;

            if (bankAccount.Balance > 1 && BankAccounts.Count == 1)
            {
                DialogWindow.Show("Для удаления счета на нем не должно быть средств");
                return;
            }

            if (bankAccount.Balance > 1 && BankAccounts.Count > 1)
            {
                var accountTo = BankAccounts.Where(a => a.Number != bankAccount.Number).FirstOrDefault();
                if (accountTo != null)
                    _accountRepository.Transfer(bankAccount, accountTo, bankAccount.Balance);
                DialogWindow.Show($"Остаток средств был переведен на счет {accountTo.Number}");
            }

            _accountRepository.DeleteAccount(bankAccount);
            BankAccounts = new ObservableCollection<BankAccount>(_accountRepository.GetAccounts(User.Id));
        }

        public void DeleteDeposit(Deposit deposit)
        {
            if (!DialogWindow.ShowYesNo("Вы уверены что хотите закрыть этот вклад?"))
                return;

            if (BankAccounts.Count == 0)
            {
                DialogWindow.Show("Нет счета на который можно перевести сумму вклада");
                return;
            }

            var accountTo = BankAccounts.FirstOrDefault();
            _accountRepository.Transfer(deposit.BankAccount, accountTo, deposit.BankAccount.Balance);
            DialogWindow.Show($"Сумма вклада была переведена на счет {accountTo.Number}");

            _depositRepository.DeleteDeposit(deposit);
            Deposits = new ObservableCollection<Deposit>(_depositRepository.GetDeposits(User.Id));
            BankAccounts = new ObservableCollection<BankAccount>(_accountRepository.GetAccounts(User.Id));
        }

        private void OpenClientsPage(object parameter)
        {
            MainWindow.Navigate(new ClientsPage());
        }
    }
}
