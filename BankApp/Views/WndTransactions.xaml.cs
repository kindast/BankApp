using BankApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BankApp.Views
{
    /// <summary>
    /// Логика взаимодействия для WndTransactions.xaml
    /// </summary>
    public partial class WndTransactions : Window
    {
        public Bank bank;
        public WndTransactions()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = bank.CurrentUser.Transactions.Count - 1; i >= 0; i--)
            {
                lbTransactions.Items.Add($"{bank.CurrentUser.Transactions[i].Type} ({bank.CurrentUser.Transactions[i].TransactionDateTime})");
            }
        }

        private void lbTransactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            tbTextTransaction.Text = bank.CurrentUser.Transactions[lbTransactions.SelectedIndex].Text;
            foreach (var transaction in bank.CurrentUser.Transactions)
            {
                if ($"{transaction.Type} ({transaction.TransactionDateTime})" == lbTransactions.SelectedItem.ToString())
                {
                    tbTextTransaction.Text = transaction.Text;
                    break;
                }
            }
        }
    }
}
