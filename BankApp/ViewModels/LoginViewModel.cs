using BankApp.Helpers;
using BankApp.Repository;
using BankApp.Views;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BankApp.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private UserRepository _userRepository = new UserRepository();
        private string _login = "test";
        private string _password = "test";
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
            LoginCommand = new Command(SignIn, CanSignIn);
        }

        public ICommand LoginCommand { get; set; }

        private void SignIn(object parameter)
        {
            if (_userRepository.UserExists(Login, Password))
            {
                CurrentUser.Id = _userRepository.GetClient(Login).Id;
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
