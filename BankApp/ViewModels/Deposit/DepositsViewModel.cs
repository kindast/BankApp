using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using BankApp.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankApp.ViewModels
{
    public class DepositsViewModel : ViewModel
    {
        private DepositRepository _depositRepository = new DepositRepository();
        private ObservableCollection<Deposit> _deposits;
        private Deposit _selectedDeposit;
        private Page _currentPage;

        public ObservableCollection<Deposit> Deposits
        {
            get => _deposits;
            set
            {
                _deposits = value;
                OnPropertyChanged(nameof(Deposits));
            }
        }

        public Deposit SelectedDeposit
        {
            get => _selectedDeposit;
            set
            {
                _selectedDeposit = value;
                OnPropertyChanged(nameof(SelectedDeposit));
                CurrentPage = new DepositPage(SelectedDeposit, this);
            }
        }

        public Page CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public DepositsViewModel()
        {
            Deposits = new ObservableCollection<Deposit>(_depositRepository.GetDeposits(CurrentUser.Id));
            SelectedDeposit = Deposits.FirstOrDefault();
        }

        public ICommand OpenCalculatorPageCommand { get => new Command(OpenDepositCalculator); }

        private void OpenDepositCalculator(object parameter)
        {
            if (_depositRepository.GetDeposits(CurrentUser.Id).Count() >= 5)
            {
                DialogWindow.Show("Вы достигли максимального количества одновременных вкладов (5)");
                return;
            }
            MainFrame.Frame.Navigate(new DepositCalculatorPage());
        }
    }
}
