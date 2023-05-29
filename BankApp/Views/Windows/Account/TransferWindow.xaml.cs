using BankApp.Models;
using BankApp.ViewModels;
using System.Windows;

namespace BankApp.Views
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
