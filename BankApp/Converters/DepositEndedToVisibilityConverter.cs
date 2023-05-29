using BankApp.Models;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace BankApp.Converters
{
    public class DepositEndedToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Deposit deposit = value as Deposit;
            if (parameter.ToString() == "True")
                return deposit.MonthsPassed == deposit.Months ? Visibility.Visible : Visibility.Collapsed;
            else
                return deposit.MonthsPassed == deposit.Months ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
