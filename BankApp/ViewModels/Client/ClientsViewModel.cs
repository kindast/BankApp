using BankApp.Models;
using BankApp.Repository;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace BankApp.ViewModels
{
    public class ClientsViewModel : INotifyPropertyChanged
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

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
