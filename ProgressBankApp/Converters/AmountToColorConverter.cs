using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace ProgressBankApp.Converters
{
    public class AmountToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal amount)
            {
                if (amount > 0)
                    return new SolidColorBrush(new Color() { R = 75, G = 176, B = 142, A = 255 });
                else
                    return new SolidColorBrush(new Color() { R = 248, G = 64, B = 126, A = 255 });
            }

            return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
