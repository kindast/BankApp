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
        public TransferWindow(BankAccount bankAccount)
        {
            InitializeComponent();
            DataContext = new TransferViewModel(this, bankAccount);
        }
    }
}
