using ProgressBankApp.Models;
using ProgressBankApp.ViewModels;
using System.Windows;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for OpenDepositWindow.xaml
    /// </summary>
    public partial class OpenDepositWindow : Window
    {
        public OpenDepositWindow(DepositRate depositRate, int months, decimal amount)
        {
            InitializeComponent();
            DataContext = new OpenDepositViewModel(depositRate, months, amount, this);
        }
    }
}
