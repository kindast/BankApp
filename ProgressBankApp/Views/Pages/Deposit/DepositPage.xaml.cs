using ProgressBankApp.Models;
using ProgressBankApp.ViewModels;
using System.Windows.Controls;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for DepositPage.xaml
    /// </summary>
    public partial class DepositPage : Page
    {
        public DepositPage(Deposit deposit, DepositsViewModel depositsViewModel)
        {
            InitializeComponent();
            DataContext = new DepositViewModel(deposit, depositsViewModel);
        }
    }
}
