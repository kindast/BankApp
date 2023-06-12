using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using ProgressBankApp.Views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class DepositCalculatorViewModel : ViewModel
    {
        private int _amount = 50000;
        private int _months = 3;
        private DepositRateRepository _depositRateRepository = new DepositRateRepository();
        private ObservableCollection<DepositRate> _depositsRates;

        public int Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                if (_amount < 50000)
                    _amount = 50000;
                if (_amount > 30000000)
                    _amount = 30000000;
                OnPropertyChanged(nameof(Amount));
                UpdateDeposits();
            }
        }

        public int Months
        {
            get => _months;
            set
            {
                _months = value;
                if (_months < 3)
                    _months = 3;
                if (_months > 24)
                    _months = 24;
                OnPropertyChanged(nameof(Months));
                UpdateDeposits();
            }
        }

        public ObservableCollection<DepositRate> DepositsRates
        {
            get => _depositsRates;
            set
            {
                _depositsRates = value;
                OnPropertyChanged(nameof(DepositsRates));
            }
        }

        public DepositCalculatorViewModel()
        {
            UpdateDeposits();

            MainWindow.SetTitle("Калькулятор вкладов");
        }

        private void UpdateDeposits()
        {
            DepositsRates = new ObservableCollection<DepositRate>(_depositRateRepository.GetDepositsRates().OrderByDescending(d => d.Income).ToList());

            foreach (var deposit in DepositsRates)
            {
                deposit.Amount = Amount;
                deposit.Period = Months;
            }
        }

        public ICommand OpenDepositCommand { get => new Command(OpenDeposit); }

        private void OpenDeposit(object parameter)
        {
            OpenDepositWindow openDepositWindow = new OpenDepositWindow(parameter as DepositRate, Months, Amount);
            openDepositWindow.ShowDialog();
        }
    }
}
