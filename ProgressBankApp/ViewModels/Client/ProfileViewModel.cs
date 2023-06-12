using ProgressBankApp.Helpers;
using ProgressBankApp.Models;
using ProgressBankApp.Repository;
using ProgressBankApp.Views;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Input;

namespace ProgressBankApp.ViewModels
{
    public class ProfileViewModel : ViewModel
    {
        private UserRepository _userRepository = new UserRepository();
        private BankAccountRepository _accountRepository = new BankAccountRepository();
        private DepositRepository _depositRepository = new DepositRepository();
        private Chart _chart;

        private User _user;
        public User User
        {
            get => _user;
            set
            {
                _user = value;
                OnPropertyChanged(nameof(User));
            }
        }

        private string _oldPassword;
        public string OldPassword
        {
            get => _oldPassword;
            set
            {
                _oldPassword = value;
                OnPropertyChanged(nameof(OldPassword));
            }
        }

        private string _newPassword;
        public string NewPassword
        {
            get => _newPassword;
            set
            {
                _newPassword = value;
                OnPropertyChanged(nameof(NewPassword));
            }
        }

        private string _newPasswordConfirm;
        public string NewPasswordConfirm
        {
            get => _newPasswordConfirm;
            set
            {
                _newPasswordConfirm = value;
                OnPropertyChanged(nameof(NewPasswordConfirm));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        private string _accountsBalance;
        public string AccountsBalance
        {
            get => _accountsBalance;
            set
            {
                _accountsBalance = value;
                OnPropertyChanged(nameof(AccountsBalance));
            }
        }

        private string _depositsBalance;
        public string DepositsBalance
        {
            get => _depositsBalance;
            set
            {
                _depositsBalance = value;
                OnPropertyChanged(nameof(DepositsBalance));
            }
        }

        public ProfileViewModel(Chart chart)
        {
            User = _userRepository.GetUser(CurrentUser.Id);

            if (chart != null)
            {
                _chart = chart;
                InitializeChart();
            }

            MainWindow.SetTitle("Профиль");
        }

        public ICommand ChangePasswordCommand { get => new Command(ChangePassword, CanChangePassword); }

        private void ChangePassword(object parameter)
        {
            if (OldPassword != User.Password)
            {
                ErrorMessage = "Неверный старый пароль";
                return;
            }

            if (NewPassword != NewPasswordConfirm)
            {
                ErrorMessage = "Новые пароли не совпадают";
                return;
            }

            ErrorMessage = "Пароль изменен";
            User.Password = NewPassword;
            _userRepository.Save();
        }

        private bool CanChangePassword(object parameter)
        {
            if (string.IsNullOrWhiteSpace(OldPassword) || string.IsNullOrWhiteSpace(NewPassword)
                || string.IsNullOrWhiteSpace(NewPasswordConfirm))
                return false;

            return true;
        }

        private void InitializeChart()
        {
            _chart.ChartAreas.Add(new ChartArea("Default"));
            _chart.Series.Add(new Series("Series"));
            _chart.Series["Series"].ChartArea = "Default";
            _chart.Series["Series"].ChartType = SeriesChartType.Pie;

            List<string> axisXData = new List<string>();
            List<decimal> axisYData = new List<decimal>();

            var accounts = _accountRepository.GetAccounts(CurrentUser.Id);
            decimal accountsBalance = accounts.Sum(a => a.Balance);
            axisXData.Add("");
            axisYData.Add(accountsBalance);
            AccountsBalance = accountsBalance.ToString(accountsBalance % 1 == 0 ? "N0" : "N2", CultureInfo.CreateSpecificCulture("ru-RU")) + "₽";

            var deposits = _depositRepository.GetDeposits(CurrentUser.Id);
            decimal depositsBalance = deposits.Sum(d => d.BankAccount.Balance);
            axisXData.Add("");
            axisYData.Add(depositsBalance);
            DepositsBalance = depositsBalance.ToString(depositsBalance % 1 == 0 ? "N0" : "N2", CultureInfo.CreateSpecificCulture("ru-RU")) + "₽";

            _chart.Series["Series"].Points.DataBindXY(axisXData, axisYData);
            _chart.Series["Series"].Points[0].Color = Color.FromArgb(86, 140, 249);
            _chart.Series["Series"].Points[1].Color = Color.FromArgb(201, 215, 243);
        }
    }
}
