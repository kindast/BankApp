using ProgressBankApp.Helpers;
using ProgressBankApp.Repository;
using ProgressBankApp.Views;
using System.Windows;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class LoginViewModel : ViewModel
    {
        private UserRepository _userRepository = new UserRepository();
        private string _login;
        private string _password;
        private string _errorMessage;
        private Window _loginWindow;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public LoginViewModel(Window loginWindow)
        {
            _loginWindow = loginWindow;
        }

        public ICommand LoginCommand { get => new Command(SignIn, CanSignIn); }

        private void SignIn(object parameter)
        {
            if (_userRepository.UserExists(Login, Password))
            {
                CurrentUser.Id = _userRepository.GetUser(Login).Id;
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                _loginWindow.Close();
            }
            else
                ErrorMessage = "Неверный логин или пароль";
        }

        private bool CanSignIn(object parameter)
        {
            return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}
