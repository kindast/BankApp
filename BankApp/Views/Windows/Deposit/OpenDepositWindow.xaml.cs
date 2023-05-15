using BankApp.Models;
using BankApp.ViewModels;
using System.Windows;

namespace BankApp.Views
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
