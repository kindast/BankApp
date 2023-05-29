using BankApp.Helpers;
using BankApp.Repository;
using BankApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.ViewModels
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
            DepositRates = new ObservableCollection<DepositRate>(_depositRateRepository.GetDepositsRates());
        }
    }
}
