using ProgressBankApp.Models;
using ProgressBankApp.ViewModels;
using System.Windows.Controls;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for AddEditDepositRatePage.xaml
    /// </summary>
    public partial class AddEditDepositRatePage : Page
    {
        public AddEditDepositRatePage(DepositRate depositRate = null)
        {
            InitializeComponent();
            DataContext = new AddEditDepositRateViewModel(depositRate);
        }
    }
}
