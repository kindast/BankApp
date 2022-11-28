using BankApp.Model;
using BankApp.UserControls;
using BankApp.Views;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace BankApp.Pages
{
    /// <summary>
    /// Логика взаимодействия для PgMain.xaml
    /// </summary>
    public partial class PgMain : Page
    {
        public WndMain parentWindow;
        public Bank bank;

        public PgMain()
        {
            InitializeComponent();
            lbOperations.SelectedIndex = 0;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            tbUserName.Text = bank.CurrentUser.LastName + '\n' + bank.CurrentUser.FirstName;

            LoadCards();
        }

        private void ShowMessageBox(string message)
        {
            WndMessageBox messageBox = new WndMessageBox();
            messageBox.Message = message;
            messageBox.ShowDialog();
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            PgLogin login = new PgLogin();
            login.parentWindow = parentWindow;
            login.bank = bank;
            parentWindow.frame.Navigate(login);
        }

        private void OpenNewCard(object sender, RoutedEventArgs e)
        {
            if (tbCardName.Text == String.Empty)
            {
                ShowMessageBox("Введите название карты");
                return;
            }
            try
            {
                bank.CreateCard(tbCardName.Text);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            LoadCards();
        }

        private void DeleteCard(object sender, RoutedEventArgs e)
        {
            try
            {
                bank.DeleteCard(cbCardSelection1.SelectedIndex);
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            LoadCards();
        }

        private void AddMoney(object sender, RoutedEventArgs e)
        {
            if (tbMoney.Text == String.Empty)
            {
                ShowMessageBox("Введите сумму пополнения");
                return;
            }
            bank.AddMoney(cbCardSelection1.SelectedIndex, double.Parse(tbMoney.Text));
            LoadCards();
        }

        private void WithdrawMoney(object sender, RoutedEventArgs e)
        {
            if (tbMoney.Text == String.Empty)
            {
                ShowMessageBox("Введите сумму снятия");
                return;
            }
            try
            {
                bank.WithdrawMoney(cbCardSelection1.SelectedIndex, double.Parse(tbMoney.Text));
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            LoadCards();
        }

        private void Transfer(object sender, RoutedEventArgs e)
        {
            if (tbMoney.Text == String.Empty)
            {
                ShowMessageBox("Введите сумму перевода");
                return;
            }
            if (tbCardNumber.Text == String.Empty)
            {
                ShowMessageBox("Введите номер карты");
                return;
            }
            try
            {
                bank.Transfer(cbCardSelection1.SelectedIndex, tbCardNumber.Text, double.Parse(tbMoney.Text));
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            LoadCards();
        }

        private void LoadCards()
        {
            spCards.Children.Clear();
            cbCardSelection1.Items.Clear();
            cbCardSelection2.Items.Clear();
            foreach (var card in bank.CurrentUser.BankCards)
            {
                spCards.Children.Add(new CardControl(card.Name, card.Number, $"{card.Balance}₽") { Margin = new Thickness(5), Width = spCards.Width });
                cbCardSelection1.Items.Add($"{card.Name} ({card.Balance}₽)");
                cbCardSelection2.Items.Add($"{card.Name} ({card.Balance}₽)");
            }
            cbCardSelection1.SelectedIndex = 0;
        }

        private void cbMoney_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }

        private void cbMoney_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            double value;
            if (!double.TryParse(e.Text, out value) && (e.Text != "," || tbMoney.Text.Contains(",")))
                e.Handled = true;
        }

        private void tbCardNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int value;
            if (!int.TryParse(e.Text, out value))
                e.Handled = true;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            tbCardNameLabel.Visibility = Visibility.Collapsed;
            tbCardSelectionLabel.Visibility = Visibility.Collapsed;
            tbMoneyLabel.Visibility = Visibility.Collapsed;
            tbCardNumberLabel.Visibility = Visibility.Collapsed;
            tbCardName.Visibility = Visibility.Collapsed;
            btnOpenCard.Visibility = Visibility.Collapsed;
            cbCardSelection1.Visibility = Visibility.Collapsed;
            cbCardSelection2.Visibility = Visibility.Collapsed;
            btnDeleteCard.Visibility = Visibility.Collapsed;
            tbMoney.Visibility = Visibility.Collapsed;
            tbCardNumber.Visibility = Visibility.Collapsed;
            btnTransfer.Visibility = Visibility.Collapsed;
            btnAddMoney.Visibility = Visibility.Collapsed;
            btnWithdrawMoney.Visibility = Visibility.Collapsed;

            switch (listBox.SelectedIndex)
            {
                case 0:
                    tbCardNameLabel.Visibility = Visibility.Visible;
                    tbCardName.Visibility = Visibility.Visible;
                    btnOpenCard.Visibility = Visibility.Visible;
                    break;

                case 1:
                    tbCardSelectionLabel.Visibility = Visibility.Visible;
                    cbCardSelection1.Visibility = Visibility.Visible;
                    btnDeleteCard.Visibility = Visibility.Visible;
                    break;

                case 2:
                    tbCardSelectionLabel.Visibility = Visibility.Visible;
                    cbCardSelection1.Visibility = Visibility.Visible;
                    tbMoneyLabel.Visibility = Visibility.Visible;
                    tbMoney.Visibility = Visibility.Visible;
                    btnAddMoney.Visibility = Visibility.Visible;
                    break;

                case 3:
                    tbCardSelectionLabel.Visibility = Visibility.Visible;
                    cbCardSelection1.Visibility = Visibility.Visible;
                    tbMoneyLabel.Visibility = Visibility.Visible;
                    tbMoney.Visibility = Visibility.Visible;
                    btnWithdrawMoney.Visibility = Visibility.Visible;
                    break;

                case 4:
                    tbCardSelectionLabel.Visibility = Visibility.Visible;
                    cbCardSelection1.Visibility = Visibility.Visible;
                    tbMoneyLabel.Visibility = Visibility.Visible;
                    tbMoney.Visibility = Visibility.Visible;
                    tbCardNumberLabel.Visibility = Visibility.Visible;
                    tbCardNumber.Visibility = Visibility.Visible;
                    btnTransfer.Visibility = Visibility.Visible;
                    break;

                case 5:
                    break;
            }
        }
    }
}
