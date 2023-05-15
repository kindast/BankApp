using BankApp.Models;
using BankApp.ViewModels;
using System.Windows.Controls;

namespace BankApp.Views
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
