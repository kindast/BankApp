using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using ProgressBankApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class ClientsViewModel : ViewModel
    {
        private UserRepository _userRepository;
        private ObservableCollection<User> _clients;
        private string _search = "";

        public ObservableCollection<User> Clients
        {
            get => _clients;
            set
            {
                _clients = value;
                OnPropertyChanged(nameof(Clients));
            }
        }

        public string Search
        {
            get => _search;
            set
            {
                _search = value;
                OnPropertyChanged(nameof(Search));
                UpdateClients();
            }
        }

        public ClientsViewModel()
        {
            _userRepository = new UserRepository();
            UpdateClients();

            MainWindow.SetTitle("Клиенты");
        }

        private void UpdateClients()
        {
            var clients = _userRepository.GetClients();

            if (Search.Replace(" ", "").Length > 0)
            {
                clients = clients.Where(c => c.FullName.Replace(" ", "").ToLower().Contains(Search.Replace(" ", "").ToLower()) ||
                c.Phone.Replace(" ", "").Contains(Search.Replace(" ", "").Replace("+", "")) ||
                c.Email.Replace(" ", "").ToLower().Contains(Search.Replace(" ", "").ToLower())).ToList();
            }

            Clients = new ObservableCollection<User>(clients);
        }

        public ICommand OpenRegisterClientPageCommand { get => new Command(OpenRegisterClientPage); }

        private void OpenRegisterClientPage(object parameter)
        {
            MainWindow.Navigate(new RegisterClientPage());
        }
    }
}
