using ProgressBankApp.Models;
using ProgressBankApp.ViewModels;
using System.Windows;

namespace ProgressBankApp.Views
{
    /// <summary>
    /// Interaction logic for TransferWindow.xaml
    /// </summary>
    public partial class TransferWindow : Window
    {
        public TransferWindow(BankAccount bankAccountFrom, BankAccount bankAccountTo = null)
        {
            InitializeComponent();
            DataContext = new TransferViewModel(this, bankAccountFrom, bankAccountTo);
        }
    }
}
