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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BankApp.UserControls
{
    /// <summary>
    /// Логика взаимодействия для CardControl.xaml
    /// </summary>
    public partial class CardControl : UserControl
    {
        public CardControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public CardControl(string cardName, string cardNumber, string balance)
        {
            InitializeComponent();
            DataContext = this;
            CardName = cardName;
            Balance = balance;

            //CardNumber
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < cardNumber.Length; i++)
            {
                stringBuilder.Append(cardNumber[i]);
                if (((i + 1) % 4) == 0)
                    stringBuilder.Append(' ');
            }
            CardNumber = stringBuilder.ToString();
        }

        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string Balance { get; set; }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(CardNumber.Replace(" ", ""));
        }
    }
}
