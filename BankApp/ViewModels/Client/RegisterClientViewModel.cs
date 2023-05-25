using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using BankApp.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BankApp.ViewModels
{
    public class RegisterClientViewModel : ViewModel
    {
        private UserRepository _userRepository = new UserRepository();

        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _patronymic;
        public string Patronymic
        {
            get => _patronymic;
            set
            {
                _patronymic = value;
                OnPropertyChanged(nameof(Patronymic));
            }
        }

        private string _passport = "";
        public string Passport
        {
            get => _passport;
            set
            {
                _passport = value;
                OnPropertyChanged(nameof(Passport));
            }
        }

        private string _address;
        public string Address
        {
            get => _address;
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private DateTime _dateOfBirth = new DateTime(2000, 1, 1);
        public DateTime DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged(nameof(DateOfBirth));
            }
        }

        public DateTime DateStart { get => DateTime.Now.AddYears(-100); set { } }
        public DateTime DateEnd { get => DateTime.Now.AddYears(-14); set { } }

        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public ICommand CancelRegisterCommand { get => new Command(CancelRegister); }

        private void CancelRegister(object parameter)
        {
            MainFrame.Frame.Navigate(new ClientsPage());
        }

        public ICommand RegisterClientCommand { get => new Command(RegisterClient, CanRegister); }

        private void RegisterClient(object parameter)
        {
            User user = new User()
            {
                Surname = Surname,
                Name = Name,
                Patronymic = Patronymic,
                PassportSeries = Passport.ToString().Substring(0, 4),
                PassportNumber = Passport.ToString().Substring(4, 6),
                Address = Address,
                Phone = Phone,
                Email = Email,
                DateOfBirth = DateOfBirth,
                Login = Login,
                Password = Password,
                Role = Role.Client
            };

            _userRepository.CreateUser(user);
            MainFrame.Frame.Navigate(new ClientsPage());
        }

        private bool CanRegister(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Surname) || string.IsNullOrWhiteSpace(Name) 
                || Passport.Length != 10 || !long.TryParse(Passport, out _) || string.IsNullOrWhiteSpace(Address)
                || string.IsNullOrWhiteSpace(Phone) || string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Password))
            {
                return false;
            }

            return true;
        }
    }
}
