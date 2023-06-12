using ProgressBankApp.Models;
using ProgressBankApp.ViewModels;
using System.Windows.Controls;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for DepositAgreement.xaml
    /// </summary>
    public partial class DepositAgreement : Page
    {
        public DepositAgreement(User user, Deposit deposit)
        {
            InitializeComponent();
            DataContext = new DepositAgreementViewModel(user, deposit);
        }
    }
}
