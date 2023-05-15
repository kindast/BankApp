using BankApp.Helpers;
using BankApp.Models;
using BankApp.Repository;
using BankApp.ViewModels;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BankApp.Views
{
    /// <summary>
    /// Interaction logic for DepositCalculatorPage.xaml
    /// </summary>
    public partial class DepositCalculatorPage : Page
    {
        private DepositCalculatorViewModel _viewModel;
        private AccountRepository _accountRepository = new AccountRepository();
        private DepositRepository _depositRepository = new DepositRepository();

        public DepositCalculatorPage()
        {
            InitializeComponent();
            _viewModel = new DepositCalculatorViewModel();
            DataContext = _viewModel;
        }

        private void OpenDeposit(object sender, RoutedEventArgs e)
        {
            if (_depositRepository.GetDeposits(CurrentUser.Id).Count() >= 5)
            {
                DialogWindow.Show("Вы достигли максимального количества одновременных вкладов (5)");
                return;
            }

            if (_accountRepository.GetAccounts(CurrentUser.Id).Where(a => a.Balance >= 50000).Count() == 0)
            {
                DialogWindow.Show("Нет счетов с которых можно пополнить вклад или на них недостаточно средств");
                return;
            }

            OpenDepositWindow openDepositWindow = new OpenDepositWindow((sender as Button).DataContext as DepositRate, _viewModel.Months, _viewModel.Amount);
            openDepositWindow.ShowDialog();

            if (_depositRepository.GetDeposits(CurrentUser.Id).Count() > 0)
            {
                MainFrame.Frame.Navigate(new DepositsPage());
            }
        }
    }
}
