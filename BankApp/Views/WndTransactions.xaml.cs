using BankApp.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

        private void ShowMessageBox(string message)
        {
            WndMessageBox messageBox = new WndMessageBox();
            messageBox.Message = message;
            messageBox.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int i = bank.CurrentUser.Transactions.Count - 1; i >= 0; i--)
                lbTransactions.Items.Add($"{bank.CurrentUser.Transactions[i].Type} ({bank.CurrentUser.Transactions[i].TransactionDateTime})");
            if (lbTransactions.Items.Count > 0)
                lbTransactions.SelectedIndex = 0;
        }

        private void lbTransactions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var transaction in bank.CurrentUser.Transactions)
            {
                if ($"{transaction.Type} ({transaction.TransactionDateTime})" == lbTransactions.SelectedItem.ToString())
                {
                    tbTextTransaction.Text = transaction.Text;
                    break;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (tbTextTransaction.Text == "")
            {
                ShowMessageBox("Вы ещё не совершили ни одной транзакции");
                return;
            }
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Text files|*.txt|All Files|*.*";
            if ((bool)fileDialog.ShowDialog())
                File.WriteAllText(fileDialog.FileName, tbTextTransaction.Text);
        }
    }
}
