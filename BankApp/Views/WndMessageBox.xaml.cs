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
    /// Логика взаимодействия для WndMessageBox.xaml
    /// </summary>
    public partial class WndMessageBox : Window
    {
        public WndMessageBox()
        {
            InitializeComponent();
        }

        public string Message { set => tbMessage.Text = value; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
