using System.Windows;
using System.Windows.Input;

namespace BankApp.Views
{
    /// <summary>
    /// Interaction logic for DialogWindow.xaml
    /// </summary>
    public partial class DialogWindow : Window
    {
        public DialogWindow()
        {
            InitializeComponent();
        }

        public static void Show(string text, string title = "Внимание")
        {
            DialogWindow window = new DialogWindow();
            window.Title = title;
            window.tbText.Text = text;
            window.ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
