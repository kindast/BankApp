using ProgressBankApp.Models;
using ProgressBankApp.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for DepositRatesPage.xaml
    /// </summary>
    public partial class DepositRatesPage : Page
    {
        private DepositRatesViewModel _viewModel;

        public DepositRatesPage()
        {
            InitializeComponent();
            _viewModel = new DepositRatesViewModel();
            DataContext = _viewModel;
        }

        private void btnDeleteDepositRate_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.DeleteDepositRate((sender as Button).DataContext as DepositRate);
            _viewModel.UpdateDepositRates();
        }

        private void btnEditDepositRate_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.OpenAddEditDepositRatePage((sender as Button).DataContext as DepositRate);
        }
    }
}
