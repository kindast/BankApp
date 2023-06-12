using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using ProgressBankApp.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class DepositRatesViewModel : ViewModel
    {
        private DepositRateRepository _depositRateRepository = new DepositRateRepository();

        private ObservableCollection<DepositRate> _depositRates;
        public ObservableCollection<DepositRate> DepositRates
        {
            get => _depositRates;
            set
            {
                _depositRates = value;
                OnPropertyChanged(nameof(DepositRates));
            }
        }

        public DepositRatesViewModel()
        {
            UpdateDepositRates();

            MainWindow.SetTitle("Тарифы вкладов");
        }

        public void UpdateDepositRates()
        {
            DepositRates = new ObservableCollection<DepositRate>(_depositRateRepository.GetDepositsRates());
        }

        public ICommand OpenAddEditDepositRatePageCommand { get => new Command(OpenAddEditDepositRatePage); }

        private void OpenAddEditDepositRatePage(object parameter)
        {
            MainWindow.Navigate(new AddEditDepositRatePage());
        }

        public void OpenAddEditDepositRatePage(DepositRate depositRate)
        {
            if (_depositRateRepository.DepositRateUsed(depositRate.Id))
            {
                DialogWindow.Show("Невозможно редактировать тариф пока есть открытые вклады с этим тарифом");
                return;
            }

            MainWindow.Navigate(new AddEditDepositRatePage(depositRate));
        }

        public void DeleteDepositRate(DepositRate depositRate)
        {
            if (_depositRateRepository.DepositRateUsed(depositRate.Id))
            {
                DialogWindow.Show("Невозможно удалить тариф пока есть открытые вклады с этим тарифом");
                return;
            }

            if (DialogWindow.ShowYesNo("Вы уверены, что хотите удалить этот тариф?"))
            {
                _depositRateRepository.DeleteDepositRate(depositRate);
            }
        }
    }
}
