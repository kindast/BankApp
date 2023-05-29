using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using System.Windows.Input;

namespace BankApp.ViewModels
{
    public class ProfileViewModel : ViewModel
    {
        private UserRepository _userRepository = new UserRepository();

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

        private string _oldPassword;
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        private string _newPasswordConfirm;
        public string NewPasswordConfirm
        {
            get => _newPasswordConfirm;
            set
            {
                _newPasswordConfirm = value;
                OnPropertyChanged(nameof(NewPasswordConfirm));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ProfileViewModel()
        {
            User = _userRepository.GetUser(CurrentUser.Id);
        }

        public ICommand ChangePasswordCommand { get => new Command(ChangePassword, CanChangePassword); }

        private void ChangePassword(object parameter)
        {
            if (OldPassword != User.Password)
            {
                ErrorMessage = "Неверный старый пароль";
                return;
            }

            if (NewPassword != NewPasswordConfirm)
            {
                ErrorMessage = "Новые пароли не совпадают";
                return;
            }

            ErrorMessage = "Пароль изменен";
            User.Password = NewPassword;
            _userRepository.Save();
        }

        private bool CanChangePassword(object parameter)
        {
            if (string.IsNullOrWhiteSpace(OldPassword) || string.IsNullOrWhiteSpace(NewPassword)
                || string.IsNullOrWhiteSpace(NewPasswordConfirm))
                return false;

            return true;
        }
    }
}
