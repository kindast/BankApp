using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using System.ComponentModel;

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

        public ProfileViewModel()
        {
            User = _userRepository.GetUser(CurrentUser.Id);
        }
    }
}
