using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using BankApp.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace BankApp.ViewModels
{
    public class DepositsViewModel : ViewModel
    {
        private DepositRepository _depositRepository = new DepositRepository();
        private ObservableCollection<Deposit> _deposits;
        private Deposit _selectedDeposit;
        private Page _currentPage;
        private DispatcherTimer _timer;

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
                if (SelectedDeposit != null)
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

            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMinutes(1);
            _timer.Tick += CalculationPercentsDeposits;
            _timer.Start();
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

        private void CalculationPercentsDeposits(object sender, EventArgs e)
        {
            var deposits = _depositRepository.GetDeposits(CurrentUser.Id).Where(d => d.Months != d.MonthsPassed).ToList();
            foreach (var deposit in deposits)
            {
                decimal monthPercet = (decimal)(deposit.DepositRate.Percent / 12 / 100);
                while (DateTime.Now >= deposit.BankAccount.DateOpen.AddMinutes(deposit.MonthsPassed).AddMinutes(10) && deposit.MonthsPassed < deposit.Months)
                {
                    deposit.MonthsPassed += 1;
                    deposit.Accumulated += deposit.BankAccount.Balance * monthPercet;
                    if (deposit.DepositRate.IsCapitalization || deposit.Months == deposit.MonthsPassed)
                    {
                        deposit.BankAccount.Balance += deposit.Accumulated;
                        deposit.BankAccount.Histories.Add(new History()
                        {
                            Account = deposit.BankAccount,
                            Name = "Начисление процентов",
                            DateTime = DateTime.Now,
                            Amount = deposit.Accumulated
                        });
                        deposit.Accumulated = 0;
                    }
                }
            }
            _depositRepository.Save();
            (sender as DispatcherTimer).Dispatcher.Invoke(() =>
            {
                int indexDeposit = Deposits.IndexOf(SelectedDeposit);
                Deposits = new ObservableCollection<Deposit>(_depositRepository.GetDeposits(CurrentUser.Id));
                if (Deposits.Count > 0)
                    SelectedDeposit = Deposits[indexDeposit];
            });
        }
    }
}
