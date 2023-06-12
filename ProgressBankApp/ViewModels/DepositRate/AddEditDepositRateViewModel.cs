using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using ProgressBankApp.Views;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class AddEditDepositRateViewModel : ViewModel
    {
        DepositRateRepository _depositRateRepository = new DepositRateRepository();

        private DepositRate _depositRate;
        public DepositRate DepositRate
        {
            get => _depositRate;
            set
            {
                _depositRate = value;
                OnPropertyChanged(nameof(DepositRate));
            }
        }

        public AddEditDepositRateViewModel(DepositRate depositRate = null)
        {
            if (depositRate == null)
                _depositRate = new DepositRate();
            else
                _depositRate = depositRate;
        }

        public ICommand OpenDepositsRatesPageCommand { get => new Command(OpenDepositsRatesPage); }
        public ICommand SaveCommand { get => new Command(Save, CanSave); }

        private void OpenDepositsRatesPage(object parameter)
        {
            MainWindow.Navigate(new DepositRatesPage());
        }

        private void Save(object parameter)
        {
            try
            {
                if (DepositRate.Id > 0)
                    _depositRateRepository.Save();
                else
                    _depositRateRepository.CreateDepositRate(DepositRate);

                MainWindow.Navigate(new DepositRatesPage());
            }
            catch { DialogWindow.Show("Ошибка сохранения данных"); }
        }

        private bool CanSave(object parameter)
        {
            if (string.IsNullOrWhiteSpace(DepositRate.Name) || DepositRate.Percent <= 0 || DepositRate.Percent > 100)
                return false;

            return true;
        }
    }
}
